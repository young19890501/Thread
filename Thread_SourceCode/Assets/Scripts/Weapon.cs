using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	private int garCounter = 0;
	private Player player;
	private GameObject[] garObjects;
	private GameObject[] sklObjects;
	public Animator playerAnimInWeapon;
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>();
		garObjects= GameObject.FindGameObjectsWithTag ("Gargoyle");
	}

	public void OnTriggerEnter (Collider other)
	{
		playerAnimInWeapon.SetBool ("Damage", false);

		if (other.transform.tag == "Skelton")
		{
			player.playerHealth ++;
		}
		if(other.transform.tag == "Gargoyle")
		{
			garCounter ++;
			Debug.Log ("weapon"+garCounter);
			player.playerHealth ++;

			if(garCounter > 9)
			{
				foreach (GameObject garObject in garObjects)
				{
					Destroy (garObject, 2f);
					Destroy (GameObject.Find("Fire").gameObject);
					
				}

			}
		}
	}
		
}
