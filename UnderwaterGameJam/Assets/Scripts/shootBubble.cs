using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBubble : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bubblePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            shoot();
        }
    }

    void shoot()
    {
        Instantiate(bubblePrefab, firePoint.position, firePoint.rotation);

    }
}
