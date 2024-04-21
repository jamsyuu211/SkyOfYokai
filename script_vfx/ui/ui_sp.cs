using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ui_sp : MonoBehaviour
{
    private int sp_point = 0;
    private GameObject sp;
    ui_sp_bar sp_script;
    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        sp = GameObject.FindWithTag("sp_bar");
        sp_script = sp.GetComponent<ui_sp_bar>();
    }
    void UpdateSpUI()
    {
        scoreText.text = sp_point.ToString(); // Text 요소 업데이트
    }
    public void AddSp(int amount)
    {
        sp_point = amount; // 스코어 추가
        UpdateSpUI(); // UI 업데이트
    }

    // Update is called once per frame
    void Update()
    {
        AddSp(sp_script.bg_index);
    }
}
