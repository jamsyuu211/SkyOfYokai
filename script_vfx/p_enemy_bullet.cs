using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_enemy_bullet : MonoBehaviour
{
    public float b_speed = 1.0f;
    private GameObject player;

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            sp = GameObject.FindWithTag("sp_skill");
            sp_script = sp.GetComponent<sp_skill_1>();
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            // 방향 벡터를 바탕으로 Z축 회전 각도를 계산합니다. (라디안 to 도)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_script.stop_world)
        {
            transform.Translate(Vector2.up * b_speed * Time.deltaTime, Space.Self);
        }
        if (transform.position.y > 6 || transform.position.x > 4 ||
            transform.position.y < -6 || transform.position.x < -4)
        {
            Destroy(gameObject);
        }
    }
}
