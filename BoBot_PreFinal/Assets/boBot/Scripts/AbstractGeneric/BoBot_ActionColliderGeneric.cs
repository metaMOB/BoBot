using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BoBot_ActionColliderGeneric : MonoBehaviour {	
	
	public virtual string sensorValue {get; set;}
	public virtual string sensorValueGroup {get; set;}
	
	protected string reactOnTag;
	protected Rect distance;
	protected Rect distancePrepare;
	
	protected string prepareAnimationName = "";
	
	
	private GameObject objectMainOnRelease;
	private GameObject objectMain;
	
	protected bool tryToRelease;
	protected bool isBound = false;
	protected Collider other;
	protected Collider otherToUse;
	
	public bool isUseable;// {get; set;}
	public bool isActive;// {get; set;}
	
	protected bool isHorizontalOrientation;
	protected bool isVerticalOrientation;
	
	protected int perpareFound = 0;
	protected Vector2 distanceRelative;
	
	private float begin;
	
	public virtual void check(Vector2 distancePosititon, Collider other, Vector2 distanceRelative){			
		
		this.distanceRelative = distanceRelative;
		if ( other.collider.CompareTag(this.reactOnTag) ){				
			if (distance.Contains(distancePosititon)){ 						
				/*if (other.collider.CompareTag("canCarry")){
//					Debug.Log(distancePosititon);
//					Debug.Log ("edge "+ distanceRelative.x +" // "+distanceRelative.y);
				}*/
				
				isActive = true;
				this.other = other;
			} 
			if (this.prepareAnimationName != ""){
				if (this.distancePrepare.Contains(distanceRelative)){
					perpareFound++;
				}
				BoBotGlobal.animator.SetBool(this.prepareAnimationName , perpareFound > 0);					
			}			
		}		
	}	
	
	void Update(){			
	}
		
	public virtual void updateState(){
		this.isUseable = this.isActive && !this.tryToRelease;		
		if (tryToRelease){			
			if ( Time.fixedTime - begin > 0.1f){
				tryToRelease = false;
			}
		}	
	
		this.isActive = false;
		this.perpareFound = 0;
	}
	
	public virtual void moveVertical (float amount){
	}
	
	public virtual void moveHorizontal (float direction){
	}
		
	public virtual void release (){		
		
		tryToRelease = true;
		isBound = false;
		begin = Time.fixedTime;
	}	
		
	public virtual void bind (){
		this.otherToUse = this.other;
		isBound = true;
	}
}
