using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part2 : MonoBehaviour
{

    public GameObject square;
    public GameObject circle;
    public GameObject capsule;
    public GameObject ground;

    float g = -9.81f;
    float sMass = 10f;
    float sxVel;
    float syVel;

    float cMass = 15f;
    float cxVel;
    float cyVel;

    float capMass = 30f;
    float capxVel;
    float capyVel;

    
    void Update()
    {
        float dt = Time.deltaTime;
        syVel += sMass * g * dt * dt;
        square.transform.position += new Vector3(sxVel, syVel, 0);

        cyVel += cMass * g * dt * dt;
        circle.transform.position += new Vector3(cxVel, cyVel, 0);

        capyVel += capMass * g * dt * dt;
        capsule.transform.position += new Vector3(capxVel, capyVel, 0);

        if (square.transform.position.y - ground.transform.position.y <= square.transform.localScale.y /2 + ground.transform.localScale.y /2)
        {
            square.transform.position = new Vector2(square.transform.position.x, ground.transform.position.y + ground.transform.localScale.y / 2 + square.transform.localScale.y / 2);
            syVel = 0;
        }
        if (circle.transform.position.y - ground.transform.position.y <= circle.transform.localScale.y / 2 + ground.transform.localScale.y / 2)
        {
            circle.transform.position = new Vector2(circle.transform.position.x, ground.transform.position.y + ground.transform.localScale.y / 2 + circle.transform.localScale.y / 2);
            cyVel = 0;
        }
        if (capsule.transform.position.y - ground.transform.position.y <= capsule.transform.localScale.y+ ground.transform.localScale.y / 2)
        {
            capsule.transform.position = new Vector2(capsule.transform.position.x, ground.transform.position.y + ground.transform.localScale.y / 2 + capsule.transform.localScale.y);
            capyVel = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            syVel = 10.0f * dt;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            syVel = -10.0f * dt;
        }
        if (Input.GetKey(KeyCode.A))
        {
            sxVel = -10.0f * dt;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            sxVel = 10.0f * dt;
        }
        else
        {
            sxVel = 0;
        }

        if (Input.GetKey(KeyCode.T))
        {
            capyVel = 10.0f * dt;
        }
        else if (Input.GetKey(KeyCode.G))
        {
            capyVel = -10.0f * dt;
        }
        if (Input.GetKey(KeyCode.F))
        {
            capxVel = -10.0f * dt;
        }
        else if (Input.GetKey(KeyCode.H))
        {
            capxVel = 10.0f * dt;
        }
        else
        {
            capxVel = 0;
        }

        if (Input.GetKey(KeyCode.I))
        {
            cyVel = 10.0f * dt;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            cyVel = -10.0f * dt;
        }

        if (Input.GetKey(KeyCode.J))
        {
            cxVel = -10.0f * dt;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            cxVel = 10.0f * dt;
        }
        else
        {
            cxVel = 0;
        }



    }
}
