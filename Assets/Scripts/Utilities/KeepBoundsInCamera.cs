using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeepBoundsInCamera : MonoBehaviour
{
    SpriteRenderer sprite;
    Camera cam;

    Vector3 originalLocalPosition;
    Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        originalLocalPosition = transform.localPosition;
        DOTween.Init();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localPosition = originalLocalPosition;
        
        Vector3 newPosition = transform.position;
        Bounds bounds = sprite.bounds;
        Vector3 centerPos = cam.WorldToViewportPoint(bounds.center);
        
        
        Vector3 pos = cam.WorldToViewportPoint(
            Vector2.right * (bounds.center.x + bounds.extents.x)
        );

        if (pos.x > .7f)
        {
            newPosition = cam.ViewportToWorldPoint(
                centerPos - Vector3.right * (pos.x - .99f)
            );
        }

        pos = cam.WorldToViewportPoint(
            Vector2.right * (bounds.center.x - bounds.extents.x)
        );

        if (pos.x < 0)
        {
            newPosition = cam.ViewportToWorldPoint(
                centerPos + Vector3.right * (0.01f-pos.x)
            );
        }

        newPosition.y = transform.parent.position.y + originalLocalPosition.y;
        // transform.DOBlendableMoveBy(newPosition - transform.position, .1f);
        // if (tween != null) tween.Kill();
        // tween = transform.DOMove(newPosition, .1f);
        transform.position = newPosition;
    }
}
