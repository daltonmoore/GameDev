using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public event EventHandler AdvanceDialogue;

    #region Children
    const string OpenDialogueText = "Text - OpenDialogue";
    const string DialogueBox = "Img - DialogueBox";
    const string DialogueBoxText = "Text - DialogueText";
    #endregion

    Dictionary<string, GameObject> children = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject.name, child.gameObject);
            child.gameObject.SetActive(false);
        }

        // subscribing to events
        DialogueHelper.PlayerOpenDialogueToolTipShown += DialogueHelper_PlayerOpenDialogueToolTipShown;
        DialogueHelper.PlayerInitiatedDialogue += DialogueManager_StartDialogue;
        DialogueHelper.PlayerHasLeft += DialogueHelper_PlayerHasLeft;
    }

    private void DialogueHelper_PlayerHasLeft(object sender, EventArgs e)
    {
        SetChildActive(false, OpenDialogueText);
        SetChildActive(false, DialogueBox);
    }

    private void DialogueHelper_PlayerOpenDialogueToolTipShown(object sender, EventArgs e)
    {
        SetChildActive(true, OpenDialogueText);
    }

    private void DialogueManager_StartDialogue(object sender, EventArgs e)
    {
        // turns off tool-tip for opening dialogue
        SetChildActive(false, OpenDialogueText);

        // open dialogue popup window
        SetChildActive(true, DialogueBox);
    }

    public void OnClickLeaveDialogueButton()
    {
        SetChildActive(false, DialogueBox);
    }

    public void OnClickAdvanceDialogueButton()
    {
        AdvanceDialogue.Invoke(this, new EventArgs());
    }

    public void SetDialogueBoxText(string text)
    {
        GameObject gameObject = null;
        if (children.TryGetValue(DialogueBoxText, out gameObject))
        {
            gameObject.GetComponent<Text>().text = text;
        }
        else
        {
            GameObject dialoguebox = GetChild(DialogueBox);
            Text dialoguetext = dialoguebox.GetComponentInChildren<Text>();
            children.Add(DialogueBoxText, dialoguetext.gameObject);
            dialoguetext.text = text;
        }
    }

    public void CloseDialogueBox()
    {
        SetChildActive(false, DialogueBox);
    }

    #region HelperMethods

    void SetChildActive(bool active, string name)
    {
        GameObject child = GetChild(name);
        child.SetActive(active);
    }

    GameObject GetChild(string name)
    {
        GameObject child;
        children.TryGetValue(name, out child);
        return child;
    }

    #endregion
}
