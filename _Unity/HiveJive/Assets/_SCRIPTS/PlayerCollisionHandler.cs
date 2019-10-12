using System.Collections;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public LayerMask collisionLayer;
    private SerialPort sp;

    private void Start() 
    {
        sp = new SerialPort("COM11", 19200, Parity.None, 8, StopBits.One); //Replace "COM3" with whatever port your Arduino is on.
        sp.DtrEnable = false; //Prevent the Arduino from rebooting once we connect to it. 
        sp.ReadTimeout = 1; //Shortest possible read time out.
        sp.WriteTimeout = 1; //Shortest possible write time out.
        sp.Open();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(collisionLayer.value == 1<<other.gameObject.layer)
        {
            // Debug.Log("Collided");
            sp.Write("B");
        }
    }
}
