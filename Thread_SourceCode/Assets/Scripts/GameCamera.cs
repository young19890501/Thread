using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
	private Transform target;
	public float trackspeed;// camera track speed
	private float x;
	private float y;


	void Start()
	{

	
	}
	public void SetTarget (Transform t)
	{
		target = t;
	}
	
	// Update is called once per frame
	// the function works like updat, but the code in it only excute after all the codes in update is excuted.
	void LateUpdate ()
	{
		if (target) 
		{
			if (Input.GetKey(KeyCode.Joystick1Button9))
			{
				if(GetComponent<Camera>().orthographicSize < 12)
				this.GetComponent<Camera>().orthographicSize +=4*Time.deltaTime;
				else
					GetComponent<Camera>().orthographicSize = 12;
			}
			x = IncrementTowards (transform.position.x, target.position.x, trackspeed);
			//float y = IncrementTowards (transform.position.y, Mathf.Clamp(target.position.y+3f,-0.5f,100f), trackspeed);
			y = IncrementTowards (transform.position.y, Mathf.Clamp(target.position.y+Input.GetAxis("Cam")*3,10f,100f), trackspeed);
			transform.position = new Vector3 (x,y,transform.position.z);

		}

	}
	
	private float IncrementTowards (float i, float j, float k)// i is the current speed, j is the target speed, k is the accelaration
	{
		if (i == j) {
			return i;
		} else {
			float dir = Mathf.Sign (j - i);// decide the direction to move
			i += k * Time.deltaTime * dir;
			if (dir == Mathf.Sin (j - i)) {
				return i;
			} else {
				return j;
			}
		}
		
		
	}
}
