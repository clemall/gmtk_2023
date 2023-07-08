using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

public class HarpoonManager : MonoBehaviour
{
    private IEnumerator _coroutineSpawnHarpoon;

    public List<GameObject> harpoonList;
    public Collider2D spawningArea;
    
    public float waitTime = 1f;
    public float difficultyFactor = 10f;
    private float _difficultyFactorTimer = 0f;
    public float burstMode = 0.8f;

    public int maxHarpoon = 4;
    
    public List<GameObject> harpoonInGame = new List<GameObject>();

    public Transform target;
    
    private static HarpoonManager _instance;
    public static HarpoonManager instance
    {
        get
        {
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<HarpoonManager>();
            return _instance;
        }
    }
    
    void Start()
    {
        _coroutineSpawnHarpoon = Spawn();
        
        StartCoroutine(_coroutineSpawnHarpoon);
        
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
            
            if (harpoonInGame.Count < maxHarpoon)
            {
                // burst mode
                if (Random.value > burstMode)
                {
                    SpawnHarpoon(Random.Range(2,3));
                }
                else
                {
                    SpawnHarpoon(1);
                }
            }

            
            
        }
    }

    void SpawnHarpoon(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = RandomPointInBounds(spawningArea.bounds);


            GameObject p = harpoonList[Random.Range(0, harpoonList.Count)];
            GameObject go = Instantiate(p, spawnPosition, Quaternion.identity) as GameObject;

            go.transform.parent = transform;

            go.GetComponent<Harpoon>().SetTarget(target);

            harpoonInGame.Add(go);
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
