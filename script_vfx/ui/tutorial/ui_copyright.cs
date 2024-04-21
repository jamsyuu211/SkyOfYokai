using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ui_copyright : MonoBehaviour
{
    private TextMeshProUGUI text;
    float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();  
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t < 0.5f)
        {
            text.text = "C";
        }
        else if (t < 1f)
        {
            text.text = "CO";
        }
        else if (t < 1.5f)
        {
            text.text = "COP";
        }
        else if(t < 2f)
        {
            text.text = "COPY";
        }
        else if (t < 2.5f)
        {
            text.text = "COPY\nR";
        }
        else if (t < 3f)
        {
            text.text = "COPY\nRI";
        }
        else if (t < 3.5f)
        {
            text.text = "COPY\nRIG";
        }
        else if (t < 4f)
        {
            text.text = "COPY\nRIGH";
        }
        else if (t < 4.5f)
        {
            text.text = "COPY\nRIGHT";
        }
        else if(t < 5f)
        {
            text.text = ">.<";
        }
        else
        {
            t = 0f;
        }
    }
}
