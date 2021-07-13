using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    void Start()
    {
        Camera.main.cullingMask = ~(1 << 12);
    }

    // Update is called once per frame

}
