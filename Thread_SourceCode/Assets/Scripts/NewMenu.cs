using UnityEngine;
using System.Collections;

public class NewMenu : MonoBehaviour
{
	public GameObject other;

		public void StartButton()
	{
		Application.LoadLevel(2);
	}

	public void Credits()
	{
		Application.LoadLevel (4);
	}
	public void Exit()
	{
		Application.Quit ();
	}

	public void Activate()
	{
		other.SetActive (true);
	}
	public void DEActivate()
	{
		other.SetActive (false);
	}
}
