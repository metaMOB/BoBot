using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class BoBot_ClimbRopeCollider : BoBot_ActionColliderGeneric {
	
	private List<BoBot_Chain_Element> chain = new List<BoBot_Chain_Element>();	
	private float actualChainElement = 0;	
	private int actualElement = 0;
	private int nextElement = 0;
	
	void Awake () {		
		this.reactOnTag = "canClimbRope";
		this.distance = new Rect(-0.15f, -0.15f, 0.3f, 0.3f);
		this.distancePrepare = new Rect(0.15f, -1f, 5.0f, 2.5f);	
		this.prepareAnimationName = "canClimb";
		this.sensorValue = "climbRope";
		this.sensorValueGroup = "climb";		
	}
	
	void Update () { 
		if (isBound){			
			Vector3 targetPos = Vector3.Lerp (chain[actualElement].transform.position, chain[nextElement].transform.position, actualChainElement%1.0f) - transform.position; 
			targetPos.z = 0;
			BoBotGlobal.character.Move (targetPos);
				
			Vector3 targetRot = (chain.Last().transform.localPosition - chain.First().transform.localPosition).normalized;
				
			Quaternion newRot = BoBotGlobal.character.transform.rotation;
			newRot.z = Quaternion.LookRotation(targetRot).z;
			BoBotGlobal.character.transform.rotation = newRot;
		}		
	}	
	
	public override void moveVertical (float amount){
		if (isBound){
			actualChainElement += amount * Time.deltaTime;
			actualChainElement = Mathf.Min (chain.Count-2f, actualChainElement);
			
			actualElement = Mathf.FloorToInt(Mathf.Min (chain.Count-1.0f, Mathf.Max (0.0f, actualChainElement)));
			nextElement = Mathf.Min (actualElement+1, chain.Count-1);
		}
	}
	
	public override void moveHorizontal (float direction){
		if (isBound){
			chain[actualElement].rigidbody.AddForce(Vector3.right * direction * 10f);
		}
	}
		
	public override void release (){
		base.release();
		BoBotGlobal.animator.SetBool("hang", false);
		chain = new List<BoBot_Chain_Element>();
	}	
	
	public override void bind (){		
		base.bind();
		BoBotGlobal.animator.SetBool("hang", true);
		if (chain.Count == 0)
		{			
			Transform otherParent = this.otherToUse.transform.parent;
			BoBot_Chain_Element[] others = otherParent.transform.GetComponentsInChildren<BoBot_Chain_Element>();
			BoBot_Chain_Element lastEntry = others.Last(); 
			
			foreach (BoBot_Chain_Element entry in others){
				if (entry.openEnd){
					chain.Add (entry);
					lastEntry = entry;
					break;
				}
			}
			
			while (lastEntry.GetComponent<CharacterJoint>() && lastEntry.GetComponent<CharacterJoint>().connectedBody){
				chain.Add (lastEntry.GetComponent<CharacterJoint>().connectedBody.transform.GetComponent<BoBot_Chain_Element>());				
				lastEntry = lastEntry.GetComponent<CharacterJoint>().connectedBody.transform.GetComponent<BoBot_Chain_Element>();
			}			
			
			actualChainElement = chain.IndexOf(this.otherToUse.transform.GetComponent<BoBot_Chain_Element>());
			actualElement = Mathf.FloorToInt(Mathf.Min (chain.Count-1.0f, Mathf.Max (0.0f, actualChainElement)));
			nextElement = Mathf.Min (actualElement+1, chain.Count-1);
						
			Vector3 distance = (chain.First().transform.position - chain.Last().transform.position).normalized;			
			isHorizontalOrientation = (distance.x > distance.y);
			isVerticalOrientation = !isHorizontalOrientation;	
		} 
	}	
}
