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
    private float intensity;

    public void begin()
    {
        intensity = flashlight.intensity;

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
            flashlight.intensity = intensity;
            StopCoroutine(coroutine);
        }
    }

    IEnumerator FlashLightFlicker(Light flashlight)
    {
        while (true)
        {
            if (flashlight.enabled)
            {

                if(flashlight.intensity == intensity)
                {
                    flashlight.intensity = 0f;
                }
                else
                {
                    flashlight.intensity = intensity;
                }

            }
            yield return new WaitForSeconds(Random.Range(0.3f, 1.2f));
        }
    }
}