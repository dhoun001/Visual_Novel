using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable {
    bool exitClick = false;
    public string Description;

    private void OnMouseDown()
    {
        Debug.Log("Click");
        Interact();
    }

    public override void Interact()
    {
        if (!TextParser.Instance.CheckIfParsing()) {
            Debug.Log("A");
            Transform parent = Canvas.Instance.transform;
            foreach (Transform x in parent)
            {
                if (x.tag.Equals("Dialogue"))
                {
                    parent = x;
                    Debug.Log("B");
                    foreach (Transform y in parent)
                    {
                        if (y.tag.Equals("DialogueBox"))
                        {
                            y.gameObject.SetActive(!exitClick);
                            Debug.Log("C");
                        }
                    }
                    x.gameObject.SetActive(!exitClick);
                }
            }
            if (!exitClick)
            {
                this.GetComponent<BoxCollider2D>().transform.localScale = new Vector2(999, 999);
                TextParser.Instance.SetDialogue(Description);
                TextParser.Instance.CharDialogueParser();
                exitClick = true;
            }
        }
    }
}
