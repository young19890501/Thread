using UnityEngine;
using System.Collections;

public class SkeletonChild : MonoBehaviour {
	public AudioSource sklAudio;
	public GameObject parent;
	// Use this for initialization
	public void PlaySFX(AudioClip aud)
	{
		sklAudio.PlayOneShot (aud);
	}
	void Tele()
	{
		parent.transform.position = transform.position ;
	}
}
