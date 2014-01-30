using UnityEngine;
using System.Collections;

public class BoBot_InputTouch : MonoBehaviour {	
	Transform thumbPad;
	Transform thumbPadFrame;
	
	GameObject objThumbPad;
	GameObject objThumbPadFrame;
	
	Vector3 pos = Vector3.zero;
	
	float touchTime = 0f;
	Vector3 movement;
	
	void Start () {		
		this.enabled = Application.platform  == RuntimePlatform.IPhonePlayer || Application.platform  == RuntimePlatform.Android;
	}
	
	public void Update() {	
		float deltaTime = Time.deltaTime;
						
		foreach (Touch touch in Input.touches) {
			touchTime += deltaTime;
			pos.x = touch.position.x / Screen.width;
			pos.y = touch.position.y / Screen.height;
			
			if (pos.x <= 0.25f && pos.y <= 0.75f && pos.y >= 0.25f){
				BoBotGlobal.input_horizontalDirection = Mathf.Clamp(BoBotGlobal.input_horizontalDirection-Time.deltaTime, -1, 0);
			} 
			
			if (pos.x >= 0.75f && pos.y <= 0.75f && pos.y >= 0.25f){
				BoBotGlobal.input_horizontalDirection = Mathf.Clamp(BoBotGlobal.input_horizontalDirection+Time.deltaTime, 0, 1);
			}
			
			if (pos.y > 0.75f){
				BoBotGlobal.input_verticalDirection = Mathf.Clamp(BoBotGlobal.input_horizontalDirection+Time.deltaTime,0 , 1);
			}
			
			if (pos.x > 0.25f && pos.x < 0.75f && pos.y < 0.25){
				BoBotGlobal.input_verticalDirection = Mathf.Clamp(BoBotGlobal.input_horizontalDirection-Time.deltaTime,-1 , 0);
			}
			
			if ( (pos.x <= 0.25f && pos.y <= 0.25f) || (pos.x >= 0.75f && pos.y <= 0.25f)){
				BoBotGlobal.input_action = Mathf.Clamp(BoBotGlobal.input_horizontalDirection+Time.deltaTime,0 , 1);
			}
			
			if (pos.x > 0.25 && pos.x < 0.75f && pos.y > 0.25 && pos.y < 0.75f){
				BoBotGlobal.input_menu = Mathf.Clamp(BoBotGlobal.input_horizontalDirection+Time.deltaTime,0 , 1); 	
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
			BoBotGlobal.input_menu = 0f;			
		}
	}	
}
