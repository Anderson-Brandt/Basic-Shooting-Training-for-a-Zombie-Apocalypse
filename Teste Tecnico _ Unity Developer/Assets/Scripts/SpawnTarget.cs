using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public List<GameObject> TargetSpawn = new List<GameObject>();
    private GameObject currentTarget;

    private float timeCount;
    public float spawntime;

    // Update is called once per frame
    private void Update()
    {
        TargetSpawner();
    }

    void TargetSpawner()
    {
        if (currentTarget == null)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= spawntime)
            {

                CreateTarget();
                timeCount = 0f;
            }
        }
        
    }

    void CreateTarget()
    {
        GameObject e = Instantiate(TargetSpawn[Random.Range(0, TargetSpawn.Count)], transform.position, transform.rotation);
        currentTarget = e;

    }
}
