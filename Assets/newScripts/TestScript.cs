using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TestScript : MonoBehaviour
{
    public Vector3 dot;
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(dot, 3f, RotateMode.Fast);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
