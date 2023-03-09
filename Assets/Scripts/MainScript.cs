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

    //Audio
    public GameObject go;
    public GameObject one;
    public GameObject two;
    public GameObject three;

    private AudioSource sound;
    //  private bool displayText;
    private float timeRemaining;

    //Crosshair Color Config 
    public Image[] images;
    public Color red;
    public Color green;
    public Color blue;
    public Color black;
    public Color white;

    //Wall Color Config
    public GameObject[] walls;
    public Material black_mat;
    public Material white_mat;
    public Material green_mat;

    //flags
    public bool roundflag = true;
    public int crossflag = 0;
    private void Start()
    {
        //nothing happens here
        //  RunMainTest();
        SetDelayTime();
        //stopwatch = new Stopwatch();

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
                    BackgroundColorChanger(black_mat);
                    switch (crossflag)
                    {
                        case 0:
                            CrosshairColorChanger(red);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;

                        case 1:
                            CrosshairColorChanger(green);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;

                        case 2:
                            CrosshairColorChanger(blue);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                        case 3:
                            CrosshairColorChanger(black);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                        case 4:
                            CrosshairColorChanger(white);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                    }
                    
                   // targetSpawn.roundDone = false;
                    Debug.Log("BlackSesh");

                    break;

                case ColorBackground.White:
                    BackgroundColorChanger(white_mat);
                    switch (crossflag)
                    {
                        case 0:
                            CrosshairColorChanger(red);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;

                        case 1:
                            CrosshairColorChanger(green);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;

                        case 2:
                            CrosshairColorChanger(blue);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                        case 3:
                            CrosshairColorChanger(black);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                        case 4:
                            CrosshairColorChanger(white);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                    }
                    Debug.Log("White Sesh");
                    break;

                case ColorBackground.Blue:
                    BackgroundColorChanger(green_mat);
                    switch (crossflag)
                    {
                        case 0:
                            CrosshairColorChanger(red);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;

                        case 1:
                            CrosshairColorChanger(green);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;

                        case 2:
                            CrosshairColorChanger(blue);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                        case 3:
                            CrosshairColorChanger(black);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                        case 4:
                            CrosshairColorChanger(white);
                            SpawnContinueTarget();
                            SetDelayTime();
                            StartCoroutine(WaitForStartTargetDestroyed());
                            break;
                    }
                    Debug.Log("Blue Sesh");
                    break;
            }
            roundflag = false;
        }
        
    }

    private void SetDelayTime()
    {
        timeRemaining = delayTime;
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
        if(timeRemaining == 3)
        {
            sound = three.GetComponent<AudioSource>();
            sound.Play();
        }
       
        while(timeRemaining > 0)
        {
            if(timeRemaining == 2)
            {
                sound = two.GetComponent<AudioSource>();
                sound.Play();
            }
            if (timeRemaining == 1)
            {
                sound = one.GetComponent<AudioSource>();
                sound.Play();
            }
            timer_txt.text = timeRemaining.ToString("F0");
            yield return new WaitForSeconds(1f);

                
            timeRemaining--;
        }
        if (timeRemaining == 0)
        {
            sound = go.GetComponent<AudioSource>();
            sound.Play();
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

    private void BackgroundColorChanger(Material mat)
    {
        foreach (GameObject wall in walls)
        {
            Renderer renderer = wall.GetComponent<Renderer>();
            renderer.material = mat;
        }
    }

    private void SpawnContinueTarget()
    {
        Instantiate(startTargetPrefab, spawnPosition, Quaternion.identity);
    }

/*    IEnumerator WaitUntilRoundIsDone()
    {
        while(targetSpawn.roundDone != true)
        {
            yield return null;
        }
    }*/
}
