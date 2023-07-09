using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPole : MonoBehaviour
{
    public LayerMask layer;
    public LayerMask trashLayerForFishMan;
    // Start is called before the first frame update
    private bool hasBeenTrigger = false;
    public Player player;
    public AudioSource catchTrash;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (hasBeenTrigger)
        {
            return;
        }
        
        if ((layer.value & (1 <<col.gameObject.layer)) > 0)
        {
            catchTrash.Play();
            hasBeenTrigger = true;
            col.transform.SetParent(transform);
            col.gameObject.layer = Mathf.RoundToInt(Mathf.Log(trashLayerForFishMan.value, 2));
        }
    }

    public void ResetTrigger()
    {
        hasBeenTrigger = false;
    }
}
