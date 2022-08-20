using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    public List<SpriteRenderer> parentSprites;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    void LateUpdate()
    {
        if (parentSprites.Count > 0)
        {
            float sum = 0;
            foreach (SpriteRenderer sprite in parentSprites) sum += sprite.bounds.size.y;

            transform.position =
                originalPos +
                Vector3.up * (sum)
            ;
        }
    }
}
