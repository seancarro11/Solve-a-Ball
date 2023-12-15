using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileInfo : MonoBehaviour
{
    public GameObject gameM;
    public Vector3[] checkPoints; // Assign the points in the inspector or via code
    // Start is called before the first frame update
    private int index=0;
    private void Awake()
    {
        gameM = GameObject.Find("GM");
    }
    void Start()
    {
        foreach (Vector3 point in checkPoints)
        {
          // Check for hits only on trigger colliders at the specified point with the specified tag
          RaycastHit hitInfo;
            if (Physics.Raycast(new Vector3(transform.position.x,point.y,transform.position.z), Vector3.up, out hitInfo, 0.5f) && hitInfo.collider.CompareTag("Table"))
            {
                // Do something when a trigger collider with the specified tag is detected at the specified point
                Debug.Log($"Trigger collider with tag 'Table' detected at point {point}!"+ transform.position.x+" "+ transform.position.z);
                switch (index)
                { 
                case 0: gameM.GetComponent<GridFloor>().addData(transform.position, "Table"); break;
                case 1: gameM.GetComponent<GridMid>().addData(transform.position, "Table"); break;
                case 2: gameM.GetComponent<GridTop>().addData(transform.position, "Table"); break;
                }
            }
            else if (Physics.Raycast(new Vector3(transform.position.x, point.y, transform.position.z), Vector3.up, out hitInfo, 0.5f) && hitInfo.collider.CompareTag("Cusion"))
            {
                // Do something when a trigger collider with the specified tag is detected at the specified point
                Debug.Log($"Trigger collider with tag 'Cusion' detected at point {point}!" + transform.position.x + " " + transform.position.z);
                switch (index)
                {
                    case 0: gameM.GetComponent<GridFloor>().addData(transform.position, "Cusion"); break;
                    case 1: gameM.GetComponent<GridMid>().addData(transform.position, "Cusion"); break;
                    case 2: gameM.GetComponent<GridTop>().addData(transform.position, "Cusion"); break;
                }
            }
            else if (Physics.Raycast(new Vector3(transform.position.x, point.y, transform.position.z), Vector3.up, out hitInfo, 0.5f) && hitInfo.collider.CompareTag("Plank"))
            {
                // Do something when a trigger collider with the specified tag is detected at the specified point
                Debug.Log($"Trigger collider with tag 'Plank' detected at point {point}!" + transform.position.x + " " + transform.position.z);
                switch (index)
                {
                    case 0: gameM.GetComponent<GridFloor>().addData(transform.position, "Plank"); break;
                    case 1: gameM.GetComponent<GridMid>().addData(transform.position, "Plank"); break;
                    case 2: gameM.GetComponent<GridTop>().addData(transform.position, "Plank"); break;
                }
            }
            else if (Physics.Raycast(new Vector3(transform.position.x, point.y, transform.position.z), Vector3.up, out hitInfo, 0.5f) && hitInfo.collider.CompareTag("Door"))
            {
                // Do something when a trigger collider with the specified tag is detected at the specified point
                Debug.Log($"Trigger collider with tag 'Door' detected at point {point}!" + transform.position.x + " " + transform.position.z);
                switch (index)
                {
                    case 0: gameM.GetComponent<GridFloor>().addData(transform.position, "Door"); break;
                    case 1: gameM.GetComponent<GridMid>().addData(transform.position, "Door"); break;
                    case 2: gameM.GetComponent<GridTop>().addData(transform.position, "Door"); break;
                }
            }
            else if (Physics.Raycast(new Vector3(transform.position.x, point.y, transform.position.z), Vector3.up, out hitInfo, 0.5f) && hitInfo.collider.CompareTag("Wall"))
            {
                // Do something when a trigger collider with the specified tag is detected at the specified point
                Debug.Log($"Trigger collider with tag 'Door' detected at point {point}!" + transform.position.x + " " + transform.position.z);
                switch (index)
                {
                    case 0: gameM.GetComponent<GridFloor>().addData(transform.position, "Wall"); break;
                    case 1: gameM.GetComponent<GridMid>().addData(transform.position, "Wall"); break;
                    case 2: gameM.GetComponent<GridTop>().addData(transform.position, "Wall"); break;
                }
            }
            else
            {
            }

            index++;
        }

    }
}

