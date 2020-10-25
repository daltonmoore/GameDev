using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueHelper
{
    static GameObject CurrentNPC;
    static bool PlayerIsAbleToOpenDialogue;


    public static void NPCDialogueTriggerFired(GameObject source)
    {
        if (CurrentNPC == null)
        {
            CurrentNPC = source;
            PlayerIsAbleToOpenDialogue = true;
        }
    }

}
