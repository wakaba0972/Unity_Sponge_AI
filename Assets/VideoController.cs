using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private void Awake() => _videoPlayer = GetComponent<VideoPlayer>();

    private void Start()
    {
        _videoPlayer.Play();
    }
}