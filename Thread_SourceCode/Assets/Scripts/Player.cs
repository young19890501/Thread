using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	private PlayerPhysic playerphysic;
	private GameObject companion;
	public Animator animatorPlayer;
	public bool cinematic1;
	private bool barrier = false;
	private float barrierTimer = 0f;
	public AudioSource audioPlayer;
	public AudioClip hit;
	public AudioClip miss;
	public AudioClip[] attacks;
	public AudioClip attack;
	public AudioClip[] walkingSFXs;
	public AudioClip footstep;
	public AudioClip spinAttack;
	public AudioClip[] spinAttacks;
	public bool isFalling;
	public bool hasCollidedEnemy = false;
	public bool isDead = false;

	// Jean's code for player Health
	public int playerHealth = 20;
	private int maxPlayerHealth = 20;

	// From GUI Class 02
     


	// Use this for initialization
	void Start ()
	{
		companion = GameObject.FindGameObjectWithTag ("Companion");
		playerphysic = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerPhysic> ();

		//animatorPlayer = GetComponent <Animator> ();
	}

	void Update ()
	{
		animatorPlayer.SetBool ("Death", isDead);
		footstep = walkingSFXs [Random.Range (0, walkingSFXs.Length)];
		attack = attacks [Random.Range (0, attacks.Length)];
		spinAttack = spinAttacks [Random.Range (0, spinAttacks.Length)];
		if (isFalling && playerphysic.isGrounded) {
			audioPlayer.PlayOneShot (footstep);
		}

		if (barrier == true) {
			barrierTimer += Time.deltaTime;
		}
		if(animatorPlayer.IsInTransition(0))
		{
			animatorPlayer.SetBool ("Damage",false);
			animatorPlayer.SetBool ("HeavyDamage",false);
		}
		if (playerHealth <=0) 
		{
			isDead = true;

		}
		else
		{
			isDead = false;

		}

	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.name == "Checkpoint1") 
		{
			GameManager.checkPointIndex = 1;
			Destroy(other.gameObject);
		}
		if (other.name == "Checkpoint2") 
		{
			GameManager.checkPointIndex = 2;
			Destroy(other.gameObject);
		}
		if (other.name == "Checkpoint3") 
		{
			GameManager.checkPointIndex = 3;
			Destroy(other.gameObject);
		}

		if (other.transform.tag == "Barrier" && companion.GetComponent<Companion> ().enabled == true) {
			barrier = true;
		}

		if (other.transform.tag == "Skelton"||other.transform.tag == "Gargoyle"||other.transform.tag == "Attack") 
		{

			animatorPlayer.SetBool ("Damage",true);
			TakeDamage ();
		}
		if (other.transform.tag == "HeavyAttack")
		{
			animatorPlayer.SetBool ("HeavyDamage",true);
			TakeDamage ();
			TakeDamage ();
			//Debug.Log("Touching enemy");
		}
		if (other.transform.tag == "DeathZone") 
		{
			playerHealth = 0;
		}

		if(other.name == "NextLevel")
		{
			Application.LoadLevel(3);
		}

	}
	


	public int GetCurrentHealth ()
	{
		return playerHealth;
	}
	
	public float GetCurrentHealthPercent ()
	{
		return (float)playerHealth / (float)maxPlayerHealth;
	}

	void TakeDamage ()
	{
		playerHealth --;
	}
	

	public void PlaySound (AudioClip aud)
	{
		audioPlayer.PlayOneShot (aud);
	}

	public void PlayStep ()
	{
		audioPlayer.PlayOneShot (footstep);
	}
	public void PlayAttack ()
	{
		audioPlayer.PlayOneShot (attack);
	}
	public void PlaySpinAttack ()
	{
		audioPlayer.PlayOneShot (spinAttack);
	}
}
