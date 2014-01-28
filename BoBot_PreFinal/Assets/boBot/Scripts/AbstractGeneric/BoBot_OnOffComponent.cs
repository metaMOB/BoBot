using UnityEngine;
using System.Collections;

public class BoBot_OnOffComponent : BoBot_ControlComponent {
	
	public float gateLower = 0.25f;
	public float gateUpper = 0.75f;
	public bool startRunning;
	public bool manualControl;
	
	
	public AudioClip audioOn;
	public AudioClip audioOnLoop;
	public AudioClip audioOff;
	public AudioClip audioOffLoop;
	
	private AudioSource loopSound;
	private AudioSource changeSound;
	
	public bool isReady = false;
	
	//private AudioClip currentClip;
	//private AudioClip nextClip;
	//private bool loopNextClip;
	//private AudioSource audioSource;
	private float valOld = 0;
	
	// Use this for initialization
	void Awake () {
		this.state = startRunning;	
		loopSound = this.gameObject.AddComponent<AudioSource>();
		changeSound = this.gameObject.AddComponent<AudioSource>();
		//audioSource = gameObject.AddComponent<AudioSource>();		
		//audioSource.Play();
	}
	
	void LateUpdate(){
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
					if (this.state = startRunning){
						changeSound.clip = audioOn;
						changeSound.loop = false;
						changeSound.Play();
					}
					this.state = !startRunning;
					
				} else if (this.val < gateLower){	
					if (this.state != startRunning){
						changeSound.clip = audioOff;
						changeSound.loop = false;
						changeSound.Play();
					}
					this.state = startRunning;		
					
				}
			}
		}
	}
}
