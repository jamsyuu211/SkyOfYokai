using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_total_score : MonoBehaviour
{
    private TextMeshProUGUI total_score;
    public int score = 0;
    private int print_score = 0;
    float t = 0f;
    public TextMeshProUGUI basic_score;
    ui_basic_score bs_script;
    private GameObject gm;
    gm_stage gm_script;
    public bool is_finished_print_score = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("manager");
        gm_script = gm.GetComponent<gm_stage>();
        bs_script = basic_score.GetComponent<ui_basic_score>();
        total_score = GetComponent<TextMeshProUGUI>();
        total_score.text = "0";
        score = gm_script.basic_score + gm_script.total_score;
    }

    // Update is called once per frame
    void Update()
    {
        if (bs_script.is_finished)
        {
            t += Time.deltaTime;
            if (print_score <= score)
            {
                if (t > 0.00006f)
                {
                    print_score++;
                    total_score.text = print_score.ToString();
                    t = 0f;
                }
            }
            else
            {
                Destroy(gm);
                is_finished_print_score = true;
                gm_script.total_score = 0;
                gm_script.basic_score = 0;
            }
        }
    }
}
