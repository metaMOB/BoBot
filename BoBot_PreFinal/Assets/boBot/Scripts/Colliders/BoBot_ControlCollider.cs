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
	private Transform bobot;
	
	private float posTimer = 0f;
	private float timeTillPos = 0.5f;
	private float deltaPos;	
		
	private Vector3 newPos;
	void Awake () {		
		this.reactOnTag = "canControl";
		this.distance = new Rect(0f, -0.2f, 0.35f, 0.4f);
		this.sensorValue = "control";
		this.sensorValueGroup = "control";
		
		hand = GameObject.Find("hand_r").GetComponent<Transform>();
	}
	
	public void Update (){
		if (isBound){
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
			newPos.x = x;
			Debug.Log ("   sadasd "+x);
			BoBotGlobal.character.Move (newPos);
		}
	}
	
	override public void bind(){
		float dir = BoBotGlobal.animator.GetFloat("Direction");
		dir = dir / Mathf.Abs(dir);
		base.bind();
		distanceToBobot = BoBotGlobal.character.transform.position - this.otherToUse.gameObject.transform.position;
		float x = this.otherToUse.gameObject.transform.parent.GetComponent<BoBot_Switch>().getPos().x;
		deltaPos = ((x - hand.position.x) - (BoBotGlobal.animator.GetFloat("Direction") * 0.75f))   / timeTillPos;
		BoBotGlobal.physics_velocity = Vector3.zero;
		
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
