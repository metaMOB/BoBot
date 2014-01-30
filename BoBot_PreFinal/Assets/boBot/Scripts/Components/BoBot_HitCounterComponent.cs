using UnityEngine;
using System.Collections;

public class BoBot_HitCounterComponent : MonoBehaviour {
	
	public int hitsTillAction = 3;
	public BoBot_OnOffComponent [] control;
	public string reactOnTag = "Player";
	public float minimumCollisionSpeed = 0f;
	public int channel = 1;
	
	private int hits = 0;
	private bool done = false;
	private Transform thisTransform;
	private bool busy = false;
	
	void Start () {
		thisTransform = transform;
	}
	
	void Update () {
		if (!done && hits >= hitsTillAction){
			done = true;
			Debug.Log ("DESTROY!!!");
			foreach (BoBot_OnOffComponent cmp in control){
				cmp.setValue(1.0f, channel);	
			}
		}
	}
	
	void OnTriggerEnter (Collider other){
		if (!done && !busy && other.name.Equals("HitMarker")){
			BoBot_BasicPhysicsComponent otherPhys = other.gameObject.GetComponent<BoBot_BasicPhysicsComponent>();
			float otherSpeed = -1000;
			if (otherPhys){
				otherSpeed = otherPhys.delta.y;
			}
			
			Debug.Log ("Speeed "+otherSpeed+"  "+other.name);
			
			if (minimumCollisionSpeed == 0 || otherSpeed <= minimumCollisionSpeed){
				
				busy = true;
	   			Vector3 dirOther = other.transform.position - thisTransform.position;
	    		float angle = Vector3.Angle(dirOther, thisTransform.forward);
				if (angle >= 80 && angle <= 160){
					Debug.Log ("angel !!! "+angle);
					hits++;
				}
			}			
		}
	}
	
	void OnTriggerExit (Collider other){
		if (!done && busy && other.name.Equals("HitMarker") ){
			busy = false;
		}
	}
}
