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
	
	
	public float volume = 0.5f;
	public float fadeInOutTime = 1f;
	
	private AudioSource loopSound;
	private AudioSource changeSound;
	private float fadeVelocity;
	
	public bool isReady = false;
	
	//private AudioClip currentClip;
	//private AudioClip nextClip;
	//private bool loopNextClip;
	//private AudioSource audioSource;
	private float valOld = 0;
	private bool playLoopSound = false;
	
	// Use this for initialization
	void Awake () {
		this.state = startRunning;	
		loopSound = this.gameObject.AddComponent<AudioSource>();
		changeSound = this.gameObject.AddComponent<AudioSource>();
		loopSound.volume = volume;
		changeSound.volume = volume;
		loopSound.minDistance = 1f;
		changeSound.minDistance = 1f;
		loopSound.maxDistance = 10f;
		changeSound.maxDistance = 10f;
		
		loopSound.playOnAwake = false;
		changeSound.playOnAwake = false;
		
		if (!audioOn){
			loopSound.clip = audioOnLoop;
			loopSound.loop = true;
		}
		//audioSource = gameObject.AddComponent<AudioSource>();		
		//audioSource.Play();
	}
	
	void LateUpdate(){
		if (!playLoopSound){
			if (loopSound.volume > 0){
				loopSound.volume = Mathf.SmoothDamp(loopSound.volume, 0, ref fadeVelocity, fadeInOutTime);
			} else {
				loopSound.Stop();								
			}
		} else {
			if (!loopSound.isPlaying){
				loopSound.Play();	
				fadeVelocity = 0f;
				loopSound.volume = 0f;
			} else {
				loopSound.volume = Mathf.SmoothDamp(loopSound.volume, volume, ref fadeVelocity, fadeInOutTime);
			}	
		}
	}
		
	public virtual void on(){
	}
	
	public virtual void off(){
	}
	
	override public void setValue (float newVal, int channel){
		if (this.channel == channel){			
			this.val = Mathf.Round(newVal * 10f)/10f ;
			if ( Mathf.Abs(this.val) <= 0.1f){
				this.val = 0;
			}
			if (!manualControl){
				if (this.val > gateUpper){	
					if (this.state = !startRunning){						
						if (audioOnLoop){
							loopSound.clip = audioOnLoop;
							loopSound.loop = true;
						}
							
						if (audioOn){
							loopSound.PlayDelayed(audioOn.length);
							changeSound.clip = audioOn;
							changeSound.loop = false;
							changeSound.Play();
						} else {
							playLoopSound = true;							
						}
					}
					this.state = !startRunning;
					
				} else if (this.val < gateLower){	
					if (this.state == startRunning){
						if (audioOff){
							loopSound.Stop ();						
							changeSound.clip = audioOff;
							changeSound.loop = false;
							changeSound.Play();
						} else {
							playLoopSound = false;							
						}
					}
					this.state = startRunning;							
				}
			} else {
				playLoopSound = this.val != 0;
			}
		}
	}
}
