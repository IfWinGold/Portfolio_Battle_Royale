using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;    
    public float power;
    public float speed;
    Transform temp;
    bool zero;
    Vector3 v_dir;
    Vector3 dir;
    Cam cam;
    
    void Start()
    {
        temp = GameObject.Find("tempPosition").transform;
        cam = Camera.main.GetComponent<Cam>();
        //목표지점의 방향을 구한다.
        v_dir = cam.Getdir();
        if(v_dir == Vector3.zero)
        {
            zero = true;
            dir = (temp.position - transform.position).normalized;
        }
        else
        {
            zero = false;
        }

        
        transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles);
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(zero == true)
        {
            transform.position += dir * 200f * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, v_dir, speed);
        }              
        StartCoroutine(c_destroy());
    }
    void Damages(float power)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //print("hit");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.hp -= power;
            enemy.Forcedmovement();
            Destroy(this.gameObject);
        }        
    }
    IEnumerator c_destroy()
    {        
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
