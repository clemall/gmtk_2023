using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTrashFisherMan : MonoBehaviour
{
    public LayerMask layer;
    void OnTriggerEnter2D(Collider2D col)
    {
        print(col);
        
        if ((layer.value & (1 <<col.gameObject.layer)) > 0)
        {
            col.transform.SetParent(transform);
        }
    }
}
