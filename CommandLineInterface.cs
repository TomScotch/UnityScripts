using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
/// Parse Input to Dictionary and Return the Result of the Execution
*/

public class CommandLineInterface : MonoBehaviour
{

    private Dictionary<string, Procedure> CommandList = new Dictionary<string, Procedure>();
    private delegate string Procedure(string input);
    private bool isOn = false;
    public InputField input;
    // public Text text;
    public GameObject player;

    public string Print(string inString) => inString;

    private void initCommandList()
    {
        CommandList.Add("print", Print);
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.F1))
        {
            isOn = !isOn;

            input.text = "";

            if (isOn)
            {
                //    text.gameObject.SetActive(true);
                input.gameObject.SetActive(true);
                input.ActivateInputField();
                player.SetActive(false);
            }
            else
            {
                //        text.gameObject.SetActive(false);
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
        // text.gameObject.SetActive(false);
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