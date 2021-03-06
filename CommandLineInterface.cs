using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static FlashLightFlickering;
using static ShowFPS;

/*
/// Parse Input to Dictionary and Return the Result of the Execution
*/

public class CommandLineInterface : MonoBehaviour
{
    Dictionary<string, Procedure> CommandList { get; } = new Dictionary<string, Procedure>();

    private delegate string Procedure(string input);
    private bool isBloodMoon = false;
    private bool isOn = false;
    private Color MoonColor;
    private Color SwampColor;
    private GameObject[] evileyes;

    public InputField input;
    public TextMeshProUGUI output;
    public GameObject player;
    public FlashLightFlickering flicker;
    public ShowFPS fps;
    public GameObject lights;
    public AudioSource console;
    public AudioSource inputReturn;
    public GameObject button;
    public Light moon;
    public Light swamp;
    private List<string> history = new List<string>();
    private int history_cursor = 0;

    private IEnumerator MakeBloody()
    {

        float moonRed = moon.color.r + (0.03f / Time.deltaTime);
        float swampRed = swamp.color.r + (0.03f / Time.deltaTime);

        moon.color = new Color(moonRed, MoonColor.g, MoonColor.b, MoonColor.a);
        swamp.color = new Color(swampRed, SwampColor.g, SwampColor.b, SwampColor.a);

        if (swampRed >= 64f || moonRed >= 64f)
        {
            StopCoroutine(MakeBloody());
        }

        yield return new WaitForSeconds(.1f);
    }
    private IEnumerator MakeBloodyNot()
    {
        float moonRed = moon.color.r - (0.9f / Time.deltaTime);
        float swampRed = swamp.color.r - (0.9f / Time.deltaTime);

        moon.color = new Color(moonRed, MoonColor.g, MoonColor.b, MoonColor.a);
        swamp.color = new Color(swampRed, SwampColor.g, SwampColor.b, SwampColor.a);

        if (swampRed <= SwampColor.r || moonRed <= MoonColor.r)
        {
            moon.color = MoonColor;
            swamp.color = SwampColor;
            StopCoroutine(MakeBloodyNot());
        }

        yield return new WaitForSeconds(.1f);
    }
    private IEnumerator LoadScene(string saveName)
    {
        SaveGame save = JsonUtility.FromJson<SaveGame>(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + saveName + ".json"));

        if (SceneManager.GetActiveScene().name.Equals(save.sceneName))
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                GameObject.FindWithTag("Player").transform.position = save.position;
                PlayerCharacterController pc = GameObject.FindWithTag("Player").GetComponent<PlayerCharacterController>();
                pc.flashlight.enabled = save.flashlight;
                GameObject.FindWithTag("Player").transform.rotation = save.rotation;
                StopCoroutine(LoadScene(saveName));
            }
        }
        yield return new WaitForSeconds(.1f);
    }
    private IEnumerator TakeScreenShot()
    {
        switchGui(onOff: "off");
        new WaitForSeconds(.3f);
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png");
        StopCoroutine(TakeScreenShot());
        yield return new WaitForSeconds(.1f);
    }


    public string ListScenes(string inString) => GameResources._levelList;
    public string Intro(string inString) => GameResources.GameIntro;
    public string Checklist(string inString) => GameResources.checklist;
    public string Clear(string inString) => "";

    private string Print(string inString) => inString;
    private string CmdList(string inString) => GameResources._cmdlist;
    private string Credits(string inString) => GameResources._credits;


    public string StartScene(string name = "")
    {
        string returnString = "";

        if (name.Equals(""))
        {
            returnString = SceneManager.GetActiveScene().name.ToString();
        }
        else if (GameResources._levelList.Contains(name))
        {
            if (button != null && lights != null)
            {
                button.SetActive(false);
                lights.SetActive(false);
            }
            returnString = "loading Scene : " + name;
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        }
        else
        {
            returnString = GameResources._scene_missing + name;
        }

        return returnString;
    }
    public string EvilEyes(string inString)
    {
        if (inString.Equals("on"))
        {
            foreach (GameObject evileye in evileyes)
            {
                evileye.SetActive(true);
            }
        }
        else if (inString.Equals("off"))
        {
            foreach (GameObject evileye in evileyes)
            {
                evileye.SetActive(false);
            }
        }
        else if (inString.Equals(""))
        {
            inString = evileyes[0].activeSelf.ToString();
        }
        else { return GameResources._error; }
        return " EvilEyes are " + inString;
    }
    public string BloodMoon(string inString)
    {

        if (inString.Equals("on") && !isBloodMoon)
        {
            foreach (GameObject evileye in evileyes)
            {
                evileye.SetActive(true);
            }

            StartCoroutine(MakeBloody());
            isBloodMoon = true;
        }
        else if (inString.Equals("off") && isBloodMoon)
        {
            foreach (GameObject evileye in evileyes)
            {
                evileye.SetActive(false);
            }

            StartCoroutine(MakeBloodyNot());
            isBloodMoon = false;
        }
        else if (inString.Equals(""))
        {
            inString = isBloodMoon.ToString();
        }
        else { return GameResources._error; }
        return " BloodMoon is " + inString;
    }
    private string ListSaves(string inString)
    {
        string returnstring = "";

        try
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath + "/");
            foreach (string file in files)
            {
                if (file.Contains(".json"))
                {
                    string[] splitfile = file.Split('/');
                    string filename = splitfile[splitfile.Length - 1].ToString().Replace(".json", "");
                    returnstring += filename + "\n";
                }
            }
        }
        catch (Exception ex)
        {
            returnstring = ex.Message;
        }

        return returnstring;
    }
    private string ShowFPS(string inString)
    {
        if (inString.Equals("on"))
        {
            fps.enabled = true;
        }
        else if (inString.Equals("off"))
        {
            fps.enabled = false;
        }
        else if (inString.Equals(""))
        {
            inString = fps.enabled.ToString();
        }
        else { return GameResources._error; }
        return "fps is : " + inString;
    }
    private string SaveGame(string inString)
    {
        string returnstring = "";

        if (player != null)
        {
            if (!inString.Equals(""))
            {
                SaveGame save = new SaveGame();
                save.sceneName = SceneManager.GetActiveScene().name;
                PlayerCharacterController pc = player.GetComponent<PlayerCharacterController>();
                save.flashlight = pc.flashlight.enabled;
                save.rotation = player.transform.rotation;
                save.position = player.transform.position;
                System.IO.File.WriteAllText(Application.persistentDataPath + "/" + inString + ".json", JsonUtility.ToJson(save));
                returnstring = "saved your game to : " + Application.persistentDataPath + "/" + inString + ".json";
            }else
            {
                returnstring = GameResources._missingSaveName;
            }
        }
        else
        {
            returnstring = GameResources._error;
        }

        return returnstring;
    }
    private string LoadGame(string inString)
    {
        string returntext;
        if (!inString.Equals(""))
        {
            try
            {
                SaveGame save = JsonUtility.FromJson<SaveGame>(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + inString + ".json"));

                if (!SceneManager.GetActiveScene().name.Equals(save.sceneName))
                {
                    GameResources._loadSaveOnStart = inString;
                    StartScene(save.sceneName);
                }
                else
                {
                    StartCoroutine(LoadScene(inString));
                }

                returntext = "loaded your game from : " + Application.persistentDataPath + "/" + inString + ".json";
            }
            catch (Exception ex) { returntext = ex.Message; }
        }else
        {
            returntext = GameResources._missingLoadName;
        }

        return returntext;
    }
    public string TurnLights(string inString)
    {
        if (lights != null)
        {
            if (inString.Equals("on"))
            {
                lights.SetActive(true);
            }
            else if (inString.Equals("off"))
            {
                lights.SetActive(false);
            }
            else if (inString.Equals(""))
            {
                inString = lights.activeSelf.ToString();
            }
            else { return GameResources._error; }

            return "Lights are : " + inString;
        }
        else
        {
            return GameResources._error;
        }

    }

    private string Screenshot(string inString)
    {
        StartCoroutine(TakeScreenShot());
        return "";
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
        StartScene("mansion");
        return "loaded mansion";
    }
    private string EndGame(string inString)
    {
        switchGui(onOff: "off");
        StartScene("main");
        return "Returned to Main Menu";
    }
    public string Flicker(string inString)
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
            else if (inString.Equals("off"))
            {
                if (flicker.isFlickering)
                {
                    flicker.end();
                }
            }else if (inString.Equals(""))
            {
                inString = flicker.isFlickering.ToString();
            }
            else { return GameResources._error; }
        }
        else
        {
            return GameResources._error;
        }

        return "Flashlight Flickering is " + inString;
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
            case "fps":
                helpstring = GameResources._help_fps;
                break;
            case "listsaves":
                helpstring = GameResources._help_listsave;
                break;
            case "clear":
                helpstring = GameResources._help_clear;
                break;
            case "evileyes":
                helpstring = GameResources._help_evileyes;
                break;
            case "checklist":
                helpstring = GameResources._help_checklist;
                break;
            case "zoom":
                helpstring = GameResources._help_zoom;
                break;
            case "scene":
                helpstring = GameResources._help_scene;
                break;
            case "listscenes":
                helpstring = GameResources._help_listscenes;
                break;
            case "intro":
                helpstring = GameResources._help_intro;
                break;
            case "bloodmoon":
                helpstring = GameResources._help_bloodmoon;
                break;
            case "":
                helpstring = GameResources._help_empty;
                break;
            default:
                helpstring = GameResources._nohelpwiththat;
                break;
        }

        return helpstring;
    }
    private void initCommandList()
    {
        CommandList.Add("clear", Clear);
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
        CommandList.Add("load", LoadGame);
        CommandList.Add("fps", ShowFPS);
        CommandList.Add("listsaves", ListSaves);
        CommandList.Add("bloodmoon", BloodMoon);
        CommandList.Add("evileyes", EvilEyes);
        CommandList.Add("scene", StartScene);
        CommandList.Add("listscenes", ListScenes);
        CommandList.Add("intro", Intro);
        CommandList.Add("checklist", Checklist);
    }
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if ((history_cursor + 1) < history.Count)
            {
                history_cursor += 1;
                input.text = history[history_cursor];
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if ((history_cursor - 1) >= 1)
            {
                history_cursor -= 1;
                input.text = history[history_cursor];
            }

        }

        if (Input.GetKeyUp(KeyCode.F1))
        {
            isOn = !isOn;
            input.text = "";

            switchGui();
            console.Play();
        }

        if (isOn)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                string s = input.text;
                input.text = "";
                parseInputCommand(s);
                input.ActivateInputField();
                inputReturn.Play();
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
                // player.GetComponent<FlashlightController>().enabled = false;
            }
        }
        else
        {
            output.text = "";
            input.DeactivateInputField();
            input.enabled = false;
            output.enabled = false;
            if (player != null)
            {
                player.GetComponent<PlayerCharacterController>().enabled = true;
                player.GetComponent<PlayerInputHandler>().enabled = true;
                //player.GetComponent<FlashlightController>().enabled = true;
            }
        }
    }
    void Start()
    {

        MoonColor = moon.color;
        SwampColor = swamp.color;

        evileyes = GameObject.FindGameObjectsWithTag("EvilEyes");

        foreach (GameObject evileye in evileyes)
        {
            evileye.SetActive(false);
        }

        output.text = GameResources._starttip;

        if (!GameResources._loadSaveOnStart.Equals(""))
        {
            StartCoroutine(LoadScene(GameResources._loadSaveOnStart));
            GameResources._loadSaveOnStart = "";
        }

        initCommandList();
    }
    private void parseInputCommand(string command)
    {
        string[] cmd = command.ToLower().Split(' ');

        if (cmd.Length > 1 && CommandList.ContainsKey(cmd[0]))
        {
            output.text = CommandList[cmd[0]].Invoke(cmd[1]);
            history.Add(cmd[0] + " " + cmd[1]);
        }

        if (cmd.Length == 1 && CommandList.ContainsKey(cmd[0]))
        {
            output.text = CommandList[cmd[0]].Invoke("");
            history.Add(cmd[0]);
        }

        if (cmd[0].Equals(""))
        {
            output.text = GameResources._command_input_empty;
        }
    }
}