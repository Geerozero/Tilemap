using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource slappy;
    public AudioSource powerUp;
    public void Slap()
    {
        slappy.Play();
    }

    public void Boost()
    {
        powerUp.Play();
    }
}
