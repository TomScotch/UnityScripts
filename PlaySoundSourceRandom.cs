using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundSourceRandom : MonoBehaviour {

    public AudioSource source;
    public float minimum = 60.0f;
    public float maximum = 180.0f;
    private float counter = 0.0f;
    private float random = 0.0f;

    void Update () {

        counter += 0.1f; //Time.DeltaTime

        if (random == 0.0f) {

            random = Random.Range (minimum, maximum);
        } else {

            if (counter >= random) {

                source.Play ();
                counter = 0.0f;
                random = 0.0f;
            }
        }
    }
}