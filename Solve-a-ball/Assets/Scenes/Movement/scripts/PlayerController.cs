using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float dist;
    bool moving;
    Vector3 origPos, targetPos;
    float timeToMove = 0.2f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && !moving)
        {
           StartCoroutine(MovePlayer(Vector3.forward));
        }
        if (Input.GetKey(KeyCode.S) && !moving)
        {
            StartCoroutine(MovePlayer(Vector3.back));
        }
        if (Input.GetKey(KeyCode.A) && !moving)
        {
            StartCoroutine(MovePlayer(Vector3.left));
        }
        if (Input.GetKey(KeyCode.D) && !moving)
        {
            StartCoroutine(MovePlayer(Vector3.right));
        }
    }
    private IEnumerator MovePlayer(Vector3 direction)
    {
        moving = true;
        float eTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction * dist;
        while (eTime< timeToMove) 
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (eTime / timeToMove));
            eTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    
        moving = false;
    }
}