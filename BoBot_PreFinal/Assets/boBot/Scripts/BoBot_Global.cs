using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BoBotGlobal {
		//public static bool sensor_canClimbUp = false;
		//public static bool sensor_canClimbDown = false;
		public enum controlMode {onOff, hold};
		public enum controlDirection {horizontal, vertical};
	
		public static bool sensor_wallCollision = false;
	
		//public static Dictionary <string, BoBot_SensorCollider> sensor_active = new Dictionary<string, BoBot_SensorCollider>();
	
		//public static bool sensor_canClimbRope = false;
		//public static float sensor_distanceToEdge = 100f;		
		//public static float sensor_distanceToCarry = 100f;	
		
		//public static bool sensor_climb = false;
		//public static bool sensor_carry = false;
	
			/*public static float key_keyLeft;
		public static float key_keyRight;
		public static float key_keyUp;
		public static float key_keyDown;
		public static float key_keyJump;
		public static float key_keyAction;
		public static float key_keyNull;*/
	
		public static float input_verticalDirection = 0f;
		public static float input_horizontalDirection = 0f;
		public static float input_verticalDirectionShort = 0f;
		public static float input_horizontalDirectionShort = 0f;
		public static float input_action = 0f;
		public static float input_menu = 0f;
	
		public static float speed_forwardSpeed = 4f;
		public static float speed_backwardSpeed = 4f;		
		public static float speed_climbUpSpeed = 2f;
		public static float speed_climbDownSpeed = 2f;
		public static float speed_pushSpeed = 2.5f;
		public static float speed_pullSpeed = 2.5f;
		public static float speed_jumpSpeedUp = 14f;
		public static float speed_jumpSpeedForward = 14f;
		public static float speed_slideSpeed = 0.5f;
		public static float speed_fallSpeed = 0f;
		public static float sensor_distanceToWall = 100f;
	
		public static BoBot_FSMState state_idle;
		public static BoBot_FSMState state_walk;	
		public static BoBot_FSMState state_climb;
		public static BoBot_FSMState state_hang;
		public static BoBot_FSMState state_hangle;
		public static BoBot_FSMState state_fall;
		public static BoBot_FSMState state_hangedge;
		public static BoBot_FSMState state_carry;
		public static BoBot_FSMState state_duck;
		public static BoBot_FSMState state_jumpToClimbLadder;
		public static BoBot_FSMState state_jump;
		public static BoBot_FSMState state_jumpToOtherLadder;
		public static BoBot_FSMState state_swing;
		public static BoBot_FSMState state_control;
	
		public static ParticleSystem particleSystem;
	
		public static float time_timeTillWalk = 0.25f;		
		public static float time_timeTillIdle = 0.25f;
		public static float time_timeTillCarry = 1.5f;
		public static float time_timeTillIdleClimbJump = 0.25f; // 0 to 1 Second
		public static float time_timeUntilShortInput = 0.25f;
		
		public static float multiplier_inAirMultiplier = 0.05f;	
		public static float multiplier_onGroundX = 0.85f;
	
	
		public static float distance_CanGrabEdge = 2.5f;
		public static float distance_GrabEdge = 0.9f;
		public static float distance_CanCarry = 1.25f;
		public static float distance_Carry = 0.2f;
		public static float distance_ToWall = 2.5f;
	
		public static Transform activePlatform;
		public static BoBot_EnvironmentComponent environment;
		
		//public static Rect distance_climb = Rect(-0.15f, 0f, 0.15f, 0.45f);
		//0.15f;
		//public static float distance_climbY = 0.45f;
		public static float distance_cameraYOffset = 2.0f;
	
		public static Animator animator;
		public static CharacterController character;
	
		//public static boBot_CarryCollider collider_carryCollider;
		//public static boBot_MainColliderGeneric collider_climbLadderCollider;
		//public static boBot_MainColliderGeneric collider_climbRopeCollider;
		public static BoBot_MainCollider collider_mainCollider;
		
		//public static boBot_ActionColliderGeneric collider_climbCollider;
		
		
		public static Vector3 physics_movement;
		public static Vector3 physics_velocity;
		public static float physics_pushForce = 1.0f;
		public static float physics_pullForce = 1.0f;
		public static bool physics_isGravity = true;
	
		public static float variable_actVelocityWalk = 0.0f;
		public static float variable_actValueWalk = 0.0f;
		public static float variable_ropeHeight;		
		public static float variable_fallingTime = 0f;		
		
		public static bool debugging = false;
	
		public static int state_currentState;
	
		public static Transform originParent;
}