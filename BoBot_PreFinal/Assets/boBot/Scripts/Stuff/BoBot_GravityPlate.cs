using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_GravityObject {
	
	Rigidbody rigid;
	float timer;
	
	public BoBot_GravityObject(Rigidbody rigid, float time){
		this.rigid = rigid;
		this.timer = time;
	}	
}

public class BoBot_GravityPlate : BoBot_OnOffComponent {
	
	private List<Rigidbody> bodies = new List<Rigidbody> ();
	public string objectName;
	public float factor = 4f;
	private Vector3 gravity = Physics.gravity;
	private float timer = 0f;
	private bool running = false;
	
	void Update () {
		if (this.val > this.gateUpper ){
			if (!running){
				timer = 0f;
				running = true;
			}
			
			foreach (Rigidbody body in bodies){
				body.AddForce (-gravity*body.mass*factor);
			}
		} else {
			bodies = new List<Rigidbody> ();
		}
		bodies = new List<Rigidbody> ();
	}
	
	void OnTriggerStay (Collider other){
		if (objectName == "" || other.name.Equals(objectName)){
			Rigidbody or = other.gameObject.rigidbody;
			if (or != null){
				bodies.Add (or);
			}
		}
	}
}
