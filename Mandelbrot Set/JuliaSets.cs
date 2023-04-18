using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuliaSets : MonoBehaviour
{
    public int NumberOfPoints;
    public GameObject PointPrefab;
    public Vector2 OriginalPoint;
    public Vector2 ConstantPoint;
    public GameObject OriginalPointHandle;
    public GameObject ConstantPointHandle;
    public List<Vector2> _Points = new();
    private List<System.Numerics.Complex> Points = new();
    private List<GameObject> PointPrefabs = new();

    public LineRenderer lr;

    private void Start()
    {
        //Find line renderer component
        lr = gameObject.GetComponent<LineRenderer>();

        //Set the handles positions to the their respective points positions
        OriginalPointHandle.transform.position = OriginalPoint;
        ConstantPointHandle.transform.position = ConstantPoint;

        //Make the handle the first point so there isnt an overlapping point
        PointPrefabs.Add(OriginalPointHandle);
        Points.Add(new System.Numerics.Complex (OriginalPoint.x, OriginalPoint.y));
    }

    private void Update()
    {
        //f Å´c(z) = z^2 + c
        if(_Points.Count == Points.Count)
            for (int i = 0; i < Points.Count; i++)
                _Points[i] = UtilsKit.ToVector(Points[i]);

        OriginalPoint = OriginalPointHandle.transform.position;
        ConstantPoint = ConstantPointHandle.transform.position;
        Points[0] = UtilsKit.ToComplex(OriginalPoint);

        while (Points.Count <= NumberOfPoints)
        {
            //Create a new point
            GameObject point = Instantiate(PointPrefab);

            //Add the point to our list of points and repeat
            Points.Add(UtilsKit.ToComplex(point.transform.position));
            PointPrefabs.Add(point);
        }

        for (int i = 1; i < Points.Count; i++)
        {
            //Calculate new position
            System.Numerics.Complex newPosition = System.Numerics.Complex.Pow(Points[i - 1], 2) + UtilsKit.ToComplex(ConstantPoint);

            //Set points position
            Points[i] = newPosition;
            PointPrefabs[i].transform.position = UtilsKit.ToVector(Points[i]);
        }

        MoveHandle();
        BoundaryOfStability();

        //Render a line between each point
        lr.positionCount = Points.Count;
        for (int i = 0; i < Points.Count; i++)
            lr.SetPosition(i, UtilsKit.ToVector(Points[i]));
    }

    //BoundaryOfStabilityPoints
    public List<System.Numerics.Complex> BoSP = new();
    public List<Vector2> _BoSP = new();
    //BoundaryOfStabilityPointPrefabs
    private List<GameObject> BoSPP = new();
    public int NumberOfBoSPPoints;
    public GameObject SeedHandle;

    void BoundaryOfStability()
    {
        //We use the inverse equation to find the border of the fraction
        //g(z) = (z - c)^0.5

        while (_BoSP.Count < BoSP.Count)
            _BoSP.Add(new Vector2(0, 0));

        while (BoSP.Count <= NumberOfBoSPPoints)
        {
            //Create a new point
            GameObject point = Instantiate(PointPrefab);

            //Add the point to our list of points and repeat
            BoSP.Add(UtilsKit.ToComplex(point.transform.position));
            BoSPP.Add(point);
        }

        if (_BoSP.Count == BoSP.Count)
            for (int i = 0; i < BoSP.Count; i++)
                _BoSP[i] = UtilsKit.ToVector(BoSP[i]);

        BoSP[0] = UtilsKit.ToComplex(SeedHandle.transform.position);

        for (int i = 1; i < BoSP.Count; i++)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                //Calculate new position
                System.Numerics.Complex newPosition = System.Numerics.Complex.Sqrt(BoSP[i - 1] - UtilsKit.ToComplex(ConstantPoint));

                //Set points position
                BoSP[i] = newPosition;
                BoSPP[i].transform.position = UtilsKit.ToVector(BoSP[i]);
            }
            else
            {
                //Calculate new position
                System.Numerics.Complex newPosition = System.Numerics.Complex.Sqrt(BoSP[i - 1] - UtilsKit.ToComplex(ConstantPoint)) * -1;

                //Set points position
                BoSP[i] = newPosition;
                BoSPP[i].transform.position = UtilsKit.ToVector(BoSP[i]);
            }
        }
    }

    GameObject selectedObject;
    Vector3 offset;
    void MoveHandle()
    {
        //Find the mouse's position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            //Check if the target object exists
            if (targetObject)
            {
                //Convert collider to gameObject
                selectedObject = targetObject.transform.gameObject;
                //Find and set the offset
                offset = selectedObject.transform.position - mousePosition;
            }
        }

        //Check if the selected object exists
        if (selectedObject)
        {
            //Set objects position with offset
            selectedObject.transform.position = mousePosition + offset;
        }
        
        //If the selected object is not null and the mouse button is no longer being held
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }
}