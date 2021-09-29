using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Texture[] image;

    // Start is called before the first frame update
    void Start()
    {
        //print("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
        GetComponent<MeshRenderer>().material.mainTexture = image[Random.Range(0, image.Length)];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
