using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class ShortAudioPlayer : MonoBehaviour
{

   

    private MediaPlayer mediaPlayer;

    private MediaPlayer.FileLocation location = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
    private string folder = "wordbank/short/";
    private string fileName;


    private void Start()
    {
		mediaPlayer = GetComponent<MediaPlayer>();
		mediaPlayer.Events.AddListener(OnVideoEvent);
	}


    public void PlayShortAudio()
    {
        fileName = Random.Range(0, 5034).ToString() + ".mp3";
		mediaPlayer.OpenVideoFromFile(location, folder + fileName);
		
	}
	public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
	{
		switch (et)
		{
			case MediaPlayerEvent.EventType.ReadyToPlay:
				break;
			case MediaPlayerEvent.EventType.Started:
				break;
			case MediaPlayerEvent.EventType.FirstFrameReady:
				break;
			case MediaPlayerEvent.EventType.FinishedPlaying:
				PlayShortAudio();
				break;
		}

		Debug.Log("Event: " + et.ToString());
	}
	public void StopShortAudio()
	{
		mediaPlayer.Control.Pause();
		Debug.Log("StopShortAudio");
	}
}
