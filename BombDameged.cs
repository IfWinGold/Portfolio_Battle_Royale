using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDameged : MonoBehaviour
{
    // Start is called before the first frame update
    float power = 80;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enmey = other.gameObject.GetComponent<Enemy>();
            enmey.hp -= power;
        }
    }
}
