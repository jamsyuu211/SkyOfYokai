using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_loading : MonoBehaviour
{
    float loading_t = 0f;
    float text_t = 0f;
    int dot_count = 0;
    Vector3 rotation;
    public TextMeshProUGUI loading_text;
    // Start is called before the first frame update
    void Start()
    {
        rotation = new Vector3(0f, 0f, -180);
    }

    // Update is called once per frame
    void Update()
    {
        loading_t += Time.deltaTime;
        text_t += Time.deltaTime;
        transform.Rotate(rotation * Time.deltaTime);
        if(text_t > 0.3f)
        {
            text_t = 0f;
            if(dot_count == 0)
            {
                loading_text.text = ".";
            }
            else if(dot_count == 1)
            {
                loading_text.text = "..";
            }
            else if(dot_count == 2)
            {
                loading_text.text = "...";
            }
            else if(dot_count == 3)
            {
                dot_count = -1;
            }
            dot_count++;
        }
        if(loading_t > 2f)
        {
            loading_t = 0f;
            SceneManager.LoadScene("plane_shooting");//게임시작창으로 변경
        }
    }
}
