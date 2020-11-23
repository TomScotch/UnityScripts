using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TriggerVFXOnCollisionTrigger : MonoBehaviour {

    public bool continous = false;
    public AudioSource sound;
    public VisualEffect m_VisualEffect;

    void Start () {
        m_VisualEffect.enabled = false;
    }

    private void OnTriggerEnter (Collider other) {

        m_VisualEffect.enabled = true;

        if (sound != null && !sound.isPlaying) {
            sound.Play ();
        }

        m_VisualEffect.Play ();

        if (!continous) {
            gameObject.SetActive (false);
        }
    }
}