using UnityEngine;
using System.Collections;

public class BoBot_MoveableObject_HitDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	/*	Physics.IgnoreLayerCollision(11, 1);
		Physics.IgnoreLayerCollision(11, 8);
		Physics.IgnoreLayerCollision(11, 9);
		Physics.IgnoreLayerCollision(11, 10);*/
	}
	
	void OnTriggerEnter (Collider other){
		if (other.name.Equals("HitMarker") && other.collider.CompareTag("Player")) {
			transform.parent.GetComponent<BoBot_MoveableObject>().registerNewJump();
		}
	}
}
