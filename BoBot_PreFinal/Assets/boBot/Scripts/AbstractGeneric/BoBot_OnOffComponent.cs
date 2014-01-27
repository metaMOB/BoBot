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
		/*
		this.valDeltaTwo = (this.val - this.valOld) - this.valDelta;
		this.valDelta = val - valOld;
		this.valOld = val;
		//Debug.Log ("--- "+this.val+" ### "+this.valDelta + " --- "+this.valDeltaTwo);
		
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
		catch{}	*/
	}
	
	public void setAudio(AudioClip next, bool loop){
		nextClip = next;
		loopNextClip = loop;
	}
	
	public virtual void on(){
	}
	
	public virtual void off(){
	}
	
	override public void setValue (float newVal, int channel){
		if (this.channel == channel){
			
			this.val = Mathf.Round(newVal * 10f)/10f ;
			
			
			
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
