using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_boss_hp : MonoBehaviour
{
    public SpriteRenderer[] black_bg;
    private GameObject final_boss;
    finnal_boss_manager fb_script;
    public int fb_index = 9;
    public bool[] is_color = new bool[10];
    // Start is called before the first frame update
    void Start()
    {
        final_boss = GameObject.FindWithTag("final_boss");
        fb_script = final_boss.GetComponent<finnal_boss_manager>();
        for (int i = 0; i < 10; i++)
        {
            is_color[i] = true;
            black_bg[i].color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fb_script.death_fb)
        {
            Destroy(GameObject.FindWithTag("fb_hp_bar"));

        }
    }
}
