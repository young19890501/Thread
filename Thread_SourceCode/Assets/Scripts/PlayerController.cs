using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // player handles
    public float gravity = 40;
    public float walkSpeed = 8;
    public float runSpeed = 12;
    public float acceleration = 40;
    public float jumpHeight;
    private float currentSpeed;
    public float targetSpeed;
    private Vector3 amountToMove;
    public PlayerPhysic playerphysic;
    public bool wallHolding = false;
    public Animator animatorPlayer;
    private int jumpCounter= 0;
    private float speed = 0;
    private float speedSave =0;
    private float dirSave =0;
	private GameObject[] platforms;
	public bool isAttacking;
	private string[] spinAttackR = {"down","right","Fire1"};
	private string[] spinAttackL = {"down","left","Fire1"};
	public string[] buttons;
	private int currentIndex = 0;
	public float allowedTimeBetweenButtons = 0.18f;
	private float timeLastButtonPressed;
	public bool isSpinAttack;
	public bool isDodging;
	public bool spinAttackInAir;
	public bool HeavyDamagejump;
	public float initialSpeed  = 50;
	private Player player;
	private float dirX = 0f;

    
    // Use this for initialization
    void Start()
    {
        playerphysic = GetComponent <PlayerPhysic>();
		platforms = GameObject.FindGameObjectsWithTag ("Platform"); 
		player = GetComponent<Player> ();

    }
    
    // Update is called once per frame
    void Update()
    {
		// ground?
		if (playerphysic.isGrounded)
		{
			amountToMove.y = 0;
			jumpCounter= 0;
			// Wall Holding but touch ground
			if (playerphysic.canWallHold)
			{
				playerphysic.canWallHold = false;
			}
			//if (wallHolding)
			//{
			//	wallHolding = false;
				
			//}
			
		} 
		else
		{//Wall holding in air
			if (playerphysic.canWallHold && jumpCounter< 1f)
			{
				jumpCounter= 0;
			}
		}
		animatorPlayer.SetBool("Wall", playerphysic.canWallHold	);
		if (wallHolding ) 
		{
			animatorPlayer.SetBool("SpinAttack", false);
		}
		//jump through

		foreach (GameObject platform in platforms)
		{
			platform.layer = 0;
		}

		if (amountToMove.y > 0||(amountToMove.x!=0 && !playerphysic.isPlatform && playerphysic.isGrounded))
		{
			foreach (GameObject platform in platforms)
			{
				platform.layer = 2;
			}
		}

			


		//Debug.Log (amountToMove.y);
        //Debug.Log (currentSpeed);
        
        dirX = Input.GetAxis("Horizontal");
        //Debug.Log(timer);
		//Debug.Log (transform.eulerAngles);
        animatorPlayer.SetFloat("Speed", Mathf.Abs(currentSpeed));
		animatorPlayer.SetBool ("Jump", !playerphysic.isGrounded);
		animatorPlayer.SetFloat ("Ymove", amountToMove.y);
		if (animatorPlayer.IsInTransition (0)) 
		{
			animatorPlayer.SetBool("Attack", false);
		}
        // reset acceleration
        if (playerphysic.isMoveStopped)
        {
            targetSpeed = 0;
            currentSpeed = 0;
        }
        
        
        // input for the movement

        if (Input.GetAxis("Run") != 0)
            {
                speed = runSpeed;
                animatorPlayer.SetBool("Run", true);
            } else
            {
                speed = walkSpeed;
                animatorPlayer.SetBool("Run", false);
            }
		//Rotation
		if (dirX != 0 && !wallHolding && !isAttacking)
		{ 
			if (dirX < 0)
			{
				transform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				transform.localScale = new Vector3(1f, 1f, 1f);
			}
//			transform.localScale = (dirX < 0) ? Vector3.left : Vector3.right;
//			Quaternion tempQuat = Quaternion.Euler((dirX < 0) ? new Vector3(40f, 60f, 80f) : Vector3.zero);
//			transform.rotation = tempQuat;
		}
            

		if (playerphysic.isGrounded && !animatorPlayer.GetCurrentAnimatorStateInfo (0).IsName ("HeavyDamage"))
        {
            speedSave = speed;
            dirSave = Input.GetAxisRaw("Horizontal");
            targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
			jumpHeight = 20f;
			if (isAttacking ) 
			{
				float dirAttack = (transform.localScale.x == 1)? 1f:-1f;
				targetSpeed = dirAttack * 0.1f;
				jumpHeight = 0f;
			}

			animatorPlayer.SetBool ("Dodge",Input.GetButtonUp("Dodge"));
			if(isDodging)
			{
				float dirDodge = (transform.localScale.x == 1)? -1f:1f;
				targetSpeed = dirDodge*20f;
			}


        }
		else
        {
            if(wallHolding)
            {
            	dirSave = Input.GetAxisRaw("Horizontal");
				if (GameObject.Find("Companion").GetComponent<Companion>().enabled == true) 
				{
					speedSave = runSpeed; 
				}
                else 
				{
					speedSave = walkSpeed;
				}
            }

			targetSpeed = dirSave * speedSave + Input.GetAxisRaw("Horizontal")*1.5f;
        }
       
        // Running?
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
      

        //jump
		if (Input.GetButtonDown("Jump")&& Mathf.Sign(Input.GetAxis("Horizontal")) != 0)
        {
            if (playerphysic.isGrounded)
            {
                playerphysic.isGrounded = false;
                amountToMove.y = jumpHeight;
            }

        }
        //Debug.Log (amountToMove.x);

        //Attack

		if ((transform.localScale.x  == 1 && ComboCheck(spinAttackR)&&jumpCounter <1&&!wallHolding)||(transform.localScale.x != 0 && ComboCheck(spinAttackL)&&jumpCounter <1&&!wallHolding))
        {
			animatorPlayer.SetBool("SpinAttack",true);
			jumpCounter++;
			if (!isSpinAttack) 
			{
				//Debug.Log ("YAAAAAAAAAAA,Jump atack");
				amountToMove.y = 23f;
				speedSave = 2.5f +Input.GetAxisRaw("Horizontal")*2f;

			}

        }
		else if (Input.GetButtonDown("Fire1")&&!ComboCheck(spinAttackL)&&!ComboCheck(spinAttackR))
		{
			animatorPlayer.SetBool("Attack", true);

		}
		else if (playerphysic.isGrounded) 
		{
			animatorPlayer.SetBool("SpinAttack",false);

		}
       //Heavy damage
		//Debug.Log (animatorPlayer.GetCurrentAnimatorStateInfo (0).IsName ("HeavyDamage"));
		if(animatorPlayer.GetCurrentAnimatorStateInfo (0).IsName ("HeavyDamage"))
		{
			float dir = (transform.localScale.x == 1)? -1f:1f;
			initialSpeed -=20*Time.deltaTime;
			currentSpeed = dir *initialSpeed;
			if (initialSpeed < 0)
			{
				currentSpeed =0;
			}
		}
		else
		{
			initialSpeed = 20f;
		}
		if (animatorPlayer.GetCurrentAnimatorStateInfo (0).IsName ("HeavyDamage"))
						amountToMove.y = 0;
		if (animatorPlayer.GetCurrentAnimatorStateInfo (0).IsName ("HeavyDamage2"))
						jumpHeight = 0;
		if (HeavyDamagejump) 
		{
			jumpHeight = 20f;
			amountToMove.y = jumpHeight;
				
		}
        
        //Wall holding Jump
        //Debug.Log(jumpCounter);
        if (wallHolding && jumpCounter< 1f)
        {
            amountToMove.x = 0;
			amountToMove.y =0;
            if (Input.GetButtonDown("Jump")&& Mathf.Sign(Input.GetAxis("Horizontal")) != 0)
            {
				//wallHolding = false;
				transform.localScale = (Input.GetAxis("Horizontal") < 0) ? new Vector3(-1,1,1) : new Vector3 (1,1,1);    
				amountToMove.y = jumpHeight;
                playerphysic.canWallHold = false;
				jumpCounter ++;
            }
        } 
		else
        {
           // wallHolding = false;
            playerphysic.canWallHold = false;
            amountToMove.x = currentSpeed;
			if (playerphysic.jumpDir != Mathf.Sign(Input.GetAxis("Horizontal")))
            {
                jumpCounter= 0;
            }

        }

		if(spinAttackInAir)
		{
			amountToMove.y += 0.9f;
		}	
		amountToMove.y -= gravity * Time.deltaTime;
		//Death
		if (player.isDead ) 
		{
			amountToMove.y += 1f;
			currentSpeed += 0.2f*dirX;
		}
		if(animatorPlayer.GetCurrentAnimatorStateInfo (0).IsName("Damage"))
		{
			currentSpeed =0;
		}


        // set mouve amount
        playerphysic.Move(amountToMove * Time.deltaTime);
    }
    
    private float IncrementTowards(float i, float j, float k)
    {
        if (i == j)
        {
            return i;
        } else
        {
            float dir = Mathf.Sign(j - i);
            i += k * Time.deltaTime * dir;
            if (dir == Mathf.Sin(j - i))
            {
                return i;
            } else
            {
                return j;
            }
        }
        
    }
	public bool ComboCheck(string[] b)
	{
		buttons = b;
		if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons) currentIndex = 0;
		{
			if (currentIndex < buttons.Length)
			{
				if ((buttons[currentIndex] == "down" && Input.GetAxisRaw("Vertical") == -1) ||
				    (buttons[currentIndex] == "up" && Input.GetAxisRaw("Vertical") == 1) ||
				    (buttons[currentIndex] == "left" && Input.GetAxisRaw("Vertical") == -1) ||
				    (buttons[currentIndex] == "right" && Input.GetAxisRaw("Horizontal") == 1) ||
				    (buttons[currentIndex] == "Fire1" && Input.GetAxisRaw("Fire1") == 1)||
				    (buttons[currentIndex] != "down" && buttons[currentIndex] != "up" && buttons[currentIndex] != "left" && buttons[currentIndex] != "right" &&buttons[currentIndex] != "Fire1"&& Input.GetButtonDown(buttons[currentIndex])))
				{
					timeLastButtonPressed = Time.time;
					currentIndex++;
				}
				
				if (currentIndex >= buttons.Length)
				{
					currentIndex = 0;
					return true;
				}
				else return false;
			}
		}
		
		return false;
	}

	public void DamageAnim()
	{
		float dir = (transform.localScale.x == 1)? -1f:1f;
		transform.position += Vector3.right * dir* Time.deltaTime*10f;
	}

}
