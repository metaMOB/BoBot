using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class BoBot_DeathCollider : BoBot_ActionColliderGeneric {
	private float direction;	
	private Vector3 distanceToBobot = Vector3.zero;
	private Vector3 oldPosition;
	
	private Rigidbody otherRigid;
	private BoBot_DebugComponent debugInfo;
	private Vector3 otherDistance; 
	private Transform mainCollider;
			
	void Awake () {		
		this.reactOnTag = "lethal";
		this.distance = new Rect(-0.5f, -0.6f, 1f, 0.7f);
		this.sensorValue = "lethal";
		this.sensorValueGroup = "lethal";
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		//this.distancePrepare = new Rect(0f, -0.1f, 1.6f, 1.5f);		
		//this.prepareAnimationName = "die";
	
	}
	
	public void Update(){
		if (isBound){
			debugInfo.addText("DeathCollider");
			debugInfo.addText("Distance "+distanceToBobot);
		}
	}
}
