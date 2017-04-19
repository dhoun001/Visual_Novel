using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    // Use this for initialization
    public Text DialogueText;
    public Text NameDisplay;
    int line;
	void Start () {
		
	}
	
	void Update () {
        DialogueText.text = TextParser.Instance.GetDisplayDialogue();
	}
    //If we make a physics colider for when they click it will call this function
    //If Darren wants to be able to click items for flavor text(idk what his vision with that is) then we can limit this to the text box without having to rewrite code
    public void OnDialogueClick()
    {
        if (!TextParser.Instance.CheckIfParsing())
        {
            if (DialogueParser.Instance.GetName(line).Equals("/Event"))
            {
                //This is the current work around for triggering events, the Dialogue section is the type of event, and the pose is the args
                GameManager.Instance.Events(DialogueParser.Instance.GetDialogue(line),DialogueParser.Instance.GetPose(line), DialogueParser.Instance.GetPosition(line));
            }
            else
            {
                if (DialogueText.text == "")
                {
                    Transform parent = Canvas.Instance.transform;
                    foreach (Transform x in parent)
                    {
                        if (x.tag.Equals("Dialogue"))
                        {
                            parent = x;
                            foreach (Transform y in parent)
                            {
                                y.gameObject.SetActive(true);
                            }
                        }
                    }
                }
                if(DialogueParser.Instance.GetPosition(line) == "I")
                {
                    DialogueText.fontStyle = FontStyle.Italic;
                }
                else
                {
                    DialogueText.fontStyle = FontStyle.Normal;
                }
                NameDisplay.text = DialogueParser.Instance.GetName(line);
                TextParser.Instance.SetDialogue(DialogueParser.Instance.GetDialogue(line));
                //Using the public one I made
                TextParser.Instance.CharDialogueParser();
            }
            line++;
        }
        else
        {
            TextParser.Instance.SkipParse(); 
        }
    }
}
