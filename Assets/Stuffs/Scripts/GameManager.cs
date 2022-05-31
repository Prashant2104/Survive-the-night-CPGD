using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPointsParent;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] zombies;
    [SerializeField] GameObject[] healthItems;
    [SerializeField] int collectibles;    
    [SerializeField] int collected = 0;
    public bool canPass = false;

    [SerializeField] GameObject uiPanel;
    [SerializeField] GameObject winPanel;

    int randomChoice_1;
    int randomChoice_2;
    private void Awake()
    {
#if UNITY_ANDROID
        uiPanel.SetActive(true);
#endif
        for (int i = 0; i < spawnPointsParent.transform.childCount; i++)
        {
            spawnPoints[i] = spawnPointsParent.transform.GetChild(i).gameObject;
            spawnPoints[i].SetActive(false);
        }
        SpawnZombies(Random.Range(2,5));
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
                    Instantiate(healthItems[randomChoice_1], spawnPoints[i].transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                }
            }
            else
            {
                spawnPoints[i].SetActive(false);
            }
        }
    }
    public void OnCollect()
    {
        collected++;
        if (collected >= collectibles)
            canPass = true;
    }
    public void Pass()
    {
        if (!canPass)
            return;

        winPanel.SetActive(true);
        StartCoroutine(Won());
    }
    IEnumerator Won()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }
}