using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float starting_time = 60;
    float current_time;
    bool counting;

    public float current_alpha;
    public float alpha_reduction_factor;

    void Start()
    {
        Restart();
    }

    void Restart()
    {
        counting = true;
        current_time = starting_time;
        current_alpha = 1;
        GetComponent<Text>().text = starting_time.ToString("00.");
    }

    void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            counting = false;
            Debug.Log(current_time);
        }

        if (counting)
        {
            current_time -= Time.deltaTime;
            GetComponent<Text>().text = current_time.ToString("00.");

            current_alpha = Mathf.Clamp01(current_alpha - alpha_reduction_factor);
            GetComponent<Text>().color = new Color(0, 0, 0, current_alpha);
        }
    }
}
