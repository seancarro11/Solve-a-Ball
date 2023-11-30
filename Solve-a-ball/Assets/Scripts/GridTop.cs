using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTop : MonoBehaviour
{
    // Start is called before the first frame update
    public int rows;
    public int columns;
    public string[,] objList;
    private string defults = "Air";
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
    public void addData(Vector3 pos, string tag)
    {
        //objList[ (int)pos.x/2, (int)pos.z/2]=tag;
        //PrintArray();
        if ((int)pos.x / 2 >= 0 && (int)pos.x / 2 < rows && (int)pos.z / 2 >= 0 && (int)pos.z / 2 < columns)
        {
            // Update the value at the specified position
            objList[(int)pos.x / 2, (int)pos.z / 2] = tag;
            //Debug.Log( (int)pos.x / 2 + " " + (int)pos.z / 2);
        }
        else
        {
            // Handle an out-of-bounds error (you may choose to log a warning or take other actions)
            Debug.LogWarning("Attempted to access a position outside the array bounds." + (int)pos.x / 2 + " " + (int)pos.z / 2);
        }
    }


    public string getLocation(int i, int j)
    {
        //Debug.Log(""+i+j);
        // Debug.Log(objList[i, j]);
        return objList[i, j];
    }
}
