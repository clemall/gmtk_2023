using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask layerDeath;
    public LayerMask layerPlankton;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 movement;

    public float speed = 100f;
    
    public SpriteRenderer darkness;

    private SpriteRenderer sr;
    
    public int energy = 30;
    public int maxEnergy = 30;
    
    public float waitTime = 1f;
    // public float acceleration = 1f;
    // public float deacceleration = 2f;
    // public float maxSpeed = 100f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        
        StartCoroutine("LoseEnergy");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.instance.isPause || GameManager.instance.isGameover)
        {
            return;
        }
        
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x > 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -1;
            transform.localScale = newScale;

            // sr.flipX = true;
        }
        else if (movement.x < 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = 1;
            transform.localScale = newScale;
            
            // sr.flipX = false;
        }
        
        Color tmp = darkness.color;
        float ratio = Mathf.Clamp(Mathf.Abs(transform.position.y), 10f, 50f);
        ratio = ratio - 10f;
        tmp.a = Mathf.Clamp(ratio  / 40f, 0, 1f) ;
        darkness.color = tmp;
        
    }



    private void FixedUpdate()
    {
        if (GameManager.instance.isPause || GameManager.instance.isGameover)
        {
            return;
        }
        // if(speed < maxSpeed && (horizontalInput != 0 || verticalInput != 0)){
        //     speed += acceleration * Time.deltaTime;
        // }
        // if (horizontalInput == 0 && verticalInput == 0)
        // {
        //     speed -= deacceleration * Time.deltaTime;
        // }

        // speed = Mathf.Clamp(speed, 0, maxSpeed);
        
        // rb.velocity = new Vector2(horizontalInput, verticalInput).normalized * speed * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -9.2f, 9.2f);
        newPosition.y = Mathf.Clamp(newPosition.y, -53.4f, 2.40f);
        rb.MovePosition(newPosition);
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if ((layerDeath.value & (1 <<col.gameObject.layer)) > 0)
        {
            GameManager.instance.GameOver();
        }
        else if ((layerPlankton.value & (1 <<col.gameObject.layer)) > 0)
        {
            Destroy(col.gameObject);
            
            AddEnergy();
        }
    }
    
    private IEnumerator LoseEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);


            if (GameManager.instance.isPause || GameManager.instance.isGameover)
            {
                continue;
            }

            energy--;

            if (energy <= 0)
            {
                GameManager.instance.GameOver();
            }

        }
    }

    public void AddEnergy()
    {
        energy++;

        energy = Mathf.Clamp(energy, 0, maxEnergy);
    }
}
