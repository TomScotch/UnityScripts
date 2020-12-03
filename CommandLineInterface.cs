using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;
using static FlashLightFlickering;

/*
/// Parse Input to Dictionary and Return the Result of the Execution
*/

public class CommandLineInterface : MonoBehaviour
{
    Dictionary<string, Procedure> CommandList { get; } = new Dictionary<string, Procedure>();
    private delegate string Procedure(string input);
    private bool isOn = false;
    public InputField input;
    public TextMeshProUGUI output;
    public GameObject player;
    public FlashLightFlickering flicker;

    private string SaveGame(string inString)
    {
        SaveGame save = new SaveGame();
        save.x = player.transform.position.x;
        save.y = player.transform.position.y;
        save.z = player.transform.position.z;
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + inString + ".json", JsonUtility.ToJson(save));
        return "save your game to : " + Application.persistentDataPath + "/" + inString + ".json";
    }

    private string LoadGáme(string inString)
    {
        SaveGame save = JsonUtility.FromJson<SaveGame>(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + inString + ".json"));
        player.transform.position = new Vector3(save.x, save.y, save.z);
        return "loaded your game from : " + Application.persistentDataPath + "/" + inString + ".json";
    }

    private string TurnLights(string inString)
    {

        Light[] lights = GetComponents<Light>();
        VisualEffect[] effects = GetComponents<VisualEffect>();

        foreach (VisualEffect effect in effects)
        {
            if (effect.enabled)
            {
                if (inString.Equals("off"))
                {
                    effect.enabled = false;
                }
            }
            else
            {
                if (inString.Equals("on"))
                {
                    effect.enabled = true;
                }
            }
        }

        foreach (Light light in lights)
        {
            if (light.enabled)
            {
                if (inString.Equals("off"))
                {
                    light.enabled = false;
                }
                else if (inString.Equals(""))
                {
                    light.enabled = true;
                }
            }
            else
            {
                if (inString.Equals("on"))
                {
                    light.enabled = true;
                }
                else if (inString.Equals(""))
                {
                    light.enabled = false;
                }
            }
        }
        return lights.Length + " Lights are switched" + inString;
    }
    private string Help(string inString)
    {
        string helpstring = "";

        switch (helpstring)
        {
            case "print":
                helpstring = GameResources._help_print;
                break;
            case "flicker":
                helpstring = GameResources._help_flicker;
                break;
            case "start":
                helpstring = GameResources._help_start;
                break;
            case "stop":
                helpstring = GameResources._help_stop;
                break;
            case "quit":
                helpstring = GameResources._help_quit;
                break;
            case "screenshot":
                helpstring = GameResources._help_screenshots;
                break;
            case "turnlights":
                helpstring = GameResources._help_turnlights;
                break;
            case "controls":
                helpstring = GameResources._help_controls;
                break;
            case "save":
                helpstring = GameResources._help_save;
                break;
            case "load":
                helpstring = GameResources._help_load;
                break;
            case "":
                helpstring = GameResources._help_empty;
                break;
        }

        return helpstring;
    }
    private string Print(string inString) => inString;
    private string Screenshot(string inString)
    {
        isOn = false;
        switchGui();
        ScreenCapture.CaptureScreenshot(DateTime.Now + ".png");
        return "bye";
    }
    private string QuitGame(string inString)
    {
        isOn = false;
        switchGui();
        Application.Quit();
        return "bye";
    }
    public string StartGame(string inString)
    {
        isOn = false;
        switchGui();
        SceneManager.LoadScene("Mansion", LoadSceneMode.Single);
        return "loaded Mansion";
    }
    private string EndGame(string inString)
    {
        isOn = false;
        switchGui();
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        return "Loaded Main";
    }
    private string Flicker(string inString)
    {
        if (flicker != null && player != null)
        {

            if (inString.Equals("on"))
            {
                if (!flicker.isFlickering)
                {
                    player.SetActive(true);
                    flicker.begin();
                }
            }

            if (inString.Equals("off"))
            {
                if (flicker.isFlickering)
                {
                    player.SetActive(true);
                    flicker.end();
                }
            }

            if (inString.Equals(""))
            {
                player.SetActive(true);

                if (flicker.isFlickering)
                {
                    flicker.end();
                }
                else
                {
                    flicker.begin();
                }
            }
        }
        else
        {
            return "i cannot do that now";
        }

        return "Flashlight Flickering is " + inString;
    }

    private void initCommandList()
    {
        CommandList.Add("print", Print);
        CommandList.Add("help", Help);
        CommandList.Add("flicker", Flicker);
        CommandList.Add("start", StartGame);
        CommandList.Add("stop", EndGame);
        CommandList.Add("quit", QuitGame);
        CommandList.Add("screenshot", Screenshot);
        CommandList.Add("turnlights", TurnLights);
        CommandList.Add("save", SaveGame);
        CommandList.Add("load", LoadGáme);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            isOn = !isOn;
            input.text = "";
            output.text = "";
            switchGui();
        }

        if (isOn)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                string s = input.text;
                input.text = "";
                parseInputCommand(s);
                input.ActivateInputField();
            }
        }
    }

    private void switchGui()
    {
        if (isOn)
        {
            input.gameObject.SetActive(true);
            output.gameObject.SetActive(true);
            input.ActivateInputField();
            if (player != null)
                player.SetActive(false);
        }
        else
        {
            input.gameObject.SetActive(false);
            output.gameObject.SetActive(false);
            input.DeactivateInputField();
            if (player != null)
                player.SetActive(true);
        }
    }

    void Start()
    {
        initCommandList();
    }

    private void parseInputCommand(string command)
    {
        string[] cmd = command.Split(' ');

        if (cmd.Length > 0 && CommandList.ContainsKey(cmd[0]))
        {
            output.text = CommandList[cmd[0]].Invoke(cmd[1]);
        }

        if (cmd.Length == 0 && CommandList.ContainsKey(cmd[0]))
        {
            output.text = CommandList[cmd[0]].Invoke("");
        }
    }
}