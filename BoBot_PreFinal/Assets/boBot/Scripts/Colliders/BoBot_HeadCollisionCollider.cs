using UnityEngine;
using System.Collections;

public class BoBot_HeadCollisionCollider : MonoBehaviour {
	
	private int numFound;
	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision(8, 0, false);
	}
	
	// Update is called once per frame
	void Update () {		
		BoBotGlobal.animator.SetBool("collisionWall", numFound > 0);	
		numFound = 0;
	}
			
	void OnTriggerStay (Collider other){
		//Debug.Log ("hallo "+other.name);	
		numFound++;
	}
}
