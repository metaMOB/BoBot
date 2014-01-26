using UnityEngine;
using System.Collections;

public class BoBot_RiddleComponent : BoBot_OnOffComponent {
	
	private ParticleSystem [] emitters;
	private BoBot_SmoothFollow2D cameraComponent;
	
	private float timer = 0f;
	public float attackTime = 5f;
	public float holdTime = 5f;
	public float releaseTime = 5f;
	public float intensity = 1f;
	public int riddleNr = 1;
	public AudioClip earthQuakeSound;
	
	private BoBot_DebugComponent debugInfo;
	
	// Use this for initialization
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		emitters = gameObject.GetComponentsInChildren<ParticleSystem>();
		cameraComponent = GameObject.Find("Main Camera").GetComponent<BoBot_SmoothFollow2D>();
		//gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("RiddleComponent");
			debugInfo.addText ("> Active "+isRunning);
			debugInfo.addText ("> Timer "+timer);
			debugInfo.addText ("> A/H/R "+attackTime+"/"+holdTime+"/"+releaseTime);
		}
		
		if (!isRunning && state){
			isRunning = true;
			timer = 0f;
			cameraComponent.shakeItBaby(0f, intensity, attackTime, earthQuakeSound);
			foreach (ParticleSystem emitter in emitters){
				emitter.Play();		
			}
		}
		
		if (isRunning){
			timer += Time.fixedDeltaTime;	
			
			if (timer > (attackTime + holdTime)){
				foreach (ParticleSystem emitter in emitters){
					emitter.Play();		
					
				}
				cameraComponent.shakeItBaby(intensity, 0f, releaseTime, earthQuakeSound);
				foreach (ParticleSystem emitter in emitters){
					emitter.Stop();		
				}
				isRunning = false;
			}
		}
	}
	
	override public void on(){
		if (!isRunning){
			isRunning = true;
			timer = 0f;
			cameraComponent.shakeItBaby(0f, intensity, attackTime, earthQuakeSound);
			foreach (ParticleSystem emitter in emitters){
				emitter.Play();		
			}
		}
	}
}
