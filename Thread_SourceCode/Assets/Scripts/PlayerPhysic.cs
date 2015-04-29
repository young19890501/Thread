using UnityEngine;
using System.Collections;

public class PlayerPhysic : MonoBehaviour
{
	public LayerMask collisionMask;
	public bool isGrounded;
	public bool isMoveStopped;
	public int colliderDivisionX = 3;
	public int colliderDivisionY = 10;
	public bool canWallHold;
	public bool isPlatform;
	public  Ray ray;
	public Ray rayWallHold;
	public RaycastHit hit;
	public RaycastHit hitWallHold;
	public float  jumpDir;

    private BoxCollider playerCollider;
    private Vector3 size;
    private Vector3 center;
    private float skin = 0.005f;
	private Vector3  originalSize;
	private Vector3 originalCenter;
	private float colliderScale;
    
	
    void Start()
    {
        
        playerCollider = GetComponent <BoxCollider>();
        colliderScale = transform.localScale.x;
        originalSize = playerCollider.size;
        originalCenter = playerCollider.center;
        SetCollider(originalSize, originalCenter);
            
    }


    //movement of the Character
    public void Move(Vector3 moveAmount)
    {

    
        float deltaY = moveAmount.y;
        float deltaX = moveAmount.x;
        Vector3 position = transform.position;
        isGrounded = false;

        for (int i= 0; i< colliderDivisionX; i++)
        {
            float dir = Mathf.Sign(moveAmount.y);
            float x = (position.x + center.x - size.x / 2) + size.x / (colliderDivisionX - 1) * i;
            float y = position.y + center.y + size.y / 2 * dir;
            ray = new Ray(new Vector3(x, y), new Vector3(0, dir));
            Debug.DrawRay(ray.origin, ray.direction);
            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin))
            {
                float dst = Vector3.Distance(ray.origin, hit.point);
                if (dst > skin)
                {
                    deltaY = dir * dst - skin * dir;
                
                } else
                {
                    deltaY = 0;
                }
				//jump through platform
				if (hit.transform.tag == "Platform") 
				{
					isPlatform = true;
				}
				else
				{
					isPlatform = false;
				}

                isGrounded = true;
            
                break;
            
            }
        }
    
        // Collision on Left and right of the object
        isMoveStopped = false;
    
        if (deltaX != 0)
        {
            for (int i= 0; i<colliderDivisionY; i++)
            {
                isMoveStopped = false;
                float dir = Mathf.Sign(moveAmount.x);
                float x = position.x + center.x + size.x / 2 * dir;
                float y = position.y + center.y - size.y / 2 + size.y / (colliderDivisionY - 1) * i;
                float y2 = position.y + center.y - size.y / 2 + size.y / (colliderDivisionY - 1) * (i/2);
                ray = new Ray(new Vector3(x, y), new Vector3(dir, 0));
                rayWallHold = new Ray(new Vector3(x, y2), new Vector3(dir, 0));
                Debug.DrawRay(rayWallHold.origin, ray.direction);
				if (Physics.Raycast(rayWallHold, out hitWallHold, Mathf.Abs(deltaX) + skin))
				{
					if (hitWallHold.collider.tag == "Wall")
					{
						// no wall holding, if player turn right before touching the wall
						if (Mathf.Sign(Input.GetAxis("Horizontal")) == Mathf.Sign(deltaX) &&Mathf.Sign (Input.GetAxis("Horizontal")) != 0 && !isGrounded)
						{
							canWallHold = true;
						} 
					}
				}
				if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + skin))
                {
                    float dst = Vector3.Distance(ray.origin, hit.point);
                    jumpDir =Mathf.Sign(ray.direction.x);
                    if (dst > skin)
                    {
                        deltaX = dir * dst - skin * dir;
            
                    } else
                    {
                        deltaX = 0;
                    }
        
                    isMoveStopped = true;
                    break;
        
                }
            }

        }

        if (!isGrounded && !isMoveStopped)
        {
            Vector3 playerDir = new Vector3(deltaX, deltaY);
            Vector3 o = new Vector3(position.x + center.x + size.x / 2 * Mathf.Sign(deltaX), position.y + center.y + size.y / 2 * Mathf.Sign(deltaY));
            ray = new Ray(o, playerDir.normalized);
            Debug.DrawRay(o, playerDir.normalized);
            if (Physics.Raycast(ray, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY)))
            {
                isGrounded = true;
                deltaY = 0;
            }
        }
        Vector3 finalTransform = new Vector3(deltaX, deltaY);
        transform.Translate(finalTransform, Space.World);
        
    }
    
    public void SetCollider(Vector3 i, Vector3 j)
    {
        playerCollider.size = i;
        playerCollider.center = j;
        size = i * colliderScale;
        center = j * colliderScale;
        
    }
    
    
}