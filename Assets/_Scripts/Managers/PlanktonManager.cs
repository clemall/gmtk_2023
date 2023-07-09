using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

public class PlanktonManager : MonoBehaviour
{
    
    private IEnumerator _coroutineSpawnPlankton;

    public List<GameObject> planktonList;
    public Collider2D spawningArea;
    public Collider2D spawningArea2;
    public Collider2D spawningArea3;

    public float waitTime = 0.2f;

    public float burstMode = 0.8f;
    
    public List<GameObject> planktonsInGame = new List<GameObject>();
    public List<GameObject> planktonsInGame2 = new List<GameObject>();
    public List<GameObject> planktonsInGame3 = new List<GameObject>();
    
    private static PlanktonManager _instance;
    public static PlanktonManager instance
    {
        get
        {
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<PlanktonManager>();
            return _instance;
        }
    }
    
    void Start()
    {
        _coroutineSpawnPlankton = Spawn();
        
        StartCoroutine(_coroutineSpawnPlankton);
        
    }
    

    void Update()
    {
        if (GameManager.instance.isPause || GameManager.instance.isGameover)
        {
            return;
        }

      
        // waitTime -= 0.1f;
        // waitTime = Mathf.Clamp(waitTime, 0.4f, 10f);
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

            if (planktonsInGame.Count < 30)
            {
              SpawnPlankton(spawningArea, planktonsInGame); 
            }
            if (planktonsInGame2.Count < 50)
            {
              SpawnPlankton(spawningArea2,planktonsInGame2); 
            }
            if (planktonsInGame3.Count < 200)
            {
              SpawnPlankton(spawningArea3, planktonsInGame3); 
            }
            
        }
    }

    void SpawnPlankton(Collider2D area,  List<GameObject> planktons)
    {
      
        Vector3 spawnPosition = RandomPointInBounds(area.bounds);

  
        GameObject p = planktonList[Random.Range(0, planktonList.Count)];
        GameObject go = Instantiate(p, spawnPosition, Quaternion.identity) as GameObject;

        go.transform.parent = transform;

        planktons.Add(go);
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