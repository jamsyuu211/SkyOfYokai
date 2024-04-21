using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ui_hp : MonoBehaviour
{
    private int hp = 20; // ���� ���ھ�
    private TextMeshProUGUI scoreText; // ���ھ ǥ���� Text ���
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
        scoreText.text = hp.ToString(); // Text ��� ������Ʈ
    }

    public void AddScore(int amount)
    {
        hp = amount; // ���ھ� �߰�
        UpdateScoreUI(); // UI ������Ʈ
    }
}
