using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Sprite[] Poses;
    public string Pos;
    public int speed;
    bool StartingPos = true;
    Vector3 StartPos, EndPos;
    float StartTime, Length;
    SpriteRenderer SpriteR;

	void Start () {
        SpriteR = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void changePos(string newPos)
    {
        switch(newPos)
        {
            case "L":
                UpdatePos(new Vector3(-500, 0, 0));
                break;
            case "R":
                UpdatePos(new Vector3(500, 0, 0));
                break;
            case "LE":
            case "RE":
            case "C":
                UpdatePos(new Vector3(0, 0, 0));
                break;
            default:
                break;
        }
    }

    void UpdatePos(Vector3 newPos)
    {
        //For instantiation of a character
        if (StartingPos)
        {
            this.transform.position = newPos;
            StartingPos = false;
        }
        else
        {
            StartPos = this.transform.position;
            EndPos = newPos;
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        Length = Vector3.Distance(StartPos, EndPos);
        StartTime = Time.time;
        while(this.transform.position != EndPos)
        {
            float frac = ((Time.time - StartTime) * speed) / Length;
            transform.position = Vector3.Lerp(StartPos, EndPos, frac);
            yield return null;
        }
    }
}
