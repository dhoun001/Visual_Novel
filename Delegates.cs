using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delegates : MonoBehaviour {

    public delegate void UpdateUi();
    public UpdateUi UIUpdate;
    public Text hp;
    public Text amr;


	// Use this for initialization
	void Start () {
        UIUpdate += UpdateHealth;
        UIUpdate += UpdateArmor;

        UIUpdate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateHealth()
    {
        hp.text = "Updated";
    }
    void UpdateArmor()
    {
        amr.text = "Updated";
    }
}
