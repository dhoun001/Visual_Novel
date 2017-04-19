using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Emotion
{
    public int Happy, Sad, Angry;
    
    public Emotion(int A, int B, int C)
    {
        Happy = A;
        Sad = B;
        Angry = C;
    }
}

public class GameManager : Singleton<GameManager> {
    public Emotion emotions;
    //Number of different ways we can switch the scene = the size of the array
    //20 is just a temp number
    private void Start()
    {
        emotions = new Emotion();   
    }
    public void Events(string Type,int arg1, string arg2)
    {
        Debug.Log(Type);
        switch (Type)
        {
            case "Choice":
                string[] args = arg2.Split(',');
                CreateButton(arg1, args);
                break;
            case "Character":
                string[] temp = arg2.Split(',');
                CreateCharacter(temp[0], temp[1]);
                break;
            case "Scene":
                StartCoroutine(ChangeScenes(arg2));
                break;
            case "Emotion":
                UpdateEmotions(arg2, arg1);
                break;
            default:
                break;

        }
    }
    private void CreateButton(int arg1, string[] args)
    {
        Transform parent = Canvas.Instance.transform;
        foreach (Transform x in parent)
        {
            if (x.tag.Equals("Dialogue"))
            {
                x.gameObject.SetActive(false);
            }
        }
        GameObject Father = new GameObject();
        Father.transform.parent = parent;
        Father.transform.localScale = new Vector3(1, 1, 1);
        int index = 0;
        foreach (string x in args)
        {
            try
            {
                string[] temp = x.Split('&');
                GameObject Choices = Instantiate(Resources.Load("Prefabs/ChoiceButton")) as GameObject;
                Choices.transform.parent = Father.transform;
                Choices.transform.GetComponent<ChoiceButtons>().SetUp(temp[0], temp[1], int.Parse(temp[2]));
                Choices.transform.localScale = new Vector3(1, 1, 1);
                float Spacing = Screen.height / (args.Length * 2);
                Choices.transform.localPosition = new Vector3(0, 0 + (Spacing * Mathf.Floor((index+1)/2) * Mathf.Pow(-1, index)));
                index++;

            }
            catch
            {

            }

        }
    }
    private void CreateCharacter(string Name, string Pos)
    {
        GameObject Char = Instantiate(Resources.Load("Prefabs/" + Name)) as GameObject;
        Char.transform.parent = Canvas.Instance.transform;
        Character character = Char.GetComponent<Character>();
        character.changePos(Pos);
    }
    IEnumerator ChangeScenes(string arg)
    {
        float wait = this.gameObject.GetComponent<FadeScene>().SetFade(1);
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(arg);
        AudioManager.Instance.CheckScene();
        yield return null;
    }
    private void UpdateEmotions(string type, int val)
    {
        Debug.Log("Updating " + type + " by " + val);
    }
    
}
