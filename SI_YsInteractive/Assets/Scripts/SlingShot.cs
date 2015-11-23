using UnityEngine;
using System.Collections;


public class SlingShot : MonoBehaviour {

    Vector3 mouseUpPos;
    Vector3 mouseDownPos;
    Vector3 startMouseDownPos;

    public float speed;
    float dist;
    bool shoot = false;


	// Use this for initialization
	void Start () {
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
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        dist = Vector3.Distance(new Vector3(playerPos.x, 0.0f, playerPos.y), new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y));
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

        /*if(dist> 10.f){
         {
            action = true;
         }*/
    }
}
