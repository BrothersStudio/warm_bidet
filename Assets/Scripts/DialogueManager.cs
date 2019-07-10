using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text_field;

    void Start()
    {
        string text = System.IO.File.ReadAllText(Application.dataPath + "/StreamingAssets/chapter1.txt");
        string[] text_split = text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in text_split)
        {
            if (line != "")
            {
                text_field.text = line;
            }
        }

        StartCoroutine(RevealCharacters());
    }

    IEnumerator RevealCharacters()
    {
        text_field.ForceMeshUpdate();

        TMP_TextInfo textInfo = text_field.textInfo;

        int totalVisibleCharacters = textInfo.characterCount; 
        int visibleCount = 0;

        while (true)
        {
            text_field.maxVisibleCharacters = visibleCount;

            visibleCount += 1;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
