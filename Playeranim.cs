using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranim : MonoBehaviour
{
    Animator anim;
    Vector3 playerdir;
    Vector3 v_center;
    Vector3 v_slidecenter;
    Vector3 v_crouch_center;
    Vector3 v_slidestandup; //Local
    Vector3 v_slide; // Local
    float speed;
    Transform model;    
    
    bool iscrouch = false; //setstate
    bool isjump = false; //setstate
    bool iswalk = false;
    bool isrun = false;
    bool isidle = false;
    bool isequipgun = false;
    bool isThrow = false;
    enum playerstate
    {
    idle,
    walk,
    run,
    jump,
    crouch,
    slide,
    Syringer,
    ThrowReady,
    Throw            
    }

    playerstate state;
    PlayerMove playermove;
    CharacterController controller;
    AudioSource[] audio;    
    float crouch_height = 1.06f;    
    float standup_Height = 1.82f;


    void Start()
    {        
        model = transform.GetChild(0);
        v_slide = new Vector3(0, -1.5f, 0);
        v_slidestandup = new Vector3(0, -1f, 0);
        state = playerstate.idle;
        v_slidecenter = new Vector3(0, -0.38f, 0);
        audio = GetComponents<AudioSource>();
        controller = GetComponent<CharacterController>();
        playermove = GetComponent<PlayerMove>();
        state = playerstate.idle;
        anim = GetComponent<Animator>();
        v_center = controller.center;
        v_crouch_center = new Vector3(0, -0.38f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.ConsoleView != true)
        {
            anim.SetFloat("Speed", speed);
            anim.SetFloat("dir.x", playerdir.x);
            anim.SetFloat("dir.z", playerdir.z);
            speed = playermove.Getanimspeed();
            playerdir = playermove.Getanimdir();
            //print(state);        
            if (playermove.isSyringer == true)
            {
                state = playerstate.Syringer;
                audio[0].Stop();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                isjump = true;
                state = playerstate.jump;
            }
            else if (playermove.isThrow == true)
            {
                state = playerstate.ThrowReady;
                isThrow = true;
                audio[0].Stop();
            }
            else if (isThrow == true && playermove.isThrow == false)
            {
                isThrow = false;
                state = playerstate.Throw;
            }
            else
            {
                Setstate();
                anim.SetBool("b_Syringer", false);
                anim.SetBool("b_ThrowReady", false);
            }
            //print(state);

            if (playermove.b_equippistol == true)
            {
                anim.SetBool("EquipGun", true);
                isequipgun = true;
            }
            else if (playermove.b_equippistol == false)
            {
                anim.SetBool("EquipGun", false);
            }

            switch (state)
            {
                case playerstate.idle://V
                    Walksound();
                    idleNwalk();
                    break;
                case playerstate.walk://V     
                    isidle = false;
                    Walksound();
                    idleNwalk();
                    break;
                case playerstate.run://V
                    Run();
                    Walksound();
                    break;
                case playerstate.jump:
                    Jump();
                    break;
                case playerstate.crouch:
                    Crouch();
                    break;
                case playerstate.slide:
                    Slide();
                    break;
                case playerstate.Syringer:
                    anim.SetBool("b_Syringer", true);
                    break;
                case playerstate.ThrowReady:
                    anim.SetBool("b_ThrowReady", true);
                    break;
                case playerstate.Throw:
                    anim.SetBool("b_Throw", true);
                    break;
            }
        }
        
    }
    void Walksound()
    {
        if(state == playerstate.walk)
        {

            //print(audio[0].isPlaying);
            if (iswalk == false)
            {
                audio[0].Stop();
                audio[0].pitch = 1f;
                audio[0].Play();                
                iswalk = true;
                isrun = false;
            }
            else if (iswalk == true && audio[0].isPlaying == false)
            {
                audio[0].Play();
            }
        }
        else if(state == playerstate.idle)
        {
            iswalk = false;
            isrun = false;
            audio[0].Stop();
        }
        else if(state == playerstate.run)
        {
            if (isrun == false)
            {
                audio[0].Stop();
                audio[0].pitch = 1.5f;
                audio[0].Play();                
                isrun = true;
                iswalk = false;
            }
            else if(isrun == true && audio[0].isPlaying == false)
            {
                audio[0].Play();
            }
        }
    }
    void idleNwalk()
    {
        controller.center = v_center;
        controller.height = standup_Height;
        anim.SetBool("b_isrun", false);
        anim.SetBool("b_crouch", false);
    }
    void Run()
    {
        anim.SetBool("b_crouch", false);
        anim.SetBool("b_isrun", true);
    }
    void Jump()
    {
        anim.SetBool("b_isJump", true);
        if(audio[1].isPlaying!=true&&isjump==true)
        {
            audio[0].Stop();
            audio[1].Play();
        }        
    }
    void Crouch()
    {
        //if(playermove.b_equippistol==true)
        //{   
        //    if(iscrouch == false)
        //    {
        //        anim.SetBool("EquipGun_crouch", false);
        //    }
        //    anim.SetBool("EquipGun_crouch", true);

        //}
        //else
        //{
        //    if (iscrouch == false)
        //    {
        //        anim.SetBool("b_crouch", false);
        //    }
        //    else
        //    {
        //        anim.SetBool("b_crouch", true);
        //        audio[0].Stop();
        //        controller.center = v_crouch_center;
        //        controller.height = crouch_height;
        //        if (speed > 3f)
        //        {
        //            anim.SetBool("b_crouch_F", true);
        //        }
        //        else
        //        {
        //            anim.SetBool("b_crouch_F", false);
        //        }
        //    }
        //}
        if (iscrouch == false)
        {
            anim.SetBool("b_crouch", false);
        }
        else
        {
            anim.SetBool("b_crouch", true);
            audio[0].Stop();
            controller.center = v_crouch_center;
            controller.height = crouch_height;
            if (speed > 3f)
            {
                anim.SetBool("b_crouch_F", true);
            }
            else
            {
                anim.SetBool("b_crouch_F", false);
            }
        }
    }
    void Slide()
    {
        controller.center = v_slidecenter;
        model.localPosition = v_slide;
        audio[0].Stop();
        anim.SetBool("b_isslide",true);
    }
    public void Slidestandup()
    {
        anim.SetBool("b_isslide", false);
        controller.center = v_center;
        model.localPosition = v_slidestandup;
        state = playerstate.crouch;
    }
    void Setstate()
    {
        if(controller.isGrounded)
        {
            audio[1].Stop();
            isjump = false;
            anim.SetBool("b_isJump", false);            
            if (iscrouch == true)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    iscrouch = false;                    
                }
                else
                {
                    state = playerstate.crouch;
                }
            }            
            else
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (state == playerstate.run)
                    {
                        state = playerstate.slide;
                        iscrouch = true;
                    }                    
                    else
                    {
                        state = playerstate.crouch;
                        iscrouch = true;
                    }
                }
                else if(isjump!=true)
                {
                    if (speed < 2f)
                    {
                        state = playerstate.idle;

                    }
                    else if (speed > 2f && speed < 9f)
                    {
                        state = playerstate.walk;

                    }
                    else if (speed > 9f)
                    {
                        state = playerstate.run;
                    }
                }
            }
            
        }
    }       
    public void ThrowEnd()
    {
        anim.SetBool("b_Throw", false);
    }
}
