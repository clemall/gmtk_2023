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

    private bool hisGoingUp = false;

    public Transform pixel;

    private Sequence tweenDown;

    void Start()
    {
        
        tweenDown = DOTween.Sequence();
        
        initialPosition = transform.position.y;
        float finalPosition = Random.Range(minDepthDistance, maxDepthDistance);
        float ratio = Mathf.Abs(finalPosition) /  Mathf.Abs(maxDepthDistance);
        float extraTime = ratio * durationAnimation;
        tweenDown.Append(transform.DOMoveY(finalPosition, durationAnimation + extraTime).SetId("toBePause").SetEase(Ease.OutQuad).OnComplete(GoUpNormal));

        float finalScale = Mathf.Abs(finalPosition - initialPosition) * 32; // unity unit of the sprite
        Vector3 newScale = pixel.localScale;
        newScale.y = finalScale;
        pixel.localScale = newScale;
    }
    
    public void GoUp()
    {
        if (hisGoingUp)
        {
            return;
        }
        tweenDown.Kill();
        hisGoingUp = true;
        float d = durationAnimation;
 
        transform.DOMoveY(initialPosition, durationAnimation * 2).SetId("toBePause").SetEase(Ease.InQuad).OnComplete(Delete);;
    }

    public void GoUpNormal()
    {
        if (hisGoingUp)
        {
            return;
        }
        tweenDown.Kill();
        hisGoingUp = true;
        float d = durationAnimation* 2;;
        transform.DOMoveY(initialPosition, durationAnimation * 2).SetId("toBePause").SetDelay(durationAnimation).SetEase(Ease.InQuad).OnComplete(Delete);;
    }
    
    void Delete()
    {
        FishermanManager.instance.fishermanInGame.Remove(transform.gameObject);
        Destroy(transform.gameObject);
    }
}
