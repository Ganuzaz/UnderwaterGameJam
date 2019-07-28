using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    
    private List<GameObject> ThingsAboveBridges;
    private List<ContactPoint2D> ContactPoint2Ds;
    // Start is called before the first frame update
    void Start()
    {
        ThingsAboveBridges = new List<GameObject>();
        ContactPoint2Ds = new List<ContactPoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.GetContacts(ContactPoint2Ds);
        Debug.Log(ContactPoint2Ds[0]);
    }    
}
