using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parts4_hp : MonoBehaviour
{
    public SpriteRenderer[] black_bg;
    private GameObject parts;
    parts4_shooting parts4_script;
    private GameObject hp_bar;
    public bool[] is_color = new bool[10];
    public int hp_index = 9;
    // Start is called before the first frame update
    void Start()
    {
        hp_bar = GameObject.FindWithTag("parts4_hb");
        parts = GameObject.FindWithTag("parts4");
        parts4_script = parts.GetComponent<parts4_shooting>();
        for (int i = 0; i < 10; i++)
        {
            is_color[i] = false;
            black_bg[i].color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parts4_script.hp <= 0)
        {
            Destroy(hp_bar);
        }
    }
}
