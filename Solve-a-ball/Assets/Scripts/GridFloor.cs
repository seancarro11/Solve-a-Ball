using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class GridFloor : MonoBehaviour
{
    // Start is called before the first frame update
    public int rows;
    public int columns;
    public string[,] objList;
    private string defults = "Air";
    public GameObject cushion, plank;
    private void Awake()
    {
        objList = new string[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                objList[i, j] = defults;

            }
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PrintArray()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Debug.Log(objList[i, j]);
            }
        }
    }
    public void  addData(Vector3 pos,string tag)
    {
        //objList[ (int)pos.x/2, (int)pos.z/2]=tag;
        //PrintArray();
        if ((int)pos.x / 2 >= 0 && (int)pos.x / 2 < rows && (int)pos.z / 2 >= 0 && (int)pos.z / 2 < columns)
        {
            // Update the value at the specified position
            objList[(int)pos.x/2, (int)pos.z / 2] = tag;
            //Debug.Log( (int)pos.x / 2 + " " + (int)pos.z / 2);
        }
        else
        {
            // Handle an out-of-bounds error (you may choose to log a warning or take other actions)
            Debug.LogWarning("Attempted to access a position outside the array bounds."+ (int)pos.x / 2+" "+ (int)pos.z / 2);
        }
    }


    public string getLocation(int i,int j)
    {
        if(i>=0&&i<=rows-1 && j>=0&&j<=columns-1)
        {
            return objList[i, j];
        }
        return "outOfBounds";
    }
    public GameObject getObject(int i, int j)
    {

        RaycastHit hitInfo;
        GameObject temp;
        //Debug.Log(""+i+j);
        // Debug.Log(objList[i, j]);
            Physics.Raycast(new Vector3(i * 2, 0.0f, j * 2), Vector3.up, out hitInfo, 0.5f);
        temp = hitInfo.transform.gameObject;
        return temp;
    }
    public void updateGridTableMove(int i, int j,Vector3 dirction)
    {
        objList[i, j] = "Air";
        if (dirction.x != 0.0f)
        {
            i = i+(int)dirction.x;
        }
        if (dirction.z != 0.0f)
        {
            j = j+(int)dirction.z;
        }
        objList[i, j] = "Table";
    }
    public void RemoveCushion(int i, int j)
    {
        objList[i, j] = "Air";
    }
    public void addCushion(int i, int j)
    {
        objList[i, j] = "Cusion";
        Instantiate(cushion, new Vector3(i*2, 1, j*2), Quaternion.identity);//have to edit the y 
    }
}
