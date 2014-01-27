using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BoBot_AnimationComponent : BoBot_OnOffComponent {	
	
	
	public enum mode {manual, bounce, onOff};
	
	public mode animationType;
	
	public float rotationSpeed = 0f;
	
	private bool animationState;
	protected Rigidbody rigidToAniamte;
	
	public float speed = 1f;
	
	public float actualElement = 0f;
	public AudioClip waypointSound;
	
	
	
		
		
	private float direction = 1;
	
	public List<BoBot_AnimationTag> waypoints;
	public float waitTimeAtEndings = 0f;
	
	private BoBot_AnimationTag currentWP;
	private BoBot_AnimationTag nextWP;
	
	private Transform currentWPTransform;
	private Transform nextWPTransform;
	
	private BoBot_AnimationComponent animationComponent;
	//private float actualValue = 0f;
	
	private BoBot_DebugComponent debugInfo;
	private bool ready = false;
	
	public float delayOn = 0f;
	public float delayOff = 0f;
	
	private float timer = 0f;
	private float timerEnd = 0f;
	private float active = 1;
	
	
	
	public AudioClip soundMoving;
	public AudioClip soundHalt;
		
	public void Start(){
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		rigidToAniamte = this.GetComponent<Rigidbody>();
		
		/*if (firstAnimationWaypoint != null){
			waypoints.Add(firstAnimationWaypoint);
			while (waypoints[waypoints.Count-1].nextWaypoint != null && !waypoints.Contains(waypoints[waypoints.Count-1].nextWaypoint)){
				waypoints.Add (waypoints[waypoints.Count-1].nextWaypoint);
			}
			
			currentWP = firstAnimationWaypoint;
			nextWP = currentWP.nextWaypoint;		
			currentWPTransform = currentWP.transform;
			nextWPTransform = nextWP.transform;
		}*/
		
		/*if (rigidToAniamte && waypoints.Count > 0){
			rigidToAniamte.MovePosition ( Vector3.zero);// waypoints[0].transform.position );
		}*/
		
	}
	
	void Update () {
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("AnimationComponent");
			debugInfo.addText ("> Active "+isRunning);
			debugInfo.addText ("> current WP "+currentWP);
			debugInfo.addText ("> next WP "+nextWP);
			debugInfo.addText ("> rotation spd "+rotationSpeed);
		}
		
		if (rigidToAniamte.isKinematic && ready){
			
			
			if ( rotationSpeed > 0f){
				rigidToAniamte.transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));	
			}						
			
			if (waypoints.Count > 0){
				if (animationType == mode.onOff){
					direction = 0;
										
					if (val > gateUpper && actualElement < waypoints.Count -1){
						timer += Time.deltaTime;						
						if (timer > delayOn || actualElement > 0){
							direction = 1;
						}
					} else if ( val < gateLower && actualElement > 0){		
						timer += Time.deltaTime;	
						if (timer > delayOn || actualElement < waypoints.Count -1){
							direction = -1;
						}
					}	
				} else if (animationType == mode.bounce){
					if (actualElement <= 0 || actualElement >= waypoints.Count -1){
						if (timerEnd < waitTimeAtEndings){
							timerEnd += Time.deltaTime;
							active = 0f;
						} else {
							direction = -direction;	
							timerEnd = 0f;
							active = 1f;
						}
					}
					
				} else {
					direction = val;
					Debug.Log ("delta "+valDelta+" val "+val);
					/*
					if ( -gateLower < val && val < gateLower){
						setAudio (soundHalt, false);
					} else {
						setAudio (soundMoving, false);
					}*/
				}
					
				if (actualElement < 0 || actualElement > waypoints.Count -1){
					actualElement = Mathf.RoundToInt (actualElement);
					timer = 0f;
				}		
					
				currentWPTransform = waypoints[ Mathf.FloorToInt(actualElement)].transform;
				nextWPTransform	= waypoints[ Mathf.CeilToInt(actualElement)].transform;
					
				Vector3 newPos = Vector3.Lerp(currentWPTransform.position, nextWPTransform.position, actualElement % 1);						
				rigidToAniamte.MovePosition ( newPos );				
				actualElement += Time.deltaTime * speed * direction * active;
				
			}
			
		}
			
		//	if (!manualControl){
				// * actualValue ;
		//	}
			/*
			if (firstAnimationWaypoint != null && currentWP.nextWaypoint.nextWaypoint != null){		
				if (actualElement >= 1f){
					actualElement = 0;
					currentWP = currentWP.nextWaypoint;
					nextWP = currentWP.nextWaypoint;
					
					currentWPTransform = currentWP.transform;
					nextWPTransform = nextWP.transform;
				} else 	if (actualElement < 0f){
					actualElement = 1;
					foreach (BoBot_AnimationTag waypoint in waypoints){
						if (waypoint.nextWaypoint == currentWP){
							currentWP = waypoint;
							nextWP = currentWP.nextWaypoint;
							break;
						}
					}
					
					currentWPTransform = currentWP.transform;
					nextWPTransform = nextWP.transform;
					
				}
				
				Vector3 newPos = Vector3.Lerp(currentWPTransform.position, nextWPTransform.position, actualElement);						
				rigidToAniamte.MovePosition ( newPos );
			}*/
		ready = true;
		
	}	
}
