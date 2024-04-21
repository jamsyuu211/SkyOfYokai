using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_cloud : MonoBehaviour
{
    public float scroll_speed;
    private GameObject sp;
    private sp_skill_1 sp_script;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindWithTag("sp_skill");
        sp_script = sp.GetComponent<sp_skill_1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_script.stop_world)
        {
            transform.Translate(Vector2.down * (scroll_speed + Random.Range(0f, 2f))* Time.deltaTime);
            if (transform.position.y < -7f)
            {
                transform.position = new Vector3(Random.Range(-1.8f, 1.8f), 6.5f, 0f);
            }
        }
    }
}
