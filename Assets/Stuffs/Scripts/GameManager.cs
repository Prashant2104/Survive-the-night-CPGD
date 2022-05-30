using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPointsParent;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] zombies;
    [SerializeField] GameObject[] collectibles;

    int randomChoice_1;
    int randomChoice_2;
    private void Awake()
    {
        for (int i = 0; i < spawnPointsParent.transform.childCount; i++)
        {
            spawnPoints[i] = spawnPointsParent.transform.GetChild(i).gameObject;
            spawnPoints[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
            SpawnZombies(Random.Range(2, 10));
    }
    public void SpawnZombies(int _lvl)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            randomChoice_1 = Random.Range(0, 2);
            if (randomChoice_1 == 1)
            {
                spawnPoints[i].SetActive(true);
                for (int j = 0; j < _lvl/2; ++j)
                {
                    randomChoice_1 = Random.Range(0, zombies.Length);
                    randomChoice_2 = Random.Range(0, 2);
                    switch (randomChoice_2)
                    {
                        case 0:
                            Instantiate(zombies[randomChoice_1], spawnPoints[i].transform.position + new Vector3(j, 0, 0), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(zombies[randomChoice_1], spawnPoints[i].transform.position + new Vector3(0, 0, j), Quaternion.identity);
                            break;
                    }                   
                }
            }
            else
            {
                spawnPoints[i].SetActive(false);
            }
        }
    }
}