using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager> {

    public AudioClip[] songList;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
    public void CheckScene()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == "Scene1")
        {
            this.GetComponent<AudioSource>().clip = songList[1];
            this.GetComponent<AudioSource>().Play();
        }
        if (SceneManager.GetActiveScene().name == "Scene4")
        {
            this.GetComponent<AudioSource>().clip = songList[0];
            this.GetComponent<AudioSource>().Play();
        }
    }
}
