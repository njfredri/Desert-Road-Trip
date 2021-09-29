using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderManager : MonoBehaviour
{

    float distanceSinceLastWonder = 0;
    public float distanceBetweenWonders = 100 * 60;
    public float spawnDistance = 10000;
    public float despawnDistance = 1000;
    public float[] horizontalDistMin;
    public float[] horizontalDistMax;
    public GameObject[] wonders;
    List<GameObject> currentWonders = new List<GameObject>();

    void Start()
    {
        
    }


    void Update()
    {
        distanceSinceLastWonder += Movement.uMovement.z * Time.deltaTime;
        if (distanceSinceLastWonder > distanceBetweenWonders)
        {
            SpawnWonder();
            distanceSinceLastWonder = 0;
        }

        for (int i = 0; i < currentWonders.Count; i++)
        {
            GameObject g = currentWonders[i];
            g.transform.Translate(Vector3.back * Movement.uMovement.z * Time.deltaTime);
            if (g.transform.position.z < despawnDistance * -1)
            {
                currentWonders.Remove(g);
                i--;
                Destroy(g);
            }
        }

    }

    void SpawnWonder()
    {
        int choicenum = Random.Range(0, wonders.Length);
        GameObject choice = wonders[choicenum];
        GameObject newWonder;
        //Debug.Log("bigigi");
        if (Random.Range(0, 2) == 0) 
        {
            newWonder = Instantiate(choice, new Vector3(Random.Range(horizontalDistMin[choicenum], horizontalDistMax[choicenum]), 0, spawnDistance), Quaternion.identity);
        }
        else 
        {
            newWonder = Instantiate(choice, new Vector3(-1 * Random.Range(horizontalDistMin[choicenum], horizontalDistMax[choicenum]), 0, spawnDistance), Quaternion.identity); 
            newWonder.transform.localScale.Set(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        currentWonders.Add(newWonder);
    }
}
