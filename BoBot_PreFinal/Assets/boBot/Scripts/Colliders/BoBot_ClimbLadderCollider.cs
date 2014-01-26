using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoBot_ClimbLadderCollider : BoBot_ActionColliderGeneric {
	
	void Awake () {
		this.reactOnTag = "canClimb";
		this.distance = new Rect(-0.15f, -0.15f, 0.3f, 0.3f);
		this.distancePrepare = new Rect(0.15f, -0.1f, 2.0f, 1.5f);	
		this.prepareAnimationName = "canClimb";
		this.sensorValue = "climbLadder";
		this.sensorValueGroup = "climb";
	}
	
	void Update (){
		BoBotGlobal.animator.SetBool("hangle", false);
	}
	
	public override void moveVertical (float amount){		
		if (isBound && (amount > 0 || !BoBotGlobal.character.isGrounded)){
			BoBotGlobal.animator.SetBool("climb", true);
			Vector3 targetPos = BoBotGlobal.character.transform.position;
			targetPos.y += amount *0.5f * Time.deltaTime;
			BoBotGlobal.character.transform.position = targetPos;
		}
	}
	
	public override void moveHorizontal (float direction){
		if (isBound){
			BoBotGlobal.animator.SetBool("hangle", true);
			BoBotGlobal.animator.SetFloat("Direction", direction);
			Vector3 targetPos = BoBotGlobal.character.transform.position;
			targetPos.x += direction * BoBotGlobal.speed_slideSpeed * Time.deltaTime;
			BoBotGlobal.character.transform.position = targetPos;
		}
	}
	
	public override void bind(){
		base.bind();
		BoBotGlobal.animator.SetBool("hang", true);
	}
}
