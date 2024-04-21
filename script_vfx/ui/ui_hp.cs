using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ui_hp : MonoBehaviour
{
    private int hp = 20; // 현재 스코어
    private TextMeshProUGUI scoreText; // 스코어를 표시할 Text 요소
    private GameObject player;
    PlayerCtrl player_script;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_script.is_move_point)
        {
            AddScore(player_script.hp);
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = hp.ToString(); // Text 요소 업데이트
    }

    public void AddScore(int amount)
    {
        hp = amount; // 스코어 추가
        UpdateScoreUI(); // UI 업데이트
    }
}
