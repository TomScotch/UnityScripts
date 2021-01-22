using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject button;
    public GameObject lights;
    private float alpha = 1.0f;
    private bool upORdown;
    public float timeMod = 0.5f;

    void Update()
    {
        if (alpha < 1f && upORdown)
        {
            alpha += (timeMod * Time.deltaTime);
        }

        if (alpha > 0f && !upORdown)
        {
            alpha -= (timeMod * Time.deltaTime);
        }

        if (alpha < 0f && !upORdown)
        {
            upORdown = true;
        }

        if (alpha > 1f && upORdown)
        {
            upORdown = false;
        }

        button.GetComponent<Text>().color = new Color(1, 0, 0, alpha);

        if (Input.anyKeyDown)
        {
            lights.SetActive(false);
            button.SetActive(false);
            this.enabled = false;
            SceneManager.LoadSceneAsync("mansion", LoadSceneMode.Single);
        }
    }
}
