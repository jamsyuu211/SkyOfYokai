using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parts3_shooting : MonoBehaviour
{
    float shooting_t = 0f;
    float delay_t = 0f;
    public GameObject parts3_bullet;
    public Transform fire_pos;
    int count = 0;

    //hp 관련 변수
    private SpriteRenderer sr_color;
    bool attack_color = false;
    float attack_time = 0f;
    public int hp = 60;
    bool attacked = false;
    AudioSource sound;
    Vector3 big_size = new Vector3(1f, 1f, 1f);
    Vector3 small_size = new Vector3(0.5f, 0.5f, 0.5f);
    float lerpTime = 0f;
    float up_blink_t = 0.5f;
    float down_blink_t = 0.5f;
    float blink_t = 0f;
    int blink_count = 0;
    public float changeSpeed = 0.5f;
    public Sprite image;
    bool bgm_played = false;

    GameObject final_boss;
    finnal_boss_manager fb_scirpt;

    private GameObject gm;
    gm_stage gm_script;
    // Start is called before the first frame update

    //player무적 상태 변수
    GameObject player;
    PlayerCtrl player_script;

    private bool is_outside = true;

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        gm = GameObject.FindWithTag("manager");
        gm_script = gm.GetComponent<gm_stage>();
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();
        sound = GetComponent<AudioSource>();
        final_boss = GameObject.FindWithTag("final_boss");
        fb_scirpt = final_boss.GetComponent<finnal_boss_manager>();
        sr_color = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 4f)
        {
            is_outside = true;
        }
        else
        {
            is_outside = false;
        }

        if (!sp_script.stop_world)
        {
            if (player_script.is_spawn)
            {
                delay_t += Time.deltaTime;
                shooting_t += Time.deltaTime;
                if (shooting_t > 2.5f)
                {
                    if (delay_t > 0.2f)
                    {
                        if (count < 2) //총알 발사 횟수
                        {
                            Instantiate(parts3_bullet, fire_pos.position, Quaternion.identity);
                            count++;
                        }
                        else
                        {
                            count = 0;
                            shooting_t = 0f;
                        }
                        delay_t = 0f;
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

            if (attacked)
            {
                blink_t += Time.deltaTime;
                if (blink_t > 0.5)
                {
                    if (blink_count < 3)
                    {
                        StartCoroutine(FadeInRoutine());
                        blink_t = 0f;
                        blink_count++;
                    }
                    else
                    {
                        if (!bgm_played)
                        {
                            bgm_played = true;
                            transform.GetComponent<SpriteRenderer>().sprite = image;
                            sound.mute = false;
                            sound.Play();
                        }
                        lerpTime += Time.deltaTime * changeSpeed;
                        transform.localScale = Vector3.Lerp(big_size, small_size, lerpTime);

                        // 크기 감소가 완료되면 오브젝트 파괴
                        if (lerpTime >= 0.5f)
                        {
                            fb_scirpt.is_destroy[2] = true;
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }
    IEnumerator FadeInRoutine()
    {
        float currentTime = 0.0f;
        float down_currentTime = 0f;
        Color currentColor = sr_color.color;

        while (currentTime < up_blink_t)
        {
            float alpha = Mathf.Lerp(0, 1, currentTime / up_blink_t);
            sr_color.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        // 최종 투명도를 1로 설정
        sr_color.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);

        while (down_currentTime > down_blink_t)
        {
            float alpha = Mathf.Lerp(1, 0, down_currentTime / down_blink_t);
            sr_color.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            down_currentTime += Time.deltaTime;
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!is_outside)
        {
            if (collision.tag == "Bullet")
            {
                if (!attacked)
                {
                    hp--;
                    Destroy(collision.gameObject);
                    sr_color.color = Color.red;
                    attack_color = true;
                    if (hp <= 0)
                    {
                        gm_script.basic_score += 10;
                        sr_color.color = Color.white;
                        attacked = true;
                        lerpTime = 0f;
                        Destroy(collision.gameObject);
                    }
                }
            }
            else if(collision.tag == "p_lazer")
            {
                if (!attacked)
                {
                    hp -= 10;
                    sr_color.color = Color.red;
                    attack_color = true;
                    if (hp <= 0)
                    {
                        gm_script.basic_score += 10;
                        sr_color.color = Color.white;
                        attacked = true;
                        lerpTime = 0f;
                    }
                }
            }
        }
    }
}
