public static class GameResources
{

    public const string _levelList = "Main, Mansion, Garden, BookRoom";

    // use for loading player data from save after scene change during loading a savegame
    public static string _loadSaveOnStart = "";

    // list of the commands available 
    public const string _cmdlist =
        "listscenes \n" +
        "scene <name> \n" +
        "credits \n" +
        "clear \n" +
        "cmdlist \n" +
        "help <name> \n" +
        "quit \n" +
        "fps <on/off> \n" +
        "start \n" +
        "stop \n" +
        "load <name> \n" +
        "save <name> \n" +
        "screenshot \n" +
        "turnlights <on/off>\n" +
        "flicker <on/off>  \n" +
        "help controls \n" +
        "bloodmoon <on/off> \n" +
        "evileyes <on/off> \n" +
        "listsaves \n";

    // description of player controls
    public const string _help_controls =
        " Move mouse to look around \n" +
        " F1 - OpenConsole \n" +
        " WASD - Forward,Back,Left,Right \n" +
        " Space - Jump \n" +
        " Shift - Run \n" +
        " C - Crouch \n" +
        " F - Flashlight \n" +
        " RightMouse - Zoom \n";

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
    public const string _help_scene = "loads the scene";
    public const string _help_listscenes = "list available scenes";
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
    public const string _help_zoom = "brings your sight closer to the scene";

    // CHECKLIST FOR TESTING    
    public const string _help_checklist =
        "Hide Cursor \n" +
        "Fade In \n" +
        "Lightning Strike \n" +
        "Fog \n" +
        "Statues Heads Move \n" +
        "Toy Box on the Floor Jumps up \n" +
        "Box on the Table with Mirror Jumps Up \n" +
        "Flashlight \n" +
        "Crow Scream \n" +
        "Lights go crazy and flashlight turns off and on \n" +
        "Scary Sound on the end of the Stairs \n" +
        "Flashlight Won't cast Shadows when Camera is close to wall \n" +
        "Footsteps Sound & Sound when Running \n" +
        "Jump, Crouch, Flashlight, Zoom, Sprint, Console \n" +
        "Scary Whispers on the Second Floor \n" +
        "Second floor Adler flews from wall \n" +
        "Second floor Girl Screams \n" +
        "Second floor Water Drops \n" +
        "Candles and Fireplaces are Flickering \n" +
        "Vignette effect when crouching \n" +
        "Wind howls at the start point \n" +
        "Volumetric Lights \n" +
        "Shadows \n" +
        "Mirror First Floor \n" +
        "Moon \n" +
        "Collisions \n" +
        "Sounds for console, bgm, events, jumping, landing \n" +
        "Zoom \n" +
        "Cli Tooltip \n" +
        "Active Console disables player controls \n" +
        "Bloodmoon \n" +
        "Evil Eyes \n" +
        "listscenes \n" +
        "scene \n" +
        "Main Scene loads Mansion Smoothly \n" +
        "Cli help -> listscenes, scene, cmdlist, listsaves, save, load, begin, end, quit, clear, flicker, fps, turnights, credits, help controls, screenshot,clear,zoom \n" +
        "Cli cmd main -> listscenes, scene, cmdlist, listsaves, save, load, begin, end, quit, clear, flicker, fps, turnights, credits, help controls, screenshot, clear \n" +
        "Cli error -> error, success, fail -> listscenes, scene, cmdlist, listsaves, save, load, begin, end, quit, clear, flicker, fps, turnights, credits, help controls, screenshot, clear \n";
}
