using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    
    
    public void Play()
    {
        
        SceneManager.LoadScene("michas");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void click()
    {
        source.PlayOneShot(clip);
    }
    
    public void Replay() {
//        
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        SceneManager.LoadScene("michas");
    }
}
