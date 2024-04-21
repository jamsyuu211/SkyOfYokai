using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class score_board : MonoBehaviour
{
    public int score = 0; // ���� ���ھ�
    private TextMeshProUGUI scoreText; // ���ھ ǥ���� Text ���
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
        scoreText.text = "Score : " + score.ToString(); // Text ��� ������Ʈ
    }

    public void AddScore(int amount)
    {
        score = amount; // ���ھ� �߰�
        UpdateScoreUI(); // UI ������Ʈ
    }

}
