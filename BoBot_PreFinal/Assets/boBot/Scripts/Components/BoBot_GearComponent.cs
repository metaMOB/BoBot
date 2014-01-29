using UnityEngine;
using System.Collections;

public class BoBot_GearComponent : BoBot_OnOffComponent {
	
	public float rpm = 1f;
	public bool oneShot = false;
	
	public GameObject connectedGear;
	public AudioClip audioRunning;
	public AudioClip audioStop;
	public float gearVolume = 10f;
	
	private bool done = false;
	private Vector3 rotationSpeedVector;
	private Vector3 connectedRotation;
	
	private BoBot_GearComponent connGear;
	private AudioSource gearSoundRun;
	private AudioSource gearSoundStop;
	
	// Use this for initialization
	void Start () {
		rotationSpeedVector = Vector3.forward * (Mathf.PI * 2) * -rpm;
		connectedRotation = Vector3.zero;
		gearSoundRun = gameObject.AddComponent<AudioSource>();
		gearSoundRun.clip = audioRunning;
		gearSoundRun.minDistance = 0.4f;
			
		gearSoundStop = gameObject.AddComponent<AudioSource>();
		gearSoundStop.clip = audioStop;
		gearSoundStop.minDistance = 0.4f;
		
		if (connectedGear){
			connGear = connectedGear.GetComponent<BoBot_GearComponent>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (val > gateUpper/2){
			if (!gearSoundStop.isPlaying){
				gearSoundStop.loop = false;
				gearSoundStop.Play();	
			}
			
			gearSoundStop.volume = (val-gateUpper/2) * 10 * gearVolume * 3;
		} else {
			if (gearSoundStop.isPlaying){	
				gearSoundStop.Stop();
			}
		}
		
		if (!done && (this.state || this.manualControl)){			
			float factor = 0f;
			
			done = oneShot && val > gateUpper;
			
			if (!done){
				if (!gearSoundRun.isPlaying){
					gearSoundRun.loop = true;
					gearSoundRun.volume = gearVolume;
					gearSoundRun.Play();
				}
				
				factor = val;
			
			
			
				if (startRunning){
					factor = 1f - factor;
				}
		
				
				
				Vector3 rotationSpeedVectorNew = rotationSpeedVector * factor + (connectedRotation / this.transform.localScale.x);
				transform.Rotate(rotationSpeedVectorNew * Time.deltaTime);
				//transform.Rotate(connectedRotation / this.transform.localScale.x * Time.deltaTime);
				
				if (connectedGear){
					connGear.setRotation (-rotationSpeedVectorNew * this.transform.localScale.x);
				}
			}
		} else {
			gearSoundStop.Stop();	
			gearSoundRun.Stop();	
		}
	}
	
	public void setRotation (Vector3 vec){
		connectedRotation = vec;
	}
}

