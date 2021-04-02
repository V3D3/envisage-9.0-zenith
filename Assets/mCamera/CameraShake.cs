using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    IEnumerator CamShake(float duration, float magnitude, float intensity)
    {
        Vector3 orig = transform.localPosition;

        float elapsed = 0.0f;
        Vector2 axis = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;

        while (elapsed < duration)
        {
            float x = axis.x * (magnitude * (duration - elapsed)) * Mathf.Sin(elapsed * intensity / duration);
            float y = axis.y * (magnitude * (duration - elapsed)) * Mathf.Sin(elapsed * intensity / duration);

            float noise = Random.Range(-x / 100, x / 100);

            transform.localPosition = new Vector3(x + noise, y + noise, orig.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = orig;
    }
}
