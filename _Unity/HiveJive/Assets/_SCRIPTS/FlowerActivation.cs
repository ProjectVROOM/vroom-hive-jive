using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerActivation : MonoBehaviour
{
    Collider collider;
    public ParticleSystem particleEffect;
    public ParticleSystem activatedParticles;

    public AudioClip flowerSound;
    AudioSource soundSource;

    private void Start()
    {
        collider = GetComponent<Collider>();
        if(flowerSound != null && soundSource != null)
            soundSource.clip = flowerSound;
    }

    /*
     * If the player collides with this object, the flower action will happen.
     */
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collider.enabled = false;
            FlowerAction();
        }
    }


    /*
     * General actions the flower will perform: activate a particle effect and play a sound clip
     */
    private void FlowerAction()
    {
        FindObjectOfType<PlayerUIManager>().AddFlower();

        if(particleEffect != null)
            particleEffect.Play();
            activatedParticles.Play();

        if(flowerSound != null && soundSource != null)
            soundSource.PlayOneShot(flowerSound);

        Debug.Log("Flower activated.");
    }
}
