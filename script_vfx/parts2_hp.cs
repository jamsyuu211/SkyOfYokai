using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class parts2_hp : MonoBehaviour
{
    public SpriteRenderer[] black_bg;
    private GameObject parts;
    parts2_shooting parts2_script;
    private GameObject hp_bar;
    public int hp_index = 9;
    public bool[] is_color = new bool[10];
    // Start is called before the first frame update
    void Start()
    {
        hp_bar = GameObject.FindWithTag("parts2_hb");
        parts = GameObject.FindWithTag("parts2");
        parts2_script = parts.GetComponent<parts2_shooting>();
        for (int i = 0; i < 10; i++)
        {
            black_bg[i].color = Color.clear;
            is_color[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parts2_script.hp <= 0)
        {
            Destroy(hp_bar);
        }
    }
}
