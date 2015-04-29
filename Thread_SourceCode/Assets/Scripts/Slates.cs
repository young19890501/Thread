using UnityEngine;
using System.Collections;

public class Slates : MonoBehaviour {
public static GameObject menuMusic;
	public void Awake()
	{
		menuMusic = GameObject.Find ("MenuMusic");
		DontDestroyOnLoad(menuMusic);
	}


 public void ToMenu()
	{
		Application.LoadLevel (1);
	}
}
