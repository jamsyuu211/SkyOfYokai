using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_script : MonoBehaviour
{
    public AudioClip[] mb_sound;
    private GameObject spawn_point;
    private GameObject move_point;
    private bool is_outside = true;
    public float move_speed = 0.3f;
    private float t = 0f;
    public float attack_speed = 0.3f;
    [SerializeField] private Transform fire_pos;
    [SerializeField] private GameObject enemy_bullet;

    private GameObject gm;
    gm_stage gm_script;
    public int level;

    bool is_move = false;

    public int enemy_hp = 20;
    private SpriteRenderer sr_color;
    bool attack_color = false;
    float attack_time = 0f;


    public Sprite image;
    public GameObject coin;
    public GameObject shooting_type_item;
    public GameObject shooting_change_item;
    public GameObject healing_item;
    Vector3 big_size = new Vector3(1.5f, 1.5f, 1.5f);
    Vector3 small_size = new Vector3(0f, 0f, 0f);
    public float changeSpeed = 0.5f;
    float lerpTime = 0f; // 보간에 사용될 시간 변수
    bool attacked = false;
    private AudioSource sound;

    //player무적 상태 변수
    GameObject player;
    PlayerCtrl player_script;

    //중보 영역전개 관련 변수
    public GameObject circle_skill;
    public bool is_create_circle = false;
    public float spawn_circle_t = 0f;
    float spawn_time = 60f; //스킬 사용 쿨타임

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    void game_level(int coin_count)
    {
        for (int i = 0; i < 5; i++)
        {
            if (coin_count / 40 == i)
            {
                level = i + 1; //레벨 설정한다고 i의 초기값이 0임
                enemy_hp = 50 * i;
                break;
            }
        }
    }
    
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();
        sr_color = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        gm = GameObject.FindWithTag("manager");
        gm_script = gm.GetComponent<gm_stage>();
        spawn_point = GameObject.FindWithTag("boss_spawn_point");
        move_point = GameObject.FindWithTag("boss_move_point");
        transform.position = spawn_point.transform.position;
        game_level(gm_script.coin_count);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > move_point.transform.position.y)
        {
            is_outside = true;
        }
        else
        {
            is_outside = false;
        }

        if (!sp_script.stop_world)
        {
            if (is_outside)
            {
                transform.Translate(Vector2.down * move_speed * Time.deltaTime);
            }
            else
            {
                if (transform.position.x < -2f)
                {
                    is_move = true;
                }
                else if (transform.position.x > 2f)
                {
                    is_move = false;
                }

                if (is_move)
                {
                    transform.Translate(Vector2.right * move_speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.left * move_speed * Time.deltaTime);
                }

                //중보 스킬 코드
                spawn_circle_t += Time.deltaTime;
                if (spawn_circle_t > spawn_time + 11.2f)
                {
                    is_create_circle = false;
                    spawn_circle_t = 0f;
                }
                else if (spawn_circle_t > spawn_time)
                {
                    if (!is_create_circle)
                    {
                        Instantiate(circle_skill, transform.position, Quaternion.identity);
                        is_create_circle = true;
                    }
                }

                if (!attacked)
                {
                    //공격 코드
                    if (player_script.is_spawn)
                    {
                        t += attack_speed * Time.deltaTime;
                        if (level % 2 == 0)//level : 2, 4짝수 확산형 보스 / 기본레벨이 1이라
                        {
                            if (t >= 1f)
                            {
                                float startAngle = -45f;
                                float angleIncrement = 90f / (level * 2); // 총알 사이의 각도 간격 계산
                                for (int i = 0; i < level * 3; i++)
                                {
                                    float angle = startAngle + angleIncrement * i;
                                    Instantiate(enemy_bullet, fire_pos.position, Quaternion.Euler(0f, 0f, angle));
                                }
                                sound.mute = false;
                                sound.volume = 0.25f;
                                sound.clip = mb_sound[0];
                                sound.Play();
                                t = 0f;
                            }
                        }
                        else if (level % 2 == 1) // level : 3, 5 홀수 확산형 보스 / 기본레벨이 1이라
                        {
                            if (t >= 0.5f)
                            {
                                for (int i = 0; i < level + 2; i++)
                                {
                                    if (level == 3)
                                    {
                                        Instantiate(enemy_bullet, fire_pos.position + new Vector3((2.5f * i - (level + 2)) * 0.07f, 0f, 0.0f), Quaternion.identity);
                                    }
                                    else
                                    {
                                        Instantiate(enemy_bullet, fire_pos.position + new Vector3((2.5f * i - level) * 0.07f, 0f, 0.0f), Quaternion.identity);
                                    }
                                }
                                sound.mute = false;
                                sound.volume = 0.25f;
                                sound.clip = mb_sound[0];
                                sound.Play();
                                t = 0f;
                            }
                        }
                    }
                    if (t > 1f)
                    {
                        t = 0f;
                    }
                }
                else
                {
                    lerpTime += Time.deltaTime * changeSpeed;
                    transform.localScale = Vector3.Lerp(big_size, small_size, lerpTime);

                    // 크기 감소가 완료되면 오브젝트 파괴
                    if (lerpTime >= 1f)
                    {
                        float rand_item = Random.Range(0f, 100f);
                        if (rand_item < 75f)
                        {
                            Instantiate(coin, transform.position, Quaternion.identity);
                            Instantiate(healing_item, transform.position, Quaternion.identity);
                        }
                        else if (rand_item < 80f)
                        {
                            Instantiate(coin, transform.position, Quaternion.identity);
                            Instantiate(shooting_type_item, transform.position, Quaternion.identity);
                            Instantiate(healing_item, transform.position, Quaternion.identity);
                        }
                        else if (rand_item < 95f)
                        {
                            Instantiate(coin, transform.position, Quaternion.identity);
                            Instantiate(shooting_change_item, transform.position, Quaternion.identity);
                            Instantiate(shooting_type_item, transform.position, Quaternion.identity);
                        }
                        else if (rand_item < 100f)
                        {
                            for (int i = 0; i < 2; i++)
                                Instantiate(healing_item, transform.position, Quaternion.identity);
                        }
                        sound.volume = 0.3f;
                        sound.clip = mb_sound[1];
                        sound.Play();
                        Destroy(gameObject);
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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!is_outside)
        {
            if (other.tag == "Bullet")
            {
                if (!attacked)
                {
                    enemy_hp--;
                    sr_color.color = Color.red;
                    attack_color = true;
                    Destroy(other.gameObject);
                }
                if (enemy_hp <= 0)
                {
                    if (!attacked)
                    {
                        gm_script.basic_score += 20;
                        sound.mute = false;
                        sound.Play();
                        transform.position = other.gameObject.transform.position;
                        sr_color.color = Color.white;
                        transform.GetComponent<SpriteRenderer>().sprite = image;
                        attacked = true;
                        lerpTime = 0f;
                        Destroy(other.gameObject);
                    }
                }
            }
            else if (other.tag == "p_lazer")
            {
                if (!attacked)
                {
                    enemy_hp -= 10;
                    sr_color.color = Color.red;
                    attack_color = true;   
                }
                if (enemy_hp <= 0)
                {
                    if (!attacked)
                    {
                        gm_script.basic_score += 20;
                        sound.mute = false;
                        sound.Play();
                        transform.position = other.gameObject.transform.position;
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
