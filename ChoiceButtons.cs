using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButtons : MonoBehaviour {
    string Key2;
    string Value;
    int Key;

	public void SetUp(string argChoice, string argValue, int argKey)
    {
        Debug.Log("Settingup");
        Text choice = GetComponentInChildren<Text>();
        choice.text = argChoice;
        if (argValue.Contains("Emotion"))
        {
            Key2 = argValue.Substring(7);
            Value = "Emotion";
            Key = argKey;
        }
        else if (argValue.Contains("Scene"))
        {
            Value = argValue;
            Key2 = "Scene" + argKey;
        }
        else
        {
            Value = argValue;
            Key = argKey;
        }
    }
    public void WhenClicked()
    {
        Debug.Log(Value);
        GameManager.Instance.Events(Value, Key, Key2);
        Transform parent = Canvas.Instance.transform;
        foreach (Transform x in parent)
        {
            if (x.tag.Equals("Dialogue"))
            {
                x.gameObject.SetActive(true);
            }
        }
        Destroy(transform.parent.gameObject);
    }

}
