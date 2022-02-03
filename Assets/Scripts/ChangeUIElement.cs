using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUIElement : MonoBehaviour
{
    [SerializeField] Image actionImageUI, fileImageUI;
    [SerializeField] TMP_Text textUI;

    public void Init(string text, Sprite actionSprite, Sprite fileSprite)
    {
        actionImageUI.sprite = actionSprite;
        fileImageUI.sprite = fileSprite;

        textUI.text = text;
    }
}
