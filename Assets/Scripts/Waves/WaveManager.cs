using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaveManager : MonoBehaviour
{

    [SerializeField] List<Wave> waves = new List<Wave>();
    public float timeBetweenSpawns;
    float countDown = 2f;
    int currentWave = 0;
    public List<GameObject> enemiesInWave = new List<GameObject>();

    public static WaveManager instance;
    public bool canCountDownWave;
    bool stilSpawningEnemies = false;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canCountDownWave)
        {
            countDown -= Time.deltaTime;
        }
        if(countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = 2f;
            canCountDownWave = false;
        }
    }

    private IEnumerator SpawnWave()
    {
        for(int i = 0; i < waves[currentWave].enemyCluster.Count; i++)
        {
            for(int j = 0; j < waves[currentWave].enemyCluster[i].amount; j++)
            {
                stilSpawningEnemies = true;
                GameObject newEnemy = Instantiate(waves[currentWave].enemyCluster[i].enemyPrefab, transform.position, Quaternion.identity);
                enemiesInWave.Add(newEnemy);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
        currentWave++;
        stilSpawningEnemies = false;
        yield return null;
    }

    public void RemoveEnemyFromList(GameObject enemyToRemove)
    {
        enemiesInWave.Remove(enemyToRemove);
        if(enemiesInWave.Count == 0 && !stilSpawningEnemies)
        {
            ShopManager.instance.StartShop();
        }
    }
}

[System.Serializable]
public class Cluster
{
    public int amount;
    public GameObject enemyPrefab;
}

[System.Serializable]
public class Wave
{
    public List<Cluster> enemyCluster;
}