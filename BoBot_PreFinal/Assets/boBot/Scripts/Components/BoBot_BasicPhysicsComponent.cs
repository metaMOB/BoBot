using UnityEngine;
using System.Collections;

public class BoBot_BasicPhysicsComponent : MonoBehaviour {
	public Vector3 lastPos = Vector3.zero;
	public Vector3 lastDelta = Vector3.zero;
	public Vector3 delta = Vector3.zero; 
	public Vector3 deltaTwo = Vector3.zero; 	
	public float maximumVelocity = 0f;
	
	private Transform rigid;
	private BoBot_DebugComponent debugInfo;

	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		if (gameObject.rigidbody != null){
			rigid = gameObject.rigidbody.transform;
		} else {
			rigid = this.transform;	
		}
		lastPos = rigid.position;
	}
	
	void Update () {
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("BasicPhysicsComponent");
			debugInfo.addText ("> Delta  "+(delta.x).ToString("#0.00")+ "/"+(delta.y).ToString("#0.00"));
			debugInfo.addText ("> Delta2 "+(deltaTwo.x).ToString("#0.00")+ "/"+(deltaTwo.y).ToString("#0.00"));
		}
	}
	
	void FixedUpdate(){
		if (maximumVelocity > 0f){
			rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maximumVelocity);	
		}		
	}
	
	void LateUpdate () {
		Vector3 tempPos = rigid.position;
		
		delta = ( tempPos - lastPos);
		deltaTwo = (delta - lastDelta);
		
		lastPos = tempPos;
		lastDelta = delta;	
	}
	
	private Vector3 RoundToSignificantDigits(Vector3 d, int digits){
		float dig = 10*digits;
    	d.x = Mathf.RoundToInt( d.x*dig )/dig;
		d.y = Mathf.RoundToInt( d.y*dig )/dig;
		d.z = Mathf.RoundToInt( d.z*dig )/dig;
		return d;
	}
}
