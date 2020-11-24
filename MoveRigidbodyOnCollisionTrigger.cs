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
    public float force1 = 50f;
    public float force2 = 50f;
    public Directions dir1;
    public Directions dir2;
    public bool continous = false;
    public AudioSource sound;
    public bool ActivateGravityOnCollision = false;

    private void OnTriggerEnter (Collider other) {

        if (sound != null && !sound.isPlaying) {
            sound.Play ();
        }

        Vector3 vec1 = new Vector3 ();
        Vector3 vec2 = new Vector3 ();

        switch (dir1) {
            case Directions.Up:
                vec1 = (transform.up * force1);
                break;
            case Directions.Right:
                vec1 = (transform.right * force1);
                break;
            case Directions.Forward:
                vec1 = (transform.forward * force1);
                break;
        }

        switch (dir2) {
            case Directions.Up:
                vec2 = (transform.up * force2);
                break;
            case Directions.Right:
                vec2 = (transform.right * force2);
                break;
            case Directions.Forward:
                vec2 = (transform.forward * force2);
                break;
        }

        rb.isKinematic = false;

        if (ActivateGravityOnCollision) {

            rb.useGravity = true;
        }

        rb.AddForce (vec1 + vec2);

        if (!continous) {
            gameObject.SetActive (false);
        }
    }
}