using UnityEngine;
using System.Collections;

public class OnGUI : MonoBehaviour {

	public Player player;
	public Transform healthForeground;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthForeground.localScale = new Vector3(1, player.GetCurrentHealthPercent (),1);
	}
}
