﻿using UnityEngine;
using System.Collections;

public class LandingDetectionManager : MonoBehaviour
{
    [Tooltip("What speed does a ship crash at.")]
    public float maxLandingSpeed;   //over what speed does a ship crash at
    [Tooltip("Delay for the ship to reset after crashing.")]
    public float animationDelay;    //delay for animation to play

    [HideInInspector]
    public bool frontLanded;        //the bool connected with the front

    [HideInInspector]
    public bool endLanded;          // the bool connected with the back

    public GameObject question;     // Needs to be public because inactive gameObjects cannot be "found"

    [HideInInspector]
    public int platformPoints;      //the points coming from the platform you're landing

    public static bool hasFinished; //the activation of the win condition, prevents the effect of winning from happening multiple times

    private bool isDelayed;         //checking if delay is happening

    private void Start()
    {
        hasFinished = false;
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (frontLanded || endLanded) //when either one points have hit the pad
            Crashing();

        if (frontLanded && endLanded) //when both points have landed
        {
            if (!hasFinished)                                                                //activate win condition once
            {
                Debug.Log("YOU'VE LANDED!");
                PointsManager.AddPoints(platformPoints);                                    // add the points of the platform
                int fuelRemaining = (int) FuelConsumption.fuelAmount;                       // get points for fuel
                PointsManager.AddPoints(fuelRemaining);                                     // add the remaining fuel
                Time.timeScale = 0;                                                         // Stop time while question is being answered
                question.SetActive(true);                                                   // Allow pop up to show
                hasFinished = true;                                                         
            }
        }
    }

    public void Crashing()
    {
        if (Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.y) > maxLandingSpeed)   //if speed is greater...
        {
            Debug.LogError("WE'VE Crashed!");
            hasFinished = true;                                                 //prevents score from being added

            StartCoroutine(DeathDelay());    
        }
    }

    IEnumerator DeathDelay()
    {
        if (!isDelayed)
        {
            isDelayed = true;                                                   //prevents delay from happening twice
            GetComponent<ShipControls>().enabled = false;                       //prevents player from moving when crashed
            yield return new WaitForSeconds(animationDelay);                    //delay for animation
            DeathManager.DeathActions();                                        //DEATH OCCURS
            GetComponent<ShipControls>().enabled = true;                        //reset controls
            isDelayed = false;                                                  //allows delay to happen again
        }
    }
}