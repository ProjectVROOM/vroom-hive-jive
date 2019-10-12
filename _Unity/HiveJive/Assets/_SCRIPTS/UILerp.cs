using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILerp : MonoBehaviour
{
    public GameObject uiTarget;
    public GameObject uiPivot;
    public BikeMovement bikeMovement;
    public float rotationSpeed = 1000f;
    public float driftTime = 0.0001f;
    public float lagScale = 100f;
    public float lagMagnitude;
    public float lagCap = 0.3f;

    private Vector3 velocity = Vector3.zero;
    
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, uiPivot.transform.rotation, Time.deltaTime * rotationSpeed);

        Vector3 lagPosition = uiTarget.transform.position - Vector3.ClampMagnitude((bikeMovement.curVelocity / lagScale), 1);
        lagMagnitude = Vector3.Distance(uiTarget.transform.position, lagPosition);
        if(lagMagnitude < lagCap)
            transform.position = Vector3.SmoothDamp(transform.position, lagPosition, ref velocity, Time.deltaTime * driftTime);
        else if(lagMagnitude > lagCap && lagMagnitude < lagCap + 0.2f)
            transform.position = Vector3.SmoothDamp(transform.position, lagPosition, ref velocity, Time.deltaTime * driftTime/1000);
        else
            transform.position = lagPosition;

    }
}
