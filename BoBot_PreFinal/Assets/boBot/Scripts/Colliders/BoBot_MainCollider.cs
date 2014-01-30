using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class BoBot_MainCollider : MonoBehaviour {
	
	
	private Dictionary<string, BoBot_ActionColliderGeneric> sensors = new Dictionary<string, BoBot_ActionColliderGeneric>();
	private BoBot_DebugComponent debugInfo;
	
	private GameObject visual;
	private string isBound = "";
		
	void Start () {		
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		visual = GameObject.Find("Visual");
			
		BoBot_ActionColliderGeneric [] colliders = gameObject.GetComponents<BoBot_ActionColliderGeneric>();
		foreach (BoBot_ActionColliderGeneric collider in colliders){
			sensors.Add(collider.sensorValue, collider);	
			Debug.Log (collider.sensorValueGroup+"   "+collider.sensorValue);
		}
		
		Physics.IgnoreLayerCollision(10, 1);
		Physics.IgnoreLayerCollision(10, 2);
		Physics.IgnoreLayerCollision(10, 3);
		Physics.IgnoreLayerCollision(10, 4);
		Physics.IgnoreLayerCollision(10, 5);
		Physics.IgnoreLayerCollision(10, 6);
		Physics.IgnoreLayerCollision(10, 7);
		Physics.IgnoreLayerCollision(10, 8);
	}
		
	void Update (){		
		if (BoBotGlobal.debugging && debugInfo){			
			foreach (string sensor in sensors.Keys){
				debugInfo.addText("> "+sensor+" active: "+ sensors[sensor].isUseable);
			}
		}
		
		foreach (BoBot_ActionColliderGeneric sensor in sensors.Values){	
			sensor.updateState();
		}	
	}
	
	void OnTriggerStay (Collider other){
		Transform mainCollider = BoBotGlobal.collider_mainCollider.transform;		
		Vector3 otherDistance = (other.ClosestPointOnBounds (mainCollider.position) - mainCollider.transform.position);
		Vector3 otherDistanceCor = otherDistance;
		otherDistanceCor.x *= BoBotGlobal.animator.GetFloat("Direction");
		Vector2 newDist = new Vector2(otherDistanceCor.x, otherDistanceCor.y);
		
		BoBot_BasicPhysicsComponent basicPhys = other.GetComponent<BoBot_BasicPhysicsComponent>();
		if (basicPhys != null){
			if ( Mathf.Abs(basicPhys.deltaTwo.x) > 0.3f || Mathf.Abs(basicPhys.deltaTwo.y) > 0.3f){
				Debug.Log ("Er ist Tot Jim!!!");	
			}
		}
				
		foreach ( BoBot_ActionColliderGeneric sensor in sensors.Values){			
			Vector2 relDist = newDist;
			try {
				if (other.tag != "Untagged"){
					if (BoBotGlobal.debugging){
						visual.transform.position = mainCollider.transform.position + otherDistance;
						visual.GetComponent<TextMesh>().text = other.name;
					}
					sensor.check(newDist, other, relDist);
				}
			} catch {}
		}			
		
	}
	
	public void bind (string name){
		sensors[name].bind();
		isBound = sensors[name].sensorValueGroup;
	}
	
	public void release (string name){
		try {
			sensors[name].release();	
		} catch {}
		isBound = "";
	}
	
	public void moveVertical (string collider, float direction){			
		sensors[collider].moveVertical (direction);
	}
	
	public void moveHorizontal (string collider, float direction){
		sensors[collider].moveHorizontal (direction);
	}
		
	public string getCurrentSensors(){
		string output = "";
		foreach (string sensor in sensors.Keys){
			if (sensors[sensor].isUseable){
				output += " "+sensor;
			}
		}
		output += " bound "+isBound;
		return output;
	}
	
	public string sensorActive(string name){
		foreach (BoBot_ActionColliderGeneric sensor in sensors.Values){
			if ( (isBound.Equals("") || isBound.Equals(name)) && sensor.sensorValueGroup.Equals(name) && sensor.isUseable){
				return sensor.sensorValue;	
			}
		}		
		return null;
	}
}
