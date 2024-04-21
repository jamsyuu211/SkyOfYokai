using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parts3_hp : MonoBehaviour
{
    public SpriteRenderer[] black_bg;
    private GameObject parts;
    parts3_shooting parts3_script;
    private GameObject hp_bar;
    public int hp_index = 9;
    public bool[] is_color = new bool[10];
    // Start is called before the first frame update
    void Start()
    {
        hp_bar = GameObject.FindWithTag("parts3_hb");
        parts = GameObject.FindWithTag("parts3");
        parts3_script = parts.GetComponent<parts3_shooting>();
        for (int i = 0; i < 10; i++)
        {
            is_color[i] = false;
            black_bg[i].color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parts3_script.hp <= 0)
        {
            Destroy(hp_bar);
        }
    }
}
