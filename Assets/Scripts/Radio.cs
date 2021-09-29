using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Radio : MonoBehaviour
{
    public static Radio instance;
    public List<RadioStation> Stations;
    public int stationIndex = 1;
    private List<AudioSource>[] stations;
    public static PlayerLook controller;

    void Awake()
    {
        instance = this;
        foreach (RadioStation r in Stations)
        {
            r.GenerateSources();
            if (!r.emptyStation) r.GetNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach (RadioStation r in Stations)
        {
            if (r.songObjects.Count > 0)
            {
                if (!r.current) r.GetNext();
                if (r.current.clip.length > r.curSongPlayTime)
                {
                    r.GetNext();
                }

                r.curSongPlayTime+= Time.deltaTime;
                r.current.volume = 0;
            }
        }
        */

        if (controller)
        {
            if (controller.useKeyboard)
            {
                if (Keyboard.current.iKey.wasPressedThisFrame) stationIndex+= 1;
                if (Keyboard.current.uKey.wasPressedThisFrame) stationIndex-= 1;
            }
            else if (controller.gamepad.enabled)
            {
                if (controller.gamepad.rightTrigger.wasPressedThisFrame) stationIndex += 1;
                if (controller.gamepad.leftTrigger.wasPressedThisFrame) stationIndex -= 1;
            }
        }
        stationIndex+= Stations.Count;
        stationIndex %= Stations.Count;

        for (int i = 0; i < Stations.Count; i++)
        {
            if (Stations[i].emptyStation) continue;
            //Debug.Log(i);
            //Debug.Log(stationIndex);
            //Debug.Log("Pe " + Stations.Count);
            if (!Stations[stationIndex].current) Stations[stationIndex].GetNext();
            if (i == stationIndex) Stations[i].current.UnPause();
            else if (Stations[i].current.isPlaying) Stations[i].current.Pause();
        }
        if (stationIndex != 0)
        {
            if (Stations[stationIndex].curSongPlayTime > Stations[stationIndex].current.clip.length)
                Stations[stationIndex].GetNext();
            Stations[stationIndex].curSongPlayTime+= Time.deltaTime;
        }

    }

    [System.Serializable]
    public class RadioStation
    {
        public string RadioName;
        //public GameObject radio;
        public List<Sound> songs;
        public List<AudioSource> songObjects;
        public AudioSource current;
        public float curSongPlayTime = 0.0f;
        public bool emptyStation;

        public void GenerateSources()
        {
            GameObject radio = Radio.instance.gameObject;
            foreach (Sound s in songs)
            {
                AudioSource current = radio.AddComponent<AudioSource>();
                //current.name = s.Name;
                current.clip = s.source;
                current.loop = false;
                current.playOnAwake = false;
                current.volume = s.volume;
                //current.Play();
                songObjects.Add(current);
            }
        }
        
        public void GetNext()
        {
            if (emptyStation) return;
            current = songObjects[Random.Range(0, songObjects.Count)];
            if (!current.isPlaying) current.Play();
            curSongPlayTime = 0.0f;
        }
    }
}
