using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour {
	public AudioSource aud;
	public AudioClip gameOver;
	void Awake()
	{
		aud.PlayOneShot (gameOver);
	}
	public void Retry()
	{
		Time.timeScale = 1;

		Application.LoadLevel(Application.loadedLevel);

	}

	public void Menu()
	{
		Time.timeScale = 1;
		Application.LoadLevel (1);
	}
}
