using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_PulsingComponent : BoBot_OnOffComponent {

	// Use this for initialization
	private float fadePulseVelocity;
	private float actualValue;
	private float targetVal;
	private List<Material> colors = new List<Material>();
	private Color clr;
	
	public float frequency = 1f;
	
	void Start () {
		actualValue = 0;
		targetVal = 1;
		clr = renderer.material.color;
		clr.a = actualValue;
		renderer.material.color = clr;
		frequency = 1f/frequency;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (state){
			actualValue = Mathf.SmoothDamp(actualValue, targetVal, ref fadePulseVelocity, frequency/2f);
			//Debug.Log ("ac "+actualValue+"  "+targetVal);
			clr.a = actualValue;
			renderer.material.color = clr;
			
			if ( Mathf.Abs (actualValue - targetVal) < 0.01f){
				targetVal = Mathf.Abs (targetVal - 1);
			}	
		} else if (actualValue > 0) {
			actualValue = Mathf.SmoothDamp(actualValue, 0, ref fadePulseVelocity, frequency/2f);
			clr.a = actualValue;
			renderer.material.color = clr;
		}
	}
}
