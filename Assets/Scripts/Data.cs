using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : SingletonBehaviour<Data>
{
    public string PlayerName = "Player";
    public string Password = "";
    public bool NeedsGitUpdate = false;
    public bool HasGitUpdate = false;
}
