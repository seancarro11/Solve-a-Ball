using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankController : MonoBehaviour
{
    public bool turned;
    private Vector3 curDirection;
    public float offset;
    private int index;
    private void Start()
    {
        if (turned)
        {
            transform.GetChild(0).transform.Rotate(0, 0, 90);
        }

    }
    public bool getCurDirection()
    {
        return turned;
    }
    public void setCurDirection()
    {
        transform.GetChild(0).transform.Rotate(0, 0, 90);
        if (turned) 
        {
            turned = false;
        }
        else
        {
            turned = true;
        }
    }

}
