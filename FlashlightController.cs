using UnityEngine;

public class FlashlightController : MonoBehaviour {

    bool isOn = false;
    public GameObject flashlight;
    public AudioSource flashlightOn;
    public AudioSource flashlightOff;

    void Update () {

        if (Input.GetKeyUp (KeyCode.F)) {

            if(!isOn)
            {
                flashlightOff.Play();
               
            }else
            {
                 flashlightOn.Play();
            }

            flashlight.SetActive (!isOn);
            isOn = !isOn;
        }
    }
}