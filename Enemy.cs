using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    enum FSM
    {
        Idle,
        Discovery,
        Attack,
        Move,
        Die,
        Damaged,
        Mag
    }
    
    FSM fsm;
    NavMeshAgent agent;
    public float hp;
    //발견범위
    public float m_discovery;
    //공격범위
    public float m_attackarea;
    //이동속도
    public float speed;
    bool islive;
    bool isDelay;

    public GameObject BulletFactory;
    public GameObject ef;
    ParticleSystem ps;

    public Slider hpbar;
    
    public Transform player;
    [SerializeField]Transform gunposition;
    CharacterController cc;
    Animator anim;
    AudioSource[] sound;
    Vector3 originPos; //초기위치
    Quaternion originRot; //초기회전값
    Vector3 dir;
    public GameObject Mag;
    bool hit;
    float Magdis;
    void Start()
    {
        islive = true;
        anim = GetComponentInChildren<Animator>();
        sound = GetComponents<AudioSource>();
        ps = ef.GetComponent<ParticleSystem>();
        //초기위치
        originPos = transform.position;
        originRot = transform.rotation;
        agent = GetComponent<NavMeshAgent>();

        cc = GetComponent<CharacterController>();
                
        fsm = FSM.Idle;
        ef = GameObject.Find("Particle System_A");
        Mag = GameObject.Find("Magnetic");
    }

    // Update is called once per frame
    void Update()
    {
        Magnetic();
        //print(transform.position);
        if (islive == true)
        {
            if (hp <= 0)
            {
                fsm = FSM.Die;
                islive = false;                
            }
            hpbar.value = hp / 100;
            dir = (player.transform.position - transform.position).normalized;
            //감지범위
            Debug.DrawRay(this.transform.position, dir * 18f, Color.green);
            //공격범위

            Debug.DrawRay(this.transform.position, dir * 12f, Color.red);
            switch (fsm)
            {
                case FSM.Idle:
                    Idle();
                    anim.SetBool("Idle", true);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Walk", false);
                    break;
                case FSM.Discovery:
                    Discovery();
                    break;
                case FSM.Attack:
                    Attack();
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Attack", true);
                    break;
                case FSM.Move:
                    Move();
                    anim.SetBool("Idle", false);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Walk", true);
                    break;
                case FSM.Die:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Dead", true);                    
                    Die();
                    break;
                case FSM.Damaged:
                    Forcedmovement();
                    break;
                case FSM.Mag:
                    MagRun();
                    anim.SetBool("Walk", true);                    
                    break;
            }
        }
    }
    void Idle()
    {        
        if (Vector3.Distance(transform.position,player.position)< m_discovery)
        {            
            fsm = FSM.Discovery;             
        }
    }
    void Discovery()
    {
        //스캔범위 안에 적이 스캔됐으므로 공격을 하거나 , 이동을 한다.
        if (Vector3.Distance(transform.position, player.position) < m_discovery)
        {
            //에너미와 플레이어의 거리 구하기
            //거리를 normalize 하기(방향)  
            Vector3 dir = (player.transform.position - transform.position).normalized;
            dir.y = 0f;

            Ray ray = new Ray(gunposition.transform.position, dir);
            Debug.DrawRay(gunposition.transform.position, dir,Color.blue);

            RaycastHit hitInfo = new RaycastHit();
            Physics.Raycast(ray, out hitInfo);

            
            //공격범위 안에 들어오면
            if (Vector3.Distance(transform.position,player.position)<m_attackarea)
            {
                                
                //장애물이 있는가 확인한다. 장애물이 있는지 확인한다.
                if (hitInfo.transform.tag == "Player") 
                {
                    //장애물이 없을시 스캔(공격)                    
                    fsm = FSM.Attack;
                    //print(hitInfo.transform.name);
                }
                else 
                {
                    //장애물이 있을시 스캔(이동)                    
                    fsm = FSM.Move;                    
                }
                
            }
            else
            {
                fsm = FSM.Move;
            }
           
        } //시야 범위에서 넘어가면 대기상태로 전환
        else
        {
            fsm = FSM.Idle;
        }
    }
    public void Forcedmovement()
    {
        fsm = FSM.Move;
    }
    void Attack()
    {
        //에너미와 플레이어의 거리 구하기
        //거리를 normalize 하기(방향)  
        Vector3 dir = (player.transform.position - gunposition.transform.position).normalized;
        dir.y = 0f;

        Ray ray = new Ray(gunposition.transform.position, dir);
        Debug.DrawRay(gunposition.transform.position, dir);
        RaycastHit hitInfo = new RaycastHit();
        int Layer = (1 << LayerMask.NameToLayer("PositionBox"));
        Physics.Raycast(ray, out hitInfo,Layer);
        
        
        //장애물이 있는가 확인한다. 장애물이 있는지 확인한다.
        if (hitInfo.transform.CompareTag("Player"))
        {
            //print(hitInfo.transform.name);
            //장애물이 없을시 스캔(공격)            
            if (!isDelay)
            {
                transform.LookAt(player);
                isDelay = true;
                GameObject bullet = Instantiate(BulletFactory);
                bullet.transform.position = gunposition.position;
                StartCoroutine(Attackdelay());
            }

        }
        else
        {
            //장애물이 있을시 스캔(이동)    
            //print(hitInfo.transform.name);
            fsm = FSM.Move;
        }

    }

    void Move()
    {
        if(agent == null)
        {
            //print("agent is null");
            agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            agent.stoppingDistance = m_attackarea;
            agent.destination = player.position;
        }        
        //이동중 공격범위 안에 들어가면
        if (Vector3.Distance(transform.position, player.position) < m_attackarea)
        {                        
            fsm = FSM.Attack;            
            return;
        }

    }
    IEnumerator Attackdelay()
    {
        yield return new WaitForSeconds(1f);        
        isDelay = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {   
            if(hp<=0)
            {
                sound[1].Play();
            }
            else
            {
                sound[0].Play();
            }
            ef.transform.position = other.transform.position;
            ef.transform.forward = -other.transform.forward;         
            ps.Play();
        }
    }
    void Die()
    {
        agent.enabled = false;
        StartCoroutine(C_Die());                        
    }

    IEnumerator C_Die()
    {        
        yield return new WaitForSeconds(1.5f);            
        Destroy(this.gameObject);
    }
    void DiePosition()
    {        
        Vector3 temp = transform.position;
        temp.y = -1f;
        transform.position = temp;                                
    }
    void Magnetic()
    {
        if(Mag == null)
        {
            Mag = GameObject.Find("Magnetic");
        }
        else
        {
            Magneticfield temp = Mag.GetComponent<Magneticfield>();
            Magdis = Vector3.Distance(Mag.transform.position, this.transform.position);
            if (temp.Hitradios <= Magdis && !hit)
            {
                hit = true;
                hp -= 5f;
                StartCoroutine(Damege());
                fsm = FSM.Mag;
            }
        }   
    }
    void MagRun()
    {
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            Magneticfield temp = Mag.GetComponent<Magneticfield>();
            agent.destination = temp.transform.position;
            if (temp.Hitradios >= Magdis)
            {
                fsm = FSM.Idle;
            }
        }
    }
    IEnumerator Damege()
    {
        yield return new WaitForSeconds(2f);
        hit = false;
    }




}
