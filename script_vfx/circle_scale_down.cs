using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class circle_scale_down : MonoBehaviour
{
    float maintain = 0f;
    SpriteRenderer sr;
    public SpriteRenderer bg;
    float t = 0f;
    float alpha_speed = 0.25f;

    GameObject sp;
    ui_sp_bar sp_script;
    GameObject sp_skill;
    sp_skill_1 skill_script;
    public GameObject lazer_circle;
    GameObject player;

    //애니메이션 관련 변수
    Animator animator;
    bool is_trigger = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        sp_skill = GameObject.FindWithTag("sp_skill");
        skill_script = sp_skill.GetComponent<sp_skill_1>();
        sp = GameObject.FindWithTag("sp_bar");
        sp_script = sp.GetComponent<ui_sp_bar>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        maintain += Time.deltaTime;
        if (maintain > 8f)//스킬 유지시간
        {
            sp_script.get_sp = true;
            skill_script.bullet_split = false;
            Instantiate(lazer_circle, player.transform.position + new Vector3(-1f, 1f, 0f), Quaternion.identity);
            Instantiate(lazer_circle, player.transform.position + new Vector3(1f, 1f, 0f), Quaternion.identity);
            Destroy(gameObject);

        }
        else if (maintain >= 5f)
        {
            if (!is_trigger)
            {
                animator.SetTrigger("burn");
                is_trigger = true;
            }
            t += Time.deltaTime * alpha_speed;
            if(t < 0.8f)
            {
                bg.color = new Color(0f, 0f, 0f, 0.8f - t);
            }
        }
    }
}