using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallStuffSpawner : MonoBehaviour
{
    float distanceSinceLastGeneration = 0;
    public float spawnDistance = 10;
    public float despawnDistance = 10;
    public float[] horizontalDistMin; //absolute value
    public float[] horizontalDistMax;
    public GameObject[] structures;
    List<GameObject> currentStructures = new List<GameObject>();
    public float[] likelyhoodOutOf1000;

    void Start()
    {
    }


    void Update()
    {
        distanceSinceLastGeneration += Movement.uMovement.z * Time.deltaTime;
        if (distanceSinceLastGeneration > 10)
        {
            SpawnStructure();
            distanceSinceLastGeneration = 0;
        }

        for (int i = 0; i < currentStructures.Count; i++)
        {
            GameObject g = currentStructures[i];
            g.transform.Translate(Vector3.back * Movement.uMovement.z * Time.deltaTime);
            if (g.transform.position.z < despawnDistance * -1)
            {
                currentStructures.Remove(g);
                i--;
                Destroy(g);
            }
        }
    }

    void SpawnStructure()
    {
        for (int i = Random.Range(1, 20); i > 0; i--)
        {
            float xOffset = Random.Range(0, 20);
            float zOffset = Random.Range(0, 20);
            int choicenum = Random.Range(0, structures.Length);
            GameObject choice = this.structures[choicenum];
            GameObject newWonder;
            if (Random.Range(0, 1000) < likelyhoodOutOf1000[choicenum])
            {
                if (Random.Range(0, 2) == 0)
                {
                    newWonder = Instantiate(choice, new Vector3(xOffset + Random.Range(horizontalDistMin[choicenum], horizontalDistMax[choicenum]), 0, spawnDistance + zOffset), Quaternion.identity);
                    newWonder.transform.parent = this.transform;
                }
                else
                {
                    newWonder = Instantiate(choice, new Vector3(-xOffset - 1 * Random.Range(horizontalDistMin[choicenum], horizontalDistMax[choicenum]), 0, spawnDistance + zOffset), Quaternion.identity);
                    newWonder.transform.localScale.Set(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                currentStructures.Add(newWonder);
            }
        }
    }
}
