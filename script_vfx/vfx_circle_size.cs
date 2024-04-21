using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfx_circle_size : MonoBehaviour
{
    float t = 0;
    float maintain = 0f;
    float down_t = 0f;
    float up_t = 0;
    float up_speed = 1.2f;
    float down_speed = 1.3f;
    bool is_touched = false;
    Vector3 big_size = new Vector3(1f,1f,1f);
    Vector3 small_size = new Vector3(0f, 0f, 0f);
    Vector3 middle_size = new Vector3(0.5f, 0.5f, 0.5f);
    Vector3 ms_size = new Vector3(0.1f, 0.1f, 0.1f);

    public GameObject particle;
    CircleCollider2D bcollider;
    public GameObject player_lazer;


    //레이저 발사 관련 변수
    float destroy_t = 0f;
    float dis_t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.position.x < -2.1f || transform.position.x > 2.1f)
        {
            Destroy(gameObject);
        }
        bcollider = GetComponent<CircleCollider2D>();
        transform.localScale = small_size;    
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_touched)
        {
            maintain += Time.deltaTime;
            Vector3 circle_size;
            if (maintain < 2f)
            {
                circle_size = middle_size - transform.localScale;
                if (!(circle_size.x < 0.1f && circle_size.y < 0.1f))
                {
                    t += Time.deltaTime;
                    transform.localScale = Vector3.Lerp(small_size, middle_size, t);
                }
            }
            else if (maintain < 3f)
            {
                circle_size = transform.localScale - ms_size;
                if (!(circle_size.x < 0.1f && circle_size.y < 0.1f))
                {
                    down_t += Time.deltaTime * down_speed;
                    transform.localScale = Vector3.Lerp(transform.localScale, ms_size, down_t);
                }
            }
            else if (maintain < 4f)
            {
                circle_size = big_size - transform.localScale;
                if (!(circle_size.x < 0.2f && circle_size.y < 0.2f))
                {
                    up_t += Time.deltaTime * up_speed;
                    transform.localScale = Vector3.Lerp(transform.localScale, big_size, up_t);
                }
                else
                {
                    bcollider.enabled = true;
                    Destroy(particle);
                }
            }
        }
        else  //레이저 발사
        {
            destroy_t += Time.deltaTime;
            dis_t += Time.deltaTime;
            if (!(transform.localScale.x < 0.1f && transform.localScale.y < 0.1f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, small_size, dis_t);
            }
            else
            {
                Instantiate(player_lazer, transform.position + new Vector3(0f,0f,1f), Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            is_touched = true;
        }
    }
}
