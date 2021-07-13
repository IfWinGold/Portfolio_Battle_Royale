using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PlayerMove : MonoBehaviour
{
 float speed = 6.0f;
 float gravity = 20.0f;
 float Jump =8f;
 float animspeed;
    public float hp;
    Cam cam;
    Transform spine;
    
    public Transform gunposition; //총의 위치입니다. 건들면 안됩니다.
    public Transform Firstperson; //1인칭 위치입니다.
    public Transform origin; //줌을 하지 않은상태의 포지션입니다.    

    public Vector3 dir;
    Vector3 animdir;
    
    //spintarget은 메인카메라의 +3f(x) 위치에 있다.
    public Transform spintarget;

    bool isFirst = false; //현재 1인칭인지 아닌지에 대한 여부입니다.
    bool isJump = false;   

    
    //현재 장착이 가능한지에 대한 여부입니다.
    public bool Equip = false;
    public bool b_equippistol = false; //현재 이 무기를 가지고 있는지 여부입니다.
    public bool isSyringer = false;
    public bool isThrowReady = false;
    public bool isThrow = false;

    public int m_equipbomb = 0; //현재 이 무기를 가지고 있는지 여부 입니다.
    public bool invincibility = false;
    bool ConsoleSpeed = false;


    public GameObject Gun1;
    public GameObject Handgun;
    public GameObject hitImage;    
    public GameObject corsshair;
    public GameObject BOMB;
    public Slider hpbar;


    public GameObject mygun;
    EquipGun equipgun;


    Bag bag;
    PlayerRotate playerRotate;
    AudioClip walk;
    CamRotate camrotate;

    float spiney;
    float spinex;

    enum weaponstate
    {
        none,
    pistol,
    bomb,
    syringe
    }
    enum BeinUse
    {
    none,
    pistol,
    bomb,
    syringe
    }



    weaponstate weapon;
    BeinUse beinuse;




    CharacterController controller;
    void Start()
    {        
        beinuse = BeinUse.none;
        bag = GetComponent<Bag>();
        Vector3 dir = Vector3.zero;
       controller = this.GetComponent<CharacterController>();
        camrotate = GameObject.Find("Cam").GetComponent<CamRotate>();
        playerRotate = GetComponent<PlayerRotate>();        
        cam = Camera.main.GetComponent<Cam>();        
        //↑상체값 가져오기 (허리 위)                  
        Cursor.lockState = CursorLockMode.Locked;        
        equipgun = Gun1.GetComponent<EquipGun>();
        Equip = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {        
            //float my = camrotate.my;
            //my = Mathf.Clamp(my, -75f, 75f);
            //spine.eulerAngles = new Vector3(-my, spine.eulerAngles.y, spine.eulerAngles.z);
    }
    void Update()
    {
        if(GameManager.Instance.ConsoleView != true)
        {
            if (isSyringer == true)
            {
                speed = 3.0f;
            }
            GetAngle();
            hpbar.value = hp / 100;
            // 캐릭터 움직임 구현입니다.
            //print("speed :" + speed);        
            if (controller.isGrounded)
            {
                //위,아래 움직임 셋팅
                dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                dir = transform.TransformDirection(dir);

                dir *= speed;
                if (Input.GetButton("Jump"))
                {
                    dir.y = Jump;
                }
            }
            dir.y -= gravity * Time.deltaTime;
            controller.Move(dir * Time.deltaTime);

            if(ConsoleSpeed != true)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = 10.0f;
                }
                else
                {
                    speed = 6.0f;
                }
            }
            else
            {
                speed = 30f;
            }
            


            //무기장착 관련입니다.
            //만일 현재 장착이 가능한경우
            if (Equip == true)
            {
                //장착이 가능한상태에서 E키를 눌렀을경우
                if (Input.GetKeyDown(KeyCode.E))
                {

                    switch (weapon)
                    {
                        case weaponstate.none:
                            break;
                        case weaponstate.pistol:
                            Handgun.SetActive(true);
                            b_equippistol = true;
                            mygun = Handgun;

                            PlayerWeapon.Instance.playergun = Handgun;
                            break;
                        case weaponstate.bomb:
                            ItemManager.Instance.GetItem(0);
                            break;
                        case weaponstate.syringe:
                            ItemManager.Instance.GetItem(1);
                            break;

                    }
                }

            }


            //-----------------------------------------
            //줌 관련입니다.
            //만일 현재 건을 소지하고있고, 마우스 오른쪽 버튼을 눌렀을경우
            if (Input.GetMouseButtonDown(1) && mygun != null)
            {
                switch (isFirst) //처음에는 isFirst는 false인 상태입니다.
                {
                    //줌을 합니다.
                    case false:
                        isFirst = true;
                        break;
                    //줌을 취소합니다.
                    case true:
                        isFirst = false;
                        break;
                }
            }

            animspeed = dir.magnitude;
            animdir = transform.InverseTransformDirection(dir).normalized;
        }
        
    }
    public void DamegeAction(float damage)
    {
        hp -= damage;        
        if(hp > 0)
        {
            StartCoroutine(HitEffect());            
        }
        if(invincibility == true)
        {
            hp += damage;
        }
    }
    IEnumerator HitEffect()
    {
        hitImage.SetActive(true);
        speed -= 2f;
        yield return new WaitForSeconds(0.5f);
        hitImage.SetActive(false);
        speed += 2f;
    }
    public float Getanimspeed()
    {
        return animspeed;
    }
    public Vector3 Getanimdir()
    {
        return animdir;
    }
    public void SetWeapon(int n)
    {
        switch (n)
        {
            case 0:print("Error ! Weapon is null");
                break;
            case 1:weapon = weaponstate.pistol;
                break;
            case 2:weapon = weaponstate.bomb;
                break;
            case 3:weapon = weaponstate.syringe;
                break;
        }

    }
    public void GetAngle()
    {
        Vector3 Parallel = Camera.main.transform.forward * 10f;
       //float angle = Mathf.Atan2()                                
    }
    public void ConsoleSpeedup()
    {
        ConsoleSpeed = true;
    }
    public void ConsoleSpeedDown()
    {
        ConsoleSpeed = false;
    }


}
