using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            transform.Rotate(new Vector3(0, -50 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            transform.Rotate(new Vector3(0, 50 * Time.deltaTime, 0));
        }
        if(Input.GetKey(KeyCode.J))
        {
            transform.Rotate(new Vector3(-50 * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(new Vector3(50 * Time.deltaTime, 0, 0));
        }
    }
}
