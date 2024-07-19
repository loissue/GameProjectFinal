using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioSource vfxAudioSource;
    public AudioClip backgroudClip;
    public AudioClip itemClip;
    public AudioClip eyeClip;
    public AudioClip speedClip;
    public AudioClip burnClip;
    public AudioClip teleClip;
    public AudioClip healthClip;
    public AudioClip shieldClip;
    public AudioClip hurtClip;
    void Start()
    {
        PlayBackground(backgroudClip);
    }

    public void PlayBackground(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    // Update is called once per frame
    public void PlaySfx(AudioClip clip)
    {
        vfxAudioSource.clip = clip;
        vfxAudioSource.PlayOneShot(clip);
    }
    public void SetBackgroundSpeed(float speed)
    {
        audioSource.pitch = speed;
    }

}
