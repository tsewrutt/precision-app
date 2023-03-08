using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Diagnostics;
using System;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // the target prefab to be spawned
    public Vector3 boxMinPt;
    public Vector3 boxMaxPt;// the box region where targets can spawn
    public int numTargets; // the number of targets to spawn
    public MainScript ms;
    //stopwatch stuff
    private Stopwatch stopwatch;
    private bool checkStopWatch = false;
    private float totalTimeTaken;
   
    public List<GameObject> targets = new List<GameObject>();

    //game script help
    public bool roundDone;
    void Start()
    {
        Bounds boxRegion = new Bounds((boxMaxPt + boxMinPt) / 2, boxMaxPt - boxMinPt);
        roundDone = false;
        stopwatch = new Stopwatch();
    }

    private Vector3 GetRandomPosition()
    {
        // Get a random position within the spawn range
        float x = Random.Range(boxMinPt.x, boxMaxPt.x);
        float y = Random.Range(boxMinPt.y, boxMaxPt.y);
        float z = Random.Range(boxMinPt.z, boxMaxPt.z);
        return new Vector3(x, y, z);
    }

    public void SpawnTargets()
    {
        if(checkStopWatch == false)
        {
            stopwatch.Reset();
            stopwatch.Start();
            checkStopWatch = true;
        }

        if(targets.Count > 0)
        {
            // Select a random target prefab from the list
            GameObject prefab = targets[Random.Range(0, targets.Count)];

            // Spawn the selected target at a random position within the region
            Vector3 spawnPosition = GetRandomPosition();
            Instantiate(prefab, spawnPosition, Quaternion.identity);

            // Remove the selected target from the list
            targets.Remove(prefab);

            // Wait until the target is destroyed before spawning the next one
            StartCoroutine(WaitForTargetDestroyed());
        }
        else
        {
            //when all targets are destroyed
            stopwatch.Stop();
            //set to false again for next target run, so it resets and start recording again
            checkStopWatch = false;
            displayRoundTime();
            roundDone = true;
            ms.game_status = MainScript.ColorBackground.White;
            ms.roundflag = true;
        }
    }

    public IEnumerator WaitForTargetDestroyed()
    {

        while(GameObject.FindWithTag("target") != null)
        {
            yield return null;
        }

        //otherwise spawn next target
        SpawnTargets();
    }

    private void displayRecordTable()
    {
        UnityEngine.Debug.Log("==========================");
        UnityEngine.Debug.Log("Results");
        UnityEngine.Debug.Log("==========================");
        UnityEngine.Debug.Log("Background: Black");
        UnityEngine.Debug.Log("---------------------------");
        UnityEngine.Debug.Log("Crosshair Color: Red");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Green");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Blue");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Black");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: White");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("---------------------------");
        UnityEngine.Debug.Log("Background: White");
        UnityEngine.Debug.Log("---------------------------");
        UnityEngine.Debug.Log("Crosshair Color: Red");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Green");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Blue");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Black");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: White");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("---------------------------");
        UnityEngine.Debug.Log("Background: Green");
        UnityEngine.Debug.Log("---------------------------");
        UnityEngine.Debug.Log("Crosshair Color: Red");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Green");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Blue");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: Black");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("Crosshair Color: White");
        UnityEngine.Debug.Log("Time Taken to hit targets:" + 0);
        UnityEngine.Debug.Log("---------------------------");

        //for colorblindness// UnityEngine.Debug.Log("Crosshair Color: Purple");
    }
    private void displayRoundTime()
    {

        TimeSpan elapsedTime = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
        totalTimeTaken += stopwatch.ElapsedMilliseconds;

        UnityEngine.Debug.Log("Elapsed Time: " + elapsedTime.ToString("mm':'ss"));
    }



    //this will be displayed at the end of the testing
    private void displayTotalTimeTaken()
    {
        TimeSpan elapsedTime = TimeSpan.FromMilliseconds(totalTimeTaken);
        UnityEngine.Debug.Log("Elapsed Time: " + elapsedTime.ToString("mm':'ss"));
    }

}
