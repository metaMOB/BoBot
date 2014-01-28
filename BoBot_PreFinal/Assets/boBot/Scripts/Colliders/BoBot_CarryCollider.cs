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
		this.distance = new Rect(0.15f, -0.45f, 0.6f, 0.5f);
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
				
				//newPos.x = BoBotGlobal.character.transform.position.x + distanceToBobot.x*1.05f;
			} else  if (direction == 0){
				BoBotGlobal.animator.SetBool("push", false);
				BoBotGlobal.animator.SetBool("pull", true);
				factor = 1.1f;
				//newPos.x = BoBotGlobal.character.transform.position.x + distanceToBobot.x*0.97f;
			} else {
				BoBotGlobal.animator.SetBool("push", false);
				BoBotGlobal.animator.SetBool("pull", false);
			}
			
			BoBotGlobal.physics_velocity = new Vector3( BoBotGlobal.input_horizontalDirection /2f,0,0);
			//this.otherRigid.AddForce ( Vector3.right * (BoBotGlobal.input_horizontalDirection) * factor );
			this.otherRigid.velocity = ( Vector3.right * (BoBotGlobal.input_horizontalDirection / 2f)  );
			//this.otherRigid.MovePosition ( this.otherRigid.transform.position + Vector3.right * ((BoBotGlobal.input_horizontalDirection * Time.deltaTime) / 2f) * factor);
			
			Vector3 newPos = this.otherRigid.transform.position;
			
			/*if (BoBotGlobal.animator.GetFloat("Direction") > 0){
				newPos.x = BoBotGlobal.character.transform.position.x + 0.51f; //distanceToBobot.x;
			} else {
				newPos.x = BoBotGlobal.character.transform.position.x - 1.7f;
			}*/
			
			newPos = mainCollider.transform.position + otherDistance;
				
			//newPos.x = BoBotGlobal.character.transform.position.x + distanceToBobot.x *factor;
			/////Debug.Log ( "pos "+(BoBotGlobal.character.transform.position.x - newPos.x));
			//newPos.x += BoBotGlobal.input_horizontalDirection /2f;
			
			//this.otherRigid.AddForce(Vector3.right * BoBotGlobal.input_horizontalDirection * 1.5f);
		   
			
			//this.otherRigid.MovePosition (newPos);
		}
	}
	
		
	public override void moveHorizontal (float push){
		if (isBound){
			
			direction = push;
			oldPosition = this.otherRigid.position;
			
			
			
			
			
			/*Debug.Log (push);
			Vector3 newPos = other.rigidbody.transform.position;
			BoBotGlobal.physics_velocity = Vector3.right * BoBotGlobal.input_horizontalDirection;		
			if (push == 1f){
				BoBotGlobal.animator.SetBool("push", true);
				BoBotGlobal.animator.SetBool("pull", false);
				newPos.x = BoBotGlobal.character.transform.position.x + distanceToBobot.x*1.05f;
			} else {
				BoBotGlobal.animator.SetBool("push", false);
				BoBotGlobal.animator.SetBool("pull", true);
				newPos.x = BoBotGlobal.character.transform.position.x + distanceToBobot.x*0.97f;
			}			
			
			other.rigidbody.MovePosition ( newPos);
			
			Debug.Log ("distance "+distance.x+ " "+ BoBotGlobal.input_horizontalDirection+" m: "+other.rigidbody.mass );
			*/
		}
	}
	
	override public void bind(){
		base.bind();
		//Physics.IgnoreLayerCollision(8, 0, true);
		//Debug.Log ("name "+this.otherToUse.name);
		if (this.otherToUse.rigidbody){
			this.otherRigid = this.otherToUse.rigidbody;
		} else {
			this.otherRigid = this.otherToUse.transform.parent.rigidbody;	
		}
		
		mainCollider = BoBotGlobal.collider_mainCollider.transform;		
		otherDistance = (otherToUse.ClosestPointOnBounds (mainCollider.position) - mainCollider.transform.position);
		
		//Debug.Log ("di "+otherDistance.x);
		distanceToBobot = this.otherRigid.transform.position - BoBotGlobal.character.transform.position ;
	}	
	
	override public void release(){
		base.release();
		//Physics.IgnoreLayerCollision(8, 0, false);
		//distanceToBobot = this.otherToUse.transform.position - BoBotGlobal.character.transform.position ;
	}	
}
