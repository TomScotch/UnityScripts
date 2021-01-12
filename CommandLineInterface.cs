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

    private bool isOn = false;
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
    private Color MoonColor;
    private Color SwampColor;
    public GameObject[] evileyes;

    public void menuStartGame()
    {
        lights.SetActive(false);
        if (button != null)
            button.SetActive(false);
        SceneManager.LoadSceneAsync("Mansion", LoadSceneMode.Single);
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
        else
        {
            foreach (GameObject evileye in evileyes)
            {
                evileye.SetActive(false);
            }
        }
        return " EvilEyes are " + inString;
    }
    public string BloodMoon(string inString)
    {

        if (inString.Equals("on"))
        {

            foreach (GameObject evileye in evileyes)
            {
                evileye.SetActive(true);
            }

            moon.color = Color.red;
            swamp.color = Color.red;
        }
        else
        {

            foreach (GameObject evileye in evileyes)
            {
                evileye.SetActive(false);
            }

            moon.color = MoonColor;
            swamp.color = SwampColor;
        }
        return " BloodMoon is " + inString;
    }
    public string Clear(string inString)
    {
        return "";
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
        else
        {
            fps.enabled = false;
        }

        return "fps counter is : " + inString;
    }
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
        string returnstring = "";

        if (player != null)
        {
            SaveGame save = new SaveGame();
            save.sceneName = SceneManager.GetActiveScene().name;
            PlayerCharacterController pc = player.GetComponent<PlayerCharacterController>();
            save.flashlight = pc.flashlight.enabled;
            save.rotation = player.transform.rotation;
            save.position = player.transform.position;
            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + inString + ".json", JsonUtility.ToJson(save));
            returnstring = "saved your game to : " + Application.persistentDataPath + "/" + inString + ".json";
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

        try
        {
            SaveGame save = JsonUtility.FromJson<SaveGame>(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + inString + ".json"));

            if (!SceneManager.GetActiveScene().name.Equals(save.sceneName))
            {
                GameResources._loadSaveOnStart = inString;
                SceneManager.LoadScene(save.sceneName, LoadSceneMode.Single);
            }
            else
            {
                StartCoroutine(LoadScene(inString));
            }

            returntext = "loaded your game from : " + Application.persistentDataPath + "/" + inString + ".json";
        }
        catch (Exception ex) { returntext = GameResources._error + ex.Message; }

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
            else
            {
                lights.SetActive(false);
            }

            return "Lights are switched" + inString;
        }
        else
        {
            return GameResources._error;
        }

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
            case "":
                helpstring = GameResources._help_empty;
                break;
        }

        return helpstring;
    }
    private string Print(string inString) => inString;
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
        menuStartGame();
        //SceneManager.LoadScene("Mansion", LoadSceneMode.Single);
        return "loaded Mansion";
    }
    private string EndGame(string inString)
    {
        switchGui(onOff: "off");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
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
    }
    void Update()
    {
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