using UnityEngine;
using System.Collections;


public class SlingShot : MonoBehaviour {

    Vector3 mouseUpPos;
    Vector3 mouseDownPos;
    Vector3 startMouseDownPos;

    public float speed;
    float dist;
    public bool shoot = false;
	public bool playing = false;
	public bool action = false;

	LineRenderer lineRenderer;
    Player player;
	public int myID;

	public float playerSpeed;

	// Use this for initialization
	void Start () {
		lineRenderer = transform.parent.gameObject.GetComponent<LineRenderer>();
		lineRenderer.enabled = false;
        player = transform.gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        

	}

    void FixedUpdate()
    {
        transform.position = transform.parent.position;
        Vector3 vel = transform.parent.GetComponent<Rigidbody>().velocity;
        playerSpeed = vel.magnitude;

        if (myID == GameManagerScript.instance.currentId)
        {
            if (playerSpeed <= 0.1f && !playing && action)
            {

                transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                //GameManagerScript.instance.changeTurn();
                player.actionReady = false;
                action = false;
            }
        }


        if (Input.GetMouseButton(0) && shoot) // si le joueur est en train de tirer
        {
            // on scale la jauge de puissance en fonction de la distance
            ScaleJauge();
        }
    }

    void ScaleJauge()
    {
		lineRenderer.enabled = true;
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Camera.main.transform.position.y - transform.parent.position.y;
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

        dist = Vector3.Distance(new Vector3(playerPos.x, 0.0f, playerPos.y), new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y));
		lineRenderer.SetPosition(0, new Vector3(transform.parent.position.x, 0.0f, transform.parent.position.z));
		lineRenderer.SetPosition(1, new Vector3(mousePosInWorld.x, 0.0f, mousePosInWorld.z));
		lineRenderer.SetColors(Color.yellow, new Color(255.0f, 255.0f-(100+dist), 0.0f));
    }

    void OnMouseDown()
    {
		if(!action && myID == GameManagerScript.instance.currentId)
		{
			shoot = true;
			playing = true;
            transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
			mouseDownPos = new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y); //on stock la position de départ
			startMouseDownPos = mouseDownPos;
			mouseDownPos.y = 0;
		}
        
        
    }

    void OnMouseUp()
    {
		if(!action && playing && myID == GameManagerScript.instance.currentId)
		{
            mouseUpPos = new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y); //on stock la position de d'arrivée
            mouseUpPos.y = 0;
            dist = Vector3.Distance(mouseDownPos, mouseUpPos);
            Debug.Log(dist);

            if (dist > 24.0f)
            {
                shoot = false;
                action = true;
                var direction = mouseDownPos - mouseUpPos;
                transform.parent.GetComponent<Rigidbody>().AddForce(direction * speed * dist / 2);
                

                StartCoroutine(Wait(0.1f));

            }
            lineRenderer.enabled = false;
			
		}
    }

	IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
		playing = false;
	}
}
