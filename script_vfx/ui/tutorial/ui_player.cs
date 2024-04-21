using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ui_player : MonoBehaviour
{
    public float rotation_speed;
    bool is_rotate = false; //true == 90도 이상 / false == -90도 이하
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentRotationAngle = transform.eulerAngles.z;
        currentRotationAngle = (currentRotationAngle > 180) ? currentRotationAngle - 360 : currentRotationAngle;
        if (currentRotationAngle > 45f) 
        {
            is_rotate = true;
        }
        else if(currentRotationAngle < -45f)
        {
            is_rotate= false;
        }
        if(!is_rotate)
        {
            Vector3 angle = new Vector3(0f, 0f, 20f);
            transform.Rotate(angle * rotation_speed * Time.deltaTime);
        }
        else
        {
            Vector3 angle = new Vector3(0f, 0f, -20f);
            transform.Rotate(angle * rotation_speed * Time.deltaTime);
        }
    }
}
