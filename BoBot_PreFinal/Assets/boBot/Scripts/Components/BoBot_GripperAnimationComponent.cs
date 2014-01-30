using UnityEngine;
using System.Collections;

public class BoBot_GripperAnimationComponent : BoBot_OnOffComponent {
	
	public Transform leftSide;
	public Transform rightSide;
	
	public Rigidbody objectToGrap;
	private Transform orgParent;
	
	private float angle = 0;
	private int directionGrap = 0;
	private bool grapping = true;
	
	public AudioClip openSound;	
	public AudioClip closeSound;
	
	private Vector3 offset;
	private BoBot_DebugComponent debugInfo;
	
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		objectToGrap.useGravity = false;
		objectToGrap.isKinematic = true;
		
		offset = objectToGrap.transform.position - this.transform.position;		
		orgParent = objectToGrap.transform.parent;
		objectToGrap.transform.parent = this.transform.parent;
	}	
	
	void Update () {
		if (!state){
			this.directionGrap = -1;
		} else {
			this.directionGrap = 1;	
		}
		
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("GripperAnimationComponent");
			debugInfo.addText ("> Grapping "+grapping);
			debugInfo.addText ("> Angle "+angle);
			debugInfo.addText ("> Direction "+directionGrap);
		}
		
		if (grapping && angle > 5){
			grapping = false;
			objectToGrap.useGravity = true;
			objectToGrap.isKinematic = false;
			objectToGrap.transform.parent = orgParent; 
		}
		
		if (  (this.directionGrap > 0 && angle <=10) || (this.directionGrap < 0 && angle >=0)   ){
			leftSide.transform.Rotate( Vector3.back * 0.4f * this.directionGrap);
			rightSide.transform.Rotate( Vector3.forward * 0.4f * this.directionGrap);
			angle += 0.4f * this.directionGrap;						
		} else {
			directionGrap = 0;	
		}
	}
	
	public override void on(){
		Debug.Log ("grip ON");
		this.directionGrap = 1;
	}
	
	public override void off(){
		Debug.Log ("grip Off");
		this.directionGrap = -1;
	}	
}
