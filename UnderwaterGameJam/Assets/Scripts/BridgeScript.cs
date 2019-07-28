using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    
    private List<Enemy> onColliderList;

    // Start is called before the first frame update
    void Start()
    {
        onColliderList = new List<Enemy>();
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
        }else if (collision.transform.GetComponent<MonsterMovement>() != null)
        {
            if (collision.transform.GetComponent<MonsterMovement>().headbutting)
            {
                DamageAll();
            }
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
            if (element != null)
            {
                int health = element.Damage();
                if(health<=0)
                    onColliderList.Remove(element);
            }
            else
                onColliderList.Remove(element);
        }
    }




}
