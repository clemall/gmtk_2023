using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.UIElements;

public class Patrol : MonoBehaviour {

        public Transform[] points;
        private float speed = 1.8f;
        private float fastSpeed = 4.5f;
        private int pIndex = 0;
        
        private float chaseTime = 1.3f;
        private bool canChase = true;
        private bool doChase = false;
        private float chaseDistance = 5.5f;
        private float waitTime = 2f;
        
        private SpriteRenderer sr;

        public Transform target;

        public AudioSource patrolSound;
        public AudioSource attackSound;


        void Start () {
           transform.position = points[pIndex].transform.position;
           
           sr = gameObject.GetComponent<SpriteRenderer>();
        }

        private IEnumerator StopChase()
        {
            while (true)
            {
                yield return new WaitForSeconds(chaseTime);
                doChase = false;
                StartCoroutine("CanChaseAgainChase");
                StopCoroutine("StopChase");
            }
        }
        
        private IEnumerator CanChaseAgainChase()
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                canChase = true;
                StopCoroutine("CanChaseAgainChase");
            }
        }


        void Update () {
            
            if (GameManager.instance.isPause || GameManager.instance.isGameover)
            {
                return;
            }

            if (canChase && Vector2.Distance(target.transform.position, transform.position) < chaseDistance)
            {
                doChase = true;
                canChase = false;
                StartCoroutine("StopChase");
            }

            if (doChase)
            {
                patrolSound.enabled = false;
                attackSound.enabled = true;
                transform.position = Vector2.MoveTowards(transform.position,target.position, fastSpeed * Time.deltaTime);
                
                if (transform.position.x > target.position.x)
                {
                    Vector3 newScale = transform.localScale;
                    newScale.x = 1;
                    transform.localScale = newScale;
                }
                else
                {
                    Vector3 newScale = transform.localScale;
                    newScale.x = -1;
                    transform.localScale = newScale;
                }
                return;
            }
            
            
            if (pIndex <= points.Length - 1)
            {
                patrolSound.enabled = true;
                attackSound.enabled = false;
                transform.position = Vector2.MoveTowards(transform.position,points[pIndex].transform.position, speed * Time.deltaTime);
                if (transform.position == points[pIndex].transform.position)
                {
                    pIndex++;
                    
                    if (pIndex == points.Length)
                    {
                        pIndex = 0;
                    }
                    
                    if (transform.position.x > points[pIndex].transform.position.x)
                    {
                        Vector3 newScale = transform.localScale;
                        newScale.x = 1;
                        transform.localScale = newScale;
                    }
                    else
                    {
                        Vector3 newScale = transform.localScale;
                        newScale.x = -1;
                        transform.localScale = newScale;
                    }
                }
            }
        }
    }