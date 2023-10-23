using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float deltatime;
    private Vector3 velDownRight,velDownLeft;
    private bool inAir;
    private double timer;

    public Vector3 position,velocity,acceleration;

    void Start()
    {
        deltatime = 0.02f;
        timer = 0;
        velDownRight = new Vector3(1, 5.5f, -1);
        velDownLeft = new Vector3(-1, 5.5f, -1);
        inAir = true;

        float gravity = 9.8f * 2;
        acceleration = new Vector3(0.0f, -gravity, 0.0f);
    }
    void Update()
    {
        if (!inAir)
        {
            if (timer > 0.5f)
            {
                if (Random.Range(1, 3) == 1) JumpDown(true);
                else JumpDown(false);
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (inAir)
        {
            position = transform.position;
            velocity += acceleration * deltatime;
            position += (velocity * deltatime);
            transform.position = position;
        }
    }
    void JumpDown(bool Right)
    {
        inAir = true;
        if (Right)
        {
            velocity = velDownRight;
            transform.rotation = Quaternion.Euler(0, 135, 0);
        }
        else // Left
        {
            transform.rotation = Quaternion.Euler(0, 225, 0);
            velocity = velDownLeft;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag( "Plane"))
        {
            inAir = false;
            Vector3 offSet = new Vector3(0, 0.2f, 0);
            transform.position = collision.transform.position + offSet;
        }
    }
}
