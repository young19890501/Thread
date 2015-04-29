using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// For now is onlu for character spawn, maybe will have some check point management
	// Use this for initialization
	public GameObject player;
	public GameCamera cam;
	public AudioSource Intro;
	public AudioSource wind;
	public GameObject companion;
	private GameObject[] skeltons;
	private GameObject[] gargoyles;
	public PlayerPhysic playerphysic;
	public PlayerController playerController;
	private float timer = 0;
	private float i;
	private AudioClip footstep;
	public bool isPause = false;
	public GameObject pauseMenu;
	public GameObject endScreen;
	public GameObject deathScreen;
	public Player playerScript;
	public Transform[] checkPoints;
	public static int checkPointIndex;
	
	void Awake ()
	{
		cam = Camera.main.GetComponent <GameCamera > ();
		if(GameObject.Find("MenuMusic"))
		{
			Destroy(GameObject.Find("MenuMusic"));
		}
		if (Application.loadedLevel == 3) 
		{
			checkPointIndex = 4;
			
		}
		else
		{
			checkPointIndex = 0;
		}
		SpawnPlayer ();

	}

	void Start ()
	{
		// you have to have the camera first and then give the camera a target, which is th play
		wind.Play ();
		Intro.Play();
		companion = GameObject.FindGameObjectWithTag ("Companion");
		if (companion != null)
		{
			companion.GetComponent<Companion>().enabled = false;
		}
		playerphysic = player.GetComponent <PlayerPhysic>();
		playerScript = player.GetComponent <Player>();
		Time.timeScale = 1;
		


	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		//Debug.Log (timer);
		//if (player.transform.position.x >= 59f && !introMusic.isPlaying)
		//{
		 //introMusic.Play ();
		//}

		if (16f <= player.transform.position.x && player.transform.position.x <= 17f && player.transform.position.y >= -1.5f && player.transform.position.y< 0f)
		{
			companion.GetComponent<Companion>().enabled = true;
		}
		if(Input.GetKeyDown (KeyCode.Keypad3))
		{
			Application.LoadLevel (3);
		}

		if(Input.GetKeyUp(KeyCode.Joystick1Button7))
		{
			isPause = !isPause;
			if(isPause)
			{
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
			}
			else
			{
				Time.timeScale =1;
				pauseMenu.SetActive(false);
			}

		}
		if(Application.loadedLevelName == "Boss" && GameObject.Find("Gargoyle")==null)
		{
			endScreen.SetActive (true);
			Time.timeScale = 0;
			if(Input.GetKey(KeyCode.Escape))
			{
			Application.LoadLevel(4);
			}

		}
		//Debug.Log (playerScript.isDead);
		if(playerScript.isDead)
		{
			//Debug.Log ("dead");
			Intro.Stop ();
			Time.timeScale -=Time.deltaTime;
			if(Time.timeScale<0.08)
			{
			deathScreen.SetActive (true);
			Time.timeScale = 0;
			}

		}


	}

	private void SpawnPlayer()
	{
		player = Instantiate (player, checkPoints[checkPointIndex].position, Quaternion.identity) as GameObject;
		cam.SetTarget (player.transform);
	}


}
