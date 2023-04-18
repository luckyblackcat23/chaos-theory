using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFieldReadOnly : MonoBehaviour
{
    public static readonly Dictionary<Vector2, float> Vectors = new();
    private readonly FastNoise fastNoise = new();
    public GameObject particle;

    public static float SchmooveMoves;
    public static int _height, _width;
    public int height, width;
    public float Schmoovin;
    public float scale;
    public float t;

    public bool update;

    // Start is called before the first frame update
    void Start()
    {
        SchmooveMoves = Schmoovin;
        _height = height;
        _width = width;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Vectors.Add(new Vector2(x, y), fastNoise.GetSimplex(x * scale, y * scale, t));
                Instantiate(particle, new Vector2(x - width / 2 + 0.5f, y - height / 2 + 0.5f), new Quaternion());

                float noise = fastNoise.GetSimplex(x * scale, y * scale, t);
                Vectors[new Vector2(x - width / 2, y - height / 2)] = noise;
            }
    }

    void Update()
    {
        if (update)
        {
            t += Schmoovin;

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    float noise = fastNoise.GetSimplex(x * scale, y * scale, t);
                    Vectors[new Vector2(x, y)] = noise;
                }
        }
    }
}