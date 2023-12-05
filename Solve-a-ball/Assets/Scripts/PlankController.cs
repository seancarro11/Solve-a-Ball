using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankController : MonoBehaviour
{
    public bool left, right, back, forward;
    private Vector3 curDirection;
    public float offset;
    private int index;
    private void Start()
    {
        if (left)
        {
            curDirection = Vector3.left;
            index = 1;
            transform.GetChild(0).localPosition = curDirection * offset;
            transform.GetChild(0).transform.Rotate(Vector3.up, 90f);
        }
        if (right)
        {
            curDirection = Vector3.right;
            index = 2;
            transform.GetChild(0).localPosition = curDirection * offset;
            transform.GetChild(0).transform.Rotate(Vector3.up, 90f);
        }
        if (back)
        {
            curDirection = Vector3.back;
            index = 3;
            transform.GetChild(0).localPosition = curDirection * offset;
        }
        if (forward)
        {
            curDirection = Vector3.forward;
            index = 0;
            transform.GetChild(0).localPosition = curDirection * offset;
        }

    }
    public Vector3 getCurDirection()
    {
        return curDirection;
    }
    public void setCurDirection()
    {
        switch (index) 
        {
            case 0:
                curDirection = Vector3.left;
                index = 1;
                left = true;
                forward = false;

                break;
            case 1:
                curDirection = Vector3.right;
                index = 2;
                right = true;
                left = false;
                break;
            case 2:
                curDirection = Vector3.back;
                index = 3;
                back = true;
                right = false;
                break;
            case 3:
                curDirection = Vector3.forward;
                index = 0;
                forward = true;
                back = false;
                break;
        }
        transform.GetChild(0).localPosition=curDirection * offset;
        if(Vector3.left== curDirection)
        {
            transform.GetChild(0).transform.Rotate(Vector3.up, 90f);
        }
        if (Vector3.back == curDirection)
        {
            transform.GetChild(0).transform.Rotate(Vector3.up, 90f);
        }
    }

}
