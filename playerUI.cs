using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerUI : MonoBehaviour
{
    public GameObject player;

    PlayerMove playermove;
    Door c_door;
    GameObject g_door;
    public GameObject Getgun;
    public GameObject Bag;
    [SerializeField] GameObject DoorUI;
    bool isbag = false;
    bool ismap = false;
    Cam cam;
    [SerializeField] GameObject map;

    
    void Start()
    {        
        
        cam = Camera.main.GetComponent<Cam>();
        playermove = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.ConsoleView != true)
        {
            if (Input.GetKeyDown(KeyCode.M) && ismap == false)
            {
                map.SetActive(true);
                ismap = true;
            }
            else if (Input.GetKeyDown(KeyCode.M) && ismap == true)
            {
                map.SetActive(false);
                ismap = false;
            }

            //플레이어가 아이템을 가리키고 있고,
            if (cam.hitItem() == true)
            {
                int n = 0;
                GameObject temp = cam.Getobject();
                if (temp.name == "Gun")
                {
                    n = 1;
                }
                else if (temp.name == "EquipBomb")
                {
                    n = 2;
                }
                else if (temp.name == "syringe")
                {
                    n = 3;
                }
                playermove.SetWeapon(n);
                EquipGun Weapon = temp.GetComponent<EquipGun>();
                //print(Weapon.Getdistance());
                if (Weapon.Getdistance() <= 2f)
                {
                    Getgun.SetActive(true);
                    playermove.Equip = true;
                }
            }
            else
            {
                Getgun.SetActive(false);
                playermove.Equip = false;
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (isbag == true)
                {
                    Bag.SetActive(false);
                    isbag = false;
                }
                else if (isbag == false)
                {
                    Bag.SetActive(true);
                    isbag = true;
                }
            }
        }
      
    }
 
}
