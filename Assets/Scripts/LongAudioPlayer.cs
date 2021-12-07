using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongAudioPlayer : MonoBehaviour
{
	private AudioSource audioSource;
	
	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}


	
	public void Play()
	{
		string fileName = Random.Range(1,9).ToString();
		//Load an AudioClip (Assets/Resources/long/*.mp3)
		var audioClip = Resources.Load<AudioClip>("long/" + fileName);
		audioSource.clip = audioClip;
		int sec = (int)audioSource.clip.length;
		int seek = Random.Range(0, sec - 3);
		audioSource.time = seek;
		audioSource.Play();
		StartCoroutine(PlayNextClip());
	}
	
	private IEnumerator PlayNextClip()
	{
		yield return new WaitForSeconds(3);
		Play();
	}
	public void Stop()
	{
		audioSource.Stop();
		StopAllCoroutines();
		Debug.Log("StopLongAudio");
	}
}
