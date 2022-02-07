using IronPython.Hosting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Scripting.Hosting;
using System.IO;
using System;
using TMPro;

public class PythonHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] RectTransform content;
    [SerializeField] TMP_Text textPrefab;
    [SerializeField] Sprite teamsSprite;

    ScriptEngine engine;
    ScriptScope scope;
    Stream stream;
    PythonIOWriter writer;

    List<PythonMethod> methods = new List<PythonMethod>();

    private void Awake()
    {
        Setup();
        writeMsgToConsole("Welcome to Python 2.99!");
        writeMsgToConsole("Try \"help()\"");
    }

    public void submit()
    {
        var text = input.text;
        if (String.IsNullOrEmpty(text))
            return;
        writeMsgToConsole(">>" + text);

        run(text);
        input.text = "";
        input.Select();
    }

    public void run(string code)
    {
        try
        {
            ScriptSource source = engine.CreateScriptSourceFromString(code,
                                  Microsoft.Scripting.SourceCodeKind.AutoDetect);
            source.Execute(scope);
        }
        catch (Exception e)
        {
            var inst = writeMsgToConsole(e.Message);
            inst.color = Color.red;
        }
    }

    private void Setup()
    {
        stream = new MemoryStream();
        writer = new PythonIOWriter(stream);

        engine = Python.CreateEngine();
        scope = engine.CreateScope();

        // Add the python library path to the engine. Note that this will
        // not work for builds; you will need to manually place the python
        // library files in a place that your code can find it at runtime.
        var paths = engine.GetSearchPaths();
        paths.Add(Application.dataPath + "/Python/Lib");
        engine.SetSearchPaths(paths);


        engine.Runtime.IO.SetOutput(stream, writer);
        engine.Runtime.IO.SetErrorOutput(stream, writer);

        writer.IORecieved += onIORecieved;

        setupVariables();
    }

    private void setupVariables()
    {
        methods.Add(new PythonMethod("help", "displays useful available methods", py_help));
        methods.Add(new PythonMethod("password", "displays your password", py_password));
        methods.Add(new PythonMethod("uninstall_teams", "teams python api - uninstaller", py_uninstall_teams));
        methods.Add(new PythonMethod("spotify_next", "spotify api - play next song", py_spotify_next));

        foreach (var method in methods)
        {
            scope.SetVariable(method.Name, method.Callback);
        }

        methods.Sort((x, y) => string.Compare(x.Name, y.Name));
    }



    private void onIORecieved(string obj)
    {
        writeMsgToConsole(obj);
    }

    private TMP_Text writeMsgToConsole(string obj)
    {
        var textInstance = Instantiate(textPrefab, content);
        textInstance.text = obj;
        content.sizeDelta = new Vector2(content.sizeDelta.x, content.sizeDelta.y + 10);

        if (content.sizeDelta.y > 125)
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, content.sizeDelta.y - 125);

        return textInstance;
    }

    public void close()
    {
        gameObject.SetActive(false);
    }

    private void py_help()
    {
        writeMsgToConsole("Method summary:");
        foreach (var method in methods)
        {
            writeMsgToConsole($"{method.Name} : {method.HelpMessage}");
        }
    }

    private void py_password()
    {
        writeMsgToConsole(Data.Instance.Password);
    }

    private void py_uninstall_teams()
    {
        if (AnnoyingTeamsManager.Instance && AnnoyingTeamsManager.Instance.IsInstalled)
        {
            AnnoyingTeamsManager.Instance.CloseTeams();
            InstallationHandler.Instance.Install("Uninstalling Teams", teamsSprite, 10, () => writeMsgToConsole("Teams Uninstalled Successfully."), overrideText: true);
        }
        else
        {
            var msg = writeMsgToConsole("Teams not found");
            msg.color = Color.red;
        }
    }

    private void py_spotify_next()
    {
        if (SpotifyManager.Instance)
        {
            SpotifyManager.Instance.NextSong();
            writeMsgToConsole("Spotify - Playing Next Song");
        }
        else
        {
            var msg = writeMsgToConsole("Spotify not found");
            msg.color = Color.red;
        }

    }
}

public class PythonMethod
{
    public string Name;
    public string HelpMessage;
    public object Callback;

    public PythonMethod(string name, string helpMessage, object method)
    {
        Name = name;
        HelpMessage = helpMessage;
        Callback = method;
    }

    public PythonMethod(string name, string helpMessage, System.Action method)
    {
        Name = name;
        HelpMessage = helpMessage;
        Callback = method;
    }
}


public class PythonIOWriter : StreamWriter
{
    public event System.Action<string> IORecieved;

    public PythonIOWriter(Stream s) : base(s)
    {
    }

    public override void Write(string value)
    {
        base.Write(value);

        IORecieved?.Invoke(value);
    }
}