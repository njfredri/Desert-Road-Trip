using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceTravelled : MonoBehaviour
{
    // Start is called before the first frame update
    static double distanceTraveled = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += Movement.uMovement.z;
        TextMeshPro textmeshpro = GetComponent<TextMeshPro>();
        textmeshpro.SetText((long)(distanceTraveled/1000) + " traveled out of 999999999999");
    }
}
