using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip sideToSideSound;  // Asigna el sonido desde el Editor de Unity
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySideToSideSound() //para el sonido de cambiar de rail
    {
        audioSource.clip = sideToSideSound;
        audioSource.Play();
    }
}
