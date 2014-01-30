using UnityEngine;
using System.Collections;

public class BoBot_MoveableObject_HitDetector : MonoBehaviour {
	void OnTriggerEnter (Collider other){
		if (other.name.Equals("HitMarker") && other.collider.CompareTag("Player")) {
			transform.parent.GetComponent<BoBot_MoveableObject>().registerNewJump();
		}
	}
}
