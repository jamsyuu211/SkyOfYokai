using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ui_text : MonoBehaviour
{
    TextMeshProUGUI text;
    float t = 0f;
    public float change_speed;
    Color ori_color;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ori_color = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        t += change_speed * Time.deltaTime;
        if (t < 0.5f)
        {
            text.text = "C";
        }
        else if (t < 1f)
        {
            text.text = "Cl";
        }
        else if (t < 1.5f)
        {
            text.text = "Cli";
        }
        else if (t < 2f)
        {
            text.text = "Clic";
        }
        else if (t < 2.5f)
        {
            text.text = "Click";
        }
        else if (t < 3f)
        {
            text.text = "Click ";
        }
        else if (t < 3.5f)
        {
            text.text = "Click t";
        }
        else if (t < 4f)
        {
            text.text = "Click th";
        }
        else if (t < 4.5f)
        {
            text.text = "Click the";
        }
        else if (t < 5f)
        {
            text.text = "Click the ";
        }
        else if (t < 5.5f)
        {
            text.text = "Click the i";
        }
        else if (t < 6f)
        {
            text.text = "Click the ic";
        }
        else if (t < 6.5f)
        {
            text.text = "Click the ico";
        }
        else if (t < 7f)
        {
            text.text = "Click the icon";
        }
        else if (t < 7.5f)
        {
            text.text = "Click the icon ";
        }
        else if (t < 8f)
        {
            text.text = "Click the icon :";
        }
        else if (t < 8.5f)
        {
            text.text = "Click the icon :)";
        }
        else if(t < 12f)
        { 
            t = 0f;
        }
    }
}
