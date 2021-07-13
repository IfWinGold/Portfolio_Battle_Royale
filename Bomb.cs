using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    bool touch;

    public GameObject ef;
    AudioSource sound;
    
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {        
       
    }
    IEnumerator BOMB()
    {
        yield return new WaitForSeconds(3f);
        GameObject BombEffect = Instantiate(ef);
        BombEffect.transform.position = this.transform.position;
        sound.Play();
        yield return new WaitForSeconds(1.5f);
        Destroy(BombEffect);
        Destroy(gameObject);        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player" && touch != true)
        {
            //print("touch");
            touch = true;            
            StartCoroutine(BOMB());
        }        
    }




}
