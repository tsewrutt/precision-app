using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // the target prefab to be spawned
    public Vector3 boxMinPt;
    public Vector3 boxMaxPt;// the box region where targets can spawn
    public int numTargets; // the number of targets to spawn
    private float spawnDelay;
    private int targetsSpawned = 0;
    public List<GameObject> targets = new List<GameObject>();

    void Start()
    {
        Bounds boxRegion = new Bounds((boxMaxPt + boxMinPt) / 2, boxMaxPt - boxMinPt);

        SpawnTargets();
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
    }

    private IEnumerator WaitForTargetDestroyed()
    {

        while(GameObject.FindWithTag("target") != null)
        {
            yield return null;
        }

        //otherwise spawn next target
        SpawnTargets();
    }

}
