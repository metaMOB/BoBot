using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class BoBot_ControlCollider : BoBot_ActionColliderGeneric {
	private float direction;	
	private Vector3 distanceToBobot = Vector3.zero;
	private Vector3 oldPosition;
	
	private BoBotGlobal.controlMode controlMode;
	private BoBotGlobal.controlDirection controlDirection;
	private Quaternion rot;
	
	private Transform hand;
	
	private float posTimer = 0f;
	private float timeTillPos = 0.5f;
	private float deltaPos;
	
		
	private Vector3 newPos;
	void Awake () {		
		this.reactOnTag = "canControl";
		// Left 0.2 / -0.1
		// Right 
		this.distance = new Rect(0f, -0.2f, 0.5f, 0.4f);
		this.sensorValue = "control";
		this.sensorValueGroup = "control";
		
		hand = GameObject.Find("hand_r").GetComponent<Transform>();
	}
	
	/*public void FixedUpdate (){
		if (isBound){ // && this.otherToUse.rigidbody){
			
		}
	}*/
	
	public void Update (){
		if (isBound){ // && this.otherToUse.rigidbody){
			if (posTimer < timeTillPos){
				BoBotGlobal.character.Move (new Vector3(deltaPos, 0, 0) * Time.deltaTime);	
				posTimer += Time.deltaTime;
			}
		}
	}
		
	public override void moveHorizontal (float push){
		if (isBound){
			float dir = BoBotGlobal.animator.GetFloat("Direction");
			if (push == 1f){
				BoBotGlobal.animator.SetBool("push", true);
				BoBotGlobal.animator.SetBool("pull", false);
			} else {
				BoBotGlobal.animator.SetBool("push", false);
				BoBotGlobal.animator.SetBool("pull", true);
			}	
			
			newPos = Vector3.zero;
			
			float x = this.otherToUse.gameObject.transform.parent.GetComponent<BoBot_Switch>().moveSwitch(-BoBotGlobal.input_horizontalDirection).x;
			//float x = this.otherToUse.gameObject.transform.parent.GetComponent<BoBot_Switch>().getPos().x;
			//newPos.x =  ((x - hand.position.x) - 0.7f * dir) ;
			BoBotGlobal.character.Move (newPos * 1.5f);
		}
	}
	
	override public void bind(){
		float dir = BoBotGlobal.animator.GetFloat("Direction");
		dir = dir / Mathf.Abs(dir);
		base.bind();
		distanceToBobot = BoBotGlobal.character.transform.position - this.otherToUse.gameObject.transform.position;
		float x = this.otherToUse.gameObject.transform.parent.GetComponent<BoBot_Switch>().getPos().x;
		deltaPos = ((x - hand.position.x) - 0.7f * dir) / timeTillPos;
		BoBotGlobal.physics_velocity = Vector3.zero;
		//BoBotGlobal.character.Move (new Vector3(deltaPos, 0, 0)); // * Time.deltaTime);	
		
		Debug.Log ("hand "+ hand.position.x +"  "+x+ " d "+deltaPos);
		/*
		newPos = Vector3.zero;
		newPos.x = -xx/2;
		
		
		BoBotGlobal.character.Move (newPos * 1.5f);
		*/
		posTimer = 0f;
		BoBotGlobal.animator.SetBool("canCarry", true);
	}	
	
	override public void release(){
		base.release();
		BoBotGlobal.animator.SetBool("canCarry", false);
		BoBotGlobal.animator.SetBool("push", false);
		BoBotGlobal.animator.SetBool("pull", false);
	}	
}
