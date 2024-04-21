using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //2d
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_pos.z = 0;

            Debug.DrawRay(mouse_pos, Vector3.forward * 10, Color.red, 0.3f);

            //3d
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);

            //레이 캐스트
            RaycastHit2D hit = Physics2D.Raycast(mouse_pos, transform.forward, 0.5f);

            //색상 변경
            hit.transform.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
