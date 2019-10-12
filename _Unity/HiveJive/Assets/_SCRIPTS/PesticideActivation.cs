using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PesticideActivation : MonoBehaviour
{
    public string playerBikeName = "Bike_Player";
    public GameObject objectToActivate;

    /*
     * If the player collides with this object, the attached object will activate
     */
    void OnTriggerEnter(Collider collision)
    {
        // Debug.Log(collision.transform.name);
        if (collision.gameObject.name == playerBikeName)
        {
            // Animator BeeAnimator = collision.gameObject.GetComponent<Animator>();
            // BeeAnimator.SetTrigger("DamageTrigger");
            FindObjectOfType<Leader>().KillBee();
            // objectToActivate.SetActive(true);
            Debug.Log("Player entered pesticide cloud.");
        }
    }


    /*
     * If the player leaves the object, the attached object will deactivate
     */
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // objectToActivate.SetActive(false);
            Debug.Log("Player exited pesticide cloud.");
        }
    }

}
