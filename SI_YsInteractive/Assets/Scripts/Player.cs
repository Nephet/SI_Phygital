using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public int ID;
    public int health;
    public bool isShieldUp;
    public TYPE playerType;
    public int Damage;
    public bool actionReady;
    public bool attackReady;
	// Use this for initialization

    void OnGUI()
    {
        if(actionReady)
        {
            //Vector2 pos = Camera.main.WorldToScreenPoint(this.transform.position);
            /*if (GUI.Button(new Rect(new Vector2(pos.x, pos.y), new Vector2(50f, 50f)), "jouer"))
            {
                Debug.Log("joueur " + ID + " a jouer");
                actionReady = false;
            };*/
        }
    }

	void Start () {

        playerType = TYPE.NONE;
        isShieldUp = false;
        actionReady = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (actionReady)
        {
            
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if((GameManagerScript.instance.getCurrentID()== this.ID)&&(collision.collider.GetComponent<Player>())&&attackReady)
        {
            collision.collider.GetComponent<Player>().takeDamage(this.Damage);
            attackReady = false;
        }
    }

    public bool turnEnded()
    {
        return !actionReady;
    }

    public void setTurn(bool turn)
    {
        actionReady = turn;
        attackReady = turn;
    }

    public void setType(TYPE elem)
    {
        playerType = elem;
    }

    public void setID(int id)
    {
        ID = id;
		SlingShot ss= gameObject.GetComponentInChildren<SlingShot>();
		ss.myID = id;
    }

    public int getLife()
    {
        return health;
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
            if((health > damage)&&(damage > 0))
            {
                health -= damage;
                
            }
            else
            {
                Debug.Log("Player "+ID+" lost");
                // mort
            }
            HUDManager.Instance.updateLife(this.ID, health);
        }
    }

    void attackCAC()
    {

    }



}
