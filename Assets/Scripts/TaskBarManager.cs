using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBarManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenExplorer()
    {
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe");
        p.StartInfo.Arguments = "/select";
        p.Start();
    }

    public void OpenMinetestWebsite()
    {
        Application.OpenURL("http://www.minetest.net/");
    }

    public void OpenSteamWebsite()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=QJW_ML5aE9E");
    }

}