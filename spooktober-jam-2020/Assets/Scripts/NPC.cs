using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogueHelper.NPCDialogueTriggerFired(gameObject);
            Debug.Log("Player Inside Dialogue Trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogueHelper.NPCDialogueTriggerLeft(gameObject);
            Debug.Log("Player Left Dialogue Trigger");
        }
    }
}
