using System.Collections;
using UnityEngine;

/*
/// Switch the light on and off at random.
*/

public class FlashLightFlickering : MonoBehaviour
{

    public bool isFlickering { get; set; } = false;

    public Light flashlight;

    private Coroutine coroutine;

    public void begin()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            coroutine = StartCoroutine(FlashLightFlicker(flashlight));
        }
    }
    public void end()
    {
        if (isFlickering)
        {
            isFlickering = false;
            StopCoroutine(coroutine);
        }
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