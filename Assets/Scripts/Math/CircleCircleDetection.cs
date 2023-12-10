using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CircleCircleDetection : MonoBehaviour
{
    public GameObject square;
    public GameObject circle;
    public GameObject capsule;
    public GameObject capsuleTop;
    public GameObject capsuleBot;


    // Homework 10: complete the CheckCollisionCirleCapsule function.
    bool CheckCollisionCircleCapsule(
        Vector3 circlePosition, float circleRadius,
        Vector3 capsulePosition, float capsuleRadius, Vector3 direction, float halfHeight, out Vector3 mtv)
    {

        Vector3 topCap;
        Vector3 botCap;

        CapsulePoints(capsulePosition, direction, halfHeight, out topCap, out botCap);

        Vector3 projection = ProjectPointLine(circlePosition, topCap, botCap);
        return CheckCollisionCircles(circlePosition, circleRadius, projection, capsuleRadius, out mtv);

    }
    bool CheckCollisionSquareCapsule(Vector3 sPos, float sRad, Vector3 capPos, float capRad,
            Vector3 capDir, float HalfHeight, out Vector3 mtv)
    {
        Vector3 topCap;
        Vector3 botCap;
        CapsulePoints(capPos, capDir, HalfHeight, out topCap, out botCap);
        return CheckCollisionSquareCircles(sPos, sRad, topCap, botCap, capRad, out mtv);
    }

    void CapsulePoints(Vector3 position, Vector3 direction, float halfHeight, out Vector3 top, out Vector3 bot)
    {
        top = position + direction * halfHeight;
        bot = position - direction * halfHeight;
    }

    // Projects point P onto line AB
    Vector3 ProjectPointLine(Vector3 P, Vector3 A, Vector3 B)
    {
        Vector3 AB = B - A;
        Vector3 AP = P - A;
        float t = Vector3.Dot(AB, AP) / Vector3.Dot(AB, AB);
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        return A + AB * t;
    }


    bool CheckCollisionSquareCircles(Vector3 position1, float radius1, Vector3 topCap, Vector3 botCap, float radius2, out Vector3 mtv)
    {

        Vector3 tleft = position1;
        tleft.x -= radius1;
        tleft.y += radius1;
        Vector3 tright = position1;
        tright.x += radius1;
        tright.y += radius1;
        Vector3 bleft = position1;
        bleft.x -= radius1;
        bleft.y -= radius1;
        Vector3 bright = position1;
        bright.x += radius1;
        bright.y -= radius1;

        // Distance between position 1 and position 2
        float distancetleft = (topCap - tleft).magnitude;
        float distancetright = (topCap - tright).magnitude;
        float distancebleft = (topCap - bleft).magnitude;
        float distancebright = (topCap - bright).magnitude;

        float distancetleftbot = (botCap - tleft).magnitude;
        float distancetrightbot = (botCap - tright).magnitude;
        float distancebleftbot = (botCap - bleft).magnitude;
        float distancebrightbot = (botCap - bright).magnitude;

        // Collision if distance between circles is less than the sum of their radii!
        bool collision;
        if (distancetleft <= radius2)
        {
            collision = true;
        }
        else if (distancetright <= radius2)
        {
            collision = true;
        }
        else if(distancebleft <= radius2)
        {
            collision = true;
        }
        else if(distancebright <= radius2)
        {
            collision = true;
        }
        else if (distancetleftbot <= radius2)
        {
            collision = true;
        }
        else if (distancetrightbot <= radius2)
        {
            collision = true;
        }
        else if (distancebleftbot <= radius2)
        {
            collision = true;
        }
        else if (distancebrightbot <= radius2)
        {
            collision = true;
        }
        else if((botCap - position1).magnitude <= radius1 + radius2)
        {
            collision = true;
        }
        else if ((topCap - position1).magnitude <= radius1 + radius2)
        {
            collision = true;
        }
        else
        {
            collision = false;
        }
        mtv = Vector3.zero;

        return collision;
    }

    bool CheckCollisionCircles(Vector3 position1, float radius1, Vector3 position2, float radius2, out Vector3 mtv)
    {
        // Distance between position 1 and position 2
        float distance = (position1 - position2).magnitude;

        // Direction from to position 2 to position 1
        Vector3 direction = (position1 - position2).normalized;

        // Sum of radii
        float radiiSum = radius1 + radius2;

        // Collision if distance between circles is less than the sum of their radii!
        bool collision = distance < radiiSum;
        if (collision)
        {
            // Calculate mtv only if there's a collision
            float depth = radiiSum - distance;
            mtv = direction * depth;
        }
        else
        {
            mtv = Vector3.zero;
        }
        return collision;
    }

    void Update()
    {

        Vector2 position1 = square.transform.position;
        float radius1 = square.transform.localScale.x * 0.5f;
        Vector2 position2 = circle.transform.position;
        float radius2 = circle.transform.localScale.x * 0.5f;

        // MTV resolves 1 from 2
        Vector3 mtv = Vector3.zero;
        bool collision = CheckCollisionCircles(position1, radius1, position2, radius2, out mtv);
        //square.transform.position += new Vector3(mtv.x, mtv.y, 0.0f);
        Color color = collision ? Color.green : Color.red;

        //Vector3 A = mouse;
        //Vector3 B = position1 - Vector2.zero;
        //Vector3 proj = Vector3.Project(A, B);

        square.GetComponent<SpriteRenderer>().color = color;
        circle.GetComponent<SpriteRenderer>().color = color;

        Vector3 capsulePosition = capsule.transform.position;
        Vector3 capsuleDirection = capsule.transform.up;
        float capsuleHalfHeight = capsule.transform.localScale.y * 0.5f;
        float capsuleRadius = capsule.transform.localScale.x * 0.5f;
        Vector3 top, bot;
        CapsulePoints(capsulePosition, capsuleDirection, capsuleHalfHeight, out top, out bot);
        capsuleTop.transform.position = top;
        capsuleBot.transform.position = bot;

        bool capsuleCircleCollision = CheckCollisionCircleCapsule(position2, radius2,
            capsulePosition, capsuleRadius, capsuleDirection.normalized, capsuleHalfHeight, out mtv);
        //square.transform.position += mtv;
        if (capsuleCircleCollision)
        {
            capsule.GetComponent<SpriteRenderer>().color = Color.green;
            circle.GetComponent<SpriteRenderer>().color = Color.green;

        }
        else
        {
            capsule.GetComponent<SpriteRenderer>().color = Color.red;
        }

        bool capsuleSquareCollision = CheckCollisionSquareCapsule(position1, radius1, capsulePosition, capsuleRadius,
            capsuleDirection.normalized, capsuleHalfHeight, out mtv);
        if (capsuleSquareCollision)
        {
            capsule.GetComponent<SpriteRenderer>().color = Color.green;
            square.GetComponent<SpriteRenderer>().color = Color.green;

        }
        else
        {
            capsule.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
