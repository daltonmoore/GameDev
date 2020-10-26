using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public CanvasManager canvasManager { get { return this; } }

    const string OpenDialogueGameObjectName = "Text - OpenDialogue";

    Dictionary<string, Text> textChildren = new Dictionary<string, Text>();
    DialogueManager dialogueManager;

    private void Start()
    {
        Text[] childrenWithText = GetComponentsInChildren<Text>();
        foreach (Text child in childrenWithText)
        {
            textChildren.Add(child.gameObject.name, child);
        }

        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        dialogueManager.StartDialogue += DialogueManager_StartDialogue;
    }

    private void DialogueManager_StartDialogue(object sender, StartDialogueEventArgs e)
    {
        Text text; 
        textChildren.TryGetValue(OpenDialogueGameObjectName, out text);
        text.gameObject.SetActive(false);

        Debug.Log(text.gameObject.name);
        Debug.Log(sender);
    }
}
