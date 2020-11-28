using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static FlashLightFlickering;

/*
/// Parse Input to Dictionary and Return the Result of the Execution
*/

public class CommandLineInterface : MonoBehaviour
{
    private Dictionary<string, Procedure> CommandList { get; } = new Dictionary<string, Procedure>();
    private delegate string Procedure(string input);
    private bool isOn = false;
    public InputField input;
    public GameObject player;

    private string Help(string inString)
    {
        //@TODO FILL RESOURCE FILE WITH COMMAND DESCRIPTIONS AND RETURN THEM
        return "Need Help with " + inString + "?";
    }
    private string Print(string inString) => inString;
    public FlashLightFlickering flicker;

    private string Flicker(string inString)
    {
        if (flicker != null)
        {

            if (inString.Equals("on"))
            {
                player.SetActive(true);
                flicker.begin();
                //player.SetActive(false);
            }
            else if (inString.Equals("off"))
            {
                player.SetActive(true);
                flicker.end();
              //  player.SetActive(false);
            }
        }

        return "Flashlight Flickering is " + inString;
    }

    private void initCommandList()
    {
        CommandList.Add("print", Print);
        CommandList.Add("help", Help);
        CommandList.Add("flicker", Flicker);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            isOn = !isOn;

            input.text = "";

            if (isOn)
            {
                input.gameObject.SetActive(true);
                input.ActivateInputField();
                player.SetActive(false);
            }
            else
            {
                input.gameObject.SetActive(false);
                input.DeactivateInputField();
                player.SetActive(true);
            }
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

    void Start()
    {
        input.gameObject.SetActive(false);
        initCommandList();
    }

    private void parseInputCommand(string command)
    {
        string[] cmd = command.Split(' ');
        if (cmd.Length > 0 && CommandList.ContainsKey(cmd[0]))
        {
            input.text = CommandList[cmd[0]].Invoke(cmd[1]);
        }
    }
}