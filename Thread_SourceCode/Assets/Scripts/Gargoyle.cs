using UnityEngine;
using System.Collections;

public class Gargoyle : MonoBehaviour
{
	private GameObject player;
	public float gargoyleSpeed = 12f;
	private float timer;
	public Animator garAnim;
	public Animator fireAim;
	public float attackTimer = 5.0f;
	private int attackType;
	private float targetPos;
	public float flyHeight = 90;

	//	enum GargoyleStates
//	{
//		Fly,
//		Fire,
//		Tale,
//		Spear,
//
//		Max,
//	}
//
//	GargoyleStates currentState = GargoyleStates.Fly;
	// Use this for initialization
	void Awake ()
	{

		player = GameObject.FindGameObjectWithTag ("Player");
		if(player == null)
		{
			Debug.Log ("no player");
		}

		targetPos = transform.position.x;
		
	}
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log (attackType);
		//Wake up
		if (player) {
						garAnim.SetFloat ("Dis", (player.transform.position.x - transform.position.x));
				
						//movement
						if (garAnim.GetCurrentAnimatorStateInfo (0).IsName ("fly")) {
								if (Mathf.Abs (targetPos - transform.position.x) > 1) {
										transform.position += Mathf.Sign (targetPos - transform.position.x) * Vector3.right * gargoyleSpeed * Time.deltaTime;
								} else {
										targetPos = Random.Range (player.transform.position.x - 14, player.transform.position.x + 14);
								}
								if (transform.position.y > flyHeight) {
										transform.position += Vector3.down * 2 * Time.deltaTime;
								}

						}
				}

		//attack

		if(garAnim.GetCurrentAnimatorStateInfo(0).IsName("FirstFrame"))
		{
			fireAim.SetBool("On",false);
		}
		else
		{
			attackTimer -= Time.deltaTime;
			if(attackTimer <=0)
			{
				attackType =Random.Range (1,4);
				
				attackTimer = 5.0f;
			}
		}

		switch(attackType)
		{
		case 0:
		{
			garAnim.SetBool("Spear",false);
			garAnim.SetBool("FireBreath",false);
			garAnim.SetBool("Tail",false);
			if (GameObject.Find("Fire")) 
			{
				fireAim.SetBool ("On",false);
			}
			break;
		}
		case 1:
		{
			garAnim.SetBool("Spear",true);
			if (garAnim.IsInTransition(0))
			{
				attackType =0;
			}
			break;
		}
		case 2:
		{
			garAnim.SetBool("FireBreath",true);
			if (GameObject.Find("Fire")) 
			{
				fireAim.SetBool ("On",true);
			}
				if (garAnim.IsInTransition(0))
			{
				attackType =0;
			}
			break;			
		}
		case 3:
		{
			garAnim.SetBool("Tail",true);
			if (garAnim.IsInTransition(0))
			{
				attackType =0;
			}
			break;
		}

		}

//		if(Mathf.Abs(transform.position.x - player.transform.position.x)<2.0f)
//		{
//			garAnim.SetBool("Attack",true);
//		}
//		else
//		{
//			garAnim.SetBool("Attack",false);
//			
//		}



	}


}
