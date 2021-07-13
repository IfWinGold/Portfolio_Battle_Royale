using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody rigid;
    public float speed;
    public float power;
    
    Vector3 v_dir;
    Transform t_player;
    PlayerMove g_player;
    
    
    
    void Start()
    {
        g_player = GameObject.Find("Player").GetComponent<PlayerMove>();
        t_player = GameObject.Find("Player").transform;
        //목표지점의 방향을 구한다.
        v_dir = (t_player.position - transform.position).normalized;

        transform.rotation = Quaternion.Euler(-t_player.transform.eulerAngles);

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += v_dir * speed * Time.deltaTime;
        StartCoroutine(c_Destroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            g_player.DamegeAction(power);
            //print("hit");
            Destroy(this.gameObject);
            
        }
    }
    IEnumerator c_Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
