using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class menuSettings : MonoBehaviour
{

    public AudioMixer audiomixer;

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log(isFullScreen);
    }
}
