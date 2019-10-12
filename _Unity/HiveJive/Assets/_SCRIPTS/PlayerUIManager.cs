using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    public Image[] beeFillImages;
    public TextMeshProUGUI flowerCountText;
    public TextMeshProUGUI timerText;
    public Sprite deadBeeSprite;
    public bool hasBees;

    private int flowerCount = 0;
    [SerializeField]
    private int beeCount = 0;
    private int deadBeeCount = 0;

    public void AddBee()
    {
        hasBees = true;
        if(beeCount <= beeFillImages.Length)
        {
            beeFillImages[beeCount].color = Color.white;
            deadBeeCount = beeCount;
            beeCount++;
        }
    }

    public void KillBee()
    {
        if(hasBees)
        {
            int loopCheck = 0;
            while(beeFillImages[deadBeeCount].sprite.Equals(deadBeeSprite) && loopCheck < 6)
            {
                deadBeeCount -= 1;
                if(deadBeeCount == -1)
                    deadBeeCount = 5;
                loopCheck++;
            }
            beeFillImages[deadBeeCount].sprite = deadBeeSprite;
            if(deadBeeCount > 0)
                deadBeeCount -= 1;
        }
    }

    public void AddFlower()
    {
        flowerCount += 1;
        flowerCountText.text = "x " + flowerCount;
    }
}
