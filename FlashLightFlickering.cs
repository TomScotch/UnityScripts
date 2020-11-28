using System.Collections;
using UnityEngine;

/*
/// Switch the light on and off at random. Use isActivated property to control the effect.
*/

public class FlashLightFlickering : MonoBehaviour {

    public bool isActivated { set; get; }

    private void Start () => StartCoroutine (FlashLightFlicker ());
    private void Stop () => StopCoroutine (FlashLightFlicker ());

    IEnumerator FlashLightFlicker () {

        while (isActivated) {

            Light flashlight = gameObject.GetComponent (typeof (Light)) as Light;
            flashlight.enabled = !flashlight.enabled;
            yield return new WaitForSeconds (Random.Range (0, 1));
        }
    }
}