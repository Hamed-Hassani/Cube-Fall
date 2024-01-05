using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject spikePlatformPrefab;
    public GameObject [] movingPlatforms;
    public GameObject breakeablePlatform;

    public float platform_Spawn_Timer = 1.8f;
    private float current_Platform_Spawn_Timer;

    private int Platform_Spwan_Count;

    public float min_X = -2, max_X=2 ;

    void Start()
    {
        current_Platform_Spawn_Timer += platform_Spawn_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPlatform();
    }

    void SpawnPlatform()
    {
        current_Platform_Spawn_Timer += Time.deltaTime;

        if (current_Platform_Spawn_Timer >= platform_Spawn_Timer)
        {
            Platform_Spwan_Count++;
            Vector3 temp = transform.position;
            temp.x = Random.Range(min_X, max_X);

            GameObject newPlatform = null;
            if (Platform_Spwan_Count < 2)
            {
                newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
            } else if (Platform_Spwan_Count == 2)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate
                        (movingPlatforms[Random.Range(0, movingPlatforms.Length)],
                        temp, Quaternion.identity);
                }
            } else if (Platform_Spwan_Count == 3)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(spikePlatformPrefab, temp, Quaternion.identity);
                }
            }
            else if (Platform_Spwan_Count == 4)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(breakeablePlatform, temp, Quaternion.identity);
                }

                Platform_Spwan_Count = 0;
            }

            if(newPlatform)
            newPlatform.transform.parent = transform;

            current_Platform_Spawn_Timer = 0f;
        }
    }
}
