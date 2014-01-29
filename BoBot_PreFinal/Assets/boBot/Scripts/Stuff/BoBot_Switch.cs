using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoBot_Switch : MonoBehaviour {
	
	public enum mode {onOff, analog};
	public mode functionality;
	public int channel = 1;
	
	public float timeTillOff = 5f;
	public float timeAtPosition = 1;
	public GameObject [] control = null;	
		
	private List<BoBot_ControlComponent> controlComponents = new List<BoBot_ControlComponent>();
	
	private float angle = 90;
	private int direction = 1;
	private int angleMinMax = 45;
	
	private float anglePerSec;
	private Vector3 rotPoint;
	private Vector3 axis = new Vector3 (0,0,1);
	
	private bool active = false;
	private float onTime = 0;
	private float tollerance = 5;
	
	private bool manualInteraction = false;
	private float lastValue = 0;
	private BoBot_RiddleComponent riddle;
	
	private BoBot_DebugComponent debugInfo;
	
	// Use this for initialization
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		anglePerSec = angleMinMax / timeTillOff ;
		rotPoint = this.gameObject.transform.position;
		
		GameObject currObj = this.gameObject;
		while (currObj.GetComponent<BoBot_RiddleComponent>() == null){
			//if (currObj.transform.parent && currObj.transform.parent.gameObject){
				currObj = currObj.transform.parent.gameObject;	
			//}
		}
		
		foreach (GameObject obj in control){
			BoBot_ControlComponent [] cmps = obj.GetComponents<BoBot_ControlComponent>();
			foreach (BoBot_ControlComponent cmp in cmps){
				controlComponents.Add(cmp);
			}			
			
			/*
			cmps = obj.GetComponentsInChildren<BoBot_ControlComponent>();
			foreach (BoBot_ControlComponent cmp in cmps){
				controlComponents.Add(cmp);
			}*/		
		}
				
		riddle = currObj.GetComponent<BoBot_RiddleComponent>();
		//angle = -45;
		//this.gameObject.transform.FindChild("Hebel").transform.RotateAround (rotPoint, axis, -45); 
	}
	
	public Vector3 moveSwitch (float direction){
		manualInteraction = true;
		onTime = 0;
		float newAngle = anglePerSec * Time.deltaTime * direction;
		float possibleAngle = angle + newAngle;
			
		Vector3 old = this.gameObject.transform.FindChild("Hebel").transform.position;
		
		if (functionality == mode.analog || direction < 0){
			if (control.Length > 0){
				if (possibleAngle >= 90 - angleMinMax && possibleAngle <= 90 + angleMinMax){
					angle = possibleAngle;
					this.gameObject.transform.FindChild("Hebel").transform.RotateAround (rotPoint, axis, newAngle);	
					float dir = (90 - angle)/angleMinMax;
					
					foreach (BoBot_ControlComponent animate in controlComponents){						
						animate.setValue(dir, channel);
					}
					//Debug.Log ("angel "+angle);
				}
			}
	//		Debug.Log (" " + this.gameObject.transform.FindChild("Hebel").collider.transform.localPosition.x+ "  " );
			
		}
		return this.gameObject.transform.FindChild("Hebel").transform.position - old;
	}
	
	public Vector3 getPos(){
		return this.gameObject.transform.FindChild("Hebel").transform.FindChild("Griff").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("Switch");
			debugInfo.addText ("> Control# "+control.Length );
			debugInfo.addText ("> Value "+(90 - angle)/angleMinMax);
		}
		
		if (Input.GetKey (KeyCode.Y) )
		{ 
			moveSwitch (1);
		}
		
		if (Input.GetKey (KeyCode.X) )
		{ 
			moveSwitch (-1);
		}
		
		if (control.Length > 0){
			if ( (angle < 89 || angle > 91) && !manualInteraction){
				foreach (BoBot_ControlComponent animate in controlComponents){
						animate.setValue((90 - angle)/angleMinMax, channel);
					}
				
				if (onTime <= timeAtPosition){
					onTime += Time.deltaTime;	
				} else {				
					float dir = 90 - angle;
					dir = Mathf.Abs(dir)/dir;
					
					float newAngle = anglePerSec * Time.deltaTime * dir;
					angle += newAngle;
					this.gameObject.transform.FindChild("Hebel").transform.RotateAround (rotPoint, axis, newAngle);			
				}
			}
			float actValue =  (float)Math.Round((90 - angle) / angleMinMax, 1, MidpointRounding.ToEven);
			
			if (lastValue != actValue){
				lastValue = actValue;
			}
		}
		manualInteraction = false;
	}
}
