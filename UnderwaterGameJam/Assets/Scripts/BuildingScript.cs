using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour, Enemy
{
    public int health =2;
    public GameObject smokeParticle;
    public virtual int Damage(){
        if(health<=0)
        return health;
        health-=1;
       
        var temp = Instantiate(smokeParticle);
        temp.transform.position = transform.position;
        GetComponent<Animator>().SetTrigger("GetsHit");
        return health;
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.transform.GetComponent<MonsterMovement>()!=null){
            if(col.transform.GetComponent<MonsterMovement>().headbutting){
                Damage();
            }
        }
    }

}
