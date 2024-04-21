using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parts1_hp : MonoBehaviour
{
    public SpriteRenderer[] black_bg;
    private GameObject parts;
    parts1_shooting parts1_script;
    public int hp_index = 9;
    public bool[] is_color = new bool[10];
    private GameObject hp_bar;
    // Start is called before the first frame update
    void Start()
    {
        hp_bar = GameObject.FindWithTag("parts1_hb");
        parts = GameObject.FindWithTag("parts1");
        parts1_script = parts.GetComponent<parts1_shooting>();
        for (int i = 0; i < 10; i++)
        {
            is_color[i] = false;
            black_bg[i].color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(parts1_script.hp <= 0)
        {
            Destroy(hp_bar);
        }
    }
}
