using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ui_basic_score : MonoBehaviour
{
    private TextMeshProUGUI basic_score;
    public int score = 0;
    float t = 0f;
    private int print_score = 0;
    public bool is_finished = false;
    private GameObject gm;
    gm_stage gm_script;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("manager");
        gm_script = gm.GetComponent<gm_stage>();
        basic_score = GetComponent<TextMeshProUGUI>();
        basic_score.text = "0";

        score = gm_script.basic_score;


        //재시작 되기 전에 스코어 계산에 필요없는 변수 초기화
        gm_script.coin_count = 0;
        gm_script.spawn_counting = 0;
        for (int i = 0; i < 4; i++)
        {
            gm_script.b_check[i] = false;
            gm_script.boss_spawn[i] = false;
        }
        gm_script.final_boss_spawn = false;
        gm_script.fb_check = false;
        gm_script.t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (print_score <= score)
        {
            if (t > 0.0001f)
            {
                print_score++;
                basic_score.text = print_score.ToString();
                t = 0f;
            }
        }
        else
        {
            is_finished = true;
        }
    }
}
