using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class BoBot_MainCollider : MonoBehaviour {
	
	
	private Dictionary<string, BoBot_ActionColliderGeneric> sensors = new Dictionary<string, BoBot_ActionColliderGeneric>();
	private BoBot_DebugComponent debugInfo;
	
	private GameObject visual;
		
	void Start () {		
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		visual = GameObject.Find("Visual");
		
			
		BoBot_ActionColliderGeneric [] colliders = gameObject.GetComponents<BoBot_ActionColliderGeneric>();
		foreach (BoBot_ActionColliderGeneric collider in colliders){
			sensors.Add(collider.sensorValue, collider);	
			Debug.Log (collider.sensorValueGroup+"   "+collider.sensorValue);
		}
		
		
		//Physics.IgnoreLayerCollision(10, 0);
		Physics.IgnoreLayerCollision(10, 1);
		Physics.IgnoreLayerCollision(10, 2);
		Physics.IgnoreLayerCollision(10, 3);
		Physics.IgnoreLayerCollision(10, 4);
		Physics.IgnoreLayerCollision(10, 5);
		Physics.IgnoreLayerCollision(10, 6);
		Physics.IgnoreLayerCollision(10, 7);
		Physics.IgnoreLayerCollision(10, 8);
	}
	
	// Update is called once per frame
	
	void Update (){
		
		
		if (BoBotGlobal.debugging && debugInfo){			
			foreach (string sensor in sensors.Keys){
				debugInfo.addText("> "+sensor+" active: "+ sensors[sensor].isUseable);
			}
		}
		
		foreach (BoBot_ActionColliderGeneric sensor in sensors.Values){	
			sensor.updateState();
			/*
			
			*/
		}	
	}
	
	void FixedUpdate(){
		//Dictionary<string, boBot_ActionColliderGeneric> sensorsTemp = new Dictionary<string, boBot_ActionColliderGeneric>();
		
		
			
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
		
		/*GameObject newObject = null;		
		try { newObject = other.transform.parent.gameObject; }
		catch (Exception e){ }		
		if (newObject == null){	newObject = other.gameObject; }	
		*/		
		foreach ( BoBot_ActionColliderGeneric sensor in sensors.Values){			
			Vector2 relDist = newDist;
			//relDist.x = Mathf.Clamp (relDist.x * Mathf.Abs( BoBotGlobal.input_horizontalDirection), 0.1f, 1f);
			try {
				if (other.tag != "Untagged"){
					visual.transform.position = mainCollider.transform.position + otherDistance;
					sensor.check(newDist, other, relDist);
				}
			}
			catch {
			}
		}			
	}
	
	public void bind (string name){
		Debug.Log ("tryBind "+name);
		sensors[name].bind();
	}
	
	public void release (string name){
		sensors[name].release();	
	}
	
	public void moveVertical (string collider, float direction){			
		sensors[collider].moveVertical (direction);
	}
	
	public void moveHorizontal (string collider, float direction){
		sensors[collider].moveHorizontal (direction);
	}
	
	/*public void bind(string collider){
		Debug.Log ("BIND "+ currentColliderObject.name);
		//currentCollider.bind (ref currentColliderObject);	
		//sensors[collider].bind (ref sensorsOtherCollider[collider]);
		sensors[collider].tryToBind = true;
	}
	
	public void release(string collider){
		Debug.Log ("RELEASE "+ currentColliderObject.name);
		sensors[collider].tryToRelease = true;
		//tryToBind[collider] = false;
		//sensors[collider].release(); 
		//currentCollider.release ();
	}*/
	
	public string getCurrentSensors(){
		string output = "";
		foreach (string sensor in sensors.Keys){
			if (sensors[sensor].isUseable){
				output += " "+sensor;
			}
		}
		return output;
	}
	
	public string sensorActive(string name){
		foreach (BoBot_ActionColliderGeneric sensor in sensors.Values){
			if (sensor.sensorValueGroup.Equals(name) && sensor.isUseable){
				return sensor.sensorValue;	
			}
		}
		
		/*if (sensors.ContainsKey(name) && sensors[name].isUseable){
			return name;	
		}*/
		return null;
		//return sensors[name].isUseable();
	}
	
	//public Dictionary <string, BoBot_SensorCollider> getActiveSensors(){
	//	return BoBotGlobal.sensor_active;
	//}
}
