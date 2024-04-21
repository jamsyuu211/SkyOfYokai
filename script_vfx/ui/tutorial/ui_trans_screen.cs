using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_trans_screen : MonoBehaviour
{
    public GameObject[] ui_obj;
    public GameObject my_sprite;
    private AudioSource sound;
    Vector2 mouse_pos;
    Vector2 touch_pos;
    int obj_index = 0;
    bool is_change = false;
    bool is_touch = false;
    // Start is called before the first frame update
    void Start()
    {
        sound = my_sprite.GetComponent<AudioSource>();
        Instantiate(ui_obj[obj_index]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        { 
            Touch touch = Input.GetTouch(0);
            touch_pos = Camera.main.ScreenToWorldPoint(new Vector2(touch.position.x, touch.position.y));
            if (touch_pos.x > 0.9f && touch_pos.x < 1.87f && touch_pos.y > -4.7f && touch_pos.y < 3.29f)
            {
                sound.mute = false;
                sound.Play();
                if (obj_index < 5)
                {
                    obj_index++;
                }
                else
                {
                    obj_index = 0;
                }
                is_change = true;
            }

            if (is_change)
            {
                is_change = false;
                if (obj_index > 0)
                {
                    Destroy(GameObject.FindWithTag(ui_obj[obj_index - 1].tag));
                }
                else
                {
                    Destroy(GameObject.FindWithTag(ui_obj[5].tag));
                }
                Instantiate(ui_obj[obj_index]);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            sound.mute = false;
            sound.Play();
            if (obj_index < 5)
            {
                obj_index++;
            }
            else
            {
                obj_index = 0;
            }
            is_change = true;

            if (is_change)
            {
                is_change = false;
                if (obj_index > 0)
                {
                    Destroy(GameObject.FindWithTag(ui_obj[obj_index - 1].tag));
                }
                else
                {
                    Destroy(GameObject.FindWithTag(ui_obj[5].tag));
                }
                Instantiate(ui_obj[obj_index]);
            }
        }
    }
}
