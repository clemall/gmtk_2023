using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fisherman : MonoBehaviour
{

    public float maxDepthDistance = -40f;
    public float minDepthDistance = 1.5f;

    public float durationAnimation = 4f;

    private float initialPosition;

    public Transform pixel;

    void Start()
    {
        initialPosition = transform.position.y;
        float finalPosition = Random.Range(minDepthDistance, maxDepthDistance);
        float ratio = Mathf.Abs(finalPosition) /  Mathf.Abs(maxDepthDistance);
        float extraTime = ratio * durationAnimation;
        transform.DOMoveY(finalPosition, durationAnimation + extraTime).SetId("toBePause").SetEase(Ease.OutQuad).OnComplete(GoUp);

        float finalScale = Mathf.Abs(finalPosition - initialPosition) * 32; // unity unit of the sprite
        Vector3 newScale = pixel.localScale;
        newScale.y = finalScale;
        pixel.localScale = newScale;
    }

    void GoUp()
    {
        transform.DOMoveY(initialPosition, durationAnimation * 2).SetId("toBePause").SetDelay(durationAnimation).SetEase(Ease.InQuad).OnComplete(Delete);;
    }
    
    void Delete()
    {
        FishermanManager.instance.fishermanInGame.Remove(transform.gameObject);
        Destroy(transform.gameObject);
    }
}
