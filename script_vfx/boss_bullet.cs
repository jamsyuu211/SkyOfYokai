using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_bullet : MonoBehaviour
{
    public float b_speed = 1.0f;
    private GameObject player;

    //플레이어 스킬 관련변수
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!sp_script.stop_world)
        {
            transform.Translate(Vector2.down * b_speed * Time.deltaTime, Space.Self);
        }
        if (transform.position.y >= 6 || transform.position.x >= 4 ||
            transform.position.y <= -6 || transform.position.x <= -4)
        {
            Destroy(gameObject);
        }
    }
}
