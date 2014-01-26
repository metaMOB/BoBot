using UnityEngine;
using System.Collections;

public class BoBot_InputTouch : MonoBehaviour {
	
	Transform thumbPad;
	Transform thumbPadFrame;
	
	GameObject objThumbPad;
	GameObject objThumbPadFrame;
	
	Vector3 pos = Vector3.zero;
	//float tolleranceUp = 1.1f;
	//float tolleranceDown = 0.9f;
	
	//float maxDistance = 0.0025f;
	
	//bool isTouch = false;
	float touchTime = 0f;
	Vector3 movement;
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void Update() {	
		float deltaTime = Time.deltaTime;
				
		foreach (Touch touch in Input.touches) {
			touchTime += deltaTime;
			pos.x = touch.position.x / Screen.width;
			pos.y = touch.position.y / Screen.height;
			
			Debug.Log (pos.x);
			
			if (pos.x <= 0.25f){
				Debug.Log ("jj");
				BoBotGlobal.input_horizontalDirection = Mathf.Min ( 0f, Mathf.Max ( -1f, BoBotGlobal.input_horizontalDirection-Time.deltaTime));
			} 
			
			if (pos.x >= 0.75f){
				BoBotGlobal.input_horizontalDirection = Mathf.Min ( 1f, Mathf.Max ( 0f, BoBotGlobal.input_horizontalDirection+Time.deltaTime));
			}
			
			if (pos.y >= 0.75f){
				BoBotGlobal.input_verticalDirection = Mathf.Min ( 1f, Mathf.Max ( 0f, BoBotGlobal.input_verticalDirection+Time.deltaTime));
			}
			
			if (pos.x > 0.25f && pos.x < 0.75f && pos.y > 0.25f && pos.y < 0.75){
				BoBotGlobal.input_action = Mathf.Min ( 1f, Mathf.Max ( 0f, BoBotGlobal.input_action+deltaTime));
			}		
		} 
			
		if (Input.touchCount == 0){
			float absDirection = Mathf.Abs(BoBotGlobal.input_horizontalDirection); 
			if ( BoBotGlobal.input_horizontalDirection != 0f && absDirection <= BoBotGlobal.time_timeUntilShortInput){
				BoBotGlobal.input_horizontalDirectionShort = BoBotGlobal.input_horizontalDirection / absDirection;
			} else {
				BoBotGlobal.input_horizontalDirectionShort = 0f;	
			}
			
			absDirection = Mathf.Abs(BoBotGlobal.input_verticalDirection); 
			if ( BoBotGlobal.input_verticalDirection != 0f && absDirection <= BoBotGlobal.time_timeUntilShortInput){
				BoBotGlobal.input_verticalDirectionShort = BoBotGlobal.input_verticalDirection / absDirection;
			} else {
				BoBotGlobal.input_verticalDirectionShort = 0f;	
			}
			
			BoBotGlobal.input_horizontalDirection = 0f;
			BoBotGlobal.input_verticalDirection = 0f;
			BoBotGlobal.input_action = 0f;
			
		}
	}	
}
