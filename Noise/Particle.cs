using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("EveryXSeconds", 0.0f, NoiseFieldReadOnly.SchmooveMoves);
    }

    // Update is called once per frame
    void EveryXSeconds()
    {
        NoiseFieldReadOnly.Vectors.TryGetValue(new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), out float noise);
        transform.localEulerAngles = new Vector3(0, 0, noise * 180);
        transform.position += transform.up / 10;

        if (transform.position.x > NoiseFieldReadOnly._width / 2)
            transform.position = new Vector2(NoiseFieldReadOnly._width / 2 * -1, transform.position.y);

        if (transform.position.x < NoiseFieldReadOnly._width / 2 * -1)
            transform.position = new Vector2(NoiseFieldReadOnly._width / 2, transform.position.y);

        if (transform.position.y > NoiseFieldReadOnly._height / 2)
            transform.position = new Vector2(transform.position.x, NoiseFieldReadOnly._height / 2 * -1);

        if (transform.position.y < NoiseFieldReadOnly._height / 2 * -1)
            transform.position = new Vector2(transform.position.x, NoiseFieldReadOnly._height / 2);
    }
}