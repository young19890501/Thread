using UnityEngine;
using System.Collections;

public class Skelton : MonoBehaviour
{
	private GameObject player;
	private int counter= 0;
	public float skeltonSpeed = 2f;
	private float dir;
	public Animator skeletonAnimator;
	public Animator shieldAnimator;
	public AudioSource sklAudio;
	public AudioClip shield;
	public AudioClip bone;
	public float walkTimer;
	public int awakeTrigger;

		// Use this for initialization
		void Start ()
		{
		player = GameObject.FindGameObjectWithTag ("Player");
		skeletonAnimator.SetInteger ("Awake", awakeTrigger);

		}
	
		// Update is called once per frame
		void Update ()
		{
				//Debug.Log (i);
		if(player)
		{
				dir = Mathf.Sign (transform.position.x - player.transform.position.x);
			transform.localScale = (dir > 0) ? new Vector3 (1,1,1):new Vector3 (-1,1,1);
		}
		if (skeletonAnimator) 
		{
			skeletonAnimator.SetInteger ("Awake", awakeTrigger);
			if(awakeTrigger != 0)
			{
				//collider.enabled = false;
			}
			//Debug.Log (counter);
			if (shieldAnimator.IsInTransition (0)) {
				shieldAnimator.SetBool ("Shield", false);
			}
			skeletonAnimator.SetInteger ("Counter", counter);
			
			if (Mathf.Abs (transform.position.x - player.transform.position.x) < 2.5f) {
				skeletonAnimator.SetBool ("Attack", true);
			} else {
				skeletonAnimator.SetBool ("Attack", false);
			}
			if (skeletonAnimator.GetCurrentAnimatorStateInfo (0).IsName ("walk") || skeletonAnimator.GetCurrentAnimatorStateInfo (0).IsName ("walk_shield")) 
			{
				walkTimer += Time.deltaTime;
				if (walkTimer<3) 
				{
					transform.position += Vector3.left * skeltonSpeed * Time.deltaTime;
										//Debug.Log ("it works");
				}
				else 
				{
					transform.position += Vector3.right * skeltonSpeed * Time.deltaTime;
				}
				if (walkTimer>6) 
				{
					walkTimer = 0;
		
				}
			}
		}
		else
		{
			Destroy(gameObject);
		}

			
		}

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Weapon")
		{
			counter ++;
			if (counter ==2) 
			{
				shieldAnimator.SetBool("Shield",true);

			}
			if(counter >=3)
			{
			skeletonAnimator.SetBool ("Dead", true);
				Destroy (gameObject, 2);
			}
			if(counter<3)
			{
				sklAudio.PlayOneShot(shield);
			}
			else
			{
				sklAudio.PlayOneShot (bone);
			}
		}
		if(other.tag =="Player")
		{
			awakeTrigger ++;
			//collider.enabled = false;
		}
	}

}
