﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forceX;//水平推力
    public static bool isDead;
    Rigidbody2D playerRigidBody2D;
    readonly float toLeft = -1;//向左
    readonly float toRight = 1;//向右
    readonly float stop = 0 ;//原地
    float directionX;
    void Start()
    {
        isDead = false;
        playerRigidBody2D = GetComponent<Rigidbody2D>();
    }

        void Update()
    {
     if (Input.GetKey(KeyCode.LeftArrow))
        {
            directionX = toLeft;
        }   
     else if (Input.GetKey(KeyCode.RightArrow))
        {
            directionX = toRight;
        }
     else
        {
            directionX = stop;
        }

        Vector2 newDirection = new Vector2(directionX, 0);
        playerRigidBody2D.AddForce(newDirection* forceX);
         
    }
}
