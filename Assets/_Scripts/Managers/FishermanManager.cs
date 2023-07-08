using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

public class FishermanManager : MonoBehaviour
{
    private IEnumerator _coroutineSpawnFisherman;

    public List<GameObject> fishermanList;
    public Collider2D spawningArea;
    
    public float waitTime = 1f;
    public float difficultyFactor = 10f;
    private float _difficultyFactorTimer = 0f;
    public float burstMode = 0.8f;

    public int maxFisherman = 15;
    
    public List<GameObject> fishermanInGame = new List<GameObject>();
    
    
    private static FishermanManager _instance;
    public static FishermanManager instance
    {
        get
        {
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<FishermanManager>();
            return _instance;
        }
    }
    
    void Start()
    {
        _coroutineSpawnFisherman = Spawn();
        
        StartCoroutine(_coroutineSpawnFisherman);
        
    }
    
    void Update()
    {
        if (GameManager.instance.isPause || GameManager.instance.isGameover)
        {
            return;
        }

        _difficultyFactorTimer += Time.deltaTime;

        if (_difficultyFactorTimer > difficultyFactor)
        {
            _difficultyFactorTimer = 0;

            waitTime -= 0.1f;
            waitTime = Mathf.Clamp(waitTime, 0.4f, 10f);
        }
    }
    
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);


            if (GameManager.instance.isPause || GameManager.instance.isGameover)
            {
                continue;
            }
            
            if (fishermanInGame.Count < maxFisherman)
            {
                // burst mode
                if (Random.value > burstMode)
                {
                    SpawnFisherman(Random.Range(2,5));
                }
                else
                {
                    SpawnFisherman(1);
                }
            }

            
            
        }
    }

    void SpawnFisherman(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = RandomPointInBounds(spawningArea.bounds);


            GameObject p = fishermanList[Random.Range(0, fishermanList.Count)];
            GameObject go = Instantiate(p, spawnPosition, Quaternion.identity) as GameObject;

            go.transform.parent = transform;

            fishermanInGame.Add(go);
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
