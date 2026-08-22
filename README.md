# Unity IP Video Streamer

A small Unity controller for streaming video onto a Renderer in a scene. Supports IP camera streams, arbitrary URLs, and local VideoClip resources.

## Features

- Stream from an IP camera via RTSP.
- Stream from a URL.
- Play a local `VideoClip` from Resources.
- Runtime control of playback, looping, speed, time, and volume.
- Material override render mode.

## Usage

1. Attach `VideoPlayerController.cs` to a GameObject.
2. Assign a Renderer to `videoRenderer`.
3. Choose the source type and fill in the source path.

```csharp
public enum VideoSourceType
{
    StreamIP,    // rtsp://{videoSource}/axis-media/media.amp
    StreamURL,   // any URL
    PlayFromFile // Resources/{videoSource}
}
```

## Methods

- `Play()` / `Pause()` / `Stop()`
- `SetLooping(bool)`
- `SetPlaybackSpeed(float)`
- `SetTime(float)`
- `SetVolume(float)`
- `SetRenderer(Renderer)`
- `SetVideoFormat(string)`: HTTP, MP4, MOV, WEBM
- `SetVideoSourceType(VideoSourceType)`

## Requirements

- Unity 2019.4+ with `UnityEngine.Video` support.

## License

MIT
