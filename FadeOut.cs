using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
/// A screensized black 2D texture is created and rendered on screen for the given duration. 
/// Each frame Stepsize is used to decrement the alpha value.
*/

public class FadeOut : MonoBehaviour
{
    public float Stepsize = 3f;

    private Color _Color = Color.black;
    private float _Alpha = 0.0f;
    private Texture2D _Texture;

    private void Start()
    {
        _Texture = new Texture2D(1, 1);
        _Texture.SetPixel(0, 0, new Color(_Color.r, _Color.g, _Color.b, _Alpha));
        _Texture.Apply();
    }

    public void OnGUI()
    {

        if (_Alpha >= 1.0f)
        {
            this.enabled = false;
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _Texture);

        _Alpha += Time.deltaTime / Stepsize;
        _Texture.SetPixel(0, 0, new Color(_Color.r, _Color.g, _Color.b, _Alpha));
        _Texture.Apply();
    }
}
