using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    public int startHeight;
    private int ballHeight;
    public float dist;
    bool Moving,isFalling;
    Vector3 targetPos, origPos,curHDrection, curDrection, curGridPos;
    float timeToMove = 0.2f;
    public Vector3 startDirection;
    private GridFloor gFloor;
    private GridMid gMid;
    private GridTop gTop;
    private string belowTag, forwardTag;
    // Start is called before the first frame update
    void Start()
    {
        curHDrection = startDirection;
        ballHeight = startHeight;
        gFloor = GameObject.Find("GM").GetComponent<GridFloor>();
        gMid = GameObject.Find("GM").GetComponent<GridMid>();
        gTop = GameObject.Find("GM").GetComponent<GridTop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void moveStart()
    {
        InvokeRepeating("Move", 1.0f, 2.0f);
    }
    void Move() 
    {
        curGridPos = transform.position / 2;
        //checkUnder if something or on ground. false fall (direction down).true keep going
        if (ballHeight > 1)
        {
            switch (ballHeight-1)
            {
                case 1: belowTag = gFloor.getLocation(((int)transform.position.x/2), ((int)transform.position.z / 2)); break;
                case 2: belowTag = gMid.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2)); break;
            }
            switch (belowTag)
            {
                case "Air": curDrection = Vector3.down; StartCoroutine(MoveBall(curDrection)); ballHeight= ballHeight - 1; isFalling=true; break;
                case "Table":
                    if (isFalling == true)
                    {
                        Debug.Log("Shatter");
                    }
                    else
                    {
                        checkAhead(); break;
                    }
                    break;
                case "Cusion": curDrection = Vector3.down; StartCoroutine(MoveBall(curDrection)); ballHeight = ballHeight - 1; break;//falls to cushion
                case "Plank":
                    if (isFalling == true)
                    {
                        Debug.Log("Shatter");
                    }
                    else
                    {
                        checkAhead(); break;
                    }
                    break;
            }
        }
        else
        {
            belowTag=gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2));
            if (belowTag == "Cusion"&& isFalling==true)
            {
                isFalling = false;
                checkAhead();
            }
            else if(isFalling == false)
            {
                checkAhead();
            }
            else
            {
                Debug.Log("Shatter");
            }
        }
        //Debug.Log("Tryed to move");
    }
    void checkAhead()       //check one forward in current direction,if false move there. true collide(stop or break)
    {
        switch (ballHeight)
        {
            case 1: forwardTag = gFloor.getLocation(((int)transform.position.x / 2)+((int)curHDrection.x), ((int)transform.position.z / 2) + ((int)curHDrection.z)); break;
            case 2: forwardTag = gMid.getLocation(((int)transform.position.x / 2) + ((int)curHDrection.x), ((int)transform.position.z / 2) + ((int)curHDrection.z)); break;
            case 3: forwardTag = gTop.getLocation(((int)transform.position.x / 2) + ((int)curHDrection.x), ((int)transform.position.z / 2) + ((int)curHDrection.z)); break;
        }
        switch (forwardTag)
        {
            case "Air":
                {
                    if (!Moving)
                    {
                        StartCoroutine(MoveBall(curHDrection));
                    }
                    break; 
                }
            case "Table": Debug.Log("Shatter"); break;
            case "Cusion": break;
            case "Plank": Debug.Log("Shatter"); break;
            case "Door": StartCoroutine(MoveBall(curHDrection)); Debug.Log("Unlocked Door"); break;
        }
    }
    private IEnumerator MoveBall(Vector3 direction)
    {
        Moving = true;

        float eTime = 0;
        origPos = transform.position;
        if (direction == Vector3.down)
        {
            targetPos = origPos + direction;
            while (eTime < timeToMove)
            {
                transform.position = Vector3.Lerp(origPos, targetPos, (eTime / timeToMove));
                eTime += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPos;
            Moving = false;
        }
        else
        {
            targetPos = origPos + direction * dist;
            while (eTime < timeToMove)
            {
                transform.position = Vector3.Lerp(origPos, targetPos, (eTime / timeToMove));
                eTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;
            Moving = false;
        }
    }
    void shatter()
    {
        Destroy(gameObject);
    }
}
