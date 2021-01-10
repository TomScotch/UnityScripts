public static class GameResources
{
    // use for loading player data from save after scene change during loading a savegame
    public static string _loadSaveOnStart = "";

    // list of the commands available 
    public const string _cmdlist = 
        "- credits \n" +
        "- clear \n" +
        "  - cmdlist \n" +
        "  - help <name> \n" +
        "  - quit \n" +
        " - fps <on/off> \n" +
        " - start \n" +
        "  - stop \n" +
        " - load <name> \n" +
        " - save <name> \n" +
        " - screenshot \n" +
        "  - turnlights <on/off>\n" +
        " - flicker <on/off>  \n" +
        " - help controls \n" +
        " - bloodmoon <on/off> \n" +
        " - evileyes <on/off> \n" +
        " - listsaves \n";

    // description of player controls
    public const string _help_controls =
        "Use mouse to look around \n" +
        " F1 - OpenConsole \n" +
        " WASD - Forward,Back,Left,Right \n" +
        " Space - Jump \n" +
        " Shift - Run \n" +
        " C - Crouch \n" +
        " F - Flashlight \n";

    // credits
    public const string _credits =
        "Unity Technologies - Unity Engine \n \n" +
        " Quixel - Megascans \n \n" +
        " Microsoft - Visual Studio \n \n" +
        " Created by TomScotch@web.de \n \n" +
        " Thanks to all my Friends and Family \n \n" +
        " Special Thanks to all who played this game \n \n" +
        " THANK YOU ALL, YOU ARE THE BEST";

    // user feedback messages
    public const string _error = "iam sorry i can't do that right now";
    public const string _success = "successfully done";
    public const string _missing = "please provide a parameter";
    public const string _command_input_empty = "Please type help or cmdlist";

    // description of command when used with help
    public const string _help_credits = "Show Game Credits";
    public const string _help_print = "Print the text to the screen";
    public const string _help_flicker = "Activate Flashlight Flickering ";
    public const string _help_quit = "Quit the Game";
    public const string _help_stop = "Back to Main Menu";
    public const string _help_start = "Start the Game";
    public const string _help_screenshots = "Take a Screenshot";
    public const string _help_fps = "Shows frames per second";
    public const string _help_turnlights = "Switch all lights";
    public const string _help_save = "Save your game to disk";
    public const string _help_load = "Load your game from disk";
    public const string _help_cmdlist = "Shows a list of commands";
    public const string _help_empty = "Get help with a specific command";
    public const string _help_listsave = "print a list of your save files";
    public const string _help_clear = "Clears Console";
    public const string _starttip = "type cmdlist for a list of commands";
    public const string _help_bloodmoon = "turns the moon a bloody red";
    public const string _help_evileyes = "activate evil eyes for statues";
    //public const string _ = "";
}
