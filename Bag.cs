using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    [SerializeField] Sprite Pistol;
    [SerializeField] Sprite Bomb;
    [SerializeField] GameObject ui_bag_weapon;
    [SerializeField] Text[] name_slot;
    [SerializeField] Image[] Image_slot;
    [SerializeField] Text[] num_slot;

    public Sprite sprite_Pistol;
    public Sprite sprite_Syringer;
    public Sprite sprite_bomb;

    public Image slot_1;
    public Image slot_2;
    public Image slot_3;


    List<Item> slot = new List<Item>();



    Image ui_bag_weapon01;
    Image ui_bag_weapon02;
    int slotnumber = -1;
    int slotsize = 7;

    PlayerMove playermove;
    


    // Start is called before the first frame update
    void Start()
    {
        //item = new List<Item>();
        playermove = GetComponent<PlayerMove>();
        ui_bag_weapon01 = ui_bag_weapon.GetComponentInChildren<Image>();
        ui_bag_weapon02 = ui_bag_weapon.transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {        
        //print("slotCount : " + slot.Count);
       if(playermove.mygun != null)
        {
            ui_bag_weapon01.sprite = Pistol;
            ui_bag_weapon01.color = new Color(255, 255, 255, 255);    slot_1.sprite = sprite_Pistol;       
        }       
       

       //라이플 등이 추가될경우 여기에 표시합니다.
        //if (playermove.b_equippistol == true)
        //{
        //    ui_bag_weapon01.sprite = Pistol;
        //    ui_bag_weapon01.color = new Color(255, 255, 255, 255);
        //}


      
    }
    public void GetItem(Item item)
    {
        //Item c_Enemy = item;
        //그 아이템이 있는지 확인하고 있을경우 갯수를 늘리고 없을경우 새로 만든다음 Thenum을 1로 만든다.
        if(HaveAnyItems(item.num)==true)//있을경우
        {
            //print("아이템이 있다.");
            Item temp = item;            
            int index = slot.FindIndex(x => x.num == temp.num);
            slot[index].Thenum++;
            temp.Thenum = slot[index].Thenum;
            //Synchronize( c_Enemy,index);
            Synchronize();
            //TestItem();
        }
        else//없을경우 아이템을 추가한다.
        {
            //print("아이템이 없다.");
            slotnumber++;
            Item temp = item;
            temp.Thenum = 1;
            slot.Add(temp);
            //Synchronize( c_Enemy);
            Synchronize();
            //TestItem();
        }

    }
    public void UseItem(int code)
    {
        int index = slot.FindIndex(x => x.num == code);
        slot[index].Thenum--;
        Item temp = slot[index];
        //Synchronize( c_Enemy, index);
        Synchronize();
        print(slot[index].name + " " + slot[index].Thenum);
        //모두 사용할경우
        if(slot[index].Thenum == 0)
        {                        
            slot.Remove(slot[index]);
            Image_slot[index].sprite = null;
            Image_slot[index].color = new Color(255, 255, 255, 0);
            name_slot[index].text = null;
            num_slot[index].text = null;            
            slotnumber--;
            Synchronize();
        }
    }
    public bool HaveAnyItems(int code)
    {
        if(slot.Count == 0)
        {
            //print("slot.Count = " + slot.Count);
            return false;
        }
        else
        {            
            for(int i =0; i <slot.Count;++i)
            {
                if(slot[i].num == code)
                {
                    return true;
                }
            }
            return false;
        }
    }

    void Synchronize()
    {
        for(int i = 0; i < slotsize;i++)
        {
            Image_slot[i].sprite = null;
            Image_slot[i].color = new Color(255, 255, 255, 0);
            name_slot[i].text = null;
            name_slot[i].color = new Color(255, 255, 255, 0);
            num_slot[i].text = null;
            num_slot[i].color = new Color(255, 255, 255, 0);
        }
        for(int i=0;i<slot.Count;i++)
        {
            Image_slot[i].sprite = slot[i].Image;
            Image_slot[i].color = new Color(255, 255, 255, 255);
            name_slot[i].text = slot[i].name;
            name_slot[i].color = new Color(255, 255, 255, 255);
            num_slot[i].text = slot[i].Thenum.ToString();
            num_slot[i].color = new Color(255, 255, 255, 255);
        }
    }
    void TestItem()
    {
        //for(int i = 0; i < slot.Count;i++)
        //{
        //    print(slot[i].name + " " + slot[i].num + "  INDEX:"+i);
        //}
    }

}
