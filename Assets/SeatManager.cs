using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
    public Movement movement;
    public Transform[] seats;
    int nextOpenSeat = 0;

    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (movement.driver == null) movement.driver = g.GetComponent<PlayerLook>();
            else if (Radio.controller == null) Radio.controller = g.GetComponent<PlayerLook>();
            else if (PlayerLook.kicker == null) PlayerLook.kicker = g.GetComponent<PlayerLook>();
            g.transform.SetParent(seats[nextOpenSeat]);
            g.transform.localPosition = Vector3.zero;
            nextOpenSeat++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
