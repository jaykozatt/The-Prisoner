using System.Collections;
using UnityEngine;

public class CameraFX
{
    public static IEnumerator Shake(float duration, float magnitude)
    {
        Camera cam = Camera.main;
        Vector3 origin = cam.transform.localPosition;

        float x,y;
        for (float elapsed=0; elapsed < duration; elapsed += Time.deltaTime)
        {
            x = Random.Range(-1f,1f) * magnitude;
            y = Random.Range(-1f,1f) * magnitude;

            cam.transform.localPosition = origin + new Vector3(x,y,0);

            yield return null;
        }

        cam.transform.localPosition = origin;
    }    
}