using UnityEngine;
using System.Collections;

public class BoBot_OnOffComponent : BoBot_ControlComponent {
	
	public float gateLower = 0.25f;
	public float gateUpper = 0.75f;
	public bool startRunning;
	public bool manualControl;
	
	public AudioClip onSound;
	public AudioClip offSound;
	
	public AudioClip onToOffSound;
	public AudioClip OffToOnSound;
	
	public bool isReady = false;
	
	
	// Use this for initialization
	void Awake () {
		this.state = startRunning;		
	}
	
	// Update is called once per frame
	void Update () {		
	}
	
	public virtual void on(){
	}
	
	public virtual void off(){
	}
	
	override public void setValue (float val, int channel){
		if (this.channel == channel){
			this.val = val;		
			if (!manualControl){
				if (this.val > gateUpper){		
					this.state = !startRunning;
				} else if (this.val < gateLower){		
					this.state = startRunning;		
				}
			}
		}
	}
}
