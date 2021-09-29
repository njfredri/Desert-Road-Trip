using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlane : MonoBehaviour
{
    static float frontSpwan = 500.0f;
    static float backDeath = -200.0f;

    bool madeRoadAhead = false;
    bool madeRoadSide = false;
    public float sizeOfPlane = 25;

    public enum Direction {Left, Forward, Right};
    public Direction myDir;
    static public int horizontalRoadAmount = 25;
    // Start is called before the first frame update
    void Start()
    {
        if (myDir == Direction.Left)
        {
            myDir = Direction.Forward;
            for (int i = 1; i <= horizontalRoadAmount; i++)
            {
                Vector3 location = this.transform.position + new Vector3((sizeOfPlane - 0.1f) * i * -1, 0, 0);
                Quaternion rotation = new Quaternion(0, 0, 0, 0);
                var newRoad = Instantiate(this, location, rotation);
                newRoad.transform.parent = GameObject.FindGameObjectWithTag("RoadParent").transform;
            }
        }
        if (myDir == Direction.Right)
        {
            myDir = Direction.Forward;
            for (int i = 1; i <= horizontalRoadAmount; i++)
            {
                Vector3 location = this.transform.position + new Vector3((sizeOfPlane - 0.1f) * i, 0, 0);
                Quaternion rotation = new Quaternion(0, 0, 0, 0);
                var newRoad = Instantiate(this, location, rotation);
                newRoad.transform.parent = GameObject.FindGameObjectWithTag("RoadParent").transform;
            }
        }
        checkForSpawn();
        this.name = "RoadPlane" + Random.Range(0, 10000);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, 0, -Movement.uMovement.z) * Time.deltaTime;
        this.transform.Translate(movement);
        checkForSpawn();
        checkForOutOfBoundsDestroy();
    }

    void checkForSpawn()
    {
        if (transform.position.z < frontSpwan && madeRoadAhead!=true) 
        {
            Vector3 location = this.transform.position + new Vector3(0, 0, sizeOfPlane-0.1f);
            Quaternion rotation = new Quaternion(0,0,0,0);
            var newRoad = Instantiate(this, location, rotation);
            newRoad.transform.parent = GameObject.FindGameObjectWithTag("RoadParent").transform;
            madeRoadAhead = true;
        }
    }

    void checkForOutOfBoundsDestroy()
    {
        if (transform.position.z < backDeath)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
