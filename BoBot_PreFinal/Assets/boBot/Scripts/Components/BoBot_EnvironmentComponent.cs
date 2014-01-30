using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_EnvironmentComponent : MonoBehaviour {
	
	GameObject [] riddleObjs;	
	private float timer = 0f;
	
	private float intensity;
	private float delta;
	
	private List<AudioSource> audioSrcs;
	
	void Start () {
		riddleObjs = GameObject.FindGameObjectsWithTag("riddle");
		foreach (GameObject riddleObj in riddleObjs){
			riddleObj.SetActive(false);
			Rigidbody [] rigids = riddleObj.GetComponentsInChildren<Rigidbody>();
			foreach (Rigidbody rigid in rigids){
				rigid.Sleep();
			}
		}
	}
	
	void Update (){
		if (timer > 0){
			intensity = Mathf.SmoothDamp(intensity, 0f, ref delta, 2.5f);
			float min = 1f * intensity;
			float max = 10f * intensity;
			
			try {
				foreach (AudioSource sr in audioSrcs){
					sr.minDistance = min;
					sr.maxDistance = max;
				}
			} catch {}
			timer -= Time.deltaTime;
			
			try{
				if (timer < 0){
					foreach (AudioSource sr in audioSrcs){
					sr.mute = true;
					}
				}
			} catch {}
		}
	}
	
	public void setActiveRiddle(int nr){
		
		audioSrcs = new List<AudioSource>();
		foreach (GameObject riddleObj in riddleObjs){
			BoBot_RiddleComponent riddle = riddleObj.GetComponent<BoBot_RiddleComponent>();
			if (riddle.riddleNr != nr){
				foreach (AudioSource scr in riddle.gameObject.GetComponentsInChildren<AudioSource>()){
					//scr.minDistance = 0.1f;
					//scr.maxDistance = 1f;
					audioSrcs.Add(scr);
				} 
			} else {
				foreach (AudioSource scr in riddle.gameObject.GetComponentsInChildren<AudioSource>()){
					//scr.minDistance = 0.1f;
					//scr.maxDistance = 1f;
					scr.minDistance = 1f;
					scr.maxDistance = 10f;
					scr.mute = false;
				} 
			}
			
			if (riddle.riddleNr == nr || riddle.riddleNr == nr+1 || riddle.riddleNr == nr-1){
				riddleObj.SetActive(true);
				Rigidbody [] rigids = riddleObj.GetComponentsInChildren<Rigidbody>();
				foreach (Rigidbody rigid in rigids){
					rigid.WakeUp();
				}
			} else {
				riddleObj.SetActive(false);
				Rigidbody [] rigids = riddleObj.GetComponentsInChildren<Rigidbody>();
				foreach (Rigidbody rigid in rigids){
					rigid.Sleep();
				}			
			}
		}
		if (audioSrcs.Count > 0){
			timer = 2.5f;
			intensity = 1f;
		}
	}	
}
