using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller_2D))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(Projectile_Launcher))]
public class Player : MonoBehaviour
{
	[SerializeField] public int damage = 1; // 1 is normal , 3 is fat
	//public float maxJumpHeight = 4;
	public float minJumpHeight = 1;

	public float targetVelocityX;
	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;
	float gravity;

	
	public Vector3 velocity;
	float velocityXSmoothing;

	public float maxJumpVelocity;
	public float minJumpVelocity;

	public bool HaveObjToThrow = false;
	[HideInInspector]
	public Controller_2D controller;
	public Projectile_Launcher proj;
	private Health health;

	Sprite_Controller spriteController;
	ForceJump jump;
	public bool disabled = false;
	//public Nut_Script nut;


	public Vector2 input { get; private set; } // it means that we can get outside, but change only in this class
	public void UnDisableAfterTime(float time)
	{
		Invoke("UnDisable", time);
	}
	void Awake()
	{
		proj = GetComponent<Projectile_Launcher>();
		controller = GetComponent<Controller_2D>();
		spriteController = GetComponent<Sprite_Controller>();
		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);

		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
		ForceJump.CanMove += Disable;
		jump = GetComponent<ForceJump>();
		health = GetComponent<Health>();
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!HaveObjToThrow)
		{
			var ob = collision.gameObject.GetComponent<IEatable>();
			if (ob != null)
			{
				HaveObjToThrow = true;
				Destroy(collision.gameObject);
			}
		}
	}
	


	void Update()
	{



		spriteController.UpdateSprite(velocity.x, velocity.y);
		if (!disabled)
		{
			input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			if (jump.HasJumped == true && controller.collisions.below)
			{
				targetVelocityX = Mathf.Sign(velocity.x) * moveSpeed * jump.coeffHowHigh;
			}
			else if (!jump.HasJumped)
			{
				targetVelocityX = input.x * moveSpeed;
			}



			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
			if (Input.GetKeyDown("space") && controller.collisions.below && !HaveObjToThrow)

			{
				velocity.y = maxJumpVelocity;
			}
			
			controller.Move(velocity * Time.deltaTime, input);
			if (controller.collisions.above || controller.collisions.below)
			{
				velocity.y = 0;
			}
			if (Input.GetKeyUp(KeyCode.Space))
			{
				if (velocity.y > minJumpVelocity && !HaveObjToThrow)
				{
					velocity.y = minJumpVelocity;
				}
			}


		}
		else
		{
			velocity = Vector3.zero;
			input = Vector2.zero;
		}
	}



	public void Disable()
	{
		disabled = true;
		targetVelocityX = 0;
	}
	public void UnDisable()
	{
		disabled = false;
	}
}