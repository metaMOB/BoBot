using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
public class BoBot_CameraZoomComponent : MonoBehaviour {
	
	private float isTime = 0f;
	private float duration;
	private Camera thisCamera;
	private float originalZoomLevel;
	private float newZoomLevel;
	private bool isRunning = false;	
	private BoBot_DebugComponent debugInfo;

	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		thisCamera = this.GetComponent<Camera>();
	}
	
	void Update () {
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("CameraZoomComponent");
			debugInfo.addText ("> Active "+isRunning);
			debugInfo.addText ("> Zoom "+thisCamera.orthographicSize);
			debugInfo.addText ("> From "+this.originalZoomLevel);
			debugInfo.addText ("> To "+this.newZoomLevel);
			debugInfo.addText ("> Duaration "+this.duration);
		}
		
		if (isRunning){
			if (this.isTime <= this.duration){
				this.isTime += Time.deltaTime;
				thisCamera.orthographicSize = Mathf.SmoothStep (originalZoomLevel, newZoomLevel, isTime / duration);
			} else {
				this.isRunning = false;	
			}
		}
	}
	
	public void setZoomLevel (float zoom, float duration){
		this.originalZoomLevel = thisCamera.orthographicSize;
		this.newZoomLevel = zoom;
		this.duration = duration;
		this.isTime = 0f;
		this.isRunning = true;		
	}
}
