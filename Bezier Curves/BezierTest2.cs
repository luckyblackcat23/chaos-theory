using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BezierTest2 : MonoBehaviour
{
    #region variables
    //i know i could've used a vector2 but i've already written this out and for the sake of my sleep schedule i refuse to rewrite it
    //i also realise half of these variables could be private, but to that i once again say... "i sleep"
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
    //the point between c and d
    public float cdx;
    public float cdy;
    //point d
    public Transform d;
    public float dx;
    public float dy;
    //point ab + bc lerped together
    public float abcx;
    public float abcy;
    //point bc + cd lerped together
    public float bcdx;
    public float bcdy;
    public bool UseGradient;
    //final result
    public Vector2 curve;
    [Range(0, 1)]
    public float time;
    private Gradient gradient;
    #endregion

    private void Start()
    {
        gradient = new Gradient();
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.blue;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Update is called once per frame
    void Update()
    {
        //these are just here because i cant be bothered to sort out my variables
        ax = a.position.x;
        ay = a.position.y;
        bx = b.position.x;
        by = b.position.y;
        cx = c.position.x;
        cy = c.position.y;
        dx = d.position.x;
        dy = d.position.y;

        //example:
        //ab = Mathf.Lerp(a,b,time);
        //bc = Mathf.Lerp(b,c,time);
        //curve = Mathf.Lerp(ab,bc,time);

        abx = Mathf.Lerp(ax, bx, time);
        aby = Mathf.Lerp(ay, by, time);
        bcx = Mathf.Lerp(bx, cx, time);
        bcy = Mathf.Lerp(by, cy, time);
        cdx = Mathf.Lerp(cx, dx, time);
        cdy = Mathf.Lerp(cy, dy, time);
        abcx = Mathf.Lerp(abx, bcx, time);
        abcy = Mathf.Lerp(aby, bcy, time);
        bcdx = Mathf.Lerp(bcx, cdx, time);
        bcdy = Mathf.Lerp(bcy, cdy, time);
        curve.x = Mathf.Lerp(abcx, bcdx, time);
        curve.y = Mathf.Lerp(abcy, bcdy, time);

        this.transform.position = curve;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(a.position, b.position);
        Gizmos.DrawLine(b.position, c.position);
        Gizmos.DrawLine(c.position, d.position);
        Gizmos.DrawLine(new Vector2(abx, aby), new Vector2(bcx, bcy));
        Gizmos.DrawLine(new Vector2(bcx, bcy), new Vector2(cdx, cdy));
        Gizmos.DrawLine(new Vector2(abcx, abcy), new Vector2(bcdx, bcdy));

        if (UseGradient)
        {
            for (float i = 0; i < 1.1f; i += 0.1f)
            {
                float tempabx = Mathf.Lerp(ax, bx, i);
                float tempaby = Mathf.Lerp(ay, by, i);
                float tempbcx = Mathf.Lerp(bx, cx, i);
                float tempbcy = Mathf.Lerp(by, cy, i);
                float tempcdx = Mathf.Lerp(cx, dx, i);
                float tempcdy = Mathf.Lerp(cy, dy, i);
                float tempabcx = Mathf.Lerp(tempabx, tempbcx, i);
                float tempabcy = Mathf.Lerp(tempaby, tempbcy, i);
                float tempbcdx = Mathf.Lerp(tempbcx, tempcdx, i);
                float tempbcdy = Mathf.Lerp(tempbcy, tempcdy, i);
                if (gradient != null)
                    Gizmos.color = gradient.Evaluate(i);
                Gizmos.DrawLine(new Vector2(tempabx, tempaby), new Vector2(tempbcx, tempbcy));
                Gizmos.DrawLine(new Vector2(tempbcx, tempbcy), new Vector2(tempcdx, tempcdy));
                Gizmos.DrawLine(new Vector2(tempabcx, tempabcy), new Vector2(tempbcdx, tempbcdy));
            }
        }
        //else
            //Handles.DrawBezier(a.position, d.position, b.position, c.position, Color.red, Texture2D.whiteTexture, 1f);
    }
}