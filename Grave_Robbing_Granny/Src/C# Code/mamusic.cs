using UnityEngine;

public class mamusic : MonoBehaviour
{
    public AudioSource audioSource;
    public float myVolume;

    void Awake()
    {



    }

    void Start()
    {
        audioSource.loop = true;
        audioSource.volume = myVolume;
        audioSource.Play();
    }
}
