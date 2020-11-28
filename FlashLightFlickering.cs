using System.Collections;
using UnityEngine;

/*
/// Switch the light on and off at random.
*/

public class FlashLightFlickering : MonoBehaviour
{

    public Light flashlight;
    public void begin()
    {
        StartCoroutine(FlashLightFlicker(flashlight));
    }
    public void end()
    {
        StopCoroutine(FlashLightFlicker(flashlight));
    }

    IEnumerator FlashLightFlicker(Light flashlight)
    {
        while (true)
        {
            flashlight.enabled = !flashlight.enabled;
            yield return new WaitForSeconds(Random.Range(0.3f, 1.2f));
        }
    }
}