using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_SmoothFollow2D : MonoBehaviour {

public Transform target;
public Camera [] backgroundCameras;
	
private float smoothTime = 0.3f;
	
private float zoomDuration = 1f;
private float targetZoom = 0f; 
private float originalZ;
	
private Vector3 velocity;
private float zoomVelocity;
	
private Transform thisTransform;
private Camera thisCamera;
private float shakeIntensityFrom = 0f;
private float shakeIntensityTo = 0f;
private float shakeDuration = 0f;
	
public AudioSource earthQuakeSound;
	
private float timer = 0f;
private float range;
private bool isRunning = false;
private Vector3 shaker = Vector3.zero;
	
	void Start()
	{
		thisTransform = transform;
		
		thisCamera = this.GetComponent<Camera>();
		targetZoom = thisCamera.orthographicSize;
		
		earthQuakeSound = gameObject.AddComponent<AudioSource>();
		
		thisCamera.enabled = false;
		foreach (Camera backgroundCamera in backgroundCameras){
			backgroundCamera.enabled = false;
			backgroundCamera.enabled = true;
		}
		thisCamera.enabled = false;
		thisCamera.enabled = true;
	}
		
	void Update() 
	{
		Vector3 valuesPosition = new Vector3();
		if (isRunning){
			timer += Time.fixedDeltaTime;
			
			float smoothIntensity = Mathf.Lerp(shakeIntensityFrom, shakeIntensityTo, timer / shakeDuration);
			
			shaker = new Vector3(Random.Range(-2.0F, 2.0F), Random.Range(-1.0F, 1.0F), 0) * smoothIntensity;
			earthQuakeSound.volume = (smoothIntensity / range)*10;
			
			if (smoothIntensity == 0f){
				isRunning = false;
			}
		}
			
		valuesPosition.x = Mathf.SmoothDamp( thisTransform.position.x, target.position.x + shaker.x, ref velocity.x, smoothTime);
		valuesPosition.y = Mathf.SmoothDamp( thisTransform.position.y, target.position.y + shaker.y, ref velocity.y, smoothTime);
		valuesPosition.z = thisTransform.position.z;		
		thisTransform.position = valuesPosition;
		
		
		float orthSize = Mathf.SmoothDamp( thisCamera.orthographicSize, targetZoom, ref zoomVelocity, zoomDuration);
		thisCamera.orthographicSize = orthSize;
		
		int ort = 1;
		for (int i = 0; i < backgroundCameras.Length; i++){
			backgroundCameras[i].orthographicSize = orthSize + (backgroundCameras.Length-i);
		}
	}
	
	public void setZoomLevel (float zoom, float duration){
		this.targetZoom = zoom;
		this.zoomDuration = duration;	
	}
	
	public void shakeItBaby(float intensityFrom, float intensityTo, float duration, AudioClip sound){
		if (!earthQuakeSound.isPlaying){
			earthQuakeSound.clip = sound;
			earthQuakeSound.volume = 0f;
			earthQuakeSound.loop = false;
			earthQuakeSound.Play();
		}
		
		shakeIntensityFrom = intensityFrom;
		shakeIntensityTo = intensityTo;
		shakeDuration = duration;
		range = Mathf.Max (intensityFrom, intensityTo);
		isRunning = true;
		timer = 0f;
	}
}
