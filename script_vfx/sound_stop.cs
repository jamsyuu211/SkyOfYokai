using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_stop : MonoBehaviour
{
    private AudioSource audio;

    //�÷��̾� ��ų ���ú���
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_script.stop_world)
        {
            audio.enabled = true;
        }
        else
        {
            audio.enabled = false;
        }
    }
}
