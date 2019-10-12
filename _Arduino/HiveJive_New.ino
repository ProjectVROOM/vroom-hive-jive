#include <Wire.h>

char receivedChar;
int breaker = 0;
const int actPotPin = A3;
int readActuatorPosition = 0;
int readActPos = 0;

const byte emergencyStopButton = 2; // Pin on the Arduino that the eStop button is plugged into
volatile byte eStopState = LOW; // Sets the state of the eStop to non-emergency
 
const uint8_t smcDeviceNumber = 13;
 
// Required to allow motors to move.
// Must be called when controller restarts and after any error.
void exitSafeStart()
{
  Wire.beginTransmission(smcDeviceNumber);
  Wire.write(0x83);  // Exit safe start
  Wire.endTransmission();
}
 
void setMotorSpeed(int16_t speed)
{
  uint8_t cmd = 0x85;  // Motor forward
  if (speed < 0)
  {
    cmd = 0x86;  // Motor reverse
    speed = -speed;
  }
  Wire.beginTransmission(smcDeviceNumber);
  Wire.write(cmd);
  Wire.write(speed & 0x1F);
  Wire.write(speed >> 5 & 0x7F);
  Wire.endTransmission();
}
 
uint16_t readUpTime()
{
  Wire.beginTransmission(smcDeviceNumber);
  Wire.write(0xA1);  // Command: Get variable
  Wire.write(28);    // Variable ID: Up time (low)
  Wire.endTransmission();
  Wire.requestFrom(smcDeviceNumber, (uint8_t)2);
  uint16_t upTime = Wire.read();
  upTime |= Wire.read() << 8;
  return upTime;
}

int EmergencyStopCheck()
{
  breaker = 1;
}
 
void setup()
{
  Serial.begin(9600);
  Wire.begin();
  exitSafeStart();
  SetToNeutral();
  pinMode(actPotPin, INPUT);
  pinMode(emergencyStopButton, INPUT);
  attachInterrupt(digitalPinToInterrupt(emergencyStopButton), EmergencyStopCheck, CHANGE);
}
 
void loop()
{
  // Read the up time from the controller and send it to
  // the serial monitor.
  uint16_t upTime = readUpTime();
  Serial.print(F("Up time: "));
  Serial.println(upTime);
 
  if (Serial.available() > 0)
  {
    if (breaker == 0)
    {
      receivedChar = Serial.read();
      if (receivedChar == 'B')
      {
        setMotorSpeed(-1500);
        delay(50);
        setMotorSpeed(1500);
        delay(50);
        setMotorSpeed(0);
      } 
    }
  }
}

// Initial Function to send Acutator to Neutral Position
void SetToNeutral()
{   
  for(int motorSet = 0; motorSet <= 5; motorSet++)
  {
    int readActPos = analogRead(actPotPin);
    Serial.println(readActPos);
    
    if (readActPos >= 500)
    {
      while(readActPos >= 500)
      {
        setMotorSpeed(900);
        delay(10);
        readActPos = analogRead(actPotPin);
      }
      setMotorSpeed(0);
    }
    
    else if (readActPos < 500)
    {
      while(readActPos < 500)
      {
        setMotorSpeed(-900);
        delay(10);
        readActPos = analogRead(actPotPin);
      }
      setMotorSpeed(0);
    }
    delay(1500);
  }
}
