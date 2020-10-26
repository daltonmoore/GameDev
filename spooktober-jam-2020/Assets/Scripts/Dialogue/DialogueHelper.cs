using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class DialogueHelper
{
    public static event EventHandler PlayerOpenDialogueToolTipShown;
    public static event EventHandler<DialogueEventArgs> PlayerInitiatedDialogue;
    public static event EventHandler PlayerHasLeft;
    public static GameObject CurrentNPC { get; set; }
    public static bool PlayerIsAbleToOpenDialogue { get; set; }

    // invoked by NPCs
    #region NPCInvocations
    
    public static void NPCDialogueTriggerFired(GameObject source)
    {
        if (CurrentNPC == null)
        {
            CurrentNPC = source;
            PlayerIsAbleToOpenDialogue = true;
            PlayerOpenDialogueToolTipShown.Invoke(source, new EventArgs());
        }
    }

    public static void NPCDialogueTriggerLeft(GameObject source)
    {
        if (CurrentNPC == source)
        {
            CurrentNPC = null;
            PlayerIsAbleToOpenDialogue = false;
            PlayerHasLeft.Invoke(source, new EventArgs());
        }
    }

    #endregion

    // invoked by player input
    #region PlayerInvocations
    
    public static void NPCDialogueInitiated()
    {
        if (PlayerIsAbleToOpenDialogue)
        {
            DialogueEventArgs args = new DialogueEventArgs();
            args.CurrentNPC = CurrentNPC;
            PlayerInitiatedDialogue.Invoke(CurrentNPC, args);
        }
    }

    #endregion

}

public class DialogueEventArgs : EventArgs
{
    public GameObject CurrentNPC;
}