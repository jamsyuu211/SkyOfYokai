using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class score_board : MonoBehaviour
{
    public int score = 0; // 현재 스코어
    private TextMeshProUGUI scoreText; // 스코어를 표시할 Text 요소
    private GameObject gm;
    gm_stage manager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gm = GameObject.FindWithTag("manager");
        manager = gm.GetComponent<gm_stage>();
    }

    // Update is called once per frame
    void Update()
    {
        AddScore(manager.coin_count);
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score : " + score.ToString(); // Text 요소 업데이트
    }

    public void AddScore(int amount)
    {
        score = amount; // 스코어 추가
        UpdateScoreUI(); // UI 업데이트
    }

}
