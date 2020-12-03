using System.Collections;
using UnityEngine;

/*
/// Switch the light on and off at random.
*/

public class FlashLightFlickering : MonoBehaviour
{

    public bool isFlickering { get; private set; }

    public Light flashlight;
    public void begin()
    {
        isFlickering = true;
        StartCoroutine(FlashLightFlicker(flashlight));
    }
    public void end()
    {
        isFlickering = false;
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