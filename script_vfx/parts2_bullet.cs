using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parts2_bullet : MonoBehaviour
{
    public float b_speed = 3.0f;
    public float rotate_ratio = 0.5f;

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
            Vector3 rotating = new Vector3(0f, 0f, 30f);
            transform.Translate(Vector2.down * b_speed * Time.deltaTime);
            transform.Rotate(rotating * rotate_ratio * Time.deltaTime, Space.World);
        }
        if (transform.position.y >= 5 || transform.position.x >= 3 ||
            transform.position.y <= -5 || transform.position.x <= -3)
        {
            Destroy(gameObject);
        }
    }
}
