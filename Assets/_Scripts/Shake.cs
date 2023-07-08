using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

public class Shake : MonoBehaviour
{
    // Update is called once per frame

    void Update()
    {
        float offsetX = Random.Range(-1f, 1f);
        float offsety = Random.Range(-1f, 1f);
        
        Vector3 newPosition = transform.position;
        newPosition.x = newPosition.x + Time.deltaTime * offsetX;
        newPosition.y = newPosition.y + Time.deltaTime * offsety;
        transform.transform.position = newPosition;
    }
    
    private float waitTime = 0.2f;
    
    void Start()
    {
        // StartCoroutine("move");
    }
    
    private IEnumerator move()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);


            if (GameManager.instance.isPause || GameManager.instance.isGameover)
            {
                continue;
            }

            float offsetX = Random.Range(-0.1f, 0.1f);
            float offsety = Random.Range(-0.01f, 0.1f);

            Vector3 newPosition = transform.position;
            newPosition.x = newPosition.x +  offsetX;
            newPosition.y = newPosition.y + offsety;
            transform.transform.position = newPosition;
            
        }
    }
}
