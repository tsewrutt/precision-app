using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
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
    private int gloabalCounter;
    
    //stopwatch stuff
    private Stopwatch stopwatch;
    private bool checkStopWatch = false;
    private float totalTimeTaken;
   
   
    public List<GameObject> targets = new List<GameObject>();
    private List<GameObject> resettargetarray = new List<GameObject>();
    private List<String> times = new List<String>();

    //game script help
    public bool roundDone = true;
    void Start()
    {
        Bounds boxRegion = new Bounds((boxMaxPt + boxMinPt) / 2, boxMaxPt - boxMinPt);
        //roundDone = false;
        updateTable();
        stopwatch = new Stopwatch();
        foreach(GameObject go in targets)
        {
            resettargetarray.Add(go);
        }
        gloabalCounter = 0;
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
        if(roundDone != false)
        {
            StartTimer();
            roundDone = false;
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
            roundDone = true;
            //set to false again for next target run, so it resets and start recording again
            displayRoundTime();
            //roundDone = true;
            //ms.game_status = MainScript.ColorBackground.White;
            if(ms.crossflag == 0)
            {
                ResetArrayOfPrefabs();
                ms.roundflag = true;
                ms.crossflag = 1;
            }
            else if(ms.crossflag == 1)
            {
                ResetArrayOfPrefabs();
                ms.roundflag = true;
                ms.crossflag = 2;
            }
            else if(ms.crossflag == 2)
            {
                ResetArrayOfPrefabs();
                ms.roundflag = true;
                ms.crossflag = 3;
            }
            else if(ms.crossflag == 3)
            {
                ResetArrayOfPrefabs();
                ms.roundflag = true;
                ms.crossflag = 4;
            }
            else
            {
                //resets for the next target spawner
                ms.crossflag = 0;
                ms.roundflag = true;
                //need to change to next status here
                //check other project to move enum
                ResetArrayOfPrefabs();
                ms.game_status = (MainScript.ColorBackground)(((int)ms.game_status + 1) % Enum.GetNames(typeof(MainScript.ColorBackground)).Length);

                gloabalCounter++;
               
                if(gloabalCounter == 3)
                {
                    //testing is done so update table
                    updateTable();
                    //then reset global counter to zero for next test
                    gloabalCounter = 0;

                    //reset time array
                    times.Clear();

                }
            
            }

            //ms.roundflag = true;
        }
    }

    private void ResetArrayOfPrefabs()
    {
        foreach (GameObject go in resettargetarray)
        {
            targets.Add(go);
        }
    }

    private void StartTimer()
    {
        stopwatch.Reset();
        stopwatch.Start();
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


    private void displayRoundTime()
    {

        TimeSpan elapsedTime = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
        totalTimeTaken += stopwatch.ElapsedMilliseconds;
        
        times.Add(elapsedTime.ToString("mm':'ss"));
        
        UnityEngine.Debug.Log("Elapsed Time: " + elapsedTime.ToString("mm':'ss"));
    }



    //this will be displayed at the end of the testing
    private void displayTotalTimeTaken()
    {
        TimeSpan elapsedTime = TimeSpan.FromMilliseconds(totalTimeTaken);
        UnityEngine.Debug.Log("Elapsed Time: " + elapsedTime.ToString("mm':'ss"));
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


    private void updateTable()
    {
        string filepath = "timesheet.csv";

        //Write Attempt

        string[] rows = File.ReadAllLines(filepath);
        StreamWriter wr = new StreamWriter(filepath, true);

        string[] columns = rows[0].Split(',');
        string[] updatedCol = new string[columns.Length];
        int i = 0;
        foreach(string s in times)
        {
            updatedCol[i] = s;
            i++;
        }
      
        updatedCol[i] = totalTimeTaken.ToString("mm':'ss");
        string updatedRow = string.Join(",", updatedCol);
        wr.WriteLine(updatedRow);

        wr.Close();


    }

    private int removeQuotations(string id)
    {
        int newid;

        string[] splitResult = id.Split('"');
        string s;
        
        s = string.Join("", splitResult).Trim();

        newid = int.Parse(s);

        return newid;
    }

}
