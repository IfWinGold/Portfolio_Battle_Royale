using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cam;
    public Transform one;
    public Transform spine;
    Cam g_cam;
    Animator anim;
    public float mx = 0f;
    public float my = 0f;


    public float rotspeed = 200f;
    void Start()
    {
        anim = GetComponent<Animator>();       
        g_cam = cam.GetComponent<Cam>();
    }

    // Update is called once per frame
    void Update()
    {        
        float mouse_x = Input.GetAxis("Mouse X");                
        mx += mouse_x * rotspeed * Time.deltaTime;                        
        transform.eulerAngles = new Vector3(0f, mx, 0f);                        
    }
}
