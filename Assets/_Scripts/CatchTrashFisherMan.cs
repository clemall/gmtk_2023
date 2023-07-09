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
    
    public AudioSource catchTrash;
    
    public Collider2D hook;

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
            
            hook.enabled = false;
            
            
            catchTrash.Play();
            
            playerFishPole.ResetTrigger();

            hasBeenCatch = true;
            col.transform.SetParent(transform);

            fisherman.GoUp();
        }
    }
}
