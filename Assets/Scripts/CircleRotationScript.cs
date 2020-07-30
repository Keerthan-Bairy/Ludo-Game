using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotationScript : MonoBehaviour
{
    private int mSpeed = 500;
    private void Start()
    {
        transform.localScale = new Vector2(1f, 1f);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,0f,-1*mSpeed*Time.deltaTime);
    }
}
