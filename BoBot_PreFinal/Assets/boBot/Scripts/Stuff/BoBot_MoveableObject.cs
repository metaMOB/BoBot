using UnityEngine;
using System.Collections;

public class BoBot_MoveableObject : MonoBehaviour {

	public Transform jointToDeleteAtHitA;
	public Transform jointToDeleteAtHitB;
	public int numberOfJumpsTillRemove = 1;
	private int numberOfJumps = 0;
	public bool actAsParent = false;
	public bool active = false;
	public bool recievePlayerForce = false;
	public bool canBeCarried = false;
	
	void Start () {
		active=!(jointToDeleteAtHitA == null && jointToDeleteAtHitB == null);		
			
		if (GetComponent<CharacterJoint>().connectedBody == null){
			Destroy(GetComponent<CharacterJoint>());
		}
		
		if (!canBeCarried){
			transform.tag ="";
			gameObject.layer = 0;
		} else {
			transform.tag ="canCarry";
			gameObject.layer = 11;
		}
	}
	
	public void registerNewJump (){
		numberOfJumps++;
		Debug.Log ("jumps "+numberOfJumps);
		if (active && numberOfJumps >= numberOfJumpsTillRemove){
			actAsParent = false;
			if (jointToDeleteAtHitA){
				jointToDeleteAtHitA.rigidbody.useGravity = true;
				Destroy(jointToDeleteAtHitA.GetComponent<CharacterJoint>());		
			}
			
			if (jointToDeleteAtHitB){	
				jointToDeleteAtHitA.rigidbody.useGravity = true;
				Destroy(jointToDeleteAtHitB.GetComponent<CharacterJoint>());
			}
			transform.tag ="canCarry";
			gameObject.layer = 11;
			recievePlayerForce = false;
			active = false;
			Destroy(GetComponent<CharacterJoint>());	
		}
	}	
}
