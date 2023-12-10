using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part3 : MonoBehaviour
{

    public GameObject square;
    public GameObject circle;
    public GameObject capsule;
    public GameObject ground;

    float g = -9.81f;
    float sMass = 10f;
    float syVel;

    float cMass = 15f;
    float cyVel;

    float capMass = 30f;
    float capyVel;

    float impulseForce = 50f;


    void Update()
    {
        float dt = Time.deltaTime;
        syVel += sMass * g * dt * dt;
        square.transform.position += new Vector3(0, syVel, 0);

        cyVel += cMass * g * dt * dt;
        circle.transform.position += new Vector3(0, cyVel, 0);

        capyVel += capMass * g * dt * dt;
        capsule.transform.position += new Vector3(0, capyVel, 0);

        if (square.transform.position.y - ground.transform.position.y <= square.transform.localScale.y / 2 + ground.transform.localScale.y / 2)
        {
            square.transform.position = new Vector2(square.transform.position.x, ground.transform.position.y + ground.transform.localScale.y / 2 + square.transform.localScale.y / 2);
            syVel = 0;
        }
        if (circle.transform.position.y - ground.transform.position.y <= circle.transform.localScale.y / 2 + ground.transform.localScale.y / 2)
        {
            circle.transform.position = new Vector2(circle.transform.position.x, ground.transform.position.y + ground.transform.localScale.y / 2 + circle.transform.localScale.y / 2);
            cyVel = 0;
        }
        if (capsule.transform.position.y - ground.transform.position.y <= capsule.transform.localScale.y + ground.transform.localScale.y / 2)
        {
            capsule.transform.position = new Vector2(capsule.transform.position.x, ground.transform.position.y + ground.transform.localScale.y / 2 + capsule.transform.localScale.y);
            capyVel = 0;
        }


        Vector3 mouse = Input.mousePosition;  
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);
        mousePos = new Vector3(mousePos.x,mousePos.y, 0);
        bool hoverSquare = (mousePos - square.transform.position).magnitude <= square.transform.localScale.x /2 ;
        if (Input.GetKeyDown(KeyCode.Mouse0) && hoverSquare) 
        {
            syVel = impulseForce * dt;
        }
        bool hoverCapsule = (mousePos - capsule.transform.position).magnitude <= capsule.transform.localScale.x / 2 || (mousePos - capsule.transform.position).magnitude <= capsule.transform.localScale.y;
        if (Input.GetKeyDown(KeyCode.Mouse0) && hoverCapsule)
        {
            capyVel = impulseForce * dt;
        }
        bool hoverCircle = (mousePos - circle.transform.position).magnitude <= circle.transform.localScale.x / 2;
        if (Input.GetKeyDown(KeyCode.Mouse0) && hoverCircle)
        {
            cyVel = impulseForce * dt;
        }



    }
}

