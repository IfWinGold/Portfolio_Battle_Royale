using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public static PlayerWeapon Instance = null;
    public GameObject playergun;    

    PlayerMove playermove;
    Bag playerbag;

    //Gun playergun;


    public bool b_useBomb = false;
    public bool b_useSyringe = false;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    enum BeinUse
    {
        none,
        pistol,
        bomb,
        syringe
    }
    BeinUse beinuse;
    void Start()
    {
        beinuse = BeinUse.none;
        playerbag = GameObject.Find("Player").GetComponent<Bag>();
        playermove = GameObject.Find("Player").GetComponent<PlayerMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        use();
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            beinuse = BeinUse.pistol;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            beinuse = BeinUse.bomb;            
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            beinuse = BeinUse.syringe;
        }

    }
    void use()
    {
        switch (beinuse)
        {
            case BeinUse.none:
                //print("Error! BeinUse is NONE!");
                break;
            case BeinUse.pistol:
                if(playergun == null)
                {
                    //print("gun is null");
                    beinuse = BeinUse.none;
                }
                else
                {
                    playergun.SetActive(true);
                    playermove.b_equippistol = true;
                    b_useBomb = false;
                    b_useSyringe = false;
                }
                break;
            case BeinUse.bomb:
                if(playergun == null) //총이 비었는데
                {
                    if(playerbag.HaveAnyItems(0)==false)//수류탄도 없으면
                    {
                       // print("Error! Bomb is NONE!");
                        beinuse = BeinUse.none;
                    }
                    else
                    {
                        b_useBomb = true;
                        b_useSyringe = false;
                    }
                }
                else
                {
                    playergun.SetActive(false);
                    playermove.b_equippistol = false;
                    if(playerbag.HaveAnyItems(0)==false)
                    {
                        //print("Error! Bomb is NONE!");
                        beinuse = BeinUse.none;
                    }
                    else
                    {
                        b_useBomb = true;
                        b_useSyringe = false;
                    }
                }                
                break;
            case BeinUse.syringe:
                if(playergun == null)
                {
                    if(playerbag.HaveAnyItems(1)==false)
                    {
                        beinuse = BeinUse.none;
                    }
                    else
                    {
                        b_useSyringe = true;
                        b_useBomb = false;
                    }
                }
                else
                {
                    playergun.SetActive(false);
                    playermove.b_equippistol = false;
                    if(playerbag.HaveAnyItems(1)==false)
                    {
                        beinuse = BeinUse.none;
                    }
                    else
                    {
                        b_useBomb = false;
                        b_useSyringe = true;
                    }
                }
                break;
        }

    }

}
