﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoBot_ClimbEdgeCollider : BoBot_ActionColliderGeneric {
	
	
	private float timerOff = 0f;
	private float timeTillActive = 0.5f;
	
	void Awake () {		
		this.reactOnTag = "canClimbEdge";
		this.distance = new Rect(0.15f, 0.0f, 0.5f, 0.0001f);
		this.distancePrepare = new Rect(-0.2f, -1.1f, 1.4f, 2.5f);		
		
		this.sensorValue = "climbEdge";
		this.sensorValueGroup = "climb";
		this.prepareAnimationName = "canClimbEdge";
		this.timeTillActive = 0.5f;
	}
	
	void Update (){
		timerOff += Time.deltaTime;
		
		if (isBound){
			Vector3 isPos = BoBotGlobal.collider_mainCollider.transform.position;
			try {
				Vector3 targetPos = this.otherToUse.transform.position;
				if (isPos != targetPos){				
					Vector3 newPos = Vector3.zero;
					newPos.x = targetPos.x - isPos.x;
					newPos.y = targetPos.y - isPos.y;
					BoBotGlobal.character.Move(newPos); 
				}
			} catch { release(); }
		}
	}	
	
	public override void moveVertical (float amount){
		if (isBound){
			release();
			if (amount > 0){
				BoBotGlobal.physics_velocity.y = BoBotGlobal.speed_jumpSpeedUp * 1.1f;
				BoBotGlobal.physics_velocity.x = BoBotGlobal.speed_jumpSpeedForward * 0.15f * BoBotGlobal.animator.GetFloat("Direction");
			} else {
				BoBotGlobal.physics_velocity = BoBotGlobal.character.velocity;
				BoBotGlobal.physics_velocity.y = BoBotGlobal.speed_jumpSpeedUp/1.2f;
				BoBotGlobal.physics_velocity.x = BoBotGlobal.speed_jumpSpeedForward/13 * -BoBotGlobal.animator.GetFloat("Direction");				
				BoBotGlobal.animator.SetFloat("Direction", BoBotGlobal.input_horizontalDirectionShort);
				BoBotGlobal.physics_isGravity = true;	
			}
			BoBotGlobal.animator.SetBool("canClimbEdge", false);
		}
	}
	
	public override void moveHorizontal (float direction){
		if (isBound){
			float bodyDirection = BoBotGlobal.animator.GetFloat ("Direction");
			if ( (bodyDirection+BoBotGlobal.input_horizontalDirectionShort) == 0f){
				BoBotGlobal.physics_velocity = BoBotGlobal.character.velocity;
				BoBotGlobal.physics_velocity.y = BoBotGlobal.speed_jumpSpeedUp/1.2f;
				BoBotGlobal.physics_velocity.x = BoBotGlobal.speed_jumpSpeedForward/13 * BoBotGlobal.input_horizontalDirectionShort;				
				BoBotGlobal.animator.SetFloat("Direction", BoBotGlobal.input_horizontalDirectionShort);
				BoBotGlobal.physics_isGravity = true;				
			} else {
				BoBotGlobal.physics_velocity.y = BoBotGlobal.speed_jumpSpeedUp * 1.1f;
				BoBotGlobal.physics_velocity.x = BoBotGlobal.speed_jumpSpeedForward * 0.15f * BoBotGlobal.animator.GetFloat("Direction");
			}
			BoBotGlobal.animator.SetBool("canClimbEdge", false);
		}
	}
		
	public override void release (){
		base.release();
		BoBotGlobal.animator.SetBool("canClimbEdge", false);
		BoBotGlobal.animator.SetBool("hangedge", false);
		BoBotGlobal.physics_isGravity = true;	
		timerOff = 0f;
	}	
		
	public override void bind (){
		if (timerOff > timeTillActive){
			base.bind();
			Debug.Log ("EDGE");
			BoBotGlobal.physics_isGravity = false;	
			BoBotGlobal.animator.SetBool("canClimbEdge", true);
			BoBotGlobal.animator.SetBool("hangedge", true);
		}
	}	
}
