using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController : MonoBehaviour
{
    public FallingBlock falling;
    private void Start()
    {
        //StartShake(5.0f, 5f, 10f);
    }

    public void StartShake(float duration, float magnitude, float speed)
    {
        StartCoroutine(Shake(duration, magnitude, speed));
    }

    private IEnumerator Shake(float duration, float magnitude, float speed)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float angle = magnitude * Mathf.Sin(Time.time * speed * Mathf.PI * 2);
            transform.localRotation = Quaternion.Euler(0, 0, angle);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        falling.Aftershake();
    }
}
