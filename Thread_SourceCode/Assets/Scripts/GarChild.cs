using UnityEngine;
using System.Collections;

public class GarChild : MonoBehaviour {
	public AudioSource garAudio;
	public AudioSource garAudio2;
	public Animator garAnimation;
	public GameObject gar;
	private GameObject player;
	public AudioClip garHurt;
	private int counter;
	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Update()
	{
		float dir = Mathf.Sign (player.transform.position.x - transform.position.x);
		transform.eulerAngles =(dir>0)? Vector3.up*180:Vector3.zero;
		//fire.transform.eulerAngles =(dir>0)? Vector3.up*180:Vector3.zero;
	}
	void Jump(float i)
	{
		gar.transform.position += Vector3.up * i;
	}
	void PlayAudio (AudioClip aud)
	{
		garAudio.PlayOneShot (aud);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Weapon") 
		{
			garAudio2.PlayOneShot (garHurt);
			counter ++;
			Debug.Log ("Gar"+counter);
			if(counter >9)
			{
				GetComponent<Collider>().enabled = false;
				garAnimation.SetBool ("GarDeath", true);
			}
		}
	}
	void SFXStop()
	{
		garAudio.Stop ();
	}

}
