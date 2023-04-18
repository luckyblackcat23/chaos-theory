using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoticAttractorsTest : MonoBehaviour
{
    public float x;
    public float y;
    public float x_;
    public float y_;
    public float Duration;
    public float StartingValue;
    public float TimeSteps;
    public float speed;
    public Transform PhysicalPoint;
    public float t;
    public List<Vector2> Points = new List<Vector2>();
    public bool PhysicalTime;

    // Update is called once per frame
    void Awake()
    {
        t = StartingValue;
        if (!PhysicalTime)
        {
            for (float t = StartingValue; t < Duration; t += TimeSteps)
            {
                //x = -1 * x * x + y * y + t * t - x * y + y * t - t;
                //y = y * y - x + t;
                //Points.Add(new Vector2(x, y));
            }
        }
    }

    private void Update()
    {
        if (PhysicalTime)
        {
            Time.timeScale = speed;
            if (t < Duration)
            {
                int i = 0;
                t += TimeSteps;
                x_ = (Points[i].x - x) * t - 0.1f;
                y_ = (Points[i].y - y) * t - 0.4f - x;
                Points.Add(new Vector2(x, y));
                PhysicalPoint.position = new Vector2(x_, y_);
                i++;
            }
            else
                t = StartingValue;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Vector2 point in Points)
        {
            Gizmos.DrawCube(new Vector3(point.x, point.y), new Vector3(0.01f, 0.01f));
        }
    }
}
