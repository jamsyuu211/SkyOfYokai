using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_hp_bar : MonoBehaviour
{
    public SpriteRenderer[] black_bg;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            black_bg[i].color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
