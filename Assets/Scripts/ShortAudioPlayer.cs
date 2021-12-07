using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAudioPlayer : MonoBehaviour
{


	private AudioSource audioSource;
	private bool canPlay;

    private void Start()
    {
		audioSource = GetComponent<AudioSource>();
	}


    public void Play()
    {
		canPlay = true;
		string fileName = Random.Range(0, 5034).ToString();
		//Load an AudioClip (Assets/Resources/short/*.mp3)
		var audioClip = Resources.Load<AudioClip>("short/"+fileName);
		audioSource.clip = audioClip;
		audioSource.Play();
	}
	void Update()
	{
        if (!canPlay)
        {
			return;
        }
		if (!audioSource.isPlaying)
		{
			Play();
		}
	}

	
	public void Stop()
	{
		canPlay = false;
		audioSource.Stop();
		Debug.Log("StopShortAudio");
	}
}
