using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DialogueParser : Singleton<DialogueParser>
{

    struct DialogueLine
    {
        public string name, dialogue, position;
        public int pose;

        // Struct Create w/ Options
        public DialogueLine(string Name, string Dialogue, int Pose, string Position)
        {
            name = Name;
            dialogue = Dialogue;
            pose = Pose;
            position = Position;
        }
    }

    // List of Dialogue Lines
    List<DialogueLine> lines;

    // List of Checkpoints
    // Used for Option Buttons
    // List<int> checkpoints;

    // Use this for initialization
    void Start()
    {
        // Gets a Dialogue.txt file from Assets/Scenes/SceneName/'SceneName'Dialogue.txt
        string dialogueFile = "Assets/Scenes/";
        //string eventsFile = "Assets/Scenes/";
        string sceneNum = SceneManager.GetActiveScene().name;
        dialogueFile += sceneNum;
        dialogueFile += "/" + sceneNum + "Dialogue.txt";
        //eventsFile += sceneNum;
        //eventsFile += "/" + sceneNum + "Events.txt";

        lines = new List<DialogueLine>();
        // checkpoints = new List<int>();

        LoadDialogue(dialogueFile);
        // LoadEvents(eventsFile);
        // ScanCheckpoints();
        // Debug.Log(checkpoints.Count);
    }

    // Reads Dialogue from a txt.file
    // Reads each line until the end of the file
    // Each line is properly read like this: Name;Dialogue;Pose;Position
    private void LoadDialogue(string filename)
    {
        string line;
        string[] lineData;
        // string[] options;
        StreamReader r = new StreamReader(filename);
        int lineNum = 0;

        using (r)
        {
            while (!r.EndOfStream)
            {
                line = r.ReadLine();
                if (line != null)
                {
                    lineData = line.Split(';');

                    // Makes sure that there are 4 items
                    // Basic Dialogue Line
                    // Name;Dialogue;Pose;Position
                    if (lineData.Length == 4)
                    {
                        lineNum++;
                        DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1], int.Parse(lineData[2]), lineData[3]);
                        lines.Add(lineEntry);
                    }


                    else
                    {
                        //input error here
                    }
                }
            }
            r.Close();
        }
    }


    // Use these functions to get the DialogueLine variables
    public string GetPosition(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].position;
        }
        return "";
    }

    public string GetName(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].name;
        }
        return "";
    }

    public string GetDialogue(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].dialogue;
        }
        return "";
    }

    public int GetPose(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].pose;
        }
        return 0;
    }
}