using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private float speed;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        speed = SceneDataPersistence.Instance.Speed;
    }

    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;

        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f * (speed + 2f);

        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > speed+2f)
        {
            velocity = velocity.normalized * (speed + 2f);
        }

        m_Rigidbody.velocity = velocity;
    }
}
