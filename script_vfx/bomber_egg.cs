using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomber_egg : MonoBehaviour
{
    Animator animator;
    public float move_speed;
    bool is_touch = false;
    float maintain = 0f;
    float size_t = 0f;
    Vector3 small_size = new Vector3(0f, 0f, 0f);
    public GameObject bomber;
    GameObject player;
    PlayerCtrl player_script;
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();
        animator = GetComponent<Animator>();
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_script.stop_world)
        {
            if (!is_touch)
            {
                transform.Translate(Vector3.down * move_speed * Time.deltaTime);
                if (transform.position.y < -6f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                maintain += Time.deltaTime;
                if (maintain < 2f)
                {
                    animator.SetTrigger("touched");
                }
                else
                {
                    size_t += Time.deltaTime;
                    transform.localScale = Vector3.Lerp(transform.localScale, small_size, size_t);

                    if (transform.localScale.x < 0.1f && transform.localScale.y < 0.1f)
                    {
                        Instantiate(bomber, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
            }
        }
        if (!player_script.is_spawn)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player_script.is_spawn)
        {
            if (collision.tag == "Player")
            {
                is_touch = true;
            }
        }
    }
}
