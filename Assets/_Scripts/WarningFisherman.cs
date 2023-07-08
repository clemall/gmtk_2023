using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningFisherman : MonoBehaviour
{

    public Transform camera;
    
    public List<GameObject> warnings = new List<GameObject>();
    void Start()
    {
        foreach(Transform child in transform)
        {
            warnings.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < warnings.Count; i++)
        {
            warnings[i].SetActive(false);
        }
    
        for (int i = 0; i < FishermanManager.instance.fishermanInGame.Count; i++)
        {
            if (FishermanManager.instance.fishermanInGame[i].transform.position.y > camera.position.y + 5.5f &&
                Mathf.Abs(FishermanManager.instance.fishermanInGame[i].transform.position.y - camera.position.y + 5.5f) < 23f
                )
            {
                warnings[i].SetActive(true);
                Vector3 newPosition = warnings[i].transform.position;
                newPosition.x = FishermanManager.instance.fishermanInGame[i].transform.position.x;
                warnings[i].transform.position = newPosition;
            }
        }
    }
}
