using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BuildingScript[] buildings;
    public GameObject canvas;
    public Text textStatus;
    public Text textDesc;
    int LastDestroyed =0;

    void Awake(){
        instance = this;
        
    }
    void Update(){
        if(canvas.active)
        return;

        bool allDestroyed = true;
        for(int i=LastDestroyed;i<buildings.Length;i++){
            if(buildings[i].health<=0){
                LastDestroyed =i;
            }else{
                allDestroyed=false;
                break;
            }

        }

        if(allDestroyed){
            textStatus.text = "WIN";
            textDesc.text = "All Buildings Destroyed";
            canvas.SetActive(true);
        }



    }

    public void Lose(){
        textStatus.text = "LOSE";
        textDesc.text ="Monster Died";
        canvas.SetActive(true);
    }

}
