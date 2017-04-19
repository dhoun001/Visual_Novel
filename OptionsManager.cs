using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour {
    SaveFile file;
	//This script will do all the of legwork of the save file, the different ways we would write to it
    //and how we will interperate what the file will read
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetEmotions()
    {
        GameManager.Instance.emotions = new Emotion(0, 0, 0);
    }

    
}
