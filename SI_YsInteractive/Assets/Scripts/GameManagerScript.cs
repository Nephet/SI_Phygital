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

	static GameManagerScript mInst;
	static public GameManagerScript instance { get { return mInst; } }
	void Awake()
	{
		if (mInst == null) mInst = this;
		DontDestroyOnLoad(this);
	}

    public GameObject Player1;
    public GameObject Player2;
    Player scriptP1;
    Player scriptP2;

    public GameObject PlayerPrefab;
    STATE state;
    public bool p1Turn;
	public int currentId;

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
        //if (Random.Range(0, 4) > 2) p1Turn = true;
	}
	
	// Update is called once per frame
	void Update () {
	    switch(state)
        {
            case STATE.MENU:
                break;
            case STATE.START:
				playerStartPosition();
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

    public void changeTurn()
    {
        //Debug.Log("Tour du joueur 1 : " + p1Turn);
        p1Turn = !p1Turn;
		if(p1Turn)
		{
			currentId = 1;
		}else{
			currentId = 2;
		}
        scriptP1.setTurn(p1Turn);
        scriptP2.setTurn(!p1Turn);
    }

    void playerStartPosition()
    {
        // Les joueurs choissisent leurs positions
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("TOUCH");
            Vector3 pos = Input.mousePosition;    //screenPos to worldPos of mousePos
			pos.z = Camera.main.transform.position.y;                 //Pos of player in 2D
			Vector3 playerPosInWorld = Camera.main.ScreenToWorldPoint(pos);

            if ((Input.mousePosition.x > (float)Screen.width / 2))
            {
                //Debug.Log("P1");
                if(Player1 == null)
                {
					Player1 = (GameObject)Instantiate(PlayerPrefab, playerPosInWorld, new Quaternion());
                    Player1.name = "Player1";
					scriptP1 = Player1.transform.GetChild(0).GetComponent<Player>();
                    scriptP1.setType(TYPE.FEU);
                    scriptP1.setID(1);
                    HUDManager.Instance.setPlayer(1, Player1);
                    HUDManager.Instance.setLifeUI(1, 5);
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
					Player2 = (GameObject)Instantiate(PlayerPrefab, playerPosInWorld, new Quaternion());
                    Player2.name = "Player2";
					scriptP2 = Player2.transform.GetChild(0).GetComponent<Player>();
                    scriptP2.setType(TYPE.FEU);
                    scriptP2.setID(2);
                    HUDManager.Instance.setPlayer(2, Player2);
                    HUDManager.Instance.setLifeUI(2, 5);
                }
                else
                {
                    Player2.transform.position = pos;
                }
            }
        }
        
        
    }
}
