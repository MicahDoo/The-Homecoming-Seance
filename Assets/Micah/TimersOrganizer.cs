using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimersOrganizer : MonoBehaviour {

    static public float timeLeftForGame = 60f * 60f;
    static public bool timerForGameOn = false;

    static public float timeLeftForPrompt = 5f;
    static public bool timerForPromptOn = false;
    static public bool timerForRightClickOn = false;
    static public Image rightClickImage;

    static public float timeLeftForPromptFadeIn = 1f;
    static public bool timerForPromptFadeInOn = false;

    static public float timeLeftForPromptFade = 1f;
    static public bool timerForPromptFadeOn = false;

    //How do I set up a good system to remind player?
    static public float timeSinceLastActivity = 0f;
    static public bool reminderCountdown = false;

    void Start()
    {
        rightClickImage = LanguageControl.gamePrompt.GetComponentInChildren<Image>();
    }
    void Update()
    {
        if (timerForGameOn)
        {
            timeLeftForGame -= Time.deltaTime;
            if(timeLeftForGame <= 0)
            {
                InventoryControl.GameOver();
            }
        }
        if (timerForPromptOn)
        {
            timeLeftForPrompt -= Time.deltaTime;
            if(timeLeftForPrompt <= 0)
            {
                startPromptFade();
            }
        }
        if (timerForPromptFadeOn)
        {
            timeLeftForPromptFade -= Time.deltaTime;
            LanguageControl.gamePrompt.color = new Color(LanguageControl.gamePrompt.color.r, LanguageControl.gamePrompt.color.g, LanguageControl.gamePrompt.color.b, timeLeftForPromptFade / 1f);
            if (timerForRightClickOn)
            {
                rightClickImage.color = new Color(rightClickImage.color.r, rightClickImage.color.g, rightClickImage.color.b, timeLeftForPromptFade / 1f);
            }
            if (timeLeftForPromptFade <= 0)
            {
                LanguageControl.gamePrompt.gameObject.SetActive(false);
                if (timerForRightClickOn)
                {
                    rightClickImage.gameObject.SetActive(false);
                    timeSinceLastActivity = 0f;
                    reminderCountdown = true;
                    timerForRightClickOn = false;
                }
            }
        }
        if (timerForPromptFadeInOn)
        {
            timeLeftForPromptFadeIn -= Time.deltaTime;
            LanguageControl.gamePrompt.color = new Color(LanguageControl.gamePrompt.color.r, LanguageControl.gamePrompt.color.g, LanguageControl.gamePrompt.color.b, (1f - timeLeftForPromptFadeIn) / 1f);
            if (timerForRightClickOn)
            {
                rightClickImage.color = new Color(rightClickImage.color.r, rightClickImage.color.g, rightClickImage.color.b, (1f - timeLeftForPromptFadeIn) / 1f);
            }
            if (timeLeftForPromptFadeIn <= 0)
            {
                timerForPromptFadeInOn = false;
                timerForPromptOn = true;
                timeLeftForPrompt = 5f;
            }
        }
        if (reminderCountdown)
        {
            timeSinceLastActivity += Time.deltaTime;
            if(timeSinceLastActivity >= 20f)
            {
                timerForRightClickOn = true;
                reminderCountdown = false;
                LanguageControl.setPromptText("Reminder");
                rightClickImage.color = new Color(rightClickImage.color.r, rightClickImage.color.g, rightClickImage.color.b, 0f);
                rightClickImage.gameObject.SetActive(true);
            }
        }
    }

    static public void startPromptFade()
    {
        timerForPromptOn = false;
        timerForPromptFadeOn = true;
        timeLeftForPromptFade = 1f;
    }

    static public void startPrompt()
    {
        Debug.Log("StartPrompt");
        timerForPromptFadeInOn = true;
        timerForPromptOn = false;
        timeLeftForPromptFadeIn = 1f;
        LanguageControl.gamePrompt.color = new Color(LanguageControl.gamePrompt.color.r, LanguageControl.gamePrompt.color.g, LanguageControl.gamePrompt.color.b, 0f);
        LanguageControl.gamePrompt.gameObject.SetActive(true);
    }

    static string getTimeLeft()
    {
        return (((int)timeLeftForGame) / 60).ToString() + ":" + (((int)timeLeftForGame) % 60).ToString();
    }
}
