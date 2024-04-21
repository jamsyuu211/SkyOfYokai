using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_sp_bar : MonoBehaviour
{
    public SpriteRenderer[] black_bg;
    float t = 0f;
    public int bg_index = 0;
    public GameObject player;
    PlayerCtrl player_scirpt;
    public bool use_skill = false;
    public GameObject sp_skill;
    sp_skill_1 sp_skill_script;
    public bool get_sp = true;
    private GameObject circle;
    circle_scale_up circle_script;


    //sp스킬 버튼 관련 변수
    bool is_click;
    bool is_touch;
    private AudioSource sound;
    private SpriteRenderer sp_bar_color;
    public GameObject skill_button;
    public SpriteRenderer sp_button_bg;
    float change_color_t = 0f;
    bool is_push = false;
    bool is_change_color = false;
    bool is_playing_sound = false;
    Color red_color = new Color(1f, 0f, 0f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        sp_bar_color = skill_button.GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        sp_skill_script = sp_skill.GetComponent<sp_skill_1>();
        player = GameObject.FindWithTag("Player");
        player_scirpt = player.GetComponent<PlayerCtrl>();
        for (int i = 0; i < 10; i++)
        {
            black_bg[i].color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (get_sp)
        {
            t += Time.deltaTime;
            if (t > 6f)//게이지 1칸 채워지는 시간
            {
                if (bg_index < 10)
                {
                    black_bg[bg_index++].color = Color.clear;
                }
                t = 0f;
            }
        }


        if (player_scirpt.is_spawn)
        {
            if (!player_scirpt.isMoving)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector2 touch_pos = Camera.main.ScreenToWorldPoint(new Vector2(touch.position.x, touch.position.y));
                    is_touch = (touch_pos.x < 2.15 && touch_pos.x > 1.55) && (touch_pos.y < -4.3 && touch_pos.y > -4.9);
                }
            }
                //if (Input.GetMouseButtonDown(0))
                //{
                //    Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //    is_click = (mouse_pos.x < 2.15 && mouse_pos.x > 1.55) && (mouse_pos.y < -4.2 && mouse_pos.y > -5.0);
                //}


                if (is_change_color)
            {
                change_color_t += Time.deltaTime;
                if (change_color_t > 0.5f)
                {
                    sp_bar_color.color = Color.white;
                    sp_button_bg.color = new Color(0.2705882f, 0.6313726f, 0.8705883f, 1f);
                    change_color_t = 0f;
                    is_change_color = false;
                    is_playing_sound = !is_playing_sound;
                }
            }


            if (Input.GetKeyDown(KeyCode.R) || is_touch)
            {
                is_click = false;
                is_touch = false;
                if (bg_index == 10) //스킬 사용
                {
                    get_sp = false;
                    bg_index = 0;
                    use_skill = true;
                    for (int i = 0; i < 10; i++)
                    {
                        black_bg[i].color = Color.black;
                    }
                }
                else //에러 출력
                {
                    is_push = true;
                    if (is_push)
                    {
                        is_change_color = true;
                        is_push = false;
                        if (!is_playing_sound)
                        {
                            is_playing_sound = !is_playing_sound;
                            sound.mute = false;
                            sound.Play();
                        }
                        sp_bar_color.color = red_color;
                        sp_button_bg.color = red_color;
                    }
                }
            }
        }
    }
}
