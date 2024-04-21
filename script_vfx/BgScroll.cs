using UnityEngine;

public class BgScroll : MonoBehaviour
{
    public float bg_speed = 3f;

    //플레이어 스킬 관련변수
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
            transform.Translate(Vector2.down * bg_speed * Time.deltaTime, Space.Self);
            if (transform.position.y < -11.5f)
            {
                transform.position = transform.position + new Vector3(0f, 23f, 0f);
            }
        }
    }
}
