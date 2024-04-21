using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer_point : MonoBehaviour
{
    public SpriteRenderer lazer;
    public SpriteRenderer direction;
    private float shoot_time = 0f;
    public float delay_shoot = 0.5f;
    private float alpha;
    public BoxCollider2D box_collider;
    public float before_t = 3f;
    public float after_t = 4f;
    ani_shake shake_script;
    GameObject camera_shake;
    bool is_shake_use = false;

    //피벗 포인트용 변수
    public Vector3 axis; // 회전 축
    public float rotate_speed = 10f; // 회전 속도
    private GameObject player;

    //player무적 상태 변수
    PlayerCtrl player_script;
    GameObject final_boss;
    finnal_boss_manager fb_script;

    private AudioSource sound;
    //레이저 1번만 보이도록 하기위한 변수
    bool is_use = false;

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        sound = GetComponent<AudioSource>();
        camera_shake = GameObject.FindWithTag("MainCamera");
        shake_script = camera_shake.GetComponent<ani_shake>();
        final_boss = GameObject.FindWithTag("final_boss");
        fb_script = final_boss.GetComponent<finnal_boss_manager>();
        box_collider.enabled = false;
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();
        lazer.color = new Color(lazer.color.r, lazer.color.g, lazer.color.b, 0);
        direction.color = new Color(direction.color.r, direction.color.g, direction.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_script.stop_world)
        {
            if (player_script.is_move_point)
            {
                if (!fb_script.is_outside)
                {
                    if (!fb_script.death_fb)
                    {
                        shoot_time += Time.deltaTime;
                        if (shoot_time < before_t)//플레이어 방향으로 발사하도록 계산
                        {
                            Vector3 dir = (player.transform.position - transform.position).normalized;
                            dir.z = 0f;
                            axis = Vector3.Cross(dir, transform.up);
                            transform.Rotate(axis * rotate_speed);
                        }
                        else if (shoot_time < after_t)
                        {
                            if (!is_use)// 레이저 궤도 실행 조건 변경
                            {
                                direction_use(shoot_time);

                                if (shoot_time >= after_t)
                                {
                                    is_use = true;
                                }
                                else if (shoot_time >= after_t - 0.5f)
                                {
                                    sound.mute = false;
                                    sound.Play();
                                }
                            }
                        }
                        else if (shoot_time > after_t)// 레이저 발사
                        {
                            box_collider.enabled = true;
                            if (!is_shake_use)
                            {
                                shake_script.is_shake = true;
                                is_shake_use = true;
                            }
                            lazer_use(shoot_time);

                            if (shoot_time >= after_t + 1f)
                            {
                                is_shake_use = false;
                                is_use = false;
                                shoot_time = 0f;
                                box_collider.enabled = false;
                            }
                        }
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                lazer.color = new Color(1f, 1f, 1f, 0f);
                direction.color = new Color(0f, 0f, 0f, 0f);
                is_shake_use = false;
                is_use = false;
                shoot_time = 0f;
                box_collider.enabled = false;
            }
        }
    }

    void direction_use(float t)
    {
        float duration = before_t + 0.5f;
        float down_duration = before_t + 1f;
        if(t < duration)
        {
            float alpha = t / duration;
            direction.color = new Color(0f,0f,0f,alpha);
        }
        else if(t < down_duration)
        {
            float alpha = 0.5f - (t / down_duration);
            if (alpha < 0f)
            {
                direction.color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                direction.color = new Color(1f, 1f, 1f, alpha);
            }
        }
        else
        {
            direction.color = new Color(0f, 0f, 0f, 0f);
        }
    }

    void lazer_use(float t)
    {
        float duration = after_t + 0.5f;
        float down_duration = after_t + 1f;
        if (t < duration)
        {
            float alpha = t / duration;
            lazer.color = new Color(1f, 1f, 1f, alpha);
        }
        else if (t < down_duration)
        {
            float alpha = 0.5f - (t / down_duration);
            if (alpha < 0f)
            {
                lazer.color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                lazer.color = new Color(1f, 1f, 1f, alpha);
            }
        }
        else
        {
            lazer.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
