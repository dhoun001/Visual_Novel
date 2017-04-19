using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextParser : Singleton<TextParser> {

    // ParseSpeed is the amount of characters parsed each Update()
    // If 0, it parses immediately
    // ParseBuffer is the counter that ParseSpeed adds to
    public float parseSpeed;

    // DisplayDialogue is what goes to the gameobject.text
    // CompleteDialogue is the entire dialogue
    private string displayDialogue;
    private string completeDialogue;

    //To check when the parse is done
    private bool IsParsing = false;

	// Use this for initialization
	void Start () {
        displayDialogue = "";
        completeDialogue = "";
		
	}
	

     /* This is an alternitive to the function you made that should work without using the update function
     * (After going over some logic this needs a few checks and bug tests to work 100% properly)
     * It may or may not work as I havent been able to test out this script but it's just some food for thought
     * at the very least it should give you some insight on how Coroutines work
     */
    public void CharDialogueParser()
    {
        if(parseSpeed == 0.0)
        {
            IsParsing = false;
            displayDialogue = completeDialogue;
        }
        else
        {
            IsParsing = true;
            StartCoroutine(TextParse());
        }
    }
    //The Coroutine (Why it returns IEnumerator idk)
    IEnumerator TextParse()
    {
        //Where parseSpeed = number of seconds inbetween
        while (displayDialogue.Length < completeDialogue.Length)
        {
            displayDialogue = completeDialogue.Substring
                (0, displayDialogue.Length + 1);
            yield return new WaitForSeconds(parseSpeed);
        }
        IsParsing = false;
    }
    

    // Functions to the parsespeed and completedialogue
    public void SetParseSpeed(float value)
    {
        parseSpeed = value;
    }

    public void SetDialogue(string value)
    {
        completeDialogue = value;
        displayDialogue = "";
    }
    public string GetDisplayDialogue()
    {
        return displayDialogue;
    }
    public bool CheckIfParsing()
    {
        return IsParsing;
    }
    public void SkipParse()
    {
        displayDialogue = completeDialogue;
        IsParsing = false;
    }

}
