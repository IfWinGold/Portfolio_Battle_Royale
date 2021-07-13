using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float my;
    float rotspeed = 200f;
    PlayerRotate playerrotate;
    // Start is called before the first frame update
    void Start()
    {
        playerrotate = GetComponentInParent<PlayerRotate>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouse_y = Input.GetAxis("Mouse Y");

        my += mouse_y * rotspeed * Time.deltaTime;
        my = Mathf.Clamp(my, -90f, 90f);
        transform.eulerAngles = new Vector3(-my,playerrotate.mx, 0f);
    }
}
