using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BoBot_ControlComponent : MonoBehaviour {	
			
	protected bool isRunning;
	public float val;
	public bool state = false;
	public int channel = 1;
	
	public virtual void setValue(float value, int channel){
		//Debug.Log ("or "+val);
		//this.val = value;
		//this.isRunning = value != 0f;
	}	
}
