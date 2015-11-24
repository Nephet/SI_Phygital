using UnityEngine;
using System.Collections;

public class CheckTerrain : MonoBehaviour {
    RaycastHit hit;
    public Player player;
    public TypeZone.TypeTerrain currentType;
	// Use this for initialization
	void Start () {
        player = transform.GetChild(0).GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {

        if (player.ID == GameManagerScript.instance.currentId)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                currentType = hit.collider.GetComponent<TypeZone>().typeTerrain;
                Debug.Log(currentType);
            }

        }
        
	}
}
