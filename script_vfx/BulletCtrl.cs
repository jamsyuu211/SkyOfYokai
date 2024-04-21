using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField] private int b_speed = 1;

    //플레이어 스킬 적용시 특수총알 관련 변수
    GameObject sp_skill_button;
    sp_skill_1 sp_script;
    public GameObject p_bullet;
    float split_t = 0f;
    public float split_speed;
    float split_timing = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        sp_skill_button = GameObject.FindWithTag("sp_skill");
        sp_script = sp_skill_button.GetComponent<sp_skill_1>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!sp_script.stop_world)
        {
            transform.Translate(Vector2.up * b_speed * Time.deltaTime, Space.Self);


            if (sp_script.bullet_split)
            {
                split_t += split_speed * Time.deltaTime;
                if (split_t > split_timing)
                {
                    split_t = 0f;
                    for (int i = 0; i < 3; i++)
                    {
                        Instantiate(p_bullet, transform.position + new Vector3(0.3f * i - 0.3f, 0.0f, 0.0f), Quaternion.Euler(0f, 0f, (1f * i - 1f) * 35.0f));
                    }
                    Destroy(gameObject);
                }
            }
        }
        if (transform.position.y >= 5 || transform.position.x >= 3 ||
            transform.position.y <= -5 || transform.position.x <= -3)
        {
            Destroy(gameObject);
        }
    }
}
