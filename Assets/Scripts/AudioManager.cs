using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip ExplosiAudioClip;

    public AudioClip LoseAudioClip;

    private AudioSource _audioSource;

	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
	{  	
	}

    public void PlayExplosion()
    {
        _audioSource.PlayOneShot(ExplosiAudioClip);
    }

    public void PlayLose()
    {
        _audioSource.PlayOneShot(LoseAudioClip);
    }
}

