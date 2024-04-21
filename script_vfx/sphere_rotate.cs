using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_rotate : MonoBehaviour
{
    public float rotation_speed;
    Vector3 rotate;
    GameObject sp;
    sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
        rotate = new Vector3(30f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!sp_script.stop_world)
        {
            transform.Rotate(rotate * rotation_speed * Time.deltaTime);
        }
    }
}
