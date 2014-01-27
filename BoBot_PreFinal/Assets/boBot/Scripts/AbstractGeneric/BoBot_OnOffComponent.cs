using UnityEngine;
using System.Collections;

public class BoBot_OnOffComponent : BoBot_ControlComponent {
	
	public float gateLower = 0.25f;
	public float gateUpper = 0.75f;
	public bool startRunning;
	public bool manualControl;
	
	public bool isReady = false;
	
	private AudioClip currentClip;
	private AudioClip nextClip;
	private bool loopNextClip;
	private AudioSource audioSource;
	private float valOld = 0;
	
	
	// Use this for initialization
	void Awake () {
		this.state = startRunning;		
		audioSource = gameObject.AddComponent<AudioSource>();		
		audioSource.Play();
	}
	
	// Update is called once per frame
	void LateUpdate () {				
		if (currentClip != nextClip){
			if (audioSource.isPlaying){
				audioSource.loop = false;
			} else {
				audioSource.clip = nextClip;
				currentClip = nextClip;
				audioSource.loop = loopNextClip;
				audioSource.Play();
			}
		}
		try {
			Debug.Log ("Sound "+currentClip.name+"   "+nextClip.name);
		}
		catch{}	
	}
	
	public void setAudio(AudioClip next, bool loop){
		nextClip = nextClip;
		loopNextClip = loop;
	}
	
	public virtual void on(){
	}
	
	public virtual void off(){
	}
	
	override public void setValue (float newVal, int channel){
		if (this.channel == channel){
			float delta = newVal - this.val;
			this.valDeltaTwo = delta - this.valDeltaTwo;
			this.valDelta = delta;
			this.val = newVal;	
			
			Debug.Log ("--- "+val+" ### "+valDelta + " --- "+valDeltaTwo);
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
