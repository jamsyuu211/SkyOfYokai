using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ani_shake : MonoBehaviour
{
    public float shake_speed = 1f;
    public bool is_shake = false;
    Vector3 ori_pos;
    private GameObject player;
    PlayerCtrl player_script;
    float shake_t = 0f;

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    bool is_shake_at_skill = false;
    public float skill_shake_speed;

    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        ori_pos = transform.position;
    }
    void AnimateShake()
    {
        Vector3 move = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f);
        transform.Translate(move * shake_speed * Time.deltaTime);
    }
    void player_shake()
    {
        Vector3 move = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f);
        transform.Translate(move * skill_shake_speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_shake)
        {
            shake_t += Time.deltaTime;
            if (shake_t < 0.3f)
            {
                AnimateShake();
            }
            else
            {
                shake_t = 0f;
                is_shake = false;
            }

        }
        else
        {
            transform.position = ori_pos;
        }

        if (sp_script.is_create_circle)
        {
            is_shake_at_skill = true;
        }
        else
        {
            is_shake_at_skill = false;
        }

        if (is_shake_at_skill)
        {
            for (int i = 0; i < 3; i++)
            {
                player_shake();
            }
        }
    }
}
