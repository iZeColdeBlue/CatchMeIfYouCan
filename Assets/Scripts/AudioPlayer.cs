using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    public AudioClip looseLife;
    public AudioClip failSound;
    public AudioClip pointSound;

    public AudioSource music;
    private AudioSource audioSource;
 

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playCollect()
    {
        audioSource.PlayOneShot(pointSound);
    }

    public void playDeath()
    {
        music.Stop();
    }

    public void playMiss()
    {
        audioSource.PlayOneShot(looseLife);
        // Debug.Log("Missed Fruit");
    }
}
