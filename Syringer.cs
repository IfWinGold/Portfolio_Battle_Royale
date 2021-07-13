using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Syringer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject HP_Gameobject;
    [SerializeField] Slider slider;
    IEnumerator kit;
    PlayerMove playermove;
    Bag playerbag;
    float Timer = 5f;
    float maxtime  =5f;
    AudioSource sound;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        kit = StartKit();
        playerbag = GameObject.Find("Player").GetComponent<Bag>();
        playermove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.ConsoleView != true)
        {
            if (playerbag.HaveAnyItems(1) == true)
            {

                playerbag.slot_3.sprite = playerbag.sprite_Syringer;
                //print("주사기 on");
                if (PlayerWeapon.Instance.b_useSyringe == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        sound.Play();
                        HP_Gameobject.SetActive(true);
                        StartCoroutine(kit);
                        playermove.isSyringer = true;
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        slider.value += 0.2f * Time.deltaTime;
                        if (slider.value >= 1)
                        {
                            //print("작동중");
                            sound.Stop();
                            slider.value = 0f;
                            //StopCoroutine(kit);
                            //kit = StartKit();
                            HP_Gameobject.SetActive(false);
                            playermove.isSyringer = false;
                            if (playermove.hp + 20f >= 100f)
                            {
                                playermove.hp = 100f;
                            }
                            else
                            {
                                playermove.hp += 20f;
                            }
                            HP_Gameobject.SetActive(false);
                            //playerbag.UseItem(1);
                            playermove.isSyringer = false;
                        }
                    }
                    else if (Input.GetMouseButtonUp(0)) //마우스를 땔시 취소
                    {
                        sound.Stop();
                        slider.value = 0f;
                        StopCoroutine(kit);
                        kit = StartKit();
                        HP_Gameobject.SetActive(false);
                        playermove.isSyringer = false;
                    }
                }
            }
            else
            {
                playerbag.slot_3.sprite = null;
                slider.value = 0f;
            }
        }        
    }
    IEnumerator StartKit()
    {
        yield return new WaitForSeconds(5f);
       // print("Stop");
        if(playermove.hp + 20f >=100f)
        {
            playermove.hp = 100f;
        }
        else
        {
            playermove.hp += 20f;
        }
        HP_Gameobject.SetActive(false);
        playerbag.UseItem(1);
        playermove.isSyringer = false;
    }

}
