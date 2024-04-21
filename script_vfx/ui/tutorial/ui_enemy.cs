using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_enemy : MonoBehaviour
{
    public Sprite[] icon;
    private SpriteRenderer sr_icon;
    float trans_t = 0f;
    Vector3 enemy_big_size = new Vector3(0.7f, 0.7f,1f);
    Vector3 mb_big_size = new Vector3(0.3f, 0.3f, 1f);
    Vector3 small_size = new Vector3(0f, 0f, 0f);
    float down_t = 0f;
    float up_t = 0f;
    bool which_image = false;
    bool which_size = false; //false = small / true = big
    // Start is called before the first frame update
    void Start()
    {
        sr_icon = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        trans_t += Time.deltaTime;
        if (trans_t < 2f)
        {
            if (!which_image)
            {
                which_image = !which_image;
                sr_icon.sprite = icon[0];
                transform.localScale = small_size;
                up_t = 0f;
                down_t = 0f;
            }

            if (transform.localScale.x <= 0.01f && transform.localScale.y <= 0.01f)
            {
                which_size = false;
            }
            else if (transform.localScale.x >= 0.69f && transform.localScale.y >= 0.69f)
            {
                which_size = true;
            }

            if (!which_size)
            {
                up_t += Time.deltaTime;
                transform.localScale = Vector3.Lerp(small_size, enemy_big_size, up_t);
            }
            else
            {
                down_t += Time.deltaTime;
                transform.localScale = Vector3.Lerp(enemy_big_size, small_size, down_t);
                if (transform.localScale.x <= 0.01f && transform.localScale.y <= 0.01f)
                {
                    trans_t = 2f;
                }
            }
        }
        else if (trans_t < 4f)
        {
            if (which_image)
            {
                which_image = !which_image;
                sr_icon.sprite = icon[1];
                transform.localScale = small_size;
                up_t = 0f;
                down_t = 0f;
            }

            if (transform.localScale.x <= 0.01f && transform.localScale.y <= 0.01f)
            {
                which_size = false;
            }
            else if (transform.localScale.x >= 0.29f && transform.localScale.y >= 0.29f)
            {
                which_size = true;
            }

            if (!which_size)
            {
                up_t += Time.deltaTime;
                transform.localScale = Vector3.Lerp(small_size, mb_big_size, up_t);
            }
            else
            {
                down_t += Time.deltaTime;
                transform.localScale = Vector3.Lerp(mb_big_size, small_size, down_t);
                if (transform.localScale.x <= 0.01f && transform.localScale.y <= 0.01f)
                {
                    trans_t = 4f;
                }
            }
        }
        else
        {
            which_size = false;
            trans_t = 0f;
        }
    }
}
