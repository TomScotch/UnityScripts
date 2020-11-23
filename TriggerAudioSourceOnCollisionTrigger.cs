using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TriggerAudioSourceOnCollisionTrigger : MonoBehaviour {

    public bool continous = false;
    public AudioSource sound;

    private void OnTriggerEnter (Collider other) {

        if (sound != null && !sound.isPlaying) {
            sound.Play ();
        }

        if (!continous) {
            gameObject.SetActive (false);
        }
    }
}