using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class video_play : MonoBehaviour
{
    VideoPlayer video;
    SpriteRenderer sr;
    void readyToPlay(VideoPlayer vp)
    {
        vp.Play();
        sr.color = new Color(1f, 1f, 1f, 1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        sr = GetComponent<SpriteRenderer>();
        video.prepareCompleted += readyToPlay;
        video.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
