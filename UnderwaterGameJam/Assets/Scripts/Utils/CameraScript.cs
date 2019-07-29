using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform monster;
    public float limitxleft;
    public float limitxright;

    public IEnumerator Shake (float Duration, float Magnitude)
    {
        Vector3 OriginalPos = transform.localPosition;

        float Elapsed = 0.0f;
        while(Elapsed < Duration)
        {
            float x = Random.Range(-1f, 1f) * Magnitude;
            float Y = Random.Range(-1f, 1f) * Magnitude;

            transform.localPosition = new Vector3(x, Y, OriginalPos.z);

            Elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = OriginalPos;
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(monster.transform.position.x, limitxleft, limitxright), transform.position.y, transform.position.z);

    }


}
