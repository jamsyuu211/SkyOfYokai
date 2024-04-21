using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_change_item : MonoBehaviour
{
    private Vector3 point_length;
    private Vector3 point_length_x;
    private Vector3[] ori_points = new Vector3[4];
    public float speed = 0.05f;
    private float t = 0f;
    bool is_increasing = true;
    private GameObject s_player;
    PlayerCtrl target_script;

    //아이템 자동 획득
    bool is_near = false;
    private Transform player;
    float near_speed = 2f;

    //player무적 상태 변수
    PlayerCtrl player_script;


    //아이템 유지시간 관련 변수
    float spawning_time = 0f;
    float destroy_time = 10f;
    SpriteRenderer item_color;
    float blink_t = 0f;

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        item_color = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        player_script = player.GetComponent<PlayerCtrl>();
        s_player = GameObject.FindWithTag("Player");
        target_script = s_player.GetComponent<PlayerCtrl>();

        ori_points[0] = transform.position;
        ori_points[1] = GameObject.Find("c_point2").transform.position;
        ori_points[2] = GameObject.Find("c_point3").transform.position;
        ori_points[3] = GameObject.Find("c_point4").transform.position;

        
        for (int i = 1; i <= 2; i++)
        {
            float random_x = Random.Range(-1f, 1f);
            float random_y = Random.Range(-3f, 3f);

            ori_points[i] = new Vector3(ori_points[i].x + random_x, ori_points[i].y + random_y, ori_points[i].z);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!sp_script.stop_world)
        {
            spawning_time += Time.deltaTime;
            if (spawning_time > destroy_time)
            {
                Destroy(gameObject);
            }
            else if (spawning_time > 6f)
            {
                blink_t += Time.deltaTime;
                blink(blink_t);
            }

            if (player_script.is_spawn)
            {
                if (t >= 1.0f)
                {
                    t = 1.0f; // t 값을 1로 고정
                    is_increasing = false; // 감소 방향으로 전환
                }
                else if (t <= 0f)
                {
                    t = 0.0f; // t 값을 0으로 고정
                    is_increasing = true; // 증가 방향으로 전환
                }
                if (is_increasing)
                {
                    t += Time.deltaTime * speed;
                }
                // t 값 감소
                else
                {
                    t -= Time.deltaTime * speed;
                }
            }

            Vector2 length = player.position - transform.position;
            if ((length.x < 1.5 && length.x > -1.5) && (length.y < 1.5 && length.y > -1.5))
            {
                is_near = true;
            }
            if (is_near)
            {
                if (player_script.is_spawn)
                {
                    transform.Translate(length * near_speed * Time.deltaTime);
                }
            }
            else
            {
                Vector3 coin_pos =
                   Mathf.Pow(1 - t, 3) * ori_points[0]
                   + 3 * t * Mathf.Pow(1 - t, 2) * ori_points[1]
                   + 3 * t * (1 - t) * ori_points[2]
                   + Mathf.Pow(t, 3) * ori_points[3];

                transform.position = coin_pos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player_script.is_spawn)
        {
            if (other.tag == "Player")
            {
                if (target_script.type < 1)//type에서 최댓값이 1이라
                {
                    target_script.type++;
                }
                else
                {
                    target_script.type = 0;
                }
                Destroy(gameObject);
            }
        }
    }
    void blink(float t)
    {
        float duration = 0.5f;
        float down_duration = 0.5f;
        if (t < duration)
        {
            float alpha = t / duration;
            item_color.color = new Color(1f, 1f, 1f, alpha);
        }
        else if (t < down_duration + duration)
        {
            float alpha = down_duration - (t / down_duration);
            item_color.color = new Color(1f, 1f, 1f, alpha);
        }
        else
        {
            item_color.color = new Color(1f, 1f, 1f, 0f);
            blink_t = 0f;
        }
    }
}
