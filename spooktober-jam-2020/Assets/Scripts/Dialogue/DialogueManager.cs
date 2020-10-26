using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// probaly could just remove this altogether and use the DialogueHelper with events
public class DialogueManager : MonoBehaviour
{
    int currentPage = 0;
    int endPage = 0;
    CanvasManager canvasManager;

    private void Start()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();

        // subscribe to events
        #region EventSubscriptions

        canvasManager.AdvanceDialogue += CanvasManager_OnAdvanceDialogue;
        DialogueHelper.PlayerInitiatedDialogue += DialogueHelper_OnPlayerInitiatedDialogue;

        #endregion
    }

    private void CanvasManager_OnAdvanceDialogue(object sender, EventArgs e)
    {
        currentPage++;
        switch (DialogueHelper.CurrentNPC.name)
        {
            case NPCConstants.Murdoc.name:
                switch (currentPage)
                {
                    case 1:
                        canvasManager.SetDialogueBoxText("There has been quite a bit of trouble here recently.");
                        break;
                    case 2:
                        canvasManager.SetDialogueBoxText("Monsters have ransacked the town and made off with our precious relics.");
                        break;
                    case 3:
                        canvasManager.SetDialogueBoxText("Town is up north of here. East are those horrible plant creatures." +
                            " West are the undead. And South there are some bandits.");
                        break;
                    case 4:
                        canvasManager.SetDialogueBoxText("Each of those factions made off with a piece of our most prestigious relic." +
                            " The Mask of Truth.");
                        break;
                    case 5:
                        canvasManager.SetDialogueBoxText("Please retrieve all three pieces and restore Murdurth's glory.");
                        break;
                }
                if(currentPage > NPCConstants.Murdoc.endPage)
                {
                    canvasManager.CloseDialogueBox();
                }
                break;
        }
    }

    private void DialogueHelper_OnPlayerInitiatedDialogue(object sender, DialogueEventArgs e)
    {
        switch (e.CurrentNPC.name)
        {
            case NPCConstants.Murdoc.name:
                canvasManager.SetDialogueBoxText("Welcome to Murdurth, traveller.");
                endPage = NPCConstants.Murdoc.endPage;
                break;
        }
    }
}
