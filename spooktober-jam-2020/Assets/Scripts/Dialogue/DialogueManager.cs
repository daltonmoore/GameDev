using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public event EventHandler<StartDialogueEventArgs> StartDialogue;

    public DialogueManager dialogueManager { get { return this; } }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (DialogueHelper.GetPlayerHasInitiatedDialogue())
        {
            DialogueHelper.SetPlayerHasIntiatedDialogue(this, false);
            StartDialogueEventArgs args = new StartDialogueEventArgs();
            args.CurrentNPC = DialogueHelper.GetCurrentNPC();
            OnStartDialogue(args);
        }
    }

    protected virtual void OnStartDialogue(StartDialogueEventArgs e)
    {
        EventHandler<StartDialogueEventArgs> handler = StartDialogue;
        handler?.Invoke(this, e);
    }

    

}

public class StartDialogueEventArgs : EventArgs
{
    public GameObject CurrentNPC { get; set; }
}