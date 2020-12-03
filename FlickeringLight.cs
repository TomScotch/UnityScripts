using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class FlickeringLight : MonoBehaviour {
    private Light flashlight;
    [Tooltip ("Minimum intensity")]
    public float minIntensity = 0.0025f;
    [Tooltip ("Maximum intensity")]
    public float maxIntensity = 0.025f;
    [Tooltip ("low = sparks, high = lantern")]
    [Range (1, 50)]
    public static int smoothing = 25;
    Queue<float> smoothQueue = new Queue<float> (smoothing);
    float lastSum = 0;

    public void Reset () {
        smoothQueue.Clear ();
        lastSum = 0;
    }

    void Start () {
        flashlight = GetComponent<Light>();
        smoothQueue = new Queue<float> (smoothing);
    }

    void Update () {

        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue ();
        }

        float newVal = Random.Range (minIntensity, maxIntensity);
        smoothQueue.Enqueue (newVal);
        lastSum += newVal;
        flashlight.intensity = lastSum / (float) smoothQueue.Count;
    }

}