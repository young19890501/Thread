using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		if(other.tag!="Player")
		{
			Destroy(other.gameObject);
		}
	}
}
