using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ani_size : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        for (int i = 0; i < 10; i++)
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }

    }
}
