using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierTest : MonoBehaviour
{
    #region variables
    //i know i could've used a vector2 but i've already written this out and for the sake of my sleep schedule i refuse to rewrite it
    //point a
    public Transform a;
    public float ax;
    public float ay;
    //the point between a and b
    public float abx;
    public float aby;
    //point b
    public Transform b;
    public float bx;
    public float by;// jordan :D
    //the point between b and c
    public float bcx;
    public float bcy;
    //point c
    public Transform c;
    public float cx;
    public float cy;//a later
    //final result
    public Vector2 curve;
    [Range(0,1)]
    public float time;
    #endregion

    // Update is called once per frame
    void Update()
    {
        ax = a.position.x;
        ay = a.position.y;
        bx = b.position.x;
        by = b.position.y;
        cx = c.position.x;
        cy = c.position.y;

        //example:
        //ab = Mathf.Lerp(a,b,time);
        //bc = Mathf.Lerp(b,c,time);
        //curve = Mathf.Lerp(ab,bc,time);

        abx = Mathf.Lerp(ax, bx, time);
        aby = Mathf.Lerp(ay, by, time);
        bcx = Mathf.Lerp(bx, cx, time);
        bcy = Mathf.Lerp(by, cy, time);
        curve.x = Mathf.Lerp(abx, bcx, time);
        curve.y = Mathf.Lerp(aby, bcy, time);

        this.transform.position = curve;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(a.position, b.position);
        //Gizmos.DrawLine(b.position, c.position);
        //Gizmos.DrawLine(new Vector2(abx, aby), new Vector2(bcx, bcy));

        for (float i = 0; i < 1.1f; i += 0.1f)
        {
            float tempabx;
            float tempaby;
            float tempbcx;
            float tempbcy;
            tempabx = Mathf.Lerp(ax, bx, i);
            tempaby = Mathf.Lerp(ay, by, i);
            tempbcx = Mathf.Lerp(bx, cx, i);
            tempbcy = Mathf.Lerp(by, cy, i);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector2(tempabx, tempaby), new Vector2(tempbcx, tempbcy));
        }
    }
}