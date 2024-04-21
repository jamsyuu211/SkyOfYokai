using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfx_lazer_rotate : MonoBehaviour
{
    public float shooting_speed;
    GameObject sp;
    sp_skill_1 sp_script;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 0.95f && !animator.IsInTransition(0))
        {
            Destroy(gameObject);
        }
        if (sp_script.stop_world)
        {
            animator.StopPlayback();
        }
        else
        {
            animator.Play("lazer");
        }
    }
}
