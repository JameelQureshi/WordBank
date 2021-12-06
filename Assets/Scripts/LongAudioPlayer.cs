using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class LongAudioPlayer : MonoBehaviour
{
	private MediaPlayer mediaPlayer;

	private MediaPlayer.FileLocation location = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
	private string LongVideofolder = "wordbank/long/";
	private string fileName;


	private void Start()
	{
		mediaPlayer = GetComponent<MediaPlayer>();
		mediaPlayer.Events.AddListener(OnVideoEvent);
		//PlayLongAudio();
	}
	public void PlayLongAudio()
	{
		fileName = Random.Range(1, 8).ToString() + ".mp3";
		//Debug.Log(fileName);
		mediaPlayer.OpenVideoFromFile(location, LongVideofolder+fileName, false);
	}
	public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
	{
		switch (et)
		{
			case MediaPlayerEvent.EventType.ReadyToPlay:

				int sec = (int)(mediaPlayer.Info.GetDurationMs() / 1000);
				//Debug.Log("Length: " + sec);
				int seek = Random.Range(0, sec - 3);
				//Debug.Log("Seek :" + seek);
				mediaPlayer.Control.SeekFast(seek * 1000);
				mediaPlayer.Play();
				StartCoroutine(WaitForSeconds());
				break;
			case MediaPlayerEvent.EventType.Started:
				break;
			case MediaPlayerEvent.EventType.FirstFrameReady:
				break;
			case MediaPlayerEvent.EventType.FinishedPlaying:
				break;
		}

		Debug.Log("Event: " + et.ToString());
	}
	private IEnumerator WaitForSeconds()
	{
		yield return new WaitForSeconds(3);
		PlayLongAudio();
	}
	public void StopLongAudio()
	{
		
		mediaPlayer.Control.Pause();
		StopAllCoroutines();
		Debug.Log("StopLongAudio");
	}
}
