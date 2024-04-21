using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomber_ctrl : MonoBehaviour
{
    SpriteRenderer sr;
    private GameObject sp;
    private sp_skill_1 sp_script;
    GameObject player;
    PlayerCtrl player_script;
    public float rotation_speed;

    //hp관련 변수
    int hp = 20;
    bool attack_color = false;
    bool attacked = false;
    public Sprite image;
    float lerpTime = 0f;
    Vector3 small_size = new Vector3(0f, 0f, 0f);
    float changeSpeed = 0.1f;
    float attack_time = 0f;

    //공격 관련 변수
    float timer = 0f;
    public GameObject p_enemy_bullet;
    public Transform firePos;
    float delay_t = 0f;
    int bullet_count = 0;
    AudioSource sound;
    public AudioClip[] audio;
    void fire_bullet()
    {
        Vector3 direction = (transform.position - player.transform.position);
        direction.z = 0;

        Instantiate(p_enemy_bullet, firePos.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        sr = GetComponent<SpriteRenderer>();

        sound.clip = audio[0];
        sound.mute = false;
        sound.volume = 0.25f;
        sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_script.stop_world)
        {
            //플레이어 공격 처리
            if (!attacked)
            {
                if (player_script.is_spawn)
                {
                    timer += Time.deltaTime;
                    if (timer > 4f)
                    {
                        delay_t += Time.deltaTime;
                        if (bullet_count > 4)
                        {
                            timer = 0f;
                            bullet_count = 0;
                        }
                        else
                        {
                            if (delay_t > 0.2f)
                            {
                                fire_bullet();
                                bullet_count++;
                                delay_t = 0;
                            }
                        }
                    }
                }


                //플레이어 조준 처리
                Vector3 direction = (player.transform.position - transform.position).normalized; // 플레이어 바라보게
                direction.z = 0;
                Vector3 rotateAmount = Vector3.Cross(direction, transform.up); //z축 방향으로 회적각을 정함 (외적 공식통해서)

                transform.Rotate(rotateAmount * rotation_speed * Time.deltaTime);
            }
            //피격 관련 처리
            if (attack_color)
            {
                attack_time += Time.deltaTime;
                if (attack_time > 0.5)
                {
                    attack_time = 0;
                    sr.color = Color.white;
                    attack_color = false;
                }
            }

            if (attacked)
            {
                lerpTime += Time.deltaTime * changeSpeed;
                transform.localScale = Vector3.Lerp(transform.localScale, small_size, lerpTime);

                // 크기 감소가 완료되면 오브젝트 파괴
                if (transform.localScale.x < 0.1f && transform.localScale.y < 0.1f)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            sound.mute = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (!attacked)
            {
                hp--;
                sr.color = Color.red;
                attack_color = true;
                Destroy(collision.gameObject);
                if (hp <= 0)
                {
                    sound.clip = audio[1];
                    sound.volume = 0.3f;
                    sound.mute = false;
                    sound.Play();
                    sr.color = Color.white;
                    sr.sprite = image;
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    lerpTime = 0f;
                    attacked = true;
                    Destroy(collision.gameObject);
                }
            }
        }
        else if (collision.tag == "p_lazer")
        {
            if (!attacked)
            {
                hp -= 10;
                sr.color = Color.red;
                attack_color = true;
                if (hp <= 0)
                {
                    sound.clip = audio[1];
                    sound.volume = 0.3f;
                    sound.mute = false;
                    sound.Play();
                    sr.color = Color.white;
                    sr.sprite = image;
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    lerpTime = 0f;
                    attacked = true;
                }
            }
        }
    }
}
