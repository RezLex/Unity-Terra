﻿using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    public float maxspeed = 1f;

    private Rigidbody2D rbd2;
    // Start is called before the first frame update
    void Start()
    {
        rbd2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rbd2.AddForce(Vector2.right * speed);
        float limetedSpeed = Mathf.Clamp(rbd2.velocity.x, -maxspeed, maxspeed);
        rbd2.velocity = new Vector2(limetedSpeed, rbd2.velocity.y);

        if (rbd2.velocity.x > -0.01f && rbd2.velocity.x < 0.01f)
        {
            speed = -speed;
            rbd2.velocity = new Vector2(speed, rbd2.velocity.y);
        }

        if (speed < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
        else if (speed > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            float yOffset = 0.04f;
            if (transform.position.y + yOffset < col.transform.position.y)
            {
                col.SendMessage("EnemyJump");
                Destroy(gameObject);
            }
            else
            {
                col.SendMessage("EnemyKnockBack", transform.position.x);

            }

        }
    }
}
