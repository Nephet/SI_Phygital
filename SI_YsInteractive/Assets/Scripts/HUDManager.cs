﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
    

public class HUDManager : Singleton<HUDManager> {
    protected HUDManager() { }
    GameObject player1GO=null, player2GO=null;
    List<GameObject> p1LifePoints = new List<GameObject>();
    List<GameObject> p2LifePoints = new List<GameObject>();
    GameObject p1Life, p2Life;
    public GameObject LifePoint;
    // Use this for initialization
	void Start () {
        p1Life = new GameObject();
        p1Life.name = "LifeBar1";
        p2Life = new GameObject();
        p2Life.name = "LifeBar2";
	}
	
	// Update is called once per frame
	void Update () {
        if(player1GO != null && player2GO !=null)
        {
            p1Life.transform.position = new Vector3(player1GO.transform.position.x, 0.0f, player1GO.transform.position.z - transform.localScale.z);
            p2Life.transform.position = new Vector3(player2GO.transform.position.x, 0.0f, player2GO.transform.position.z - transform.localScale.z);
        }
	}

    public void setPlayer(int ID, GameObject player)
    {
        if(ID == 1)
        {
            player1GO = player;
            p1Life.transform.parent = player1GO.transform;
        }
        else
        {
            player2GO = player;
            p2Life.transform.parent = player2GO.transform;
        }
    }

    public void setLifeUI(int ID, int Life)
    {
        GameObject cache;
        if(ID == 1)
        {
            
            foreach(GameObject go in p1LifePoints)
            {
                Destroy(go);
            }
            p1LifePoints.Clear();
            for (int i = 0; i < Life;i++ )
            {
                cache = (GameObject)Instantiate(LifePoint, new Vector3((i * 0.4f), 0, 0), Quaternion.Euler(90, 0, 0));
                cache.transform.parent = p1Life.transform;
                p1LifePoints.Add(cache);
            }
            
        }
        else
        {
            foreach (GameObject go in p2LifePoints)
            {
                Destroy(go);
            }
            p2LifePoints.Clear();
            for (int i = 0; i < Life; i++)
            {
                cache = (GameObject)Instantiate(LifePoint, new Vector3((i * 0.4f), 0, 0), Quaternion.Euler(90, 0, 0));
                cache.transform.parent = p2Life.transform;
                p2LifePoints.Add(cache);
                
            }
            
        }
        
    }
}