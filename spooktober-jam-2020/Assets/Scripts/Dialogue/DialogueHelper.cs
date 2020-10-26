using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class DialogueHelper
{
    static GameObject CurrentNPC;
    static bool PlayerIsAbleToOpenDialogue;
    static bool PlayerHasInitiatedDialogue;

    // invoked by NPCs
    #region NPCInvocations

    
    public static void NPCDialogueTriggerFired(GameObject source)
    {
        if (CurrentNPC == null)
        {
            CurrentNPC = source;
            PlayerIsAbleToOpenDialogue = true;
        }
    }

    public static void NPCDialogueTriggerLeft(GameObject source)
    {
        if (CurrentNPC == source)
        {
            CurrentNPC = null;
            PlayerIsAbleToOpenDialogue = false;
        }
    }

    #endregion

    // invoked by player input
    #region PlayerInvocations
    
    public static void NPCDialogueInitiated()
    {
        if (PlayerIsAbleToOpenDialogue)
        {
            PlayerHasInitiatedDialogue = true;
        }
    }


    #endregion

    public static bool GetPlayerHasInitiatedDialogue()
    {
        return PlayerHasInitiatedDialogue;
    }

    public static GameObject GetCurrentNPC()
    {
        return CurrentNPC;
    }

    public static void SetPlayerHasIntiatedDialogue(object sender, bool value)
    {
        if (sender is DialogueManager)
        {
            PlayerHasInitiatedDialogue = value;
        }
        else
        {
            throw new System.Exception("Only Dialogue Manager Can Set Values Within The Dialogue Helper");
        }
    }

}
