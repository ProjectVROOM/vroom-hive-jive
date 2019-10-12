using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Variables

    public Renderer rend;
    public GameObject endGameTextAnimation;
    public float fadeTime = 2.0f;
    public float gameDuration = 3.0f;

    [HideInInspector]
    public bool bikeCalibrated = false;

    private bool gameEnded = false;
    private float gameTime;
    private PlayerUIManager uIManager;
    //public GameObject fadeSphere;
    //public Material fadeMaterial;

    #endregion

    #region Unity Methods

    private void Start()
    {
        gameTime = 0f;
        StartCoroutine(TimerUpdate());
        uIManager = FindObjectOfType<PlayerUIManager>();
        //fadeMaterial = fadeSphere.gameObject.GetComponent<Material>();
    }

    private IEnumerator TimerUpdate()
    {
        yield return new WaitUntil(() => bikeCalibrated);
        while(gameTime < gameDuration)
        {
            gameTime += Time.deltaTime;
            uIManager.timerText.text = "Seconds Left: " + gameTime/1000;
            yield return null;
        }
        EndGame();
    }

    /*
     * Performs the actions indicating the game has ended
     */
    private void EndGame()
    {
        gameEnded = true;

        StartCoroutine(FadeOutTest());

        StartCoroutine(WaitToActivateAnimation(endGameTextAnimation));

        Debug.Log("End Game");
    }


    /*
     * Change alpha on black sphere to fade out for VR scene
     */
    private IEnumerator FadeOutTest()
    {
        Debug.Log("FadeOutTest Started");

        for(float f = 0f; f <= 1; f += 0.05f)
        {
            Color fadeColor = rend.material.color;
            fadeColor.a = f;
            rend.material.color = fadeColor;
            yield return new WaitForSeconds(fadeTime/20f);
        }
    }


    /*
     * Activate a gameObject after a designated amount of time
     */
    private IEnumerator WaitToActivateAnimation(GameObject objectToActivate)
    {
        yield return new WaitForSeconds(fadeTime);

        objectToActivate.SetActive(true);
    }

    #endregion
}
