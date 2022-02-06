using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangesHandler : SingletonBehaviour<ChangesHandler>
{
    [SerializeField] Sprite[] actionIcons;
    [SerializeField] Sprite fileIcon;

    [SerializeField] ChangeUIElement changePrefab;
    [SerializeField] GameObject loadingCircle;

    string[] changeFileNames;

    private void Start()
    {
        changeFileNames = new string[]
        {
            "Assets/NewFolder/NewBehaviourScript.cs",
            "Assets/NewBehaviourScript.cs",
            "Assets/Sprites/PlayerController.cs",
            "Assets/Scripts/AnnoyingTeamsManager.cs",
            "Assets/Scripts/Infrastructure/InteractionHandler.cs",
            "Assets/Prefabs/Elements/Bed.prefab",
            "Assets/Prefabs/Mobs/Creeper.prefab",
            "Assets/NewFolder/Herobrine.prefab",
            "Assets/NewFolder/Entitys/NPC.cs",
            "Assets/Sprites/Icons.png",
            "Assets/NewFolder/NewBehaviourScript1.cs",
            "Assets/Scripts/Handlers/NPCHandler.cs",
            "Assets/Sprites/Player/NiceFace.png",
            "Assets/Meshes/Sheep.fbx",
            "Assets/Meshes/Creeper.fbx",
            "Assets/Resources/Datatables/blocks.json",
            "Assets/Resources/Colors/playerMat.asset",
            "Assets/ScriptableObjects/DiamondPickaxe.asset",
            "Assets/Csongor.png",
            "Assets/NetworkingManager.cs",
            "Assets/final.png",
            "Assets/Sprites/familyphoto.jpg",
        };
    }

    public void AddNewChanges(System.Action onFirstChanges)
    {
        StartCoroutine(AddChangesRoutine(onFirstChanges));
    }

    private IEnumerator AddChangesRoutine(System.Action onFirstChanges)
    {
        yield return new WaitForSeconds(0.5f);
        onFirstChanges?.Invoke();
        loadingCircle.SetActive(true);
        for (int i = 0; i < UnityEngine.Random.Range(2,5); i++)
        {
            Instantiate(changePrefab, transform).Init(
                changeFileNames[UnityEngine.Random.Range(0, changeFileNames.Length)],
                actionIcons[UnityEngine.Random.Range(0, actionIcons.Length)],
                fileIcon
                );
            yield return new WaitForSeconds(0.25f);
        }
        loadingCircle.SetActive(false);
    }

    public void ClearChanges()
    {
        transform.DestroyAllChildren();
    }
}
