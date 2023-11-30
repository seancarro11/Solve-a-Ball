using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float dist;
    public float cushionOffset;
    bool Moving,hasCusion;
    Vector3 targetPos, origPos;
    float timeToMove = 0.2f;
    private GridFloor gFloor;
    private GridMid gMid;
    private string nextTag;
    private GameObject lookingAt,cushion;
    private Vector3 faceDirection;
    // Start is called before the first frame update
    void Start()
    {
        gFloor = GameObject.Find("GM").GetComponent<GridFloor>();
        gMid = GameObject.Find("GM").GetComponent<GridMid>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.W)&&!Moving)
        {
            if(faceDirection == Vector3.forward)
            { 
            nextTag =gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2) + 1);//check next pos
                if (nextTag != "outOfBounds")
                {
                    switch (nextTag)//check tag of next area
                    {
                        case "Air": StartCoroutine(MovePlayer(Vector3.forward)); break;
                        case "Table":
                            nextTag = gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2) + 2);
                            if (nextTag != "outOfBounds")
                            {
                                lookingAt = gFloor.getObject(((int)transform.position.x / 2), ((int)transform.position.z / 2) + 1);
                                StartCoroutine(MoveTable(Vector3.forward));
                            }
                            break;
                        case "Cusion": break;
                        case "Plank": break;
                    }

                }
            }
            else
            {
                faceDirection = Vector3.forward;
                gameObject.transform.GetChild(0).transform.localPosition = cushionOffset * faceDirection;
                StartCoroutine(Delay());
            }
        }
      if (Input.GetKey(KeyCode.S) && !Moving)
        {
            if (faceDirection == Vector3.back)
            {
                nextTag = gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2) - 1);
                if (nextTag != "outOfBounds")
                {
                    gameObject.transform.GetChild(0).transform.localPosition = cushionOffset * faceDirection;
                    switch (nextTag)
                    {
                        case "Air": StartCoroutine(MovePlayer(Vector3.back)); break;
                        case "Table":
                            nextTag = gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2) - 2);
                            if (nextTag != "outOfBounds")
                            {
                                lookingAt = gFloor.getObject(((int)transform.position.x / 2), ((int)transform.position.z / 2) - 1);
                                StartCoroutine(MoveTable(Vector3.back));
                            }
                            break;
                        case "Cusion": break;
                        case "Plank": break;
                    }
                }
            }
            else
            {
                faceDirection = Vector3.back;
                gameObject.transform.GetChild(0).transform.localPosition = cushionOffset * faceDirection;
                StartCoroutine(Delay());
            }
        }
      if (Input.GetKey(KeyCode.A) && !Moving)
        {
            if (faceDirection == Vector3.left)
            {
                nextTag = gFloor.getLocation(((int)transform.position.x / 2) - 1, ((int)transform.position.z / 2));
                if (nextTag != "outOfBounds")
                {
                    faceDirection = Vector3.left;
                    switch (nextTag)
                    {
                        case "Air": StartCoroutine(MovePlayer(Vector3.left)); break;
                        case "Table":
                            nextTag = gFloor.getLocation(((int)transform.position.x / 2) - 2, ((int)transform.position.z / 2));
                            if (nextTag != "outOfBounds")
                            {
                                lookingAt = gFloor.getObject(((int)transform.position.x / 2) - 1, ((int)transform.position.z / 2));
                                StartCoroutine(MoveTable(Vector3.left));
                            }
                            break;
                        case "Cusion": break;
                        case "Plank": break;
                    }
                }
            }
            else
            {
                faceDirection = Vector3.left;
                gameObject.transform.GetChild(0).transform.localPosition = cushionOffset * faceDirection;
                StartCoroutine(Delay());
            }
        }
      if (Input.GetKey(KeyCode.D) && !Moving)
        {
            if (faceDirection == Vector3.right)
            {
                nextTag = gFloor.getLocation(((int)transform.position.x / 2) + 1, ((int)transform.position.z / 2));
                if (nextTag != "outOfBounds")
                {
                    faceDirection = Vector3.right;
                    switch (nextTag)
                    {
                        case "Air": StartCoroutine(MovePlayer(Vector3.right)); break;
                        case "Table":
                            nextTag = gFloor.getLocation(((int)transform.position.x / 2) + 2, ((int)transform.position.z / 2));
                            if (nextTag != "outOfBounds")
                            {
                                lookingAt = gFloor.getObject(((int)transform.position.x / 2) + 1, ((int)transform.position.z / 2));
                                StartCoroutine(MoveTable(Vector3.right));
                            }
                            break;
                        case "Cusion": break;
                        case "Plank": break;
                    }
                }
            }
            else
            {
                faceDirection = Vector3.right;
                StartCoroutine(Delay());
                gameObject.transform.GetChild(0).transform.localPosition = cushionOffset * faceDirection;
            }

        }
        if (Input.GetKey(KeyCode.F) && !Moving)
        {
            if (!hasCusion)
            {


                if (faceDirection.x != 0.0f)
                {
                    nextTag = gFloor.getLocation(((int)transform.position.x / 2) + (int)faceDirection.x, ((int)transform.position.z / 2));//check next pos
                    if (nextTag != "outOfBounds")
                    {
                        switch (nextTag)//check tag of next area
                        {
                            case "Air": break;
                            case "Table":
                                nextTag=gMid.getLocation(((int)transform.position.x / 2) + (int)faceDirection.x, ((int)transform.position.z / 2));
                                if (nextTag == "Cusion")
                                {
                                    gMid.RemoveCushion(((int)transform.position.x / 2) + (int)faceDirection.x, ((int)transform.position.z / 2));
                                    cushion =gMid.getObject(((int)transform.position.x / 2) + (int)faceDirection.x, ((int)transform.position.z / 2));
                                    Destroy(cushion);
                                    hasCusion = true;
                                    gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                                    StartCoroutine(Delay());
                                    break;
                                }
                                break;

                            case "Cusion":
                                gFloor.RemoveCushion(((int)transform.position.x / 2)+(int)faceDirection.x, ((int)transform.position.z / 2));
                                Destroy(gFloor.getObject(((int)transform.position.x / 2)+(int)faceDirection.x, ((int)transform.position.z / 2)));
                                hasCusion = true;
                                gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                                StartCoroutine(Delay());
                                break;
                            case "Plank": break;
                        }
                    }
                }
                if (faceDirection.z != 0.0f)
                {
                    nextTag = gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);//check next pos
                    if (nextTag != "outOfBounds")
                    {
                        switch (nextTag)//check tag of next area
                        {
                            case "Air": break;
                            case "Table":
                                nextTag = gMid.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);
                                if (nextTag == "Cusion")
                                {
                                    gMid.RemoveCushion(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);
                                    cushion = gMid.getObject(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);
                                    Destroy(cushion);
                                    hasCusion = true;
                                    gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                                    StartCoroutine(Delay());
                                    break;
                                }
                                break;
                            case "Cusion":
                                gFloor.RemoveCushion(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);
                                Destroy(gFloor.getObject(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z));
                                hasCusion = true;
                                gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                                StartCoroutine(Delay());
                                break;
                            case "Plank": break;
                        }
                    }
                }
            }
            else
            {
                if (faceDirection.x != 0.0f)
                {
                    nextTag = gFloor.getLocation(((int)transform.position.x / 2)+(int)faceDirection.x, ((int)transform.position.z / 2));//check next pos
                    if (nextTag != "outOfBounds")
                    {
                        switch (nextTag)//check tag of next area
                        {
                            case "Air":
                                gFloor.addCushion((((int)transform.position.x / 2) + (int)faceDirection.x), ((int)transform.position.z / 2));
                                hasCusion = false;
                                gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                                StartCoroutine(Delay());
                                break;
                            case "Table":
                                nextTag=gMid.getLocation(((int)transform.position.x / 2) + (int)faceDirection.x, ((int)transform.position.z / 2));
                                if (nextTag == "Air")
                                {
                                    gMid.addCushion((((int)transform.position.x / 2) + (int)faceDirection.x), ((int)transform.position.z / 2));
                                    hasCusion = false;
                                    gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                                    StartCoroutine(Delay());
                                }
                                break;
                            case "Cusion": break;
                            case "Plank": break;
                        }
                    }
                }
                if (faceDirection.z != 0.0f)
                {
                    nextTag = gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);//check next pos
                    if (nextTag != "outOfBounds")
                    {
                        switch (nextTag)//check tag of next area
                        {
                            case "Air": gFloor.addCushion(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);
                                    hasCusion = false;
                                gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                                StartCoroutine(Delay());
                                break;
                            case "Table":
                                nextTag=gMid.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);
                                if (nextTag == "Air")
                                {
                                    gMid.addCushion(((int)transform.position.x / 2), ((int)transform.position.z / 2) + (int)faceDirection.z);
                                    hasCusion = false;
                                    gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                                    StartCoroutine(Delay());
                                }
                                break;
                            case "Cusion": break;
                            case "Plank": break;
                        }
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.G) && !Moving)
        {
            GameObject.Find("Ball").GetComponent<BallController>().moveStart();
        }
    }
    private IEnumerator MovePlayer( Vector3 direction)
    {
        Moving = true;

        float eTime = 0;
        origPos = transform.position;
        targetPos = origPos+direction*dist;
        while(eTime < timeToMove) 
        {
            transform.position =  Vector3.Lerp(origPos, targetPos, (eTime/timeToMove));
            eTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        Moving = false;
    }
    private IEnumerator MoveTable(Vector3 direction)
    {
        Moving = true;

        float eTime = 0;
        origPos = lookingAt.transform.position;
        targetPos = origPos + direction * dist;
        nextTag = gFloor.getLocation(((int)targetPos.x / 2), ((int)targetPos.z / 2));
        if (nextTag == "Air")
        {
            if (lookingAt.transform.childCount > 0)
            {
                nextTag = gMid.getLocation(((int)targetPos.x / 2), ((int)targetPos.z / 2));
                if (nextTag == "Air")
                {
                    while (eTime < timeToMove)
                    {
                        lookingAt.transform.position = Vector3.Lerp(origPos, targetPos, (eTime / timeToMove));
                        eTime += Time.deltaTime;
                        yield return null;
                    }
                    lookingAt.transform.position = targetPos;
                    Moving = false;
                    gFloor.updateGridTableMove(((int)origPos.x / 2), ((int)origPos.z / 2), direction);
                    gMid.updateGridCushionMove(((int)origPos.x / 2), ((int)origPos.z / 2), direction);
                }
            }
            else
            {
                while (eTime < timeToMove)
                {
                    lookingAt.transform.position = Vector3.Lerp(origPos, targetPos, (eTime / timeToMove));
                    eTime += Time.deltaTime;
                    yield return null;
                }
                lookingAt.transform.position = targetPos;
                Moving = false;
                gFloor.updateGridTableMove(((int)origPos.x / 2), ((int)origPos.z / 2), direction);
            }
        }
    }
    private IEnumerator Delay()
    {
        {
            Moving = true;
            float eTime = 0;
            while (eTime < timeToMove)
            {
                eTime += Time.deltaTime;
                yield return null;
            }
            Moving = false;
        }
    }
}
