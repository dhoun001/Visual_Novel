using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScene : MonoBehaviour {
    public Texture2D FadeBackground;

    [SerializeField]
    private float fadeSpeed = 0.8f;

    private float alpha = 1.0f;
    private int drawDepth = -1000;
    private int fadeDir = -1;

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeBackground);
    }

    public float SetFade(int Dir)
    {
        fadeDir = Dir;
        return fadeSpeed;
    }
}
