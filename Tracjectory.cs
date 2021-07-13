using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracjectory : MonoBehaviour
{
    //Camera cam;
    public GameObject bullet;
    Bag bag;
    bool EquipItem = false;
    bool Shot;
    public Transform shootPoint;
    LineRenderer lineRenderer;
    PlayerMove playermove;
    Cam cam;
    Vector3 target;


    private void Start()
    {
        Shot = false;
        bag = GetComponent<Bag>();
        lineRenderer = GetComponent<LineRenderer>();
        playermove = GetComponent<PlayerMove>();
        cam = Camera.main.GetComponent<Cam>();
    }
    private void Update()
    {
        if(GameManager.Instance.ConsoleView != true)
        {
            if (bag.HaveAnyItems(0) == true)
            {
                bag.slot_2.sprite = bag.sprite_bomb;
                //print("수류탄 On");
                if (Input.GetMouseButton(0) && PlayerWeapon.Instance.b_useBomb == true)
                {
                    playermove.isThrow = true;
                    Shot = true;
                    lineRenderer.enabled = true;
                    target = cam.Bombdir();
                    Vector3 Vo = CalculateVelcoity(target, transform.position, 1f);
                    DrawPath(Vo);
                }
                else if (Input.GetMouseButtonUp(0) && Shot == true)
                {
                    playermove.isThrow = false;
                    LaucherProjecttile();
                    lineRenderer.enabled = false;
                    bag.UseItem(0);
                    Shot = false;
                }
            }
            else
            {
                bag.slot_2.sprite = null;
            }
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Debug.Log("LaucherProjecttile");
            //    LaucherProjecttile();
            //}
        }
    }
    void LaucherProjecttile()
    {
        target = cam.Bombdir();
        Vector3 Vo = CalculateVelcoity(target, transform.position, 1f);
        Rigidbody obj = Instantiate(bullet, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        obj.velocity = Vo;
        //setTrajectoryPoints(transform.position, Vo);
        //DrawPath(Vo);
    }

    //이 방법은 목표 벡터와 원점의 시작점이 필요합니다.
    //time : 비행시간
    Vector3 CalculateVelcoity(Vector3 target, Vector3 origin, float time)
    {
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance; //x와z의 평면이면 기본적으로 거리와 같은 벡터
        distanceXZ.y = 0f;//y는 0으로 설정

        //create a float the represent our distance
        float Sy = distance.y;//세로 높이의 거리를 지정
        float Sxz = distanceXZ.magnitude;

        //속도 계산
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        //계산으로 인해 두축의 초기 속도 가지고 새로운 벡터를 만들수 있음
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }

    void DrawPath(Vector3 velocity)
    {
        Vector3 previousDrawPoint = transform.position;
        int resolution = 50;
        lineRenderer.positionCount = resolution;
        for (int i = 1; i <= resolution; i++)
        {
            //float simulationTime = i / (float)resolution * launchData.timeToTarget;
            float simulationTime = i / (float)resolution * 2f;

            Vector3 displacement = velocity * simulationTime + Vector3.up * Physics.gravity.y * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = transform.position + displacement;
            //DebugExtension.DebugPoint(drawPoint, 1, 1000f);//유니티 에셋스토어 Debug Extension
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            lineRenderer.SetPosition(i - 1, drawPoint);
            previousDrawPoint = drawPoint;
        }
    }
}
