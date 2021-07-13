using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance = null;

    [SerializeField] Sprite BombImage;
    [SerializeField] Sprite syringeImage;
    [SerializeField] GameObject player;
    Bag playerbag;
    PlayerMove playermove;

    enum ItemCode
    {
    Bomb,
    Syringe
    }
    ItemCode code;

    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        playerbag = player.GetComponent<Bag>();
        playermove = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetItem(int Itemnumber)//아이템을 받는다.
    {
        switch (Itemnumber)
        {
            case 0:
                code = ItemCode.Bomb;
                CreateItem(code);
                break;
            case 1:
                code = ItemCode.Syringe;
                CreateItem(code);
                break;        
        }
    }
    void CreateItem(ItemCode code)
    {
        switch (code)
        {
            case ItemCode.Bomb:
                Item bomb = new Item("Bomb", BombImage, 0);
                playerbag.GetItem(bomb);
                break;
            case ItemCode.Syringe:
                Item syringe = new Item("Syringe", syringeImage, 1);
                playerbag.GetItem(syringe);
                break;
        }

    }
    void HandoverItem()//아이템을 건네준다.
    {

    }
}


