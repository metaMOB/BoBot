using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoBot_SmoothFollow2D : MonoBehaviour {

public Transform target;
public Camera [] backgroundCameras;
//public List<Transform> backgroundCamerasTransform = new List<Camera>();
	
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
	/*
		void calcBackgroundPosition(){
		
		//Debug.Log ("pos "+this.levelBoundingBox.transform.localScale.x +" chr: "+this.BoBotGlobal.character.transform.position.x);
		float diffX = (this.mainCamera.transform.position.x  - this.levelWidth / 2) / this.levelWidth;
		float diffY = (this.mainCamera.transform.position.y  - this.levelHeight / 2) / this.levelHeight;
						
		this.backgroundPosition = this.mainCamera.transform.position;
		this.backgroundPosition.x -= backgroundBounds.extents.x /2 * diffX;
		this.backgroundPosition.y -= backgroundBounds.extents.y /2 * diffY;
		this.backgroundPosition.z = 20;
		this.background.transform.position = this.backgroundPosition;
		
	}*/
	
	void Update() 
	{
		Vector3 valuesPosition = new Vector3();
		if (isRunning){
			timer += Time.fixedDeltaTime;
		   /* Quaternion valuesRotation = new Quaternion();
			valuesRotation.y = Mathf.SmoothDamp( thisTransform.rotation.y, 
				angleY * bog * .45f, ref velocityAngle.y, smoothTime);
			
			valuesRotation.x = Mathf.SmoothDamp( thisTransform.rotation.x, 
				angleX * bog * .45f, ref velocityAngle.x, smoothTime);
			
			valuesRotation.z = thisTransform.rotation.z;
			
		    thisTransform.rotation = valuesRotation;*/
				
				//thisTransform.rotation.y = Mathf.PI / 4;
				
			
			float smoothIntensity = Mathf.Lerp(shakeIntensityFrom, shakeIntensityTo, timer / shakeDuration);
			
			shaker = new Vector3(Random.Range(-2.0F, 2.0F), Random.Range(-1.0F, 1.0F), 0) * smoothIntensity;
			earthQuakeSound.volume = (smoothIntensity / range)*10;
			
			if (smoothIntensity == 0f){
				isRunning = false;
			}
		}
		
		
		/*valuesPosition.x = Mathf.SmoothDamp( thisTransform.position.x, 
			target.position.x - Mathf.Sin (angleY * bog) * distance , ref velocity.x, smoothTime);
		valuesPosition.y = Mathf.SmoothDamp( thisTransform.position.y, 
			target.position.y  + BoBotGlobal.distance_cameraYOffset + Mathf.Sin (angleX * bog) * distance, ref velocity.y, smoothTime);
		valuesPosition.z = Mathf.SmoothDamp( thisTransform.position.z, 
			target.position.z - Mathf.Cos (angleY * bog) * distance + Mathf.Sin (angleX * bog) * distance , ref velocity.z, smoothTime);*/
		
		valuesPosition.x = Mathf.SmoothDamp( thisTransform.position.x, target.position.x + shaker.x, ref velocity.x, smoothTime);
		valuesPosition.y = Mathf.SmoothDamp( thisTransform.position.y, target.position.y + shaker.y, ref velocity.y, smoothTime);
		valuesPosition.z = thisTransform.position.z;		
		thisTransform.position = valuesPosition;
		
		
		float orthSize = Mathf.SmoothDamp( thisCamera.orthographicSize, targetZoom, ref zoomVelocity, zoomDuration);
		thisCamera.orthographicSize = orthSize;
		//foreach (Camera backgroundCamera in backgroundCameras){
		for (int i = 0; i < backgroundCameras.Length; i++){
			
			//Vector3 pos = backgroundCamera.transform.position;
			//pos.x = valuesPosition.x;
			//pos.y = valuesPosition.y;
			//backgroundCamera.transform.position = pos;
			backgroundCameras[i].orthographicSize = orthSize + i;
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
	
	/*public void setNewPosition (float x, float y, float d)
	{
		oldDistance = distance;
		oldAngleX = angleX;
		oldAngleY = angleY;
		
		distance = d;
		angleX = x;
		angleY = y;
	}
	
	public void restoreOldPosition ()
	{
		distance = oldDistance;
		angleX = oldAngleX;
		angleY = oldAngleY;
	}*/
}
