using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Door : MonoBehaviour
{
    float speed = 10f;
    Vector3 origin;
    Vector3 distance;
    bool d_open;    
    // Start is called before the first frame update
    [SerializeField] GameObject Left;
    [SerializeField] GameObject Right;
    [SerializeField] GameObject target;
    [SerializeField] GameObject player;    
    AudioSource[] sound;
    
    void Start()
    {
        sound = GetComponents<AudioSource>();
        origin = Right.transform.position;
        d_open = false;
    }

    // Update is called once per frame
    void Update()
    {
        Distance();
        
        if (distance.magnitude <= 2f)
        {
            //print(this.name + " = " + distance.magnitude);
                        
            //print(ui_text.activeSelf);
            if (Input.GetKeyDown(KeyCode.F)&&d_open == false)
            {
                d_open = true;
                sound[0].Play();
               // print("open");
            }
            else if(Input.GetKeyDown(KeyCode.F)&&d_open == true)
            {                
                d_open = false;
                sound[1].Play();
                //print("close");
            }
        }        
        if(d_open == true)
        {
            Right.transform.position = Vector3.Lerp(Right.transform.position, target.transform.position, 0.1f);
        }        
        if(d_open == false)
        {
            Right.transform.position = Vector3.Lerp(Right.transform.position, origin, 0.1f);
        }                
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Right.transform.position, 2f);
    }
    void Distance()
    {
        distance = player.transform.position - Right.transform.position;
    }
    public float GetDistance()
    {
        return player.transform.position.magnitude - Right.transform.position.magnitude;
    }
}
 