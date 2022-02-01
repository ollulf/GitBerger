using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommitListUIElement : MonoBehaviour
{
    [SerializeField] Sprite branchFirst, branchMiddle, branchLast, userPlayer, userBerger;

    [SerializeField] Image branchIconImage, profileIconImage;
    [SerializeField] RectTransform localIndicator, originIndicator;
    [SerializeField] TMP_Text messageTextUI, profileNameTextUI, timeDateTextUI;

    public void Display(Commit data, CommitListItemPosition position)
    {
        branchIconImage.sprite = GetSpriteForPosition(position);
        Display(data);
    }

    public void Display(Commit data)
    {
        profileIconImage.sprite = GetSpriteForAuthor(data.Author);

        localIndicator.gameObject.SetActive(data.State == Commit.States.Local);
        originIndicator.gameObject.SetActive(data.State == Commit.States.Origin);

        messageTextUI.text = data.Message;

        Color textColor = data.State != Commit.States.Origin ? Color.white : Color.gray;
        messageTextUI.color = textColor;
        profileNameTextUI.color = textColor;
        timeDateTextUI.color = textColor;

        profileNameTextUI.text = GetAuthorNameFromAuthor(data.Author);
        timeDateTextUI.text = data.DateTime.ToString("MMM d") + " " + data.DateTime.ToString("T");
    }

    private string GetAuthorNameFromAuthor(Commit.Authors author)
    {
        return author == Commit.Authors.Player ? "Max Bredlau" : "Herr Berger";
    }

    private Sprite GetSpriteForAuthor(Commit.Authors author)
    {
        return author == Commit.Authors.Berger ? userBerger : userPlayer;
    }

    private Sprite GetSpriteForPosition(CommitListItemPosition position)
    {
        if (position == CommitListItemPosition.Middle)
            return branchMiddle;
        else
            return position == CommitListItemPosition.First ? branchFirst : branchLast;
    }


}

public enum CommitListItemPosition
{
    First,
    Middle,
    Last
}
