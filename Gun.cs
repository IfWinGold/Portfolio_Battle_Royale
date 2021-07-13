using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class Gun : MonoBehaviour
{
    Animator anim;
    public GameObject bulletFactory;
    [SerializeField] GameObject bulletPosition;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.ConsoleView != true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Fire");
                anim.SetTrigger("FireToFireready");

                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = bulletPosition.transform.position;

            }
        }
        
    }
}
