using UnityEngine;
using System.Collections;

public enum TYPE
{
    NONE,
    FEU,
    EAU,
    AIR
};

public class GameManagerScript : MonoBehaviour {
    enum STATE
    {
        MENU,
        START,
        RUN,
        END

    }; 

    public GameObject Player1;
    public GameObject Player2;
    Player scriptP1;
    Player scriptP2;

    public GameObject PlayerPrefab;
    STATE state;
    bool p1Turn;

    void OnGUI()
    {
        if (Player1 != null && Player2 != null && state == STATE.START) state = STATE.RUN;
            /*if (GUI.Button(new Rect(Screen.width/2-40, 1, 80, 30), "Ready"))
            {
                Debug.Log("Let's Fight!");
                if (Random.Range(0, 2) > 1) p1Turn = true;
                changeTurn();
                state = STATE.RUN;
            };*/
    }

	// Use this for initialization
	void Start () {
        state = STATE.START;
        Player1 = null;
        Player2 = null;
	}
	
	// Update is called once per frame
	void Update () {
	    switch(state)
        {
            case STATE.MENU:
                break;
            case STATE.START:
                playerPosition();
                break;
            case STATE.RUN:
                gameRunning();
                break;
            case STATE.END:
                break;
            default: 
                break;
        }
	}

    void gameRunning()
    {
        if (p1Turn && scriptP1.turnEnded())
        {
            changeTurn();
        }
        if (!p1Turn && scriptP2.turnEnded())
        {
            changeTurn();
        }

    }

    void changeTurn()
    {
        Debug.Log("Tour du joueur 1 : " + p1Turn);
        p1Turn = !p1Turn;
        scriptP1.setTurn(p1Turn);
        scriptP2.setTurn(!p1Turn);
    }

    void playerPosition()
    {
        // Les joueurs choissisent leurs positions
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("TOUCH");
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //screenPos to worldPos of mousePos
            pos = new Vector3(pos.x, pos.y, 0);                 //Pos of player in 2D

            if ((Input.mousePosition.x > (float)Screen.width / 2))
            {
                //Debug.Log("P1");
                if(Player1 == null)
                {
                    Player1 = (GameObject)Instantiate(PlayerPrefab, pos, new Quaternion());
                    Player1.name = "Player1";
                    scriptP1 = Player1.GetComponent<Player>();
                    scriptP1.setType(TYPE.FEU);
                    scriptP1.setID(1);
                }
                else
                {
                    Player1.transform.position = pos;
                }
            }
            else
            {
                //Debug.Log("P2");
                if (Player2 == null)
                {
                    Player2 = (GameObject)Instantiate(PlayerPrefab, pos, new Quaternion());
                    Player2.name = "Player2";
                    scriptP2 = Player2.GetComponent<Player>();
                    scriptP2.setType(TYPE.FEU);
                    scriptP2.setID(2);
                }
                else
                {
                    Player2.transform.position = pos;
                }
            }
        }
        
        
    }
}
