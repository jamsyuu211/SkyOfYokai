using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    //[SerializeField] private Transform background;
    //private Animator _animator;

    //플레이어 이동관련 변수
    public int speed  = 10;
    private Vector2 lastTouchPos = Vector2.zero;  // 마지막 터치 위치 저장 변수
    public bool isMoving = false;
    private bool is_button_pos;

    public GameObject p_Bullet;
    public Transform firePos;

    private float iter = 0f;
    private float t_shoot = 0f;

    public int level = 1;
    public int type = 0;

    private AudioSource sound;
    public AudioClip[] audioClips;

    private float level_t = 0f;

    public int hp = 20;
    private int hp_index = 9;

    private GameObject gm_stage;

    private SpriteRenderer s_color;
    bool attack_color = false;
    float attack_time = 0f;

    gm_stage gm_script;

    ani_shake shake_script;
    GameObject camera_shake;

    //hp ui관련 변수
    private GameObject ui_hp;
    ui_hp_bar hp_script;
    //public Transform myTr;

    //레이저 포인트관련 변수
    public float damage_speed = 2f;

    //player사망 관련 변수
    Vector3 big_size = new Vector3(0.5f, 0.5f, 0.5f);
    Vector3 small_size = new Vector3(0f, 0f, 0f);
    public float changeSpeed = 0.1f;
    float lerpTime = 0f; // 보간에 사용될 시간 변수
    bool attacked = false;
    private AudioSource player_sound;
    public Sprite image;
    private SpriteRenderer sr_color;
    public Sprite plane2;
    public Sprite plane3;
    private Transform spawn_point;
    private Transform move_point;
    public float move_speed = 0.5f;
    public bool is_move_point = false;
    public int plane_type = 1;
    Vector3 ori_scale;
    public bool is_spawn = true; //적기에서 공격 중단 or 시작 체크하는 변수
    bool finished_clear_ui = false;

    //sp 스킬 관련 변수
    private GameObject sp_skill;
    sp_skill_1 sp_script;

    //파이널 스코어 관련 변수
    float score_t = 0f;
    public GameObject fb;
    finnal_boss_manager fb_script;
    public bool is_game_over = false;
    bool is_type = true;

    //보스 스킬관련 변수
    public bool is_mb_circle_spawn = false;
    public bool is_fb_circle_spawn = false;

    //자연스러운 화면 전환을 위한 변수
    GameObject camera;
    trans_score camera_script;
    // Start is called before the first frame update
    void Start()
    {
        spawn_point = GameObject.FindWithTag("p_spawn").transform;
        move_point = GameObject.FindWithTag("p_move").transform;
        //_animator = GetComponent<Animator>();
        camera = GameObject.FindWithTag("MainCamera");
        camera_script = camera.GetComponent<trans_score>();
        fb_script = fb.GetComponent<finnal_boss_manager>();
        sp_skill = GameObject.FindWithTag("sp_skill");
        sp_script = sp_skill.GetComponent<sp_skill_1>();
        is_spawn = true;
        ori_scale = transform.localScale;
        sr_color = GetComponent<SpriteRenderer>();
        player_sound = GetComponent<AudioSource>();
        ui_hp = GameObject.FindWithTag("ui_hp");
        hp_script = ui_hp.GetComponent<ui_hp_bar>();
        camera_shake = GameObject.FindWithTag("MainCamera");
        shake_script = camera_shake.GetComponent<ani_shake>();
        s_color = GetComponent<SpriteRenderer>();
        gm_stage = GameObject.FindWithTag("manager");
        gm_script = gm_stage.GetComponent<gm_stage>();

        sound = GetComponent<AudioSource>();

        transform.position = spawn_point.position;
    }

    // Update is called once per frame
    void Update()
    {
        //생존점수
        score_t += Time.deltaTime;
        if (fb_script.death_fb && !is_game_over)
        {
            is_game_over = true;
            gm_script.total_score += (int)((100 / (score_t / 60)) * 3);
        }

        //플레이어 제어
        if (plane_type == 4)
        {
            if (is_type)
            {
                is_type = false;
                camera_script.is_game_over = true;
            }
        }
        if (transform.position.x >= move_point.position.x && transform.position.y >= move_point.position.y)
        {
            is_move_point = true;
        }
        if (!sp_script.stop_world)
        {
            if (is_move_point)
            {

                if (!attacked)
                {
                    if (!finished_clear_ui)
                    {
                        for (int i = 0; i <= hp_index; i++)
                        {
                            hp_script.black_bg[i].color = Color.clear;
                        }
                        is_spawn = true;
                        finished_clear_ui = true;
                    }


                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        Vector2 touch_pos = Camera.main.ScreenToWorldPoint(new Vector2(touch.position.x, touch.position.y));
                        Vector2 player_pos = transform.position;

                        is_button_pos = (touch_pos.x < 2.15 && touch_pos.x > 1.55) && (touch_pos.y < -4.3 && touch_pos.y > -4.9);
                        if (touch_pos != lastTouchPos && !is_button_pos)
                        {
                            float x = MathF.Abs(touch_pos.x - player_pos.x);
                            float y = MathF.Abs(touch_pos.y - player_pos.y);
                            if (x > 0.018f && y > 0.018f)
                            {
                                Vector2 move_dir = (touch_pos - player_pos).normalized;
                                transform.Translate(move_dir * speed * Time.deltaTime);
                                isMoving = true;
                            }
                            else
                            {
                                isMoving = false;
                            }
                            lastTouchPos = touch_pos;
                        }
                    }
                    else
                    {
                        isMoving = false;
                        lastTouchPos = Vector2.zero;
                    }

                    //if (Input.GetMouseButton(0))
                    //{

                    //    Vector2 click_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //    Vector2 player_pos = transform.position;

                    //    Vector2 move_dir = (click_pos - player_pos).normalized;
                    //    if ((player_pos.x + move_dir.x * speed * Time.deltaTime < 1.85f && player_pos.x + move_dir.x * speed * Time.deltaTime > -1.85f) &&
                    //        (player_pos.y + move_dir.y * speed * Time.deltaTime < 4f && player_pos.y + move_dir.y * speed * Time.deltaTime > -4.6f))
                    //    {
                    //        transform.Translate(move_dir * speed * Time.deltaTime);
                    //    }
                    //}

                    float h = Input.GetAxis("Horizontal");//수평
                    float v = Input.GetAxis("Vertical");//수직
                    Vector3 moveDir;
                    if (h == 0 || v == 0)
                    {
                        moveDir = (Vector3.up * v + Vector3.right * h);
                    }
                    else
                    {
                        moveDir = (Vector3.up * v + Vector3.right * h) / MathF.Sqrt(v * v + h * h);
                    }
                    Vector3 move = moveDir * speed * Time.deltaTime;
                    //범위 제한
                    if ((transform.position.x + moveDir.x * speed * Time.deltaTime < 1.85 && transform.position.x + moveDir.x * speed * Time.deltaTime > -1.85) &&
                        (transform.position.y + moveDir.y * speed * Time.deltaTime < 4 && transform.position.y + moveDir.y * speed * Time.deltaTime > -4.5))
                    {
                        //_animator.SetFloat("p_hori", h);
                        transform.Translate(move); //본인만 가능
                                                   //background.position += new Vector3(0f, move.y, 0f);
                    }

                    if (sp_script.bullet_split)
                    {
                        t_shoot = 0.2f;
                    }
                    else
                    {
                        if (type == 0)
                        {
                            t_shoot = -0.024f * Mathf.Log10(level) + 0.5032f;
                        }
                        else
                        {
                            t_shoot = -0.024f * Mathf.Log10(level) + 0.5032f;
                        }
                    }

                    if (level > 1)
                    {
                        level_t += Time.deltaTime;
                        if (level_t > 20f)//변경 / 총알갯수 증가 아이템 쿨타임
                        {
                            level--;
                            level_t = 0;
                        }
                    }

                    iter += Time.deltaTime;
                    if (iter > t_shoot)
                    {
                        iter = 0f;
                        for (int i = 0; i <= level; i++) //level은 총알 발사 갯수
                        {
                            if (type == 0)
                            {
                                Instantiate(p_Bullet,
                                firePos.position + new Vector3((-level / 2.0f + i) * 0.3f, 0.0f, 0.0f), Quaternion.identity);
                            }
                            else if (type == 1)
                            {
                                Instantiate(p_Bullet, firePos.position,
                                    Quaternion.Euler(0f, 0f, (-level / 2.0f + i) * 5.0f));
                            }
                            sound.volume = 0.3f;
                            sound.clip = audioClips[5];
                            sound.Play();
                        }
                    }

                    if (attack_color)
                    {
                        attack_time += Time.deltaTime;
                        if (attack_time > 0.5)
                        {
                            attack_time = 0;
                            s_color.color = Color.white;
                            attack_color = false;
                        }
                    }
                }
                else//attacked 가 참이면 (파괴가 됐으면)
                {
                    lerpTime += Time.deltaTime * changeSpeed;
                    transform.localScale = Vector3.Lerp(big_size, small_size, lerpTime);

                    // 크기 감소가 완료되면 오브젝트 변경
                    if (lerpTime >= 1f)
                    {
                        plane_type++;
                        is_move_point = false;
                        transform.position = spawn_point.position;
                        type = 0;
                        level = 1;
                        attack_color = true;
                        attacked = false;
                        hp = 20;                                                  /////////플레이어 hp
                        hp_index = 9;                                             /////////플레이어 hp
                        transform.localScale = ori_scale;
                        finished_clear_ui = false;
                        if (plane_type == 2)
                        {
                            transform.GetComponent<SpriteRenderer>().sprite = plane2;
                        }
                        else if (plane_type == 3)
                        {
                            transform.GetComponent<SpriteRenderer>().sprite = plane3;
                        }
                    }
                }
            }
            else//플레이어 사망시 2번째, 3번째 비행기로 변경하고, 좌표를 move_point로 이동
            {
                Vector2 move = (move_point.position - transform.position).normalized;
                transform.Translate(move * move_speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (is_move_point)
        {
            if (other.tag == "coin")
            {
                if (!attacked)
                {
                    sound.volume = 0.3f;
                    sound.clip = audioClips[0];
                    sound.Play();
                }
            }
            else if (other.tag == "shooting_item")
            {
                if (!attacked)
                {
                    sound.volume = 0.3f;
                    sound.clip = audioClips[1];
                    sound.Play();
                }
            }
            else if (other.tag == "change_item")
            {
                if (!attacked)
                {
                    sound.volume = 0.1f;
                    sound.clip = audioClips[2];
                    sound.Play();
                }
            }
            else if (other.tag == "healing_item")
            {
                if (!attacked)
                {
                    if (hp % 2 == 1)
                    {
                        if (hp / 2 == hp_index + 1)
                        {
                            if (hp < 20)
                            {
                                hp_script.black_bg[++hp_index].color = Color.clear;
                            }
                        }
                    }
                    if (hp >= 20)
                    {
                        hp = 20;
                    }
                    else
                    {
                        hp++;
                    }
                    sound.volume = 0.3f;
                    sound.clip = audioClips[3];
                    sound.Play();
                }
            }
            else if (other.tag == "Enemy_bullet")
            {
                if (!attacked)
                {
                    if (hp / 2 == hp_index)
                    {
                        if (hp % 2 == 1)
                        {
                            hp_script.black_bg[hp_index].color = Color.black;
                            hp_index--;
                        }
                    }
                    hp--;
                    shake_script.is_shake = true;
                    s_color.color = Color.red;
                    attack_color = true;
                    Destroy(other.gameObject);
                    if (hp <= 0)
                    {
                        sound.volume = 0.4f;
                        sound.clip = audioClips[4];
                        sound.Play();
                        is_spawn = false;
                        sr_color.color = Color.white;
                        transform.GetComponent<SpriteRenderer>().sprite = image;
                        attacked = true;
                        lerpTime = 0f;
                        Destroy(other.gameObject);
                    }
                }
            }
            else if (other.tag == "boss_bullet")
            {
                if (!attacked)
                {
                    if (is_mb_circle_spawn)
                    {
                        hp_script.black_bg[hp_index].color = Color.black;
                        hp_index--;
                        if (hp >= 2)
                        {
                            hp -= 2;
                        }
                        else
                        {
                            hp = 0;
                        }
                    }
                    else
                    {
                        if (hp / 2 == hp_index)
                        {
                            if (hp % 2 == 1)
                            {
                                hp_script.black_bg[hp_index].color = Color.black;
                                hp_index--;
                            }
                        }
                        hp--;
                    }

                    shake_script.is_shake = true;
                    s_color.color = Color.red;
                    attack_color = true;
                    Destroy(other.gameObject);
                    if (hp <= 0)
                    {
                        sound.volume = 0.4f;
                        sound.clip = audioClips[4];
                        sound.Play();
                        is_spawn = false;
                        sr_color.color = Color.white;
                        transform.GetComponent<SpriteRenderer>().sprite = image;
                        attacked = true;
                        lerpTime = 0f;
                        Destroy(other.gameObject);
                    }
                }
            }
            else if (other.tag == "final_boss_bullet")
            {
                if (!attacked)
                { 
                    if (hp / 2 == hp_index)
                    {
                        if (hp % 2 == 1)
                        {
                            hp_script.black_bg[hp_index].color = Color.black;
                            hp_index--;
                        }
                    }
                    hp--;

                    shake_script.is_shake = true;
                    s_color.color = Color.red;
                    attack_color = true;
                    Destroy(other.gameObject);
                    if (hp <= 0)
                    {
                        sound.volume = 0.4f;
                        sound.clip = audioClips[4];
                        sound.Play();
                        is_spawn = false;
                        sr_color.color = Color.white;
                        transform.GetComponent<SpriteRenderer>().sprite = image;
                        attacked = true;
                        lerpTime = 0f;
                        Destroy(other.gameObject);
                    }
                }
            }
            else if (other.tag == "lazer")
            {
                if (!attacked)
                {
                    if (is_fb_circle_spawn)
                    {
                        if (hp >= 4)
                        {
                            hp_script.black_bg[hp_index].color = Color.black;
                            hp_script.black_bg[hp_index - 1].color = Color.black;
                            hp -= 4;
                            hp_index -= 2;
                        }
                        else
                        {
                            if(hp_index == 0)
                            {
                                hp_script.black_bg[hp_index].color = Color.black;
                            }
                            else
                            {
                                hp_script.black_bg[hp_index].color = Color.black;
                                hp_script.black_bg[hp_index - 1].color = Color.black;
                                hp_index -= 2;
                            }
                            hp = 0;
                        }
                        
                    }
                    else
                    {
                        hp_script.black_bg[hp_index].color = Color.black;
                        hp_index--;
                        if (hp >= 2)
                        {
                            hp -= 2;
                        }
                        else
                        {
                            hp = 0;
                        }
                    }

                    shake_script.is_shake = true;
                    s_color.color = Color.red;
                    attack_color = true;
                    if (hp <= 0)
                    {
                        sound.volume = 0.4f;
                        sound.clip = audioClips[4];
                        sound.Play();
                        is_spawn = false;
                        sr_color.color = Color.white;
                        transform.GetComponent<SpriteRenderer>().sprite = image;
                        attacked = true;
                        lerpTime = 0f;
                    }
                }
            }
        }
    }
}
