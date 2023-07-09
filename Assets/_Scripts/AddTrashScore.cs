using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

public class AddTrashScore : MonoBehaviour
{
    public LayerMask trashLayerForFishMan;
    // Start is called before the first frame update

    public Player player;
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((trashLayerForFishMan.value & (1 <<col.gameObject.layer)) > 0)
        {
            GameManager.instance.addTrashToScore(col.gameObject.name, col.transform.position.x);
            col.gameObject.SetActive(false);
            Destroy(col.transform.gameObject);
        }
    }
}
