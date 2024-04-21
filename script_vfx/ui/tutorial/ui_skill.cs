using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_skill : MonoBehaviour
{
    SpriteRenderer sr_color;
    public SpriteRenderer sp_button_bg;
    float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        sr_color = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if(t < 0.7f)
        {
            sr_color.color = Color.red;
            sp_button_bg.color = Color.red;
        }
        else if(t < 1.4f)
        {
            sr_color.color = Color.white;
            sp_button_bg.color = new Color(0.2705882f, 0.6313726f, 0.8705883f, 1f);
        }
        else
        {
            t = 0f;
        }
    }
}
