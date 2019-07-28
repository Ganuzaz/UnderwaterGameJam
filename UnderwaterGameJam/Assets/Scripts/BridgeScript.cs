using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    
    private List<Enemy> onColliderList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.transform.GetComponent<Enemy>()!=null)
        {
            onColliderList.Add(collision.transform.GetComponent<Enemy>());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<Enemy>()!=null)
            if (onColliderList.Contains(collision.transform.GetComponent<Enemy>()))
            {
                onColliderList.Remove(collision.transform.GetComponent<Enemy>());
            }
    }

    public void DamageAll()
    {
        foreach(var element in onColliderList)
        {
            element.Damage();
        }
    }

}
