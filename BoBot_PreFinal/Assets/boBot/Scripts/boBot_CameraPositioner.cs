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
	
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider c)
	{
	
		//if (c.CompareTag("Player"))
			//thisTransform.rotation.x = 90.0;
		//	thisTransform.GetComponent<boBot_SmoothFollow2D>().setNewPosition (angleX, angleY, distance);
	}
	
	void OnTriggerExit (Collider c)
	{
		//if (c.CompareTag("Player"))
		//Debug.Log("hu");
			//thisTransform.rotation.x = 10.0;
		//	thisTransform.GetComponent<boBot_SmoothFollow2D>().restoreOldPosition ();
	}
}
