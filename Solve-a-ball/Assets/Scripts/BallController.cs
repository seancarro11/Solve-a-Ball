using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    public int startHeight;
    private int ballHeight;
    public float dist;
    bool Moving,isFalling, created,dead;
    public Vector3 tempPos;
    Vector3 targetPos, origPos,curHDrection, curDrection, curGridPos;
    float timeToMove = 0.2f, timeToMove1 = 0.2f;
    public Vector3 startDirection;
    private GridFloor gFloor;
    private GridMid gMid;
    private GridTop gTop;
    private string belowTag, forwardTag;
    public AudioClip ballMove,unlock;
    private AudioSource audioSource;
    private bool plankDir;
    // Start is called before the first frame update
    void Start()
    {
        curHDrection = startDirection;
        ballHeight = startHeight;
        gFloor = GameObject.Find("GM").GetComponent<GridFloor>();
        gMid = GameObject.Find("GM").GetComponent<GridMid>();
        gTop = GameObject.Find("GM").GetComponent<GridTop>();
        audioSource = gameObject.GetComponent<AudioSource>();
        Invoke("intiDelay", 3.0f);
        Moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void moveStart()
    {
        if (created)
        {
            InvokeRepeating("Move", 1.0f, 2.0f);
            created = false;
        }
    }
    void Move()
    {
        if (!dead)
        {
            if (!Moving)
            {


                curGridPos = transform.position / 2;
                //checkUnder if something or on ground. false fall (direction down).true keep going
                if (ballHeight > 1)
                {
                    switch (ballHeight - 1)
                    {
                        case 1: belowTag = gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2)); break;
                        case 2: belowTag = gMid.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2)); break;
                    }
                    switch (belowTag)
                    {
                        case "Air": curDrection = Vector3.down; StartCoroutine(MoveBall(curDrection)); ballHeight = ballHeight - 1; isFalling = true; break;
                        case "Table":
                            if (isFalling == true)
                            {
                                Debug.Log("Shatter");
                                shatter();
                            }
                            else
                            {
                                checkAhead(); break;
                            }
                            break;
                        case "Wall":
                            if (isFalling == true)
                            {
                                Debug.Log("Shatter");
                                shatter();
                            }
                            else
                            {
                                checkAhead(); break;
                            }
                            break;
                        case "Cusion": curDrection = Vector3.down; StartCoroutine(MoveBall(curDrection)); ballHeight = ballHeight - 1; break;//falls to cushion
                        case "Plank":
                            Debug.Log("Shatter");
                            break;
                    }
                }
                else
                {
                    belowTag = gFloor.getLocation(((int)transform.position.x / 2), ((int)transform.position.z / 2));
                    if (belowTag == "Cusion" && isFalling == true)
                    {
                        isFalling = false;
                        checkAhead();
                    }
                    else if (isFalling == false)
                    {
                        checkAhead();
                    }
                    else
                    {
                        Debug.Log("Shatter");
                        shatter();
                    }
                }
                //Debug.Log("Tryed to move");
            }
            void checkAhead()       //check one forward in current direction,if false move there. true collide(stop or break)
            {
                switch (ballHeight)
                {
                    case 1: forwardTag = gFloor.getLocation(((int)transform.position.x / 2) + ((int)curHDrection.x), ((int)transform.position.z / 2) + ((int)curHDrection.z)); break;
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
                    case "Table":
                        Debug.Log("Shatter");
                        StartCoroutine(MoveBall(curHDrection));
                        StartCoroutine(Delay());
                        break;
                    case "Wall": Debug.Log("Shatter"); shatter(); break;
                    case "Cusion": break;
                    case "Plank":
                        if (!Moving)
                        {
                            tempPos = transform.position;
                            Invoke("updateDir", timeToMove);
                            StartCoroutine(MoveBall(curHDrection));
                        }
                        break;
                    case "Door": StartCoroutine(MoveBall(curHDrection)); Debug.Log("Unlocked Door"); UnlockDoor(); break;
                }
            }
        }
    }
    private IEnumerator MoveBall(Vector3 direction)
    {
        Moving = true;

        float eTime = 0;
        origPos = transform.position;
        audioSource.PlayOneShot(ballMove);
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
    private void updateDir()
    {
        switch (ballHeight)
        {
            case 1: plankDir = (gFloor.getObject(((int)tempPos.x / 2) + ((int)curHDrection.x), ((int)tempPos.z / 2) + ((int)curHDrection.z))).GetComponent<PlankController>().getCurDirection();
                if (plankDir)
                {
                    if (curHDrection == Vector3.right)
                    {
                        curHDrection= Vector3.back;
                    }
                    else if (curHDrection == Vector3.left)
                    {
                        curHDrection = Vector3.forward;
                    }
                    else if (curHDrection == Vector3.back)
                    {
                        curHDrection = Vector3.right;
                    }
                    else if (curHDrection == Vector3.forward)
                    {
                        curHDrection = Vector3.left;
                    }
                }
                else
                {
                    if (curHDrection == Vector3.right)
                    {
                        curHDrection = Vector3.forward;
                    }
                    else if (curHDrection == Vector3.left)
                    {
                        curHDrection = Vector3.back;
                    }
                    else if (curHDrection == Vector3.back)
                    {
                        curHDrection = Vector3.left;
                    }
                    else if (curHDrection == Vector3.forward)
                    {
                        curHDrection = Vector3.right;
                    }
                }
                break;
            
            
            case 2:
                plankDir = (gMid.getObject(((int)transform.position.x / 2) + ((int)curHDrection.x), ((int)transform.position.z / 2) + ((int)curHDrection.z))).GetComponent<PlankController>().getCurDirection();
                if (plankDir)
                {
                    if (curHDrection == Vector3.right)
                    {
                        curHDrection = Vector3.back;
                    }
                    if (curHDrection == Vector3.left)
                    {
                        curHDrection = Vector3.up;
                    }
                    if (curHDrection == Vector3.back)
                    {
                        curHDrection = Vector3.right;
                    }
                    if (curHDrection == Vector3.forward)
                    {
                        curHDrection = Vector3.left;
                    }
                }
                else
                {
                    if (curHDrection == Vector3.right)
                    {
                        curHDrection = Vector3.up;
                    }
                    if (curHDrection == Vector3.left)
                    {
                        curHDrection = Vector3.down;
                    }
                    if (curHDrection == Vector3.back)
                    {
                        curHDrection = Vector3.left;
                    }
                    if (curHDrection == Vector3.forward)
                    {
                        curHDrection = Vector3.right;
                    }
                }
                break;
                //case 3:curHDrection = (g.getObject(((int)transform.position.x / 2) + ((int)curHDrection.x), ((int)transform.position.z / 2) + ((int)curHDrection.z))).GetComponent<PlankController>().getCurDirection(); break;
        }
    }
    private IEnumerator Delay()
    {
        {
            float eTime1 = 0;
            while (eTime1 < timeToMove1/2.0f)
            {
                eTime1 += Time.deltaTime;
                yield return null;
            }
            shatter();
        }
    }
    private void intiDelay()
    {
        created = true;
    }
    void UnlockDoor()
    {
        audioSource.PlayOneShot(ballMove);
        
        Invoke("nextLevel", 5.0f);
    }
    void nextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    void shatter()
    {
        transform.GetComponent<Shattering>().Explode();
        transform.GetChild(0).transform.GetComponent<Shattering>().Explode();
        dead = true;
        //Destroy(gameObject);
    }
}
