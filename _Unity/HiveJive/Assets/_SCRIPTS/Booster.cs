using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float boostTime;
    public float boostMultiplier = 2;
    public ParticleSystem windParticles;

    private BikeMovement bikeMovement;

    // Start is called before the first frame update
    void Start()
    {
        bikeMovement = GetComponent<BikeMovement>();
    }

    private IEnumerator BoostTimer()
    {
        float time = boostTime;
        bikeMovement.boostMultiplier = boostMultiplier;
        // windParticles.Play();
        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        // windParticles.Stop();
        bikeMovement.boostMultiplier = 1;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.name.Equals("Booster"))
        {
            StartCoroutine(BoostTimer());
        }
    }
}
