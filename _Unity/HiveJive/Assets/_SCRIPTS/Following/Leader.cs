using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    #region Variables

    public PlayerUIManager playerUIManager;
    public Follow[] beeFriends;

    private static GameObject[] formationPositions;
    private static int formationCounter = -1; // Being initialized at -1 since we increment it before its first use for finding an array element

    #endregion

    #region Unity Methods

    /*
     * Initializes the array holding the formationPositions by creating an array with a number of elements directly 
     * equal to the number of children this gameObject has. This ensures the array is the proper size to hold 
     * each child gameObject individually.
     */
    private void Start()
    {
        formationPositions = new GameObject[transform.childCount];

        Debug.Log("Number of Children: " + transform.childCount);

        for(int i = 0; i < transform.childCount; i++)
        {
            formationPositions[i] = this.gameObject.transform.GetChild(i).gameObject;
        }

        Debug.Log("These are the formation positions: " + formationPositions);
    }

    /*
     * Allows another object to reference one of the formationPositions as well as moving the formationCounter
     * so that the next object to reference this array will get the next position available.
     */
    public GameObject GetFormationPosition(Follow beeFriend)
    {
        formationCounter++;
        playerUIManager.AddBee();
        beeFriends[formationCounter] = beeFriend;
        return formationPositions[formationCounter];
    }
    
    public void KillBee()
    {
        if(playerUIManager.hasBees)
        {
            playerUIManager.KillBee();
            beeFriends[formationCounter].StopFollowing();
            formationPositions[formationCounter].GetComponentInChildren<ParticleSystem>().Play();
            if(formationCounter > 0)
                formationCounter -= 1;
        }
    }

    #endregion

}
