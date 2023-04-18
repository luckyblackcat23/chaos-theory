using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChaosGame : MonoBehaviour
{
    //rule of the chaos game:
    //the basic concept is that you first create 3(or more) points and place them in a 2d plane(or 3d, 4d although for simplicity lets say 2d)
    //then you place a random point anywhere on the 2d plane
    //we then roll a random number say between 1 and 3
    //if we roll say a 1 we take the random point and go exactly half way between its current position and point 1's position
    //we then repeat this process and draw a point with each iteration

    public float time;
    public int Iterations = 10000;
    public GameObject PointPrefab;
    public Vector2 CurrentPosition;
    public Vector2[] Points;
    private GameObject CurrentPoint;
    private bool _Start = false;

    private void Start()
    {
        CurrentPoint = Instantiate(PointPrefab);
        CurrentPoint.transform.position = CurrentPosition;
        CurrentPoint.GetComponent<Renderer>().material.color = Color.red;
        CurrentPoint.transform.localScale = new Vector2(0.2f, 0.2f);
        for (int i = 0; i < Points.Length; i++)
        {
            GameObject GO = Instantiate(PointPrefab);
            GO.transform.localScale = new Vector2(0.5f, 0.5f);
            GO.transform.position = Points[i];
        }
        InvokeRepeating("update", 0.0f, time);
        _Start = false;
    }

    public void start()
    {
        _Start = true;
    }

    private int i;
    private int Last;
    private float _time;
    public bool SameVertexRule;
    public TextMeshProUGUI txt;
    void update()
    {
        if (_Start)
        {
            if (i < Iterations)
            {
                int Rand = Random.Range(0, Points.Length);
                if (SameVertexRule)
                    if (Rand == Last)
                        return;
                CurrentPosition = (CurrentPosition + Points[Rand]) / 2;
                Instantiate(PointPrefab, CurrentPosition, new Quaternion(0, 0, 0, 0));
                CurrentPoint.transform.position = CurrentPosition;
                i++;
                Debug.Log(Rand);
                txt.text = "iterations: " + i;
                Debug.Log(Time.realtimeSinceStartup);
                Last = Rand;
            }
            _time += Time.deltaTime;
        }
    }
}
