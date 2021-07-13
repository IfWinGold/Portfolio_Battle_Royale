using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cam : MonoBehaviour
{
    public float rotspeed = 200f;
    public Transform player;
    public Transform firstposition;
    public Transform tempposition;
    public Transform origin;
    float my;
    bool firstswitch = false;
    bool b_hit;
    
    
    RaycastHit hitInfo;
    /*
    1. 카메라를 항상 플레이어의 뒤쪽에 위치시키기
    2. 카메라를 마우스 방향에 맞춰서 회전시키기
     */

    Vector3 dir;

    void Start()
    {
        hitInfo = new RaycastHit();
    }


    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward*100f);
        
        b_hit = Physics.Raycast(ray, out hitInfo);        
        
        dir = hitInfo.point;
        
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward*100f,Color.green);
        //허공에다가 쏠경우
        

        if(Input.GetMouseButtonDown(1))
        {
            switch (firstswitch)
            {
                case false:
                    firstswitch = true;
                    transform.position = firstposition.position;
                    //Camera.main.cullingMask = ~(1 << 8);                    
                    break;
                case true:
                    firstswitch = false;
                    transform.position = origin.position;
                    //Camera.main.cullingMask = -1;
                    break;                    
            }
        }
        //print("Ray dir : " + hitInfo.point);

    }
    public Vector3 Getdir()
    {        
        if(dir == Vector3.zero)
        {
            return Vector3.zero;
        }
        else
        {
            print("dir");
            return dir;
        }        
    }
    public Vector3 Bombdir()
    {
        return tempposition.position;
    }
    public GameObject Getobject()
    {
        return hitInfo.collider.gameObject;
    }
    public bool hitItem()
    {
        if(b_hit != false)
        {
            if (hitInfo.collider.tag == "Weapon")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }



}
