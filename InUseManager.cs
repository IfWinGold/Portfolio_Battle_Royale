using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InUseManager : MonoBehaviour
{
    public static InUseManager Instance = null;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
