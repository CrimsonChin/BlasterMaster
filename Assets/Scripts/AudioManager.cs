using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip ExplosionAudioClip;

    public AudioClip LoseAudioClip;

    public AudioClip WinAudioClip;

    private AudioSource _audioSource;

	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();
	}
	
    public void PlayExplosion()
    {
        _audioSource.PlayOneShot(ExplosionAudioClip);
    }

    public void PlayLose()
    {
        _audioSource.PlayOneShot(LoseAudioClip);
    }

    public void PlayWin()
    {
        _audioSource.PlayOneShot(WinAudioClip);
    }
}

