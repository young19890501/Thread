using UnityEngine;
using System.Collections;

public class Companion : MonoBehaviour {
	public float flySpeed = 10f;
	private GameObject player;
	public bool enableWallJump = false;
	public bool bTesting = true;
	//helllllllooooooooo
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag ("Player");
		enableWallJump = true;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		Vector3 from= transform.position;
		bTesting = fales;
		if(player != null)
		{
			Vector3 to;
			Vector3 newPosition;
			float dir = Mathf.Sign (Input.GetAxisRaw ("Horizontal"));
			//Debug.Log(Input.GetAxisRaw ("Horizontal"));
			to = (dir <=0)?new Vector3 (player.transform.position.x + 1.3f,player.transform.position.y + 1.5f):new Vector3 (player.transform.position.x -1.3f,player.transform.position.y + 1.5f);
			newPosition = Vector3.Lerp (from,to,flySpeed *Time.deltaTime);
			transform.position = newPosition;
		}
	}
}
