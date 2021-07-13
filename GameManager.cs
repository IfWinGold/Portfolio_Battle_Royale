using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    // Start is called before the first frame update
    public GameObject gameover;
    public GameObject player;
    PlayerMove playermove;

    public bool ConsoleView = false;
    bool ConsoleSpeed = false;
    [SerializeField] GameObject Console;
    [SerializeField] InputField Console_Input;
    [SerializeField] Enemy Enemy;

    Enemy c_Enemy;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        playermove = player.GetComponent<PlayerMove>();
        Cursor.visible = false; //커서가 보이지 않게
    }

    // Update is called once per frame
    void Update()
    {
        if(playermove.hp <= 0)
        {
            gameover.SetActive(true);
            Destroy(player);
        }

        if(Input.GetKeyDown(KeyCode.BackQuote)&&ConsoleView != true)
        {
            Console.SetActive(true);
            ConsoleView = true;
            Time.timeScale = 0f;
        }
        else if(Input.GetKeyDown(KeyCode.BackQuote)&&ConsoleView !=false)
        {
            Console.SetActive(false);
            ConsoleView = false;
            Time.timeScale = 1f;
        }
        
        

        switch (ConsoleView)
        {
            case true:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    if(Console_Input.text != null)
                    {
                        DebugConsole(Console_Input.text);
                    }                    
                }
                break;
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }

    }
    void DebugConsole(string input)
    {

        switch(input)
        {
            case "Summon the enemy":
                Enemy obj = Instantiate(Enemy, player.transform.position, Quaternion.identity);
                c_Enemy = obj;
                obj.player = GameObject.Find("Player").transform;
                break;
            case "Get him out of the danger zone":
                Transform temp = GameObject.Find("Magnetic").transform;
                player.transform.position = new Vector3(temp.position.x, 5, temp.position.z);
                break;
            case "Speed":
                PlayerMove speedplayermove = player.GetComponent<PlayerMove>();
                if (ConsoleSpeed == true)
                {
                    speedplayermove.ConsoleSpeedDown();
                    ConsoleSpeed = false;
                }
                else
                {
                    speedplayermove.ConsoleSpeedup();
                    ConsoleSpeed = true;
                }
                break;
            case "invincibility":
                PlayerMove invinPlayerMove = player.GetComponent<PlayerMove>();
                invinPlayerMove.invincibility = true;
                break;
            case "Release invincible":
                PlayerMove relePlayerMove = player.GetComponent<PlayerMove>();
                relePlayerMove.invincibility = false;
                break;

        }
        Console_Input.text = null;
    }

}
