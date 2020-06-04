using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoplayerScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        VideoPlayer vp = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        vp.url = "http://localhost"; //Enter localhost video stream url here
        vp.isLooping = true;
        vp.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        vp.targetMaterialRenderer = GetComponent<Renderer>();
        vp.targetMaterialProperty = "_MainTex";

        vp.Play();
    }

    void Prepared(UnityEngine.Video.VideoPlayer vPlayer)
    {
        Debug.Log("End reached!");
        vPlayer.Play();
    }
}
