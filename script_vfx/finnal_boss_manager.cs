using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class finnal_boss_manager : MonoBehaviour
{
    //보스 hp관련 변수
    public int boss_hp = 400;
    public bool[] is_destroy = new bool[4];
    private SpriteRenderer sr_color;
    bool attack_color = false;
    float attack_time = 0f;

    GameObject lazer;
    lazer_point lazer_script;
    private GameObject spawn_point;
    private GameObject move_point;
    public bool is_outside = true;
    float move_speed = 0.8f;

    //파이널 스코어 관련 변수
    private GameObject gm;
    gm_stage gm_script;

    //막보 죽을때 애니메이션
    private SpriteRenderer fb_color;
    public bool death_fb = false;
    AudioSource sound;
    Vector3 big_size = new Vector3(1.5f, 1.5f, 1.5f);
    Vector3 small_size = new Vector3(0f, 0f, 0f);
    public float changeSpeed = 2f;
    float lerpTime = 0f; // 보간에 사용될 시간 변수
    GameObject enemy;
    EnemyCtrl enemy_script;
    bool is_play_sound = false;
    bool is_last_sprite = false;


    //막보 영역전개 관련 변수
    public GameObject circle_skill;
    public bool is_create_circle = false;
    public float spawn_circle_t = 0f;
    float spawn_time = 10f; //스킬 사용 쿨타임

    //자연스러운 화면 전환을 위한 변수
    GameObject camera;
    trans_score camera_script;

    //막보 죽을때 애니메이션
    public GameObject fb_ani;
    float ani_t = 0f;
    bool changed_image = false;
    public Sprite image;
    float blink_down_t = 0f;
    float blink_up_t = 0f;
    GameObject instance;

    //막보 스킬관련변수
    public GameObject bomb;
    float spawn_egg_t = 7f;
    GameObject player;
    PlayerCtrl player_script;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();
        camera = GameObject.FindWithTag("MainCamera");
        camera_script = camera.GetComponent<trans_score>();
        sound = GetComponent<AudioSource>();
        enemy = GameObject.FindWithTag("Enemy");
        enemy_script = enemy.GetComponent<EnemyCtrl>(); 
        fb_color = GetComponent<SpriteRenderer>();
        gm = GameObject.FindWithTag("manager");
        gm_script = gm.GetComponent<gm_stage>();
        spawn_point = GameObject.FindWithTag("boss_spawn_point");
        move_point = GameObject.FindWithTag("boss_move_point");
        transform.position = spawn_point.transform.position;
        sr_color = GetComponent<SpriteRenderer>();
        lazer = GameObject.FindWithTag("lazer_point");
        lazer_script = lazer.GetComponent<lazer_point>();
        for(int i = 0; i < 4; i++)
        {
            is_destroy[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!death_fb)
        {
            if (transform.position.y > move_point.transform.position.y)
            {
                is_outside = true;
            }
            else
            {
                is_outside = false;
            }

            if (is_outside)
            {
                transform.Translate(Vector2.down * move_speed * Time.deltaTime);
            }
            else
            {
                if (is_destroy[0] && is_destroy[1] && is_destroy[2] && is_destroy[3])
                {
                    lazer_script.before_t = 1.0f;
                    lazer_script.after_t = 2.0f;


                    //막보 스킬 코드
                    spawn_circle_t += Time.deltaTime;
                    if (spawn_circle_t > spawn_time + 8f)
                    {
                        spawn_circle_t = 0f;
                        is_create_circle = false;
                    }
                    else if (spawn_circle_t > spawn_time)
                    {
                        if (!is_create_circle)
                        {
                            Instantiate(circle_skill, transform.position, Quaternion.identity);
                            is_create_circle = true;
                        }
                    }

                    if (player_script.is_spawn)
                    {
                        spawn_egg_t += Time.deltaTime;
                        if (spawn_egg_t > 7f)
                        {
                            spawn_egg_t = 0f;
                            Instantiate(bomb, new Vector3(Random.Range(-1.8f, 1.8f), 6f, 0f), Quaternion.identity);
                        }
                    }
                }

                if (attack_color)
                {
                    attack_time += Time.deltaTime;
                    if (attack_time > 0.5)
                    {
                        attack_time = 0;
                        sr_color.color = Color.white;
                        attack_color = false;
                    }
                }
            }
        }
        else
        {
            ani_t += Time.deltaTime;
            if(ani_t < 3f)
            {
                sr_color.color = Color.white;
            }
            else if(ani_t < 3.5f)
            {
                blink_down_t += 2f * Time.deltaTime;
                sr_color.color = new Color(1f,1f,1f,1 - blink_down_t);
            }
            else if(ani_t < 3.8f)
            {
                blink_up_t += 2f * Time.deltaTime;
                sr_color.color = new Color(1f,1f,1f, blink_up_t);
            }
            else
            {
                if (!changed_image)
                {
                    sound.mute = false;
                    sound.Play();
                    changed_image = true;
                    sr_color.color = Color.white;
                    sr_color.sprite = image;
                }
                lerpTime += Time.deltaTime;
                size_down();
                if(transform.localScale.x < 0.3f || transform.localScale.y < 0.3f)
                {
                    camera_script.is_game_over = true;
                    gm_script.basic_score += 200;
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!is_outside)
        {
            if (is_destroy[0] && is_destroy[1] && is_destroy[2] && is_destroy[3])
            {
                if (collision.tag == "Bullet")
                {
                    boss_hp--;
                    Destroy(collision.gameObject);
                    sr_color.color = Color.red;
                    attack_color = true;
                }
                else if(collision.tag == "p_lazer")
                {
                    boss_hp -= 10;
                    sr_color.color = Color.red;
                    attack_color = true;
                }
                if (boss_hp <= 0)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    instance = Instantiate(fb_ani);
                    enemy_script.is_fb_death = true;
                    death_fb = true;
                }
            }
        }
    }

    void size_down()
    {
        transform.localScale = Vector3.Lerp(big_size, small_size, lerpTime);
    }
}
