using UnityEngine;
using System.Collections;

public class BoBot_CameraPositioner : MonoBehaviour {

	Transform target;
	int angleX;
	int angleY;
	int distance;
	
	private Transform thisTransform;	
	
	void Start () {
		thisTransform = target;
	}		
}
