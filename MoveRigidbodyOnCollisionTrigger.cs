using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbodyOnCollisionTrigger : MonoBehaviour {

    public enum Directions {
        Up,
        Right,
        Forward
    }

    public Rigidbody rb;
    public float force = 15f;
    public Directions dir;
    public bool continous = false;
    public AudioSource sound;

    private void OnTriggerEnter (Collider other) {

        if (sound != null && !sound.isPlaying) {
            sound.Play ();
        }

        switch (dir) {
            case Directions.Up:
                rb.AddForce (transform.up * force);
                break;
            case Directions.Right:
                rb.AddForce (transform.right * force);
                break;
            case Directions.Forward:
                rb.AddForce (transform.forward * force);
                break;
        }

        if (!continous) {
            gameObject.SetActive (false);
        }
    }
}