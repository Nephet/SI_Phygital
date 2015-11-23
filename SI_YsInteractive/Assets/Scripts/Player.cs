using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public int ID;
    public int health;
    public bool isShieldUp;
    public TYPE playerType;
    public int Damage;
    public bool actionReady;
	// Use this for initialization

    void OnGUI()
    {
        if(actionReady)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(this.transform.position);
            if (GUI.Button(new Rect(new Vector2(pos.x, pos.y), new Vector2(50f, 50f)), "jouer"))
            {
                Debug.Log("joueur " + ID + " a jouer");
                actionReady = false;
            };
        }
    }

	void Start () {

        playerType = TYPE.NONE;
        isShieldUp = false;
        health = 10;
        Damage = 1;
        actionReady = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (actionReady)
        {
            
        }
	}

    public bool turnEnded()
    {
        return !actionReady;
    }

    public void setTurn(bool turn)
    {
        actionReady = turn;
    }

    public void setType(TYPE elem)
    {
        playerType = elem;
    }

    public void setID(int id)
    {
        ID = id;
		SlingShot ss= gameObject.GetComponent<SlingShot>();
		ss.myID = id;
    }

    void takeDamage(int damage)
    {
        if(isShieldUp)
        {
            // no dmg -> animation de blockage ?
        }
        else
        {
            // get dmg
            if(health > damage)
            {
                health -= damage;
            }
            else
            {
                // mort
            }
        }
    }

    void attackCAC()
    {

    }



}
