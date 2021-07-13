using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipGun : MonoBehaviour
{
    public GameObject player;
    public float dis;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 playerdis = player.transform.position;
        Vector3 gundis = this.transform.position;
        //총과 플레이어의 거리
        dis = Vector3.Distance(playerdis, gundis);
        
    }
    public float Getdistance()
    {
        return dis;
    }
}
