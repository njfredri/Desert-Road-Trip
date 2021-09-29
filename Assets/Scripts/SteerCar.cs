using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerCar : MonoBehaviour
{
    public float disToSideOfRoad = 2.5f;
    public float disToStuck = 10;

    public bool isOffRoad = false;
    public static bool isStuck = false;
    float countIsStuck = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isStuck);
        if(isStuck) countIsStuck += Time.deltaTime;
        //Debug.Log(countIsStuck);
        if (countIsStuck > 4.0)
        {
            this.transform.position = new Vector3(0, 0, 0);
            Movement.uEulerDirection = 0;
            isOffRoad = false;
            isStuck = false;
            countIsStuck = 0;
        }
        checkIfOffRoad();

        this.transform.eulerAngles = new Vector3(0, Movement.uEulerDirection, 0);
        this.transform.Translate(new Vector3(Movement.uMovement.x * Time.deltaTime, 0, 0));
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
    }

    void checkIfOffRoad()
    {
        if (Mathf.Abs(this.transform.position.x) > disToSideOfRoad)
        {
            this.isOffRoad = true;
            //Debug.Log("Obama");
        }
        else
        {
            this.isOffRoad = false;
        }

        if (Mathf.Abs(this.transform.position.x) > disToStuck)
        {
            SteerCar.isStuck = true;
            //Debug.Log("stucj");
        }
        else
        {
            SteerCar.isStuck = false;
        }
    }
}
