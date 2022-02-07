using UnityEngine;

public class InstallationHandler : SingletonBehaviour<InstallationHandler>
{
    [SerializeField]
    InstallBar prefab;

    [SerializeField]
    Transform parent;

    public void Install(string _name, Sprite _icon, float _duration, System.Action _onCompleted, bool overrideText =false)
    {
        var instance = Instantiate(prefab, parent);
        instance.Init(_name, _icon, _duration, _onCompleted, overrideText);
    }
}