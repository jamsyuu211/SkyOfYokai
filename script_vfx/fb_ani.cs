using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fb_ani : MonoBehaviour
{
    float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > 3f)
        {
            Destroy(gameObject);
        }
    }
}
