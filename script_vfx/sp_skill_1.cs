using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class sp_skill_1 : MonoBehaviour
{
    public GameObject circle;
    public GameObject player;
    public GameObject sp;
    circle_scale_up circle_script;
    ui_sp_bar sp_script;
    PlayerCtrl player_scirpt;
    public bool is_create_circle = false;
    public bool stop_world = false;
    private float stop_t = 0f;
    public bool is_shake = false;
    public bool bullet_split = false;
    public bool is_effect = false;


    // Start is called before the first frame update
    void Start()
    {
        sp_script = sp.GetComponent<ui_sp_bar>();
        player_scirpt = player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_scirpt.is_spawn)
        {
            if (sp_script.use_skill)
            {
                stop_world = true;
                stop_t += Time.deltaTime;
                if (stop_t < 1f)
                {
                    //ÀÌÆåÆ®
                    if (!is_shake)
                    {
                        is_shake = true;
                        if (!is_create_circle)
                        {
                            is_create_circle = true;
                        }
                    }

                }
                else
                {
                    stop_t = 0f;
                    stop_world = false;
                    if (is_create_circle)
                    {
                        Instantiate(circle);
                        is_shake = false;
                        sp_script.use_skill = false;
                        bullet_split = true;
                    }
                }
            }
        }
    }
}
