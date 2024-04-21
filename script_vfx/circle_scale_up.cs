using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class circle_scale_up : MonoBehaviour
{
    public GameObject black_bg;
    private GameObject black_ring;
    private SpriteRenderer sr_black_ring;
    Color ori_br_color;
    Color trans_br_color;
    float delay_t = 0f;
    bool is_big = false;
    bool is_spawn = true;

    float lerpTime = 0f;
    float down_lerpTime = 0f;
    float changeSpeed = 1.2f;
    float down_change_speed = 0.6f;
    Vector2 big_size = new Vector2(25f, 25f);
    Vector2 small_size = new Vector2(0f, 0f);

    public bool is_destoryed = false;
    GameObject sp;
    ui_sp_bar sp_script;
    GameObject sp_skill;
    sp_skill_1 skill_script;
    // Start is called before the first frame update
    void Start()
    {
        black_ring = GameObject.FindWithTag("black_circle");
        sr_black_ring = black_ring.GetComponent<SpriteRenderer>();
        sp_skill = GameObject.FindWithTag("sp_skill");
        skill_script = sp_skill.GetComponent<sp_skill_1>();
        sp = GameObject.FindWithTag("sp_bar");
        sp_script = sp.GetComponent<ui_sp_bar>();
        transform.position = GameObject.FindWithTag("Player").transform.position;

        skill_script.is_effect = true;
        skill_script.stop_world = false;
        ori_br_color = sr_black_ring.color;
        skill_script.is_create_circle = false;
        trans_br_color = new Color(1f, 1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_big)
        {
            lerpTime += Time.deltaTime * changeSpeed;
            transform.localScale = Vector3.Lerp(small_size, big_size, lerpTime);
            if (transform.localScale.x >= 24.9 && transform.localScale.y >= 24.9)
            {
                delay_t += Time.deltaTime;
            }

            if (delay_t > 1f)
            {
                delay_t = 0f;
                is_big = true;
            }
        }
        else
        {
            if (is_spawn)
            {
                is_spawn = false;
                Instantiate(black_bg, new Vector3(0f, 0f, 0f), Quaternion.identity);
                sr_black_ring.color = trans_br_color;
            }
            down_lerpTime += Time.deltaTime * down_change_speed;
            transform.localScale = Vector3.Lerp(big_size, small_size, down_lerpTime);
            if (transform.localScale.x <= 0.1f && transform.localScale.y <= 0.1f)
            {
                skill_script.is_effect = false;
                is_destoryed = true;
                Destroy(gameObject);
            }
        }
    }
}
