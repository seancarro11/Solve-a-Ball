using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float dist;
    bool Moving;
    Vector3 targetPos, origPos;
    float timeToMove = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.W)&&!Moving)
        {
            StartCoroutine(MovePlayer(Vector3.forward));
        }
      if (Input.GetKey(KeyCode.S) && !Moving)
        {
            StartCoroutine(MovePlayer(Vector3.back));
        }
      if (Input.GetKey(KeyCode.A) && !Moving)
        {
            StartCoroutine(MovePlayer(Vector3.left));
        }
      if (Input.GetKey(KeyCode.D) && !Moving)
        {
            StartCoroutine(MovePlayer(Vector3.right));
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
}
