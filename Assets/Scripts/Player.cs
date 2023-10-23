using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    private Vector3 position,velocity,acceleration;
    public float timeSpeed;

    public Material yellowMaterial;
    private int _TotalPlane;
    private float deltatime, angle, speed;
    private Vector3 velUpRight,velUpLeft,velDownRight,velDownLeft;
    private bool inAir;

    List<GameObject> contactedPlane=new List<GameObject>();
    void Start()
    {
        deltatime = 0.02f;

        angle = Mathf.Atan((4.9f*timeSpeed*timeSpeed) - 1.0f);

        speed = 1 / (Mathf.Cos(angle) * timeSpeed);
        velDownRight = new Vector3(1, 6.0f, -1);
        velDownLeft = new Vector3(-1, 6.0f, -1);

        velUpRight = new Vector3(1, 9.0f, 1);
        velUpLeft = new Vector3(-1, 9.0f, 1);

        float gravity = 9.8f * 2.5f;
        acceleration = new Vector3(0.0f, -gravity, 0.0f);
        PanelFind();
    }
    void Update()
    {
        if (!inAir)
        {
            if (Input.GetKey("s"))JumpDown(true);
            else if (Input.GetKey("a"))JumpDown(false);
            else if (Input.GetKey("q"))JumpUp(false);
            else if (Input.GetKey("w"))JumpUp(true);
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
        if(Right)
        {
            velocity = velDownRight;
            transform.rotation = Quaternion.Euler(0, 135, 0);
        }else // Left
        {
            transform.rotation = Quaternion.Euler(0, 225, 0);
            velocity = velDownLeft;
        }
    }
    void JumpUp(bool Right)
    {
        inAir = true;
        if (Right)
        {
            velocity = velUpRight;
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else // Left
        {
            velocity = velUpLeft;
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    { 
        if (collision.collider.CompareTag("Plane"))
        {
            inAir = false;
            Vector3 offSet = new Vector3(0, 0, 0);
            transform.position = collision.transform.position + offSet;
            collision.collider.GetComponent<Renderer>().material = yellowMaterial;
            if (!contactedPlane.Contains(collision.gameObject))
            {
                contactedPlane.Add(collision.gameObject);
                Events.Score?.Invoke(100);
                if (contactedPlane.Count >= _TotalPlane) Events.GameFinish?.Invoke(true);
            }
        }
        else if (collision.collider.CompareTag("Ball")) Events.Healty?.Invoke();
    }
    public void PanelFind()
    {
        GameObject[] TotalObjeList = GameObject.FindGameObjectsWithTag("Plane");
        _TotalPlane = TotalObjeList.Length;
    }
}
