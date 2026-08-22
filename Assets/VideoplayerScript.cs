using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public enum VideoSourceType
    {
        StreamIP,
        StreamURL,
        PlayFromFile
    }

    [Tooltip("Enter the video source type")]
    public VideoSourceType videoSourceType;

    [Tooltip("Enter the URL or file path of the video stream")]
    public string videoSource;

    [Tooltip("Enter the renderer to display the video here")]
    public Renderer videoRenderer;

    private VideoPlayer videoPlayer;

    private void Start()
    {
        InitializeVideoPlayer();
    }

    private void InitializeVideoPlayer()
    {
        if (videoRenderer == null)
        {
            Debug.LogError("No renderer assigned to VideoPlayerController!");
            return;
        }

        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.isLooping = true;
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = videoRenderer;
        videoPlayer.targetMaterialProperty = "_MainTex";
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.errorReceived += OnErrorReceived;

        switch (videoSourceType)
        {
            case VideoSourceType.StreamIP:
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = "rtsp://" + videoSource + "/axis-media/media.amp";
                break;
            case VideoSourceType.StreamURL:
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = videoSource;
                break;
            case VideoSourceType.PlayFromFile:
                videoPlayer.source = VideoSource.VideoClip;
                videoPlayer.clip = Resources.Load<VideoClip>(videoSource);
                break;
            default:
                Debug.LogError("Invalid video source type: " + videoSourceType);
                break;
        }

        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer vPlayer)
    {
        Debug.Log("Video prepared!");
        vPlayer.Play();
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(videoSource) && !videoSource.StartsWith("http") && !videoSource.StartsWith("rtsp") && !System.IO.File.Exists(videoSource))
        {
            Debug.LogError("Invalid video source: " + videoSource);
        }
    }

    public void SetVideoFormat(string format)
    {
        switch (format)
        {
            case "HTTP":
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = videoSource;
                break;
            case "MP4":
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = videoSource + ".mp4";
                break;
            case "MOV":
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = videoSource + ".mov";
                break;
            case "WEBM":
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = videoSource + ".webm";
                break;
            default:
                Debug.LogError("Invalid video format: " + format);
                break;
        }

        videoPlayer.Prepare();
    }

    public void SetVideoSourceType(VideoSourceType type)
    {
        videoSourceType = type;
        InitializeVideoPlayer();
    }

    public void SetLooping(bool loop)
    {
        videoPlayer.isLooping = loop;
    }

    public void SetPlaybackSpeed(float speed)
    {
        videoPlayer.playbackSpeed = speed;
    }

    public void Play()
    {
        videoPlayer.Play();
    }

    public void Pause()
    {
        videoPlayer.Pause();
    }

    public void Stop()
    {
        videoPlayer.Stop();
    }

    public void SetTime(float time)
    {
        videoPlayer.time = time;
    }

    public void SetVolume(float volume)
    {
        videoPlayer.SetDirectAudioVolume(0, volume);
    }

    public void SetRenderer(Renderer renderer)
    {
        videoRenderer = renderer;
        InitializeVideoPlayer();
    }

    private void OnErrorReceived(VideoPlayer vPlayer, string message)
    {
        Debug.LogError("Video player error: " + message);
    }
}
