using UnityEngine;

public class FlashlightController : MonoBehaviour {

    public Light flashlight;
    public AudioSource flashlightOn;
    public AudioSource flashlightOff;

    void Update () {

        if (Input.GetKeyUp (KeyCode.F)) {

            if(!flashlight.enabled)
            {
                flashlightOff.Play();
               
            }else
            {
                 flashlightOn.Play();
            }

            flashlight.enabled = !flashlight.enabled;
        }
    }
}