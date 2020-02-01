using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPositionTest : MonoBehaviour
{
    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        //transform.parent = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = myCamera.transform.position;

    }
}
