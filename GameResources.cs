public static class GameResources
{

    public const string _levelList = "main, mansion, garden, bookroom";

    // use for loading player data from save after scene change during loading a savegame
    public static string _loadSaveOnStart = "";

    // list of the commands available 
    public const string _cmdlist =
        "listscenes \n" +
        "scene <name> ; " +
        "credits ; " +
        "clear \n" +
        "help <name> ; " +
        "quit \n" +
        "fps <on/off> \n" +
        "start  ; " +
        "stop \n" +
        "load <name> ; " +
        "save <name> \n" +
        "screenshot \n" +
        "turnlights <on/off>;" +
        "flicker <on/off>  \n" +
        "controls ; " +
        "checklist \n" +
        "bloodmoon <on/off> ; " +
        "evileyes <on/off> \n" +
        "intro ; " +
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
    public const string _scene_missing = "can't find a Scene with that name : ";
    public const string _nohelpwiththat = "there is no command with that name i could help you with";
    public const string _missingSaveName = "please provide a name to save your game !";
    public const string _missingLoadName = "please provide the name of your save game to load !";

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
    public const string _help_intro = "Read the Game Introduction";
    public const string _help_checklist = "Game Dev Feature Checklist";

    // CHECKLIST FOR TESTING    
    public const string checklist =
        "Hide Cursor - Fade In - Lightning Strike - Fog - Statues Heads Move \n" +
        "Toy Box on the Floor Jumps up - Cli help  - Cli cmd main - Cli error \n" +
        "Box on the Table with Mirror Jumps Up Flashlight Crow Scream \n" +
        "Lights go crazy and flashlight turns off and on \n" +
        "Flashlight Won't cast Shadows when Camera is close to wall \n" +
        "Scary Sound on top of Stairs - Footsteps Sound & Sound when Running \n" +
        "Jump, Crouch, Flashlight, Zoom, Sprint, Console - listscenes - scene\n" +
        "Vignette on crouching - Scary Whispers on the Second Floor \n" +
        "Second floor Adler flews from wall - Girl Scream - Sound of Water Drops \n" +
        "Wind howls at the start point - Volumetric Lights - Bloodmoon \n" +
        "Lights are Flickering - Moon - Collisions - Shadows - Mirror First Floor \n" +
        "Sounds for console, bgm, events, jumping, landing -Evil Eyes \n" +
        "Zoom - Cli Tooltip - Active Console disables player controls \n" +
        "Main Scene loads Mansion Smoothly - Load all Scenes \n";



    public const string GameIntro =
        "Your Sanity decreases on witnessing paranormal events.\n" + 
        "Using the Mystic Arts will drain your Willpower. \n" +
        "Willpower defines how much sanity you recover.\n" +
        "Sleep replenishes your Willpower, at the cost of time. \n" +
        "The Butterflies are drawn by your willpower. \n" +
        "Pictures marked by Blood are Doors, focus on them.\n " +
        "You have Seven Days for your Journey Good Luck.";
}
