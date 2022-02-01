using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommitMessageOptionUIElement : MonoBehaviour
{
    [SerializeField] TMP_Text textUI;
    [SerializeField] Button buttonUI;

    CommitMessageTextComponent associated;

    public void Init(CommitMessageTextComponent text)
    {
        associated = text;
        textUI.text = text.Text;
        buttonUI.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        CommitMessageComposer.Instance.AddToMessage(associated);
    }
}
