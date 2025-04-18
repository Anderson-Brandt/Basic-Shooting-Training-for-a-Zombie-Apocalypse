using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip humanShot;
    public AudioClip zombieShot;
    public AudioClip slingShot;

    private AudioSource audioSource;


    public static AudioController current;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
