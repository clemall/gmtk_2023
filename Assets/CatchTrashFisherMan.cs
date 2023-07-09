using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTrashFisherMan : MonoBehaviour
{
    public LayerMask layer;

    public Fisherman fisherman;
    
    public FishPole playerFishPole;
    
    private bool hasBeenCatch = false;

    private void Start()
    {
        playerFishPole = GameObject.Find("playerFishPole").GetComponent<FishPole>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if ((layer.value & (1 <<col.gameObject.layer)) > 0)
        {
            if (hasBeenCatch)
            {
                return;
            }
            
            playerFishPole.ResetTrigger();

            hasBeenCatch = true;
            col.transform.SetParent(transform);

            fisherman.GoUp();
        }
    }
}
