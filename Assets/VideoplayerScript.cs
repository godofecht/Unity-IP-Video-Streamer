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

    // Add error handling to handle cases where the video stream URL is not valid
    private void Update()
    {
        if (!string.IsNullOrEmpty(videoSource) && !videoSource.StartsWith("http") && !videoSource.StartsWith("rtsp") && !System.IO.File.Exists(videoSource))
        {
            Debug.LogError("Invalid video source: " + videoSource);
        }
    }

    // Add support for various video formats
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

    // Add a UI for the script to make it more user-friendly
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

    // Add methods to control video playback
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

    // Add a UI for the script to make it more user-friendly
    public void SetRenderer(Renderer renderer)
    {
        videoRenderer = renderer;
        InitializeVideoPlayer();
    }

    // Add additional error handling to handle cases where the video player fails to prepare or play the video
    private void OnErrorReceived(VideoPlayer vPlayer, string message)
    {
        Debug.LogError("Video player error: " + message);
    }
}






/*


The VideoPlayerController package provides a simple and easy-to-use solution for adding video playback functionality to your Unity projects. With this package, you can easily create and control video players in your scenes, allowing you to stream videos from an IP, stream videos from a URL, or play videos from a file.

The VideoPlayerController script is fully customizable and comes with a range of options for controlling video playback. You can choose whether to loop the video playback, adjust the playback speed, set the video format, and enable or disable audio playback. You can also control video playback using a range of methods, including Play, Pause, Stop, SetTime, SetVolume, and more.

The VideoPlayerController package is designed to be easy to use and highly customizable, making it suitable for a wide range of Unity projects. Whether you are creating a video player for a game, a virtual reality experience, or a training application, this package provides everything you need to add video playback functionality to your project.

Use cases:

Games: Add video cutscenes or trailers to your game using the VideoPlayerController script.

Training applications: Create interactive training videos that allow users to pause, rewind, or fast-forward the video using the VideoPlayerController script.

Virtual reality experiences: Use the VideoPlayerController script to add immersive video content to your virtual reality experiences.

Marketing and advertising: Create video ads or product demos using the VideoPlayerController script.

This description highlights the key features and use cases of your VideoPlayerController package, and should help to attract potential users on the Unity Asset Store. Make sure to provide clear and concise information about your package, and highlight its unique selling points to make it stand out from the competition.


*/

