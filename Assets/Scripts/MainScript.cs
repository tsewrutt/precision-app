using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;

public class MainScript : MonoBehaviour
{
    public enum ColorBackground
    {
        Black,//0
        White,//1
        Blue,//2

    }
    public ColorBackground game_status;

    //prefab
    public GameObject startTargetPrefab;
    public Vector3 spawnPosition;
    public TargetSpawner targetSpawn;
    public float delayTime = 3f;
    public Text timer_txt;

    private Stopwatch stopwatch;
    //  private bool displayText;
    private float timeRemaining;

    //Color Config
    public Image[] images;
    public Color red;
    public Color green;
    public Color blue;
    public Color black;
    public Color white;

    //flags
    public bool roundflag = true;
    private void Start()
    {
        //nothing happens here
        RunMainTest();
        timeRemaining = delayTime;
        stopwatch = new Stopwatch();
        game_status = ColorBackground.Black;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        RunMainTest();
    }

    private void RunMainTest()
    {
        if(roundflag == true)
        {
            switch (game_status)
            {
                case ColorBackground.Black:
                    CrosshairColorChanger(red);
                    SpawnContinueTarget();
                    StartCoroutine(WaitForStartTargetDestroyed());
                    // StartCoroutine(WaitUntilRoundIsDone());
                    //game_status = ColorBackground.White;
                    // CrosshairColorChanger(green);
                    // CrosshairColorChanger(blue);
                    // CrosshairColorChanger(black);
                    //  CrosshairColorChanger(white);
                    targetSpawn.roundDone = false;
                    Debug.Log("BlackSesh");

                    break;

                case ColorBackground.White:
                    CrosshairColorChanger(green);
                    SpawnContinueTarget();
                    StartCoroutine(WaitForStartTargetDestroyed());
                    Debug.Log("White Sesh");
                    break;

                case ColorBackground.Blue:

                    Debug.Log("Blue Sesh");
                    break;
            }
            roundflag = false;
        }
        
    }

    private IEnumerator WaitForStartTargetDestroyed()
    {

        while (GameObject.FindWithTag("starttarget") != null)
        {
            yield return null;
        }

        //waits for 3 seconds
        StartCoroutine(WaitForInputSeconds());
    }

    IEnumerator WaitForInputSeconds()
    {
        while(timeRemaining > 0)
        {
            timer_txt.text = timeRemaining.ToString("F0");
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }
        timer_txt.text = "Go!";
        
        yield return new WaitForSeconds(1f);
        timer_txt.text = "";

       
        StartCoroutine(targetSpawn.WaitForTargetDestroyed());

    }

    private void CrosshairColorChanger(Color color)
    {
        foreach (Image image in images)
        {
            image.color = color;
        }
    }

    private void SpawnContinueTarget()
    {
        Instantiate(startTargetPrefab, spawnPosition, Quaternion.identity);
    }

    IEnumerator WaitUntilRoundIsDone()
    {
        while(targetSpawn.roundDone != true)
        {
            yield return null;
        }
    }
}
