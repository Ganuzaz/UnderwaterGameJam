using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform monster;
    public float limitxleft;
    public float limitxright;

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(monster.transform.position.x, limitxleft, limitxright), transform.position.y, transform.position.z);

    }
}
