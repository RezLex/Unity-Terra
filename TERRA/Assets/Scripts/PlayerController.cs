﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad = 2f;
    public float maxspeed = 5f;

    public bool grounded;
    public bool wall;
    public float JumpPower = 6.5f;

    private Rigidbody2D rbd2;
    private Animator animacion;
    private SpriteRenderer spr;
    private bool jump;
    private bool doubleJump;
    private bool movement = true;

    public float x;
    public float y;
    public float z;
    public static string Equip;

    public float scale;
    public static float scal;

    public static int side;

    public GameObject[] Esf;

    void Start()
    {
        transform.localScale = new Vector3(-x, y, z);
        scal = scale;
        rbd2 = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        wall = false;
    }


    // Update is called once per frame
    void Update()
    {

        //animacion.SetFloat("Velocidad",Mathf.Abs(rbd2.velocity.x));
        //animacion.SetBool("Grounded", grounded);

        //Doble Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                jump = true;
                doubleJump = true;
            }
            else if (doubleJump)
            {
                jump = true;
                doubleJump = false;
            }
        }

        //Esferas
        if (Input.GetKeyDown(KeyCode.J))
        {
            var pl = GameObject.Find("Jugador");
            Debug.Log("Esferas");

            if (Equip == "esf_N")
            {
                if (pl != null)
                {
                    Instantiate(Esf[0], pl.transform.position, Quaternion.identity);
                    
                }
            }

            if (Equip == "esf_P")
            {
                if (pl != null)
                {
                    Instantiate(Esf[1], pl.transform.position, Quaternion.identity);

                }
            }

            if (Equip == "esf_D")
            {
                if (pl != null)
                {
                    Instantiate(Esf[2], pl.transform.position, Quaternion.identity);
                }
            }

            if (Equip == "esf_T")
            {
                if (pl != null)
                {
                    Instantiate(Esf[3], pl.transform.position, Quaternion.identity);
                }
            }

            if (Equip == "esf_H")
            {
                if (pl != null)
                {
                    Instantiate(Esf[4], pl.transform.position, Quaternion.identity);
                }
            }



        }
    }

    void FixedUpdate()
    {
        //Movimiento
        Vector3 fixedVelocity = rbd2.velocity;
        fixedVelocity.x *= 0.75f;

        if (grounded)
        {
            rbd2.velocity = fixedVelocity;
        }


        float h = Input.GetAxis("Horizontal");
        if (!movement) h = 0;

        rbd2.AddForce(Vector2.right * velocidad * h);

        float limetedSpeed = Mathf.Clamp(rbd2.velocity.x, -maxspeed, maxspeed);
        rbd2.velocity = new Vector2(limetedSpeed, rbd2.velocity.y);
        if (h > 0.1f)
        {
            
            transform.localScale = new Vector3(-x, y, z);
            side = - 1;
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(x, y, z);
            side = 1;
        }

        if (jump)
        {
            rbd2.velocity = new Vector2(rbd2.velocity.x, 0);
            rbd2.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            jump = false;
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Animacion de Vida
        if (collision.gameObject.tag == "life")
        {
            GameObject vida = GameObject.Find("Heart Bar - HUD_0");
            Heart_Bar heart_Bar = vida.GetComponent<Heart_Bar>();
            heart_Bar.hearts--;
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "life")
        {
            GameObject vida = GameObject.Find("Heart Bar - HUD_0");
            Heart_Bar heart_Bar = vida.GetComponent<Heart_Bar>();
            heart_Bar.hearts--;
        }
    }

    private void OnBecameInvisible()
   {
        
        transform.position = new Vector3(129.79f, 14f, 0);
   
    }
   
    void OnCollisionStay2D(Collision2D col)
    {

        if (col.gameObject.tag == "Wall" && !grounded)
        {
            wall = true;
        }


    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            wall = false;
        }
    }
    public void EnemyJump() { jump = true; }

    public void EnemyKnockBack(float enemyPosX)
    {
        jump = true;

        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rbd2.AddForce(Vector2.left * side * JumpPower, ForceMode2D.Impulse);

        movement = false;
        Invoke("EnableMovement", 0.7f);

        spr.color = Color.red;
    }

    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
    }
}
