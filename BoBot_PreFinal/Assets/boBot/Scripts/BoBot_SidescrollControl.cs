using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (CharacterController))]


public class BoBot_SidescrollControl : MonoBehaviour {
	
	// This script must be attached to a GameObject that has a CharacterController
	//Joystick moveTouchPad;
	//Joystick jumpTouchPad;
	
	float forwardSpeed = 4f;
	float backwardSpeed = 4f;
	float jumpSpeed = 16.3f;
	float slideSpeed = 2f;
	float inAirMultiplier = 0.25f;					// Limiter for ground speed while jumping
	
	/*private bool canCarry = false;
	private bool canClimbUp = false;
	private bool canClimbDown = false;
	private bool canClimbEdge = false;
	*/
	/*
	private bool keyLeft = false;
	private bool keyRight = false;
	private bool keyUp = false;
	private bool keyDown = false;
	private bool keyJump = false;
	private bool keyAction = false;
	private bool keyNull = false;
	*/
	
	
	
	//private float[] keyStates = new float[7]; 
	
	private BoBot_FSMState currentState;
	private BoBot_FSMState newState;
	//private float direction = 1.0f;
	//private int numOfClimbObjects = 0;
	
	//private float lastHeigh = 0;
	
	//private FSM fsm;
	
	//private BoBot_FSMState idle = new Idle();
	//private BoBot_FSMState walk = new Walk();
	
	private Transform thisTransform;
	private CharacterController character;
	//private Animation bobot;
	private GameObject background;  
	private Vector3 backgroundPosition;
	
	//private GameObject frameDisplay;
	private GameObject levelBoundingBox;
	private float levelWidth;
	private float levelHeight;
	private Bounds backgroundBounds;
	private GameObject mainCamera;
	
	
	/*private float climbSlide = 0.0f;
	private float jumpKey = 0.0f;
	private float actVelocityWalk = 0.0f;
	private float actValueWalk = 0.0f;
	private float actVelocityClimb = 0.0f;
	private float actValueClimb = 0.0f;
	private bool falling = false;
	private bool jumpedFromGround = false;
	private bool lookup = false;*/
	
	//private int numClimbUps = 0;
	//private int numOfClimbEdgeObjects = 0;
	//private int numOfClimbRopeObjects = 0;
	
	private float originZ = 0.0f;
	private Transform bobotTransform;
	private string output = "";
	private float driftSpeed = 0f;
	//TriggerVariables
	/*
	private bool isGrounded = false;
	private bool canCarry = false;
	private bool canClimbUp = false;
	private bool canClimbDown = false;
	private bool canEdgeGrab = false;
	private bool canEdgeClimbUp = false;
	private bool canEdgeClimbDown = false;,
	private bool keyLeft = false;
	private bool keyRight = false;
	private bool keyUp = false;
	private bool keyDown = false;
	private bool keyJump = false;
	private bool keyAction = false;
	private bool keyNull = false;
	private float direction = 1.0;
	*/
	
	// Variable Reflection
	/*
	var test = GetType().GetField("keyUp");
	Debug.Log (test);
	
	*/

//	public static class BoBotGlobal {
//		public static float forwardSpeed = 4f;
//		public static float backwardSpeed = 4f;
//		
//		public static float climbUpSpeed = 1f;
//		public static float climbDownSpeed = 1f;
//		public static float jumpSpeed = 16f;
//		public static float slideSpeed = 1f;
//		public static float inAirMultiplier = 0.25f;	
//		public static float timeTillMove = 0.25f;
//		public static float carrySpeed = 2.5f;
//		public static boBot_CarryCollider carryColliderComponent;
//		public static boBot_ClimbCollider collider_climbCollider;
//		
//		public static Vector3 movement;
//		public static Vector3 velocity;
//		public static bool isGravity = true;
//		public static float actVelocityWalk = 0.0f;
//		public static float actValueWalk = 0.0f;
//		public static float ropeHeight;
//		public static int currentState;
//	}
	
	public float speed_forwardSpeed = 4f;
	public float speed_backwardSpeed = 4f;		
	public float speed_climbUpSpeed = 1f;
	public float speed_climbDownSpeed = 1f;
	public float speed_pushSpeed = 2.5f;
	public float speed_pullSpeed = 2.5f;
	public float speed_jumpSpeedUp = 14f;
	public float speed_jumpSpeedForward = 14f;
	public float speed_slideSpeed = 1f;
	public float speed_fallSpeed = 0f;

	public float time_timeTillWalk = 0.125f;		
	public float time_timeTillIdle = 0.125f;
	public float time_timeTillCarry = 2.5f;
	
	public float multiplier_inAirMultiplier = 0.05f;	
	public float multiplier_onGroundX = 0.85f;	

	public float distance_CanGrabEdge = 2.5f;
	public float distance_GrabEdge = 0.85f;
	public float distance_CanCarry = 1.25f;
	public float distance_Carry = 0.26f;
	public float distance_ToWall = 2.5f;
	
	public float physics_pushForce = 1.0f;
	public float physics_pullForce = 1.0f;
	
	
	//private Transform activePlatform;
	private Vector3 activeLocalPlatformPoint;
	private Vector3 activeGlobalPlatformPoint;
	private Vector3 lastPlatformVelocity;
 
// If you want to support moving platform rotation as well:
	private Quaternion activeLocalPlatformRotation;
	private Quaternion activeGlobalPlatformRotation;
	
	
	public LayerMask slopeLayersToUse;
	private Ray rayLeft;	
	private Ray rayRight;
	private RaycastHit hitLeft;
	private RaycastHit hitRight;
	
	private BoBot_DebugComponent debugInfo;
	
	private List<GameObject> debugObjects = new List<GameObject>();
	
	private bool heIsDeadJim = false;
	private SceneFader sceneFader;
	
	private float deathTimer = 0; 
	
	public class Idle : BoBot_FSMState
	{			
		public Idle(){
			id = "Idle";
		}
		
		public override void init(){
			BoBotGlobal.animator.SetBool("climb", false);
			BoBotGlobal.animator.SetBool("hang", false);
			BoBotGlobal.animator.SetBool("hangedge", false);
			BoBotGlobal.animator.SetBool("hangle", false);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);
			BoBotGlobal.animator.SetBool("jump", false);
			BoBotGlobal.animator.SetBool("duck", false);
			BoBotGlobal.animator.SetBool("push", false);
			BoBotGlobal.physics_isGravity = true;
		}
		
		public override BoBot_FSMState transition(){			
			if (BoBotGlobal.variable_fallingTime > 0f){
			BoBotGlobal.variable_fallingTime -= Time.deltaTime;
			} else {
				BoBotGlobal.particleSystem.Stop();
			}	
			
			if (BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform){
				if ( BoBotGlobal.collider_mainCollider.sensorActive("control") != null){
					return BoBotGlobal.state_control;
				}
				
				if ( BoBotGlobal.collider_mainCollider.sensorActive("carry") != null){
					return BoBotGlobal.state_carry;
				}
			}
			
			/*if (BoBotGlobal.speed_fallSpeed > 2.0f){
				BoBotGlobal.character.transform.parent = BoBotGlobal.originParent;
			}*/
			
			if (BoBotGlobal.input_verticalDirectionShort > 0f ){//&& BoBotGlobal.character.isGrounded){
				BoBotGlobal.physics_velocity = BoBotGlobal.character.velocity;
				BoBotGlobal.physics_velocity.y = BoBotGlobal.speed_jumpSpeedUp;
				BoBotGlobal.animator.SetBool("jump", true);
				return BoBotGlobal.state_jump;
			}
			
			String sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			if (BoBotGlobal.input_verticalDirection > 0f &&  sensor != null) {
				BoBotGlobal.animator.SetBool("climb", true);
				BoBotGlobal.collider_mainCollider.bind(sensor);
				return BoBotGlobal.state_hang;
			}
			
			/*if (BoBotGlobal.input_verticalDirection > 0.0f) {
				if (BoBotGlobal.input_verticalDirection >= BoBotGlobal.time_timeTillIdleClimbJump){
					string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
					if ( sensor != null){	
						BoBotGlobal.animator.SetBool("climb", true);
						BoBotGlobal.collider_mainCollider.bind(sensor);
						return BoBotGlobal.state_hang;
					} /*else {
						BoBotGlobal.animator.SetBool("hang", true);
						if (BoBotGlobal.input_verticalDirection >= 1.0f){
							BoBotGlobal.physics_velocity = BoBotGlobal.character.velocity;
							BoBotGlobal.physics_velocity.y = BoBotGlobal.speed_jumpSpeedUp;
							return BoBotGlobal.state_jumpToClimbLadder;
						}
					}
				}
				return BoBotGlobal.state_idle;
			}*/
			
			//if (upKeyPressed){
						
			if (BoBotGlobal.input_horizontalDirection != 0.0f){
				BoBotGlobal.variable_actValueWalk = 0.0f;
		 		BoBotGlobal.variable_actVelocityWalk = 0.0f;
				BoBotGlobal.animator.SetFloat("Direction", BoBotGlobal.input_horizontalDirection);
				return BoBotGlobal.state_walk;
			} 
			
			if (BoBotGlobal.input_verticalDirection < 0.0f) {		
				return BoBotGlobal.state_duck;
			} 		
			
			return BoBotGlobal.state_idle;
		}		
	}
	
	public class Walk : BoBot_FSMState
	{
		float forwardSpeed = 4f;		
		
		public Walk(){
			id = "Walk";	
		}
		
		public override void init(){
			BoBotGlobal.animator.SetBool("climb", false);
			BoBotGlobal.animator.SetBool("hang", false);
			BoBotGlobal.animator.SetBool("hangedge", false);
			BoBotGlobal.animator.SetBool("hangle", false);
			BoBotGlobal.animator.SetBool("walk", true);
			BoBotGlobal.animator.SetBool("idle", false);
			BoBotGlobal.animator.SetBool("canCarry", false);
			BoBotGlobal.physics_isGravity = true;
		}
		
		public override BoBot_FSMState transition(){			
			//BoBotGlobal.character.transform.parent = BoBotGlobal.originParent;
			
			BoBotGlobal.animator.SetBool("canCarry", false);
			
			if (BoBotGlobal.variable_fallingTime > 0f){
			BoBotGlobal.variable_fallingTime -= Time.deltaTime;
			} else {
				BoBotGlobal.particleSystem.Stop();
			}
			
			/*if ( BoBotGlobal.collider_mainCollider.sensorActive("carry") != null){
				return BoBotGlobal.state_carry;
			}*/
							
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			if ((!BoBotGlobal.character.isGrounded && !BoBotGlobal.activePlatform) && sensor != null){				
				return BoBotGlobal.state_hang;
			}
						
			/*if (BoBotGlobal.sensor_action.Equals("canCarry")){
				//BoBotGlobal.animator.SetBool("canCarry", true);
				//BoBotGlobal.collider_carryCollider.beginCarry();
				return BoBotGlobal.state_carry;
			}*/
			
			if ( (BoBotGlobal.input_verticalDirectionShort > 0.0f || BoBotGlobal.input_verticalDirection > 0.0f) && (BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform)){
				//BoBotGlobal.physics_velocity = BoBotGlobal.character.velocity;
				BoBotGlobal.physics_velocity.y = BoBotGlobal.speed_jumpSpeedUp;
				BoBotGlobal.animator.SetBool("jump", true);
				return BoBotGlobal.state_jump;
			} 
				
			if ( BoBotGlobal.input_horizontalDirection == 0.0f){
				return BoBotGlobal.state_idle;
			} else {				
				BoBotGlobal.animator.SetFloat("Direction", BoBotGlobal.input_horizontalDirection);
				BoBotGlobal.variable_actValueWalk = Mathf.SmoothDamp( BoBotGlobal.variable_actValueWalk, BoBotGlobal.speed_forwardSpeed, ref BoBotGlobal.variable_actVelocityWalk, 0.5f);
				
				BoBotGlobal.physics_velocity = Vector3.right * BoBotGlobal.variable_actValueWalk * BoBotGlobal.input_horizontalDirection;	
				
			} 	
							
			
			
			return BoBotGlobal.state_walk;
		}
	}
	
	public class Climb : BoBot_FSMState
	{	
		public Climb(){
			id = "Climb";	
		}
		
		public override void init(){
			BoBotGlobal.animator.SetBool("climb", true);
			BoBotGlobal.animator.SetBool("hang", false);
			BoBotGlobal.animator.SetBool("jump", false);
			BoBotGlobal.animator.SetBool("hangedge", false);
			BoBotGlobal.animator.SetBool("hangle", false);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);
			BoBotGlobal.physics_isGravity = false;
			BoBotGlobal.particleSystem.Stop();
		}
		
		public override BoBot_FSMState transition(){
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			if ( sensor != null){				
				if (BoBotGlobal.input_horizontalDirection != 0.0f){
					BoBotGlobal.animator.SetFloat ("Direction", BoBotGlobal.input_horizontalDirection);
					BoBotGlobal.physics_velocity = BoBotGlobal.character.velocity;
					BoBotGlobal.physics_velocity.y += BoBotGlobal.speed_jumpSpeedUp*0.85f;
					BoBotGlobal.physics_velocity.x += BoBotGlobal.speed_jumpSpeedForward*0.25f*BoBotGlobal.input_horizontalDirection;
					return BoBotGlobal.state_jumpToOtherLadder;
				}	
				
				if (BoBotGlobal.input_verticalDirection > 0.0f || BoBotGlobal.input_verticalDirectionShort > 0.0f){
					BoBotGlobal.variable_actValueWalk = Mathf.SmoothDamp( BoBotGlobal.variable_actValueWalk, 1f, ref BoBotGlobal.variable_actVelocityWalk, 0.5f);
					BoBotGlobal.collider_mainCollider.moveVertical (sensor, BoBotGlobal.speed_climbUpSpeed * BoBotGlobal.variable_actValueWalk );
					if (sensor.Equals("climbEdge")){
						BoBotGlobal.collider_mainCollider.release("climbEdge");
						return BoBotGlobal.state_jump;
					}
					return BoBotGlobal.state_climb;
				} else if (BoBotGlobal.input_verticalDirection < 0.0f && !BoBotGlobal.character.isGrounded){
					BoBotGlobal.variable_actValueWalk = Mathf.SmoothDamp( BoBotGlobal.variable_actValueWalk, 1f, ref BoBotGlobal.variable_actVelocityWalk, 0.5f);
					BoBotGlobal.collider_mainCollider.moveVertical (sensor, -BoBotGlobal.speed_climbDownSpeed * BoBotGlobal.variable_actValueWalk );
					if (sensor.Equals("climbEdge")){
						BoBotGlobal.collider_mainCollider.release("climbEdge");
						return BoBotGlobal.state_jump;
					}
					return BoBotGlobal.state_climb;
				}  
			} else if (BoBotGlobal.character.isGrounded){ 
				return BoBotGlobal.state_idle;
			} 
			
			if (BoBotGlobal.input_verticalDirection > 0.0f){
				return BoBotGlobal.state_hang;
			}
			BoBotGlobal.physics_velocity.y = 0f;
			BoBotGlobal.physics_velocity.x = 0f;
			return BoBotGlobal.state_hang;
		}
	}
	
	public class Duck : BoBot_FSMState
	{
		public Duck(){
			id = "Duck";	
		}
		
		public override void init(){
			BoBotGlobal.animator.SetBool("climb", false);
			BoBotGlobal.animator.SetBool("hang", false);
			BoBotGlobal.animator.SetBool("hangedge", false);
			BoBotGlobal.animator.SetBool("hangle", false);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);
			BoBotGlobal.animator.SetBool("duck", true);			
			BoBotGlobal.physics_isGravity = true;
		}
		
		public override BoBot_FSMState transition(){
			
			
			if (BoBotGlobal.input_verticalDirection == 0.0f){
				return BoBotGlobal.state_idle;
			} 	
			
			
			return BoBotGlobal.state_duck;
		}
	}
	
	public class Hang : BoBot_FSMState
	{ 
		public float offset = 0.1f;
		private Vector3 oldPos;
		
		private bool keyUpPressed = false;
		private bool keyLeftPressed = false;
		private bool keyRightPressed = false;
		
		public Hang(){
			id = "Hang";	
			BoBotGlobal.physics_isGravity = false;
		}
		
		public override void init(){
			keyUpPressed = false;
		    keyLeftPressed = false;		
			keyRightPressed = false;		
			
			BoBotGlobal.physics_isGravity = false;
			BoBotGlobal.physics_movement.x = 0.0f;
			BoBotGlobal.variable_actValueWalk = 0.0f;
			BoBotGlobal.variable_actVelocityWalk = 0.0f;
			
			BoBotGlobal.animator.SetBool("climb", false);
			BoBotGlobal.animator.SetBool("hangle", false);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);
			BoBotGlobal.animator.SetBool("jump", false);
			//BoBotGlobal.animator.SetBool("hang", true);
			
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			if (sensor != "climbEdge"){
				BoBotGlobal.animator.SetBool("hang", true);
			}
		}
		
		public override BoBot_FSMState transition(){		
			
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			//Debug.Log ("sens "+sensor);
			if (sensor != null){				
				
					
				if (BoBotGlobal.input_horizontalDirectionShort != 0.0f){	
					float absInput = BoBotGlobal.input_horizontalDirectionShort/Mathf.Abs(BoBotGlobal.input_horizontalDirectionShort);
					if (sensor.Equals("climbEdge") && absInput == BoBotGlobal.animator.GetFloat("Direction")){
						BoBotGlobal.collider_mainCollider.moveVertical(sensor, 1f);
						BoBotGlobal.collider_mainCollider.release(sensor);
					} else {
						BoBotGlobal.physics_isGravity = true;
						BoBotGlobal.collider_mainCollider.release(sensor);
						BoBotGlobal.animator.SetFloat ("Direction", BoBotGlobal.input_horizontalDirectionShort);
						
						BoBotGlobal.physics_velocity = BoBotGlobal.character.velocity;
						BoBotGlobal.physics_velocity.y += BoBotGlobal.speed_jumpSpeedUp*0.85f;
						BoBotGlobal.physics_velocity.x += BoBotGlobal.speed_jumpSpeedForward*0.25f*BoBotGlobal.input_horizontalDirectionShort;
						BoBotGlobal.animator.SetBool("hang", false);
						BoBotGlobal.animator.SetBool("jump", true);
						Debug.Log ("c1");
					} 
					
					
					return BoBotGlobal.state_jumpToOtherLadder;
				}
				
				if ( Mathf.Abs (BoBotGlobal.input_horizontalDirection) > BoBotGlobal.time_timeUntilShortInput){
					BoBotGlobal.collider_mainCollider.moveHorizontal(sensor, BoBotGlobal.input_horizontalDirection);	
					Debug.Log ("c2");
					return BoBotGlobal.state_hang;
				}
				
				if (BoBotGlobal.input_verticalDirection != 0.0f){
					if ( (!BoBotGlobal.character.isGrounded && !BoBotGlobal.activePlatform) || BoBotGlobal.input_verticalDirection > 0.0f){	
						return BoBotGlobal.state_climb; 
					}
				}
				
			} else
					
			if (sensor == null){
				BoBotGlobal.collider_mainCollider.release("climb");
				return BoBotGlobal.state_fall;
			}
			
			return BoBotGlobal.state_hang;
		}
	}
	
	public class Fall : BoBot_FSMState
	{		
		float forwardSpeed = 0.5f;		
		
		public Fall(){
			id = "Fall";	
		}
		
		public override void init(){	
			BoBotGlobal.animator.SetBool("hangle", false);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);			
			BoBotGlobal.physics_isGravity = true;		
		}
		
		public override BoBot_FSMState transition(){			
			BoBotGlobal.physics_velocity.y += Physics.gravity.y * Time.deltaTime;
			BoBotGlobal.physics_movement.x *= BoBotGlobal.multiplier_inAirMultiplier;
			
			Quaternion newRot = BoBotGlobal.character.transform.rotation;
			newRot.z = 0.0f;
			BoBotGlobal.character.transform.rotation =  Quaternion.Slerp(BoBotGlobal.character.transform.rotation, newRot, 5f * Time.deltaTime);
			
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			if (sensor != null){	
				return BoBotGlobal.state_hang;
			}			
			if (BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform){
				if (BoBotGlobal.physics_movement == Vector3.zero){
					return BoBotGlobal.state_idle;
				} else {
					return BoBotGlobal.state_walk;
				}
			}								
			return BoBotGlobal.state_fall;
		}
	}
	
	public class Jump: BoBot_FSMState
	{
		float airDirection = 0.0f;		
		float minDistance = BoBotGlobal.distance_GrabEdge;
		float highestPoint = 0f;
		
		
		public Jump(){
			id = "Jump";	
			
			float highestPoint = 0f;		
		}
		
		public override void init(){
			minDistance = BoBotGlobal.distance_GrabEdge;
			BoBotGlobal.variable_fallingTime = 0f;
			BoBotGlobal.animator.SetBool("hangle", false);			
			BoBotGlobal.animator.SetBool("idle", false);
			BoBotGlobal.physics_isGravity = true;
			
			/*if (BoBotGlobal.collider_mainCollider.sensorActive("climb")){
				//BoBotGlobal.collider_mainCollider.release("climb");	
			}*/
		}
		
		public override BoBot_FSMState transition(){		
			BoBotGlobal.variable_fallingTime += Time.deltaTime;
			
			
			Quaternion newRot = BoBotGlobal.character.transform.rotation;
			newRot.z = 0.0f;
			BoBotGlobal.character.transform.rotation =  Quaternion.Slerp(BoBotGlobal.character.transform.rotation, newRot, 5f * Time.deltaTime);
			BoBotGlobal.physics_velocity.y += Physics.gravity.y * Time.deltaTime;
			airDirection = 0.0f;
			
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			if (sensor != null){
				BoBotGlobal.collider_mainCollider.bind(sensor);
				return BoBotGlobal.state_hang;	
			}			
								
			if (BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform){
				BoBotGlobal.particleSystem.startLifetime = BoBotGlobal.variable_fallingTime;
				BoBotGlobal.particleSystem.startSpeed = BoBotGlobal.variable_fallingTime * 4;
				BoBotGlobal.particleSystem.emissionRate = BoBotGlobal.variable_fallingTime * 25;
				BoBotGlobal.variable_fallingTime = 0.05f;
				BoBotGlobal.particleSystem.Play();
				BoBotGlobal.animator.SetBool("jump", false);
				if ( BoBotGlobal.input_horizontalDirection == 0f){				
					return BoBotGlobal.state_idle;
				} else {
					return BoBotGlobal.state_walk;
				}
			}				
			
			return BoBotGlobal.state_jump;
		}
	}
	
	public class JumpToClimbLadder  : BoBot_FSMState
	{
		float forwardSpeed = 0.5f;		
		
		public JumpToClimbLadder(){
			id = "JumpToClimbLadder";	
		}
		
		public override void init(){
			BoBotGlobal.animator.SetBool("hangle", false);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);			
			BoBotGlobal.physics_isGravity = true;
		}
		
		public override BoBot_FSMState transition(){			
			
			
			Quaternion newRot = BoBotGlobal.character.transform.rotation;
			newRot.z = 0.0f;
			BoBotGlobal.character.transform.rotation =  Quaternion.Slerp(BoBotGlobal.character.transform.rotation, newRot, 5f * Time.deltaTime);
			
			BoBotGlobal.physics_movement.x *= BoBotGlobal.multiplier_inAirMultiplier;
			BoBotGlobal.physics_velocity.y += Physics.gravity.y * Time.deltaTime;
			
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			if (sensor != null){	
				BoBotGlobal.collider_mainCollider.bind(sensor);
				return BoBotGlobal.state_hang;
			}			
			if (BoBotGlobal.character.isGrounded){
				if (BoBotGlobal.physics_movement == Vector3.zero){
					return BoBotGlobal.state_idle;
				} else {
					return BoBotGlobal.state_walk;
				}
			}
											
			return BoBotGlobal.state_jumpToClimbLadder;
		}
	}
	
	public class JumpToOtherLadder  : BoBot_FSMState
	{		
		float forwardSpeed = 0.5f;		
		bool exitCurrent = false;
		
		public JumpToOtherLadder(){
			id = "JumpToOtherLadder";			
		}
		
		public override void init(){
			exitCurrent = false;
			BoBotGlobal.animator.SetBool("hangle", true);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);			
			BoBotGlobal.physics_isGravity = true;
		}
				
		public override BoBot_FSMState transition(){			
			
			
			BoBotGlobal.physics_velocity.y += Physics.gravity.y * Time.deltaTime;
				
			Quaternion newRot = BoBotGlobal.character.transform.rotation;
			newRot.z = 0.0f;
			BoBotGlobal.character.transform.rotation =  Quaternion.Slerp(BoBotGlobal.character.transform.rotation, newRot, 5f * Time.deltaTime);
		
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			if (sensor != null){	
				BoBotGlobal.collider_mainCollider.bind(sensor);
				return BoBotGlobal.state_hang;
			}	
			
			if (BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform){
				if (BoBotGlobal.physics_movement == Vector3.zero){
					return BoBotGlobal.state_idle;
				} else {
					return BoBotGlobal.state_walk;
				}
			}			
								
			return BoBotGlobal.state_jumpToOtherLadder;
		}
	}	
	
	/*public class HangEdge : BoBot_FSMState
	{		
		float climbAnimationState = 0f;
		
		public HangEdge(){
			id = "HangeEdge";	
		}
		
		public override void init(){
			BoBotGlobal.animator.SetBool("climb", false);
			BoBotGlobal.animator.SetBool("hang", false);
			BoBotGlobal.animator.SetBool("hangle", false);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);					
			BoBotGlobal.physics_isGravity = false;
		}
		
		public override BoBot_FSMState transition(){			
			
			return BoBotGlobal.state_hangedge;
		}
	}*/
	
	public class Carry : BoBot_FSMState
	{	
		private float direction;
		
		public Carry(){
			id = "Carry";	
		}
		
		public override void init(){
			//carryTimer = 0f;	
			BoBotGlobal.variable_actValueWalk = 0f;
			BoBotGlobal.variable_actVelocityWalk = 0f;
			BoBotGlobal.animator.SetBool("jump", false);
			//BoBotGlobal.collider_mainCollider.bind("carry");
		}
		
		public override BoBot_FSMState transition(){
			BoBotGlobal.physics_isGravity = true;
			float direction = BoBotGlobal.animator.GetFloat("Direction");	
			
			string sensor = BoBotGlobal.collider_mainCollider.sensorActive("climb");
			/*if ( sensor != null){	
				return BoBotGlobal.state_jumpToClimbLadder;
			}*/
			
			if (BoBotGlobal.input_verticalDirection > 0f){
				BoBotGlobal.physics_velocity.y = BoBotGlobal.speed_jumpSpeedUp;
				BoBotGlobal.collider_mainCollider.release("carry");
				BoBotGlobal.animator.SetBool("canCarry", false);
				BoBotGlobal.animator.SetBool("climb", false);
				BoBotGlobal.animator.SetBool("canClimb", false);
				return BoBotGlobal.state_jumpToClimbLadder;
			}
			
			sensor = BoBotGlobal.collider_mainCollider.sensorActive("carry");			
			if (sensor == null){
				BoBotGlobal.animator.SetBool("push", false);
				BoBotGlobal.animator.SetBool("pull", false);
				BoBotGlobal.animator.SetBool("canCarry", false);
				BoBotGlobal.animator.SetBool("idle", true);
				BoBotGlobal.collider_mainCollider.release("carry");
				return BoBotGlobal.state_idle;
			} else {	
				BoBotGlobal.animator.SetBool("canCarry", true);
				if ( BoBotGlobal.input_horizontalDirection != 0.0f){					
					bool push = (direction < 0f && BoBotGlobal.input_horizontalDirection < 0f) || (direction > 0f && BoBotGlobal.input_horizontalDirection  > 0f);
				
					if ( push || (!push && BoBotGlobal.input_action > 0f)){	
						BoBotGlobal.collider_mainCollider.bind("carry");
						BoBotGlobal.collider_mainCollider.moveHorizontal(sensor, Convert.ToInt32(push));
						return BoBotGlobal.state_carry;
					} else {				
						BoBotGlobal.animator.SetBool("push", false);
						BoBotGlobal.animator.SetBool("pull", false);
						BoBotGlobal.animator.SetBool("canCarry", false);
						BoBotGlobal.animator.SetBool("walk", true);
						BoBotGlobal.collider_mainCollider.release("carry");
						return BoBotGlobal.state_walk;
					}
				} else {
					BoBotGlobal.collider_mainCollider.moveHorizontal(sensor, 0);	
				}
			}
			
			BoBotGlobal.variable_actValueWalk = 0.0f;
		 	BoBotGlobal.variable_actVelocityWalk = 0.0f;
			
			BoBotGlobal.animator.SetBool("climb", false);
			BoBotGlobal.animator.SetBool("hang", false);
			BoBotGlobal.animator.SetBool("hangle", false);
		//	BoBotGlobal.animator.SetBool("hangedge", false);
			BoBotGlobal.animator.SetBool("walk", false);
			BoBotGlobal.animator.SetBool("idle", false);
			BoBotGlobal.animator.SetBool("push", false);
			BoBotGlobal.animator.SetBool("pull", false);
			
			return BoBotGlobal.state_carry;
		}
	}
	
	public class Control : BoBot_FSMState
	{	
		private float direction;
		
		public Control(){
			id = "Control";	
		}
		
		public override void init(){
			//carryTimer = 0f;	
			BoBotGlobal.variable_actValueWalk = 0f;
			BoBotGlobal.variable_actVelocityWalk = 0f;
			BoBotGlobal.collider_mainCollider.bind("control");
		}
		
		public override BoBot_FSMState transition(){
			if ( BoBotGlobal.input_action > 0f){
				//BoBotGlobal.collider_mainCollider.moveVertical("control", BoBotGlobal.input_verticalDirection);
				direction = BoBotGlobal.animator.GetFloat("Direction");
				bool push = (direction < 0f && BoBotGlobal.input_horizontalDirection < 0f) || (direction > 0f && BoBotGlobal.input_horizontalDirection  > 0f);
				
			//	BoBotGlobal.collider_mainCollider.moveHorizontal(sensor, Convert.ToInt32(push));
					
				
				BoBotGlobal.collider_mainCollider.moveHorizontal("control", Convert.ToInt32(push));
				return BoBotGlobal.state_control;
			}
			
			if (BoBotGlobal.input_horizontalDirection != 0f){
				BoBotGlobal.collider_mainCollider.release("control");
				BoBotGlobal.animator.SetBool("canCarry", false);
				return BoBotGlobal.state_walk;
			}
			
			if (BoBotGlobal.input_verticalDirectionShort != 0f){
				BoBotGlobal.collider_mainCollider.release("control");
				BoBotGlobal.animator.SetBool("canCarry", false);
				return BoBotGlobal.state_jump;
			}
			
			
			return BoBotGlobal.state_control;
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		/*Collider otherCollider = other.collider;
		
		if (other.collider.rigidbody && !otherCollider.rigidbody.isKinematic && currentState.id.Equals("Idle")){
			BoBot_MoveableObject movable = otherCollider.GetComponent<BoBot_MoveableObject>();
				
			BoBotGlobal.physics_isGravity = true;
			if (movable){
				if (movable.recievePlayerForce){
					other.rigidbody.AddForce (BoBotGlobal.character.velocity * 15f + Physics.gravity*0.5f);
				}
				
				if (movable.actAsParent){
					BoBotGlobal.character.transform.parent = otherCollider.transform;
				} else {
					BoBotGlobal.character.transform.parent = BoBotGlobal.originParent;
				}
					
				BoBotGlobal.physics_isGravity = false;				
			}	
		}*/
		
		if (hit.moveDirection.y < -0.9 && hit.normal.y > 0.5) {
        	BoBotGlobal.activePlatform = hit.collider.transform;    
			/*if (BoBotGlobal.activePlatform.rigidbody){
				BoBotGlobal.activePlatform.rigidbody.AddForce(BoBotGlobal.physics_velocity * hit.normal.y * 2);
			}*/
  	  	}
	}
	
	void Start()
	{		
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
		//Application.targetFrameRate = 25;
		
		Physics.IgnoreLayerCollision (0,1);
		Physics.IgnoreLayerCollision (8,1);
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		// Cache component lookup at startup instead of doing this every frame		
		thisTransform = GetComponent<Transform>();
		this.background = GameObject.Find("LevelBG");	
		//this.frameDisplay = GameObject.Find("fps");
		//this.levelBoundingBox = GameObject.Find ("LevelBoundingBox");
		this.levelWidth =  GameObject.Find ("LevelBoundingBox").transform.localScale.x;
		this.levelHeight =  GameObject.Find ("LevelBoundingBox").transform.localScale.y;
		this.backgroundBounds = GameObject.Find("LevelBG").renderer.bounds;
		this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		var spawn = GameObject.Find( "PlayerSpawn" );
		if ( spawn )
			thisTransform.position = spawn.transform.position;
			
	    BoBotGlobal.animator = this.GetComponentInChildren<Animator>();
		BoBotGlobal.environment = GameObject.Find("Environment").GetComponent<BoBot_EnvironmentComponent>();
		//BoBotGlobal.collider_carryCollider = this.GetComponentInChildren<boBot_CarryCollider>();
		
		//BoBotGlobal.collider_climbLadderCollider = (boBot_MainColliderGeneric) this.GetComponentInChildren<boBot_ClimbLadder>();
	    //BoBotGlobal.collider_climbRopeCollider = (boBot_MainColliderGeneric) this.GetComponentInChildren<boBot_ClimbRope>();
		
		BoBotGlobal.animator = this.GetComponentInChildren<Animator>();		
	    BoBotGlobal.animator.SetBool("init", false);
		
		BoBotGlobal.character = this.GetComponent<CharacterController>();	
				
		//BoBotGlobal.sensor_distanceToCarry = 100f;
		//BoBotGlobal.sensor_action = "";
		//BoBotGlobal.sensor_distanceToEdge = 100f;
		//BoBotGlobal.sensor_action.Equals("canClimb") = false;
		//BoBotGlobal.sensor_action.Equals("canClimb") = false;		
		
		BoBotGlobal.originParent = BoBotGlobal.character.transform.parent;
		
		BoBotGlobal.state_idle = new Idle();
		BoBotGlobal.state_walk = new Walk();		
		BoBotGlobal.state_climb = new Climb();	
		BoBotGlobal.state_hang = new Hang();
		//BoBotGlobal.state_hangle = new Hangle();
		BoBotGlobal.state_fall = new Fall();
		//BoBotGlobal.state_hangedge = new HangEdge();
		BoBotGlobal.state_carry = new Carry();
		BoBotGlobal.state_control = new Control();
		BoBotGlobal.state_duck = new Duck();
		BoBotGlobal.state_jumpToClimbLadder = new JumpToClimbLadder();
		BoBotGlobal.state_jump = new Jump	();
		BoBotGlobal.state_jumpToOtherLadder = new JumpToOtherLadder();
		
		BoBotGlobal.particleSystem = GameObject.Find("boBotParticle").particleSystem;
		BoBotGlobal.collider_mainCollider = GameObject.Find("MainCollider").GetComponent<BoBot_MainCollider>();
		//BoBotGlobal.collider_climbCollider = GameObject.Find("MainCollider").GetComponent<boBot_ClimbRope>();
		
		BoBotGlobal.particleSystem.Stop();
		
		//lastHeigh = BoBotGlobal.character.transform.position.y;
		
		currentState = BoBotGlobal.state_idle;
		originZ = BoBotGlobal.character.transform.position.z;
		bobotTransform = BoBotGlobal.character.transform;
		
		GameObject [] objects = GameObject.FindGameObjectsWithTag("canClimbEdge");
		foreach (GameObject obj in objects){			
			debugObjects.Add (obj);
		}
		
		objects = GameObject.FindGameObjectsWithTag("debug");
		foreach (GameObject obj in objects){			
			debugObjects.Add (obj);
		}
		
		foreach (GameObject obj in debugObjects){			
			MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
			if (mesh != null){
				mesh.enabled = false;	
			}
		}
		
		BoBotGlobal.physics_isGravity = true;		
		
	}
		
	void OnEndGame()
	{
		// Disable joystick when the game ends	
		//moveTouchPad.Disable();	
		//jumpTouchPad.Disable();		
	
		// Don't allow any more control changes when the game ends
		this.enabled = false;
	}
	
	void calcBackgroundPosition(){
		
		//Debug.Log ("pos "+this.levelBoundingBox.transform.localScale.x +" chr: "+this.BoBotGlobal.character.transform.position.x);
		float diffX = (this.mainCamera.transform.position.x  - this.levelWidth / 2) / this.levelWidth;
		float diffY = (this.mainCamera.transform.position.y  - this.levelHeight / 2) / this.levelHeight;
						
		this.backgroundPosition = this.mainCamera.transform.position;
		this.backgroundPosition.x -= backgroundBounds.extents.x /2 * diffX;
		this.backgroundPosition.y -= backgroundBounds.extents.y /2 * diffY;
		this.backgroundPosition.z = 20;
		this.background.transform.position = this.backgroundPosition;
		
	}
	
	void Update(){			
		if (Input.GetKeyUp (KeyCode.D) )
		{
			BoBotGlobal.debugging = !BoBotGlobal.debugging;
			
			foreach (GameObject obj in debugObjects){
				try {
					MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
					if (mesh != null){
						mesh.enabled = BoBotGlobal.debugging;	
					}
				}
				catch{}
			}
		}
		
		
		
		
			//calcBackgroundPosition();
			BoBotGlobal.physics_movement = Vector3.zero;
			BoBotGlobal.animator.SetBool("isGrounded", BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform);
		
			Vector3 pos = bobotTransform.position;
			pos.z = originZ;// * Time.deltaTime - pos.z;		
			bobotTransform.position = pos;
						
			if (heIsDeadJim){			
				BoBotGlobal.animator.SetBool("deadBody", true);
				BoBotGlobal.physics_isGravity = true;
				BoBotGlobal.physics_velocity = Vector3.zero;
				deathTimer += Time.deltaTime;
				if (deathTimer > BoBotGlobal.time_timeTillReload){
					deathTimer = 0f;
					sceneFader.SwitchScene (Save_Load.ar_Player[1].ToString());
				}
			} else {
				
				if (BoBotGlobal.activePlatform != null) {
			        var newGlobalPlatformPoint = BoBotGlobal.activePlatform.TransformPoint(activeLocalPlatformPoint);
			        var moveDistance = (newGlobalPlatformPoint - activeGlobalPlatformPoint);
			        if (moveDistance != Vector3.zero)
			              BoBotGlobal.character.Move(moveDistance);
			        lastPlatformVelocity = (newGlobalPlatformPoint - activeGlobalPlatformPoint) / Time.deltaTime;
			 
			        var newGlobalPlatformRotation = BoBotGlobal.activePlatform.rotation * activeLocalPlatformRotation;
			        var rotationDiff = newGlobalPlatformRotation * Quaternion.Inverse(activeGlobalPlatformRotation);	      
			    }
			    else {
			        lastPlatformVelocity = Vector3.zero;
			    }
						
				rayLeft = new Ray(transform.position , new Vector3 (-0.1f,-1,0));
				rayRight = new Ray(transform.position , new Vector3 (0.1f,-1,0));
				
				
				/*if ( (BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform) && Physics.Raycast(rayLeft, out hitLeft, 1, slopeLayersToUse) & Physics.Raycast(rayRight, out hitRight, 1, slopeLayersToUse)){
					driftSpeed = Mathf.Clamp( (-(1f -(hitLeft.point.y / hitRight.point.y)) * 50), -0.5f, 0.5f);
					BoBotGlobal.physics_velocity +=  Vector3.right * driftSpeed;
				}*/
				
				
				newState = currentState.transition();
				if (newState != currentState){
					newState.init();
				}
				
				BoBotGlobal.activePlatform = null;
				
				string newOutput = "State: "+ newState.id+" Action: "+ BoBotGlobal.collider_mainCollider.getCurrentSensors() ;
				
				currentState = newState;			
				if (!newOutput.Equals(output)){
					Debug.Log(newOutput+ "  "+BoBotGlobal.character.isGrounded);
					output = newOutput;
				}
			}
			
			if (BoBotGlobal.physics_isGravity && BoBotGlobal.physics_velocity.sqrMagnitude < 1000f ){
				BoBotGlobal.physics_movement += BoBotGlobal.physics_velocity;
				BoBotGlobal.physics_movement += Physics.gravity;
			}		
	//		Debug.Log ("phy "+BoBotGlobal.physics_velocity+"   "+BoBotGlobal.character.velocity +"   "+BoBotGlobal.physics_movement +"    "+(BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform));
				
			BoBotGlobal.physics_movement *= Time.deltaTime;
			
			BoBotGlobal.character.Move( BoBotGlobal.physics_movement );
			
			if ( BoBotGlobal.character.isGrounded ) {
				BoBotGlobal.physics_velocity.x *= BoBotGlobal.multiplier_onGroundX;
				BoBotGlobal.physics_velocity.y = 0f;
			} 
			
			if (BoBotGlobal.activePlatform != null) {
		        activeGlobalPlatformPoint = transform.position;
		        activeLocalPlatformPoint = BoBotGlobal.activePlatform.InverseTransformPoint (transform.position);
		 
		        activeGlobalPlatformRotation = transform.rotation;
		        activeLocalPlatformRotation = Quaternion.Inverse(BoBotGlobal.activePlatform.rotation) * transform.rotation; 
	    	}
		
		
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("SidescrollControl");
			debugInfo.addText ("> State "+currentState.id);			
			debugInfo.addText ("> Gravity "+BoBotGlobal.physics_isGravity);
			debugInfo.addText ("> Grounded "+ (BoBotGlobal.character.isGrounded || BoBotGlobal.activePlatform != null));
			debugInfo.addText ("> Inp H "+(BoBotGlobal.input_horizontalDirection).ToString("0.00")+"/"+(BoBotGlobal.input_horizontalDirectionShort).ToString("0.00"));
			debugInfo.addText ("> Inp V "+(BoBotGlobal.input_verticalDirection).ToString("0.00")+"/"+(BoBotGlobal.input_verticalDirectionShort).ToString("0.00"));
			debugInfo.addText ("> Inp A "+(BoBotGlobal.input_action).ToString("0.00"));
			debugInfo.addText ("> Drift Spd "+(driftSpeed).ToString("0.00"));
		}
		
		if ( BoBotGlobal.collider_mainCollider.sensorActive("lethal") != null && !heIsDeadJim){
			heIsDeadJim = true;
			BoBotGlobal.animator.SetBool("die", true);
			Debug.Log ("der Vogel is dod");
		}
	}
	
	void updateGlobalVars(){
		BoBotGlobal.speed_forwardSpeed = speed_forwardSpeed;
		BoBotGlobal.speed_backwardSpeed = speed_backwardSpeed;
		BoBotGlobal.speed_climbUpSpeed = speed_climbUpSpeed;
		BoBotGlobal.speed_climbDownSpeed = speed_climbDownSpeed;
		BoBotGlobal.speed_pushSpeed = speed_pushSpeed;
		BoBotGlobal.speed_pullSpeed = speed_pullSpeed;
		BoBotGlobal.speed_jumpSpeedUp = speed_jumpSpeedUp;
		BoBotGlobal.speed_jumpSpeedForward = speed_jumpSpeedForward;
		BoBotGlobal.speed_slideSpeed = speed_slideSpeed;
		BoBotGlobal.speed_fallSpeed = speed_fallSpeed;
		
		BoBotGlobal.time_timeTillWalk = time_timeTillWalk;
		BoBotGlobal.time_timeTillIdle = time_timeTillIdle;
		BoBotGlobal.time_timeTillCarry = time_timeTillCarry;
		
		BoBotGlobal.multiplier_inAirMultiplier = multiplier_inAirMultiplier;
		BoBotGlobal.multiplier_onGroundX = multiplier_onGroundX;
		
		BoBotGlobal.distance_CanGrabEdge = distance_CanGrabEdge;
		BoBotGlobal.distance_GrabEdge = distance_GrabEdge;
		BoBotGlobal.distance_CanCarry = distance_CanCarry;
		BoBotGlobal.distance_Carry = distance_Carry;
		BoBotGlobal.distance_ToWall = distance_ToWall;
		
		BoBotGlobal.physics_pushForce = physics_pushForce;
		BoBotGlobal.physics_pullForce = physics_pullForce;
	}
}

