using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ui_score : MonoBehaviour
{
    TextMeshProUGUI text;
    float t = 0f;
    public float change_speed;
    int print_score = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        t += change_speed * Time.deltaTime;
        if(t > 0.02f)
        {
            t = 0f;
            if (print_score > 99999)
            {
                print_score = 0;
            }
            else
            {
                print_score++;
            }
            text.text = print_score.ToString();
        }
    }
}
