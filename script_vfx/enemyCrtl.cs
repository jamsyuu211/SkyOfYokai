using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemyCrtl : MonoBehaviour
{
    [SerializeField] private int e_Speed;
    [SerializeField] private int e_Type;
    [SerializeField] private int b_Type;
    private Transform targetTr;

    private int iter = 0;
    private float attackPoint;

    public GameObject e_bullet;
    // Start is called before the first frame update
    void Start()
    {
        attackPoint = Random.Range(2.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        iter++;
        targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Vector2 attackDirection = (targetTr.position - transform.position).normalized;
        float _angle = Mathf.Atan2(attackDirection.x, -attackDirection.y) * Mathf.Rad2Deg;
        Quaternion _rot = Quaternion.Euler(0f, 0f, _angle + 180);
        if (transform.position.y > attackPoint)
        {
            switch(e_Type)
            {
                case 1:
                    transform.Translate(Vector2.down * e_Speed * Time.deltaTime, Space.World);
                    break;
                case 2:
                    transform.rotation = _rot;
                    transform.Translate(Vector2.down * e_Speed * Time.deltaTime, Space.World);
                    break;
                case 3:
                    transform.Translate(Vector2.down * e_Speed * Time.deltaTime, Space.World);
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (iter / 120 > 5) //프레임마다 iter이 1씩커짐으로 120은 120프레임을 의미함
            {
                switch (e_Type)
                {
                    case 1:
                        transform.Translate(Vector2.down * e_Speed * Time.deltaTime, Space.World);
                        break;
                    case 2:
                        transform.rotation = _rot;
                        transform.Translate(Vector2.down * e_Speed * Time.deltaTime, Space.World);
                        break;
                    case 3:
                        transform.rotation = _rot;
                        transform.Translate(Vector2.up * e_Speed * Time.deltaTime, Space.Self);
                        break;
                    default:
                        break;
                }
            }
            else if (iter % 120 == 0)
            {
                switch (b_Type)
                {
                    case 1:
                        Instantiate(e_bullet, transform.position, Quaternion.Euler(0f, 0f, 180f));
                        break;
                    case 2:
                        Instantiate(e_bullet, transform.position, _rot);
                        break;
                    case 3:
                        Instantiate(e_bullet, transform.position, Quaternion.Euler(0f, 0f, _angle + 150f));
                        Instantiate(e_bullet, transform.position, Quaternion.Euler(0, 0, _angle + 210));
                        break;
                    case 4:
                        for (int i = 0; i < 5; i++)
                        {
                            Instantiate(e_bullet, transform.position, Quaternion.Euler(0f, 0f, 140 + i* 20));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
