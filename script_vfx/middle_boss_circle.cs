using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middle_boss_circle : MonoBehaviour
{
    float lerpTime = 0f;
    float down_lerpTime = 0f;
    float changeSpeed = 0.8f;
    Vector3 big_size = new Vector3(25f, 25f);
    Vector3 small_size = new Vector3(0f, 0f);
    float maintain = 0f;
    private GameObject player;
    PlayerCtrl player_script;
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    private void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();
        player_script.is_mb_circle_spawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_script.stop_world)
        {
            maintain += Time.deltaTime;
            if (maintain < 7f)//영역전개 유지시간
            {
                lerpTime += Time.deltaTime * changeSpeed;
                lerpTime = Mathf.Clamp(lerpTime, 0f, 1f); //0과 1사이로 범위제한 / 밑에 구문의 3번째 매개변수가 0과 1사이어야 해서
                transform.localScale = Vector3.Lerp(small_size, big_size, lerpTime);
            }
            else
            {
                down_lerpTime += Time.deltaTime * changeSpeed;
                transform.localScale = Vector3.Lerp(big_size, small_size, down_lerpTime);
                if (transform.localScale.x <= 0.1f && transform.localScale.y <= 0.1f)
                {
                    player_script.is_mb_circle_spawn = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}
