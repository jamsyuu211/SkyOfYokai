using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    //베지어 관련 변수
    public float speed = 0.5f;
    public float rotation_speed = 3.0f;
    private float t = 0f;
    bool is_increasing = true;
    private Vector3[] ori_pos = new Vector3[4];
    bool is_move = true;
    float move_t = 0f;

    //총알 발사 관련 변수
    private GameObject player;
    [SerializeField] private GameObject p_enemy_bullet;
    public Transform firePos;

    //총알 발사 시간
    float timer = 0.0f;
    public float interval = 3.0f;
    public int level = 1;

    //enemy 사망 변수
    public Sprite image;
    public GameObject coin;
    public GameObject shooting_type_item;
    public GameObject shooting_change_item;
    public GameObject healing_item;
    Vector3 big_size = new Vector3(1f, 1f, 1f);
    Vector3 small_size = new Vector3(0f, 0f, 0f);
    public float changeSpeed = 0.1f;
    float lerpTime = 0f; // 보간에 사용될 시간 변수
    bool attacked = false;
    private AudioSource sound;

    //enemy hp관련 변수
    public int enemy_hp = 2;
    private SpriteRenderer sr_color;
    bool attack_color = false;
    float attack_time = 0f;
    private GameObject gm;
    gm_stage gm_script;
    public GameObject enemy_bullet;
    p_enemy_bullet bullet_script;

    //player무적 상태 변수
    PlayerCtrl player_script;

    //막보 관련변수
    public GameObject fb;
    finnal_boss_manager fb_script;

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    public GameObject particle;
    public bool is_particle = false;
    private GameObject instance;
    bool is_crazy = false;
    Vector3 crazy_vec = new Vector3(0f, 0f, 50f);
    public float crazy_rotate_speed;

    public bool is_fb_death = false;
    void game_level(int coin_count)
    {
        for (int i = 1; i <= 5; i++)
        {
            if (coin_count / 40 == i)
            {
                if (i <= 4)
                {
                    enemy_hp = i + 1;
                }
                else
                {
                    enemy_hp = 5;
                }
                bullet_script.b_speed = 0.2f * i + 2.0f;
                break;
            }
        }
    }
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        fb_script = fb.GetComponent<finnal_boss_manager>();
        bullet_script = enemy_bullet.GetComponent<p_enemy_bullet>();
        gm = GameObject.FindWithTag("manager");
        gm_script = gm.GetComponent<gm_stage>();
        game_level(gm_script.coin_count);
        sr_color = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();

        gm_script.spawn_counting++;
        instance = Instantiate(particle, transform.position, Quaternion.identity, transform);

        instance.SetActive(false);

        ori_pos[0] = GameObject.Find("point1").transform.position;
        ori_pos[1] = GameObject.Find("point2").transform.position;
        ori_pos[2] = GameObject.Find("point3").transform.position;
        ori_pos[3] = GameObject.Find("point4").transform.position;



        float rand_x = Random.Range(-3f, 3f);
        ori_pos[0] = new Vector3(ori_pos[0].x + rand_x, ori_pos[0].y, ori_pos[0].z);

        for (int i = 1; i <= 3; i++)
        {
            float random_x = Random.Range(-2f, 2f);
            float random_y = Random.Range(-2.3f, 2.3f);

            ori_pos[i] = new Vector3(ori_pos[i].x + random_x, ori_pos[i].y + random_y, ori_pos[i].z);
        }
    }


    void fire_bullet()
    {
        Vector3 direction = (transform.position - player.transform.position);
        direction.z = 0;
        if (direction.x >= 0)
        {
            if (direction.y < 0)
            {
                direction.y = -direction.y;
            }
        }
        else
        {
            direction.x = -direction.x;
            if (direction.y < 0)
            {
                direction.y = -direction.y;
            }
        }

        if (direction.x <= 2 && direction.y <= 6)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                Instantiate(p_enemy_bullet,
                            firePos.position + new Vector3(0f, 0.0f, 0.0f), Quaternion.identity);
                timer = 0f;
            }
        }
    }
    // Update is called once per frame

    void Update()
    {
        if (!sp_script.stop_world)
        {
            if (fb_script.death_fb)
            {
                attacked = true;
            }
            if (player_script.is_spawn)
            {
                if (!attacked)
                {
                    if (!is_crazy)
                    {
                        fire_bullet();
                    }
                }
            }

            if (is_move)
            {
                if (is_increasing)
                {
                    t += Time.deltaTime * speed;
                    if (t >= 1.0f)
                    {
                        t = 1.0f; // t 값을 1로 고정
                        is_increasing = false; // 감소 방향으로 전환
                        is_move = false;
                    }
                }
                // t 값 감소
                else
                {
                    t -= Time.deltaTime * speed;
                    if (t <= 0.0f)
                    {
                        t = 0.0f; // t 값을 0으로 고정
                        is_increasing = true; // 증가 방향으로 전환
                    }
                }
            }
            else
            {
                move_t += Time.deltaTime;
            }


            if (!attacked)
            {
                Vector3 enemy_pos =
               Mathf.Pow(1 - t, 3) * ori_pos[0]
               + 3 * t * Mathf.Pow(1 - t, 2) * ori_pos[1]
               + 3 * t * (1 - t) * ori_pos[2]
               + Mathf.Pow(t, 3) * ori_pos[3];

                Vector3 now_pos = transform.position;
                //Vector3 direction = (enemy_pos - now_pos).normalized; //이동방향 바라보게
                Vector3 direction = (player.transform.position - now_pos).normalized; // 플레이어 바라보게
                direction.z = 0;
                Vector3 rotateAmount = Vector3.Cross(direction, transform.up); //z축 방향으로 회적각을 정함 (외적 공식통해서)

                if (is_crazy)
                {
                    transform.Rotate(crazy_vec * crazy_rotate_speed * Time.deltaTime);
                    instance.transform.Rotate(-crazy_vec * crazy_rotate_speed * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(rotateAmount * rotation_speed * Time.deltaTime);
                }
                if (is_move || move_t >= 0.2f)
                {
                    transform.position = enemy_pos;
                    move_t = 0f;
                    is_move = true;
                }


                if (sp_script.is_effect)
                {
                    if (!is_particle)
                    {
                        is_particle = true;
                        is_crazy = true;
                        instance.SetActive(true);
                    }
                }
                else
                {
                    if (is_particle)
                    {
                        is_particle = false;
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
            if (t > 1f)
            {
                t = 0f;
            }
            if (attacked)
            {
                if (is_fb_death)
                {
                    sr_color.sprite = image;
                    is_fb_death = false;
                    gm_script.basic_score += 1;
                    sr_color.color = Color.white;
                    lerpTime = 0f;
                }


                lerpTime += Time.deltaTime * changeSpeed;
                transform.localScale = Vector3.Lerp(big_size, small_size, lerpTime);

                // 크기 감소가 완료되면 오브젝트 파괴
                if (lerpTime >= 1f)
                {
                    float rand_item = Random.Range(0f, 100f);
                    if (rand_item < 80f)
                    {
                        Instantiate(coin, transform.position, Quaternion.identity);
                    }
                    else if (rand_item < 86f)
                    {
                        Instantiate(shooting_type_item, transform.position, Quaternion.identity);
                    }
                    else if (rand_item < 91f)
                    {
                        Instantiate(shooting_change_item, transform.position, Quaternion.identity);
                    }
                    else if (rand_item < 100f)
                    {
                        Instantiate(healing_item, transform.position, Quaternion.identity);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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
                    instance.SetActive(false);
                    sound.mute = false;
                    sound.Play();
                    is_fb_death = false;
                    gm_script.spawn_counting--;
                    gm_script.basic_score += 1;
                    gm_script.coin_count++;
                    sr_color.color = Color.white;
                    sr_color.sprite = image;
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    lerpTime = 0f;
                    attacked = true;
                    Destroy(other.gameObject);
                }
            }
        }
        else if(other.tag == "p_lazer")
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
                    instance.SetActive(false);
                    sound.mute = false;
                    sound.Play();
                    is_fb_death = false;
                    gm_script.spawn_counting--;
                    gm_script.basic_score += 1;
                    gm_script.coin_count++;
                    sr_color.color = Color.white;
                    sr_color.sprite = image;
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    lerpTime = 0f;
                    attacked = true;
                }
            }
        }
    }
}
