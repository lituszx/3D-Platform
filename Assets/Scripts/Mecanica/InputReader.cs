using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public GameObject clone, player;
    public int maxClones = 1, currentClones = 0;
    public void Update()
    {
        if(currentClones != maxClones)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentClones++;
                GameObject tempPlayer = Instantiate(clone, transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }
    public Vector3 ReadInput()
    {
        float x = 0;
        if (Input.GetKey(KeyCode.A))
            x = -2;
        else if (Input.GetKey(KeyCode.D))
            x = 2;
        float y = 0;
        float z = 0;
        if (Input.GetKey(KeyCode.W))
            z = 2;
        else if (Input.GetKey(KeyCode.S))
            z = -2;

        if( x !=0 || y !=0 ||z !=0)
        {
            Vector3 direction = new Vector3(x, y, z);
            return direction;
        }
        return Vector3.zero;    
    }
    internal bool ReadUndo()
    {
        return Input.GetKey(KeyCode.R);
    }
}
