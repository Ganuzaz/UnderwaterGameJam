using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour
{
    public AudioSource bubbleSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBubbleSound()
    {
        bubbleSound.Play();
    }

    public void SceneTransition(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
