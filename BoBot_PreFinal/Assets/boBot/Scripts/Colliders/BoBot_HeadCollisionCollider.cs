using UnityEngine;
using System.Collections;

public class BoBot_HeadCollisionCollider : MonoBehaviour {
	
	private int numFound;
	
	void Start () {
		Physics.IgnoreLayerCollision(8, 0, false);
	}
	
	void Update () {		
		BoBotGlobal.animator.SetBool("collisionWall", numFound > 0);	
		numFound = 0;
	}
			
	void OnTriggerStay (Collider other){
		numFound++;
	}
}
