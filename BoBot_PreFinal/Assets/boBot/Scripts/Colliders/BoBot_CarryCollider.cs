using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoBot_CarryCollider : BoBot_ActionColliderGeneric {
	private float direction;	
	private Vector3 distanceToBobot = Vector3.zero;
	private Vector3 oldPosition;
	
	private Rigidbody otherRigid;
	private BoBot_DebugComponent debugInfo;
	private Vector3 otherDistance; 
	private Transform mainCollider;
			
	void Awake () {		
		this.reactOnTag = "canCarry";
		this.distance = new Rect(0f, -0.45f, 0.6f, 0.75f);	
		this.sensorValue = "carry";
		this.sensorValueGroup = "carry";
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();

	
	}
	
	public void Update(){
		if (isBound){
			debugInfo.addText("CarryCollider");
			debugInfo.addText("Distance "+distanceToBobot);
		}
	}
	
	public void FixedUpdate (){
		if (isBound){ 
			float factor = 1;
			
			if (direction == 1f){
				BoBotGlobal.animator.SetBool("push", true);
				BoBotGlobal.animator.SetBool("pull", false);
			} else  if (direction == 0){
				BoBotGlobal.animator.SetBool("push", false);
				BoBotGlobal.animator.SetBool("pull", true);
			} else {
				BoBotGlobal.animator.SetBool("push", false);
				BoBotGlobal.animator.SetBool("pull", false);
			}
			
			BoBotGlobal.physics_velocity = new Vector3( BoBotGlobal.input_horizontalDirection * 0.7f,0,0);
			this.otherRigid.velocity = ( Vector3.right * (BoBotGlobal.input_horizontalDirection * 0.7f)  );
			Vector3 newPos = this.otherRigid.transform.position;
			
			newPos = mainCollider.transform.position + otherDistance;
		}
	}
			
	public override void moveHorizontal (float push){
		if (isBound){			
			direction = push;
			oldPosition = this.otherRigid.position;
		}
	}
	
	override public void bind(){
		base.bind();
		if (this.otherToUse.rigidbody){
			this.otherRigid = this.otherToUse.rigidbody;
		} else {
			this.otherRigid = this.otherToUse.transform.parent.rigidbody;	
		}
		
		mainCollider = BoBotGlobal.collider_mainCollider.transform;		
		otherDistance = (otherToUse.ClosestPointOnBounds (mainCollider.position) - mainCollider.transform.position);
		
		distanceToBobot = this.otherRigid.transform.position - BoBotGlobal.character.transform.position ;
	}	
	
	override public void release(){
		isBound = false;
		base.release();
		Debug.Log("RELEASE CARRY");
		this.otherToUse = null;
	}	
}
