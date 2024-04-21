using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gm_stage : MonoBehaviour
{
    public GameObject enemy;
    public float speed = 0.5f;
    public float t = 1f;
    public int coin_count = 0;

    //basic enemy 변수
    EnemyCtrl target_script;
    public int spawn_counting = 0;

    public GameObject boss;
    public GameObject final_boss;
    public bool[] boss_spawn = new bool[4];
    public bool[] b_check = new bool[4];
    public bool final_boss_spawn = false;
    public bool fb_check = false;

    //player무적 상태 변수
    public GameObject player;
    bool is_spawn_player = true;
    PlayerCtrl player_script;
    finnal_boss_manager fb_script;

    //파이널 스코어 변수
    public int basic_score = 0; //적기 파괴점수
    public int total_score = 0; //코인 획득 점수, 생존시간 점수
    public bool is_basic_ending = false;
    public bool is_total_ending = false;

    //gm 매니저 재생성 방지 관련 변수
    public static gm_stage instance = null;

    //플레이어 스킬 관련변수
    public GameObject sp;
    private sp_skill_1 sp_script;

    // Start is called before the first frame update
    void game_level(int coin_count)
    {
        for (int i = 1; i <= 5; i++)
        {
            if (coin_count / 40 == i)
            {
                speed = 0.116f * Mathf.Log(i) + 0.2689f;
                break;
            }
        }
    }

    void spawn_boss(int coin_count)
    {
        for (int i = 0; i <= 4; i++)
        {
            if (coin_count / 40 == i + 1)
            {
                if (coin_count % 40 == 0)
                {
                    if (i == 4) //level이 5면(coin_count가 125이상이면
                    {
                        if (final_boss_spawn) return;
                        final_boss_spawn = true;
                        break;
                    }
                    else//level이 5미만이면(coin_count가 125미만이면
                    {
                        if (boss_spawn[i]) return;
                        boss_spawn[i] = true;
                        break;
                    }
                }
            }
        }
    }
     
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            GameObject tmp_sp_skill = GameObject.FindWithTag("sp_skill");
            //GameObject tmp_player = GameObject.FindWithTag("Player");
            //if (tmp_player != null)
            //{
            //    player = tmp_player;
            //    player_script = player.GetComponent<PlayerCtrl>();
            //}
            if (tmp_sp_skill != null)
            {
                sp = tmp_sp_skill;
                sp_script = sp.GetComponent<sp_skill_1>();
            }
        }
        else if (instance != this)
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        basic_score = 0;
        total_score = 0;
        is_total_ending = false;
        is_basic_ending = false;
        player = GameObject.FindWithTag("Player");
        fb_script = final_boss.GetComponent<finnal_boss_manager>();
        player_script = player.GetComponent<PlayerCtrl>();
        for (int i = 0; i < 4; i++)
        {
            b_check[i] = false;
            boss_spawn[i] = false;
        }
        target_script = enemy.GetComponent<EnemyCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "plane_shooting")
        {
            if (!is_spawn_player)
            {
                is_spawn_player = true;
                Instantiate(player, player.transform.position, Quaternion.identity);
            }
            if (!sp_script.stop_world)
            {
                if (player_script.plane_type == 4)
                {
                    is_basic_ending = true;
                    is_total_ending = true;
                }
                if (player_script.is_move_point)
                {
                    game_level(coin_count);
                    spawn_boss(coin_count);
                    for (int i = 0; i <= 3; i++)
                    {
                        if (boss_spawn[i] == true)
                        {
                            if (!b_check[i])
                            {
                                boss_spawn[i] = false;
                                Instantiate(boss, boss.transform.position, Quaternion.identity);
                                b_check[i] = true;
                                break;
                            }
                        }
                    }
                    if (final_boss_spawn)
                    {
                        if (!fb_check)
                        {
                            final_boss_spawn = false;
                            Instantiate(final_boss, final_boss.transform.position, Quaternion.identity);
                            fb_check = true;
                        }
                    }


                    if (spawn_counting < 5)
                    {
                        t += speed * Time.deltaTime;
                        if (t > 0.6f)
                        {
                            Instantiate(enemy, new Vector3(Random.Range(-3f, 3f), 5f, 0f), Quaternion.identity);
                            t = 0f;
                        }
                    }
                }
            }
        }
        else if(SceneManager.GetActiveScene().name == "score")
        {
            is_spawn_player = false;
        }
    }
}
