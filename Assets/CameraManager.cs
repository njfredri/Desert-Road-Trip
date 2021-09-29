using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    int playerCount;
    int screens;
    List<Camera> playerCams = new List<Camera>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            playerCams.Add(g.GetComponent<Camera>());
        }
        playerCount = playerCams.Count;

        switch (playerCount)
        {
            case 1:
                playerCams[0].rect = new Rect(0, 0, 1, 1);
                break;
            case 2:
                playerCams[0].rect = new Rect(0, 0.5f, 1, 0.5f);
                playerCams[1].rect = new Rect(0, 0, 1, 0.5f);
                break;
            case 3:
                playerCams[0].rect = new Rect(0, 0.5f, 1, 0.5f);
                playerCams[1].rect = new Rect(0, 0, 0.5f, 0.5f);
                playerCams[2].rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;
            case 4:
                playerCams[0].rect = new Rect(0,    0.5f, 0.5f, 0.5f);
                playerCams[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                playerCams[2].rect = new Rect(0,    0,    0.5f, 0.5f);
                playerCams[3].rect = new Rect(0.5f, 0,    0.5f, 0.5f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
