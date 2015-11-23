using UnityEngine;
using System.Collections;


public class SlingShot : MonoBehaviour {

    Vector3 mouseUpPos;
    Vector3 mouseDownPos;
    Vector3 startMouseDownPos;

    public float speed;
    float dist;
    bool shoot = false;

	LineRenderer lineRenderer;


	// Use this for initialization
	void Start () {
		lineRenderer = transform.parent.gameObject.GetComponent<LineRenderer>();
		lineRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.parent.position;

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

		Debug.Log(mousePosInWorld);

        dist = Vector3.Distance(new Vector3(playerPos.x, 0.0f, playerPos.y), new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y));
		lineRenderer.SetPosition(0, new Vector3(transform.parent.position.x, 0.0f, transform.parent.position.z));
		lineRenderer.SetPosition(1, new Vector3(mousePosInWorld.x, 0.0f, mousePosInWorld.z));
		lineRenderer.SetColors(Color.yellow, new Color(255.0f, 255.0f-(100+dist), 0.0f));
        transform.localScale = new Vector3(dist / 2.5f, dist / 2.5f, dist / 2.5f);
    }

    void OnMouseDown()
    {
        shoot = true;
        mouseDownPos = new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y); //on stock la position de départ
        startMouseDownPos = mouseDownPos;
        mouseDownPos.y = 0;
        
    }

    void OnMouseUp()
    {
        shoot = false;
        mouseUpPos = new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y); //on stock la position de d'arrivée
        mouseUpPos.y = 0;
        dist = Vector3.Distance(mouseDownPos, mouseUpPos);
        var direction = mouseDownPos - mouseUpPos; 
        transform.parent.GetComponent<Rigidbody>().AddForce(direction * speed * dist/2);
		lineRenderer.enabled = false;

        /*if(dist> 10.f){
         {
            action = true;
         }*/
    }
}
