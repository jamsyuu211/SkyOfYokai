using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_move : MonoBehaviour
{
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir;
        if (h == 0 || v == 0)
        {
            moveDir = (Vector3.up * v + Vector3.right * h);
        }
        else
        {
            moveDir = (Vector3.up * v / MathF.Sqrt(2) + Vector3.right * h / MathF.Sqrt(2));
        }
        Vector3 move = moveDir * speed * Time.deltaTime;
        transform.Translate(move);
    }
}
