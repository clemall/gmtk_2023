using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Harpoon : MonoBehaviour
{
    private SpriteRenderer image;

    public ParticleSystem ps;

    private float durationAnimation;

    private Transform target;
    
    public AudioSource sound;

    public Collider2D col;

    private void Awake()
    {
         durationAnimation = Random.Range(4f, 8f);
    }

    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }

    void GoDown()
    {
        col.enabled = false;
        transform.DOMoveY(transform.position.y - 0.5f,  1).SetId("toBePause").SetEase(Ease.InQuad).OnComplete(Delete);
        image.DOFade(0, 0.5f).SetId("toBePause");
    }
    
    void Delete()
    {
        HarpoonManager.instance.harpoonInGame.Remove(transform.gameObject);
        Destroy(transform.gameObject);
    }

    public void SetTarget(Transform t)
    {
        target = t;
        transform.DOMove(t.position, durationAnimation).SetId("toBePause").SetEase(Ease.OutQuart).OnComplete(GoDown);

        Vector3 dir = t.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        StartCoroutine("stopParticle");
    }

    public void Update()
    {
        if (target && sound.enabled != true && Vector2.Distance(target.transform.position, transform.position) < 10f)
        {
            sound.enabled = true;
        }
    }

    private IEnumerator stopParticle()
    {
        yield return new WaitForSeconds(durationAnimation - 0.5f);
        ps.Stop();
    }

}
