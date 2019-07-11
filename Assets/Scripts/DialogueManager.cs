using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Countdown countdown;

    public TextMeshProUGUI text_field;
    public GameObject text_box;
    List<string> text_subsections = new List<string>();
    bool subsection_done = false;
    bool dialogue_active = false;

    void Start()
    {
        string text = System.IO.File.ReadAllText(Application.dataPath + "/StreamingAssets/chapter1.txt");
        string[] text_split = text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        text_field.text = text_split[0].Trim();
        for (int i = 1; i < text_split.Length; i++)
        {
            string current_text = text_split[i].Trim();
            if (current_text != "")
            {
                text_subsections.Add(text_split[i]);
            }
        }

        StartCoroutine(RevealCharacters());
    }

    IEnumerator RevealCharacters()
    {
        dialogue_active = true;

        text_field.ForceMeshUpdate();

        TMP_TextInfo textInfo = text_field.textInfo;

        int totalVisibleCharacters = textInfo.characterCount; 
        int visibleCount = 0;

        while (true)
        {
            if (visibleCount > totalVisibleCharacters)
            {
                subsection_done = true;
                yield break;
            }

            text_field.maxVisibleCharacters = visibleCount;

            visibleCount += 1;

            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update()
    {
        if (dialogue_active && subsection_done && Input.anyKeyDown)
        {
            if (text_subsections.Count > 0)
            {
                text_field.text = text_subsections[0];
                text_subsections.RemoveAt(0);

                subsection_done = false;
                StartCoroutine(RevealCharacters());
            }
            else
            {
                dialogue_active = false;
                text_box.SetActive(false);
                countdown.Restart();
            }
        }
    }
}
