using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enermyPrefab;
    [SerializeField] [Range(0.1f, 30f)] float spawnTime = 1f;

    [SerializeField][Range(0, 50)] int poolSize = 5;

    GameObject[] pool;



    void Awake()
    {
        PopulatePool();
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; ++i)
        {
            pool[i] = Instantiate(enermyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }



    void EnableObjectInPool()
    {
        for(int i=0; i < pool.Length; ++i)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
       
        while (true)
        {
            EnableObjectInPool();

            yield return new WaitForSeconds(spawnTime);
        }
    }


}
