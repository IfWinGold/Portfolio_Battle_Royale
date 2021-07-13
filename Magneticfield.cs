using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magneticfield : MonoBehaviour
{
    float randomx;
    float randomz;
    GameObject player;
    PlayerMove playermove;
    [SerializeField] GameObject MagTime;
    Text MagTime_Text;

    float time;
    float currenttime;
    float target;
    bool hit = false;
    bool Next_one = false;
    bool Next_two = false;

    bool shrinking = false;

    public float Hitradios = 500f;

    enum MagneticTime
    {
        one,
        two,
        three,
    }
    MagneticTime mt;

    // Start is called before the first frame update

    void Start()
    {
        MagTime_Text = MagTime.GetComponentInChildren<Text>();
        player = GameObject.Find("Player");
        //처음시작시 밑에 두 함수를 사용합니다.
        //StartCoroutine(StartMagnetic());
        //StartCoroutine(FirstMagnetic());
        playermove= player.GetComponent<PlayerMove>();
        transform.localScale = new Vector3(Hitradios * 2, Hitradios * 2, Hitradios * 2);        


        time = 5f;

        mt = MagneticTime.one;

        StartCoroutine(StartMagnetic());
    }

    // Update is called once per frame
    void Update()
    {
        MagTime.SetActive(true);
        string time_text;

        //time = time - 1f * Time.deltaTime;
        //string time_text = string.Format("{0:0.#}", time);
        //MagTime_Text.text = "다음 자기장까지 남은시간 : " + time_text;

       // print(player.transform.position - this.transform.position);
        //print((player.transform.position - this.transform.position).magnitude);
        //남은시간을 표시해줍니다.
        if(!shrinking)
        {
            time -= 1f * Time.deltaTime;
            time_text = string.Format("{0:0.#}", time);
            MagTime_Text.text = "다음 자기장까지 남은 시간 : " + time_text;
        }
        else
        {
            MagTime.SetActive(false);
            if(Hitradios>=target)
            {
                Hitradios -= 10f * Time.deltaTime;
                transform.localScale = new Vector3(Hitradios * 2, Hitradios * 2, Hitradios * 2);
            }
            else
            {
                shrinking = false;
            }
            
        }



        //시간이 다될경우
        if (time <=0)
        {            
           // print(mt);
            switch (mt)
            {
                //첫번째가 위치가 잡히고 난 후
                case MagneticTime.one: //one -> two                    
                    time = 60f;
                    mt = MagneticTime.two;
                    break;
                case MagneticTime.two: //two -> three
                    time = 70f;
                    target = 400f;
                    shrinking = true;
                    mt = MagneticTime.three;
                    break;
                case MagneticTime.three: //three -> three ...
                    time = 120f;
                    target = 300f;
                    shrinking = true;
                    break;
            }
        }



        //플레이어와의 거리
        float dis = Vector3.Distance(player.transform.position, this.transform.position);        

        if(dis >=Hitradios&&!hit)
        {
            hit = true;
            playermove.DamegeAction(5f);            
            StartCoroutine(Damege());            
        }

        if(Next_one ==true)
        {
            float target = 400;            
            if (Hitradios >= target)
            {
                Hitradios -= 10f * Time.deltaTime;
                transform.localScale = new Vector3(Hitradios * 2, Hitradios * 2, Hitradios * 2);
            }
            else
            {
            }
            if(Next_two == true)
            {
                float target_two = 300;
                if(Hitradios >= target_two)
                {
                    Hitradios -= 10f * Time.deltaTime;
                    transform.localScale = new Vector3(Hitradios * 2, Hitradios * 2, Hitradios * 2);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Hitradios);
    }

    IEnumerator Damege()
    {        
        yield return new WaitForSeconds(2f);
        hit = false;
    }
    IEnumerator StartMagnetic()
    {
        yield return new WaitForSeconds(5f);
        randomx = Random.Range(0, 1000f);
        randomz = Random.Range(0, 1000f);
        this.transform.localPosition = new Vector3(randomx, 0f, randomz);
    }
}
