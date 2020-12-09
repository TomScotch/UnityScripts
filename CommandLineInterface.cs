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
    public static IEnumerable<GameObject> GetAllRootGameObjects()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            GameObject[] rootObjs = SceneManager.GetSceneAt(i).GetRootGameObjects();
            foreach (GameObject obj in rootObjs)
                yield return obj;
        }
    }

    public static IEnumerable<T> FindAllObjectsOfTypeExpensive<T>()
        where T : MonoBehaviour
    {
        foreach (GameObject obj in GetAllRootGameObjects())
        {
            foreach (T child in obj.GetComponentsInChildren<T>(true))
                yield return child;
        }
    }

    Dictionary<string, Procedure> CommandList { get; } = new Dictionary<string, Procedure>();
    private delegate string Procedure(string input);
    private bool isOn = false;
    public InputField input;
    public TextMeshProUGUI output;
    public GameObject player;
    public FlashLightFlickering flicker;

    private string Credits(string inString)
    {
        return GameResources._credits;
    }

    private string CmdList(string inString)
    {
        return GameResources._cmdlist;
    }
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

        switch (inString)
        {
            case "cmdlist":
                helpstring = GameResources._help_cmdlist;
                break;
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
            case "credits":
                helpstring = GameResources._help_credits;
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
        string msg;
        switchGui(onOff: "off");
        try
        {
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + inString + ".png");
            msg = "Screenshot save to : " + Application.persistentDataPath + "/" + inString + ".png";
        }
        catch (Exception ex)
        {
            msg = "Error" + "\n" + ex.Message;
        }
        switchGui(onOff: "on");
        return msg;
    }
    private string QuitGame(string inString)
    {
        switchGui(onOff: "off");
        Application.Quit(0);
        return "bye";
    }
    public string StartGame(string inString)
    {
        switchGui(onOff: "off");
        SceneManager.LoadScene("Mansion", LoadSceneMode.Single);
        return "loaded Mansion";
    }
    private string EndGame(string inString)
    {
        switchGui(onOff: "off");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        return "Returned to Main Menu";
    }
    private string Flicker(string inString)
    {
        if (flicker != null && player != null)
        {

            if (inString.Equals("on"))
            {
                if (!flicker.isFlickering)
                {
                    flicker.begin();
                }
            }

            if (inString.Equals("off"))
            {
                if (flicker.isFlickering)
                {
                    flicker.end();
                }
            }

            if (inString.Equals(""))
            {

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

        return "Flashlight Flickering is " + flicker.isFlickering;
    }

    private void initCommandList()
    {
        CommandList.Add("credits", Credits);
        CommandList.Add("cmdlist", CmdList);
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

    private void switchGui(string onOff = null)
    {
        if (onOff != null)
        {
            if (onOff.Equals("on")) { isOn = true; }
            if (onOff.Equals("off")) { isOn = false; }
        }

        if (isOn)
        {
            input.enabled = true;
            output.enabled = true;
            input.ActivateInputField();
            if (player != null)
            {
                player.GetComponent<PlayerCharacterController>().enabled = false;
                player.GetComponent<PlayerInputHandler>().enabled = false;
                player.GetComponent<FlashlightController>().enabled = false;
            }
        }
        else
        {
            input.DeactivateInputField();
            input.enabled = false;
            output.enabled = false;
            if (player != null)
            {
                player.GetComponent<PlayerCharacterController>().enabled = true;
                player.GetComponent<PlayerInputHandler>().enabled = true;
                player.GetComponent<FlashlightController>().enabled = true;
            }
        }
    }

    void Start()
    {
        initCommandList();
    }

    private void parseInputCommand(string command)
    {
        string[] cmd = command.ToLower().Split(' ');

        if (cmd.Length > 1 && CommandList.ContainsKey(cmd[0]))
        {
            output.text = CommandList[cmd[0]].Invoke(cmd[1]);
        }

        if (cmd.Length == 1 && CommandList.ContainsKey(cmd[0]))
        {
            output.text = CommandList[cmd[0]].Invoke("");
        }

        if (cmd[0].Equals(""))
        {
            output.text = GameResources._command_input_empty;
        }
    }
}