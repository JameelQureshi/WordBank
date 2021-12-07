using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsController : MonoBehaviour
{
    public Image echo;
    public Image reverb;
    public Image noise;

    public Sprite echo_white;
    public Sprite echo_red;

    public Sprite reverb_white;
    public Sprite reverb_red;

    public Sprite noise_white;
    public Sprite noise_red;

    public AudioEchoFilter audioEchoFilter;
    public AudioReverbFilter audioReverbFilter;
    public AudioDistortionFilter audioDistortionFilter;

    // Start is called before the first frame update
    void Start()
    {
        audioEchoFilter.enabled = false;
        audioReverbFilter.enabled = false;
        audioDistortionFilter.enabled = false;
    }

    public void ToogleNoise()
    {
        audioDistortionFilter.enabled = !audioDistortionFilter.enabled;
        if (audioDistortionFilter.enabled)
        {
            noise.sprite = noise_red;
        }
        else
        {
            noise.sprite = noise_white;
        }
    }
    public void ToogleEcho()
    {
        audioEchoFilter.enabled = !audioEchoFilter.enabled;
        if (audioEchoFilter.enabled)
        {
            echo.sprite = echo_red;
        }
        else
        {
            echo.sprite = echo_white;
        }
    }
    public void ToogleReverb()
    {
        audioReverbFilter.enabled = !audioReverbFilter.enabled;
        if (audioReverbFilter.enabled)
        {
            reverb.sprite = reverb_red;
        }
        else
        {
            reverb.sprite = reverb_white;
        }
    }
}
