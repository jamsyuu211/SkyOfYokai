using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parts3_bullet : MonoBehaviour
{
    public float b_speed = 4.0f;
    private GameObject player;

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        player = GameObject.FindWithTag("Player");
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_script.stop_world)
        {
            transform.Translate(Vector2.down * b_speed * Time.deltaTime);
        }
        if (transform.position.y >= 5 || transform.position.x >= 3 ||
            transform.position.y <= -5 || transform.position.x <= -3)
        {
            Destroy(gameObject);
        }
    }
}
