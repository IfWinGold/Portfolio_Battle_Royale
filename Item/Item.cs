using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item 
{
    public string name;
    public Sprite Image;
    public int num; //아이템의 코드입니다.
    public int Thenum = 0; //아이템의 갯수입니다.


    private float m_uphp;
    public Item(string m_name , Sprite m_Image,int Code)
    {
        name = m_name;
        Image = m_Image;
        num = Code;
    }

}
