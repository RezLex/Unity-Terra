﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esferas : MonoBehaviour
{

    private Rigidbody2D rbd2;
    public float JumpPower = 1.0f;
    public float scale;




    // Start is called before the first frame update
    void Start()
    {
        scale = PlayerController.scal * 0.5714f;
        transform.localScale = new Vector3(scale, scale);

        
        this.GetComponent<Rigidbody2D>().velocity = new Vector3(8f*(-PlayerController.side), 2f);


        rbd2 = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x - (1.5f * PlayerController.side), transform.position.y + 0.6f);

        rbd2.AddForce(Vector2.left * PlayerController.side * JumpPower, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x - (0.01f * scale), transform.localScale.x - (0.01f * scale));
        Destroy(gameObject,1.5f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
    }
}
