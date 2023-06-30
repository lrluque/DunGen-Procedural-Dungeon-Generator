using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    // Up Down Right Left -> 0 1 2 3

    public GameObject[] walls; 


    /// If status[i] = true theres no wall.
    public void UpdateRoom(bool[] status)
    {
        for(int i = 0; i < status.Length; i++)
        {
            walls[i].SetActive(!status[i]);
        }
    }
}
