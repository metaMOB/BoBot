using UnityEngine;
using System.Collections;

public class BoBot_InputKeyboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BoBotGlobal.input_horizontalDirection = 0f;
		this.enabled = Application.platform  != RuntimePlatform.IPhonePlayer && Application.platform  != RuntimePlatform.Android;
	}
	
	// Update is called once per frame
	void Update () {
		processInput();
	}
	
	public void processInput() {			
		float deltaTime = Time.deltaTime;
		if (Input.GetKey (KeyCode.LeftArrow) )
		{	
			BoBotGlobal.input_horizontalDirection = Mathf.Max (-1f, Mathf.Min ( 0f, BoBotGlobal.input_horizontalDirection-deltaTime));
		} else if (Input.GetKey (KeyCode.RightArrow) )
		{	
			BoBotGlobal.input_horizontalDirection = Mathf.Min ( 1f, Mathf.Max ( 0f, BoBotGlobal.input_horizontalDirection+deltaTime));
		} else {
			
			float absDirection = Mathf.Abs(BoBotGlobal.input_horizontalDirection); 
			if ( BoBotGlobal.input_horizontalDirection != 0f && absDirection <= BoBotGlobal.time_timeUntilShortInput){
				BoBotGlobal.input_horizontalDirectionShort = BoBotGlobal.input_horizontalDirection / absDirection;
			} else {
				BoBotGlobal.input_horizontalDirectionShort = 0f;	
			}
			
			BoBotGlobal.input_horizontalDirection = 0f;
		}
		
		if (Input.GetKey (KeyCode.UpArrow) )
		{	
			BoBotGlobal.input_verticalDirection = Mathf.Min ( 1f, Mathf.Max ( 0f, BoBotGlobal.input_verticalDirection+deltaTime));
		} else if (Input.GetKey (KeyCode.DownArrow) )
		{	
			BoBotGlobal.input_verticalDirection = Mathf.Max (-1f, Mathf.Min ( 0f, BoBotGlobal.input_verticalDirection-deltaTime));
		} else {
			float absDirection = Mathf.Abs(BoBotGlobal.input_verticalDirection); 
			if ( BoBotGlobal.input_verticalDirection != 0f && absDirection <= BoBotGlobal.time_timeUntilShortInput){
				BoBotGlobal.input_verticalDirectionShort = BoBotGlobal.input_verticalDirection / absDirection;
			} else {
				BoBotGlobal.input_verticalDirectionShort = 0f;	
			}
			
			BoBotGlobal.input_verticalDirection = 0f;
		}
		
		if (Input.GetKey (KeyCode.LeftShift) )
		{	
			BoBotGlobal.input_action = Mathf.Min ( 1f, Mathf.Max ( 0f, BoBotGlobal.input_action+deltaTime));
		} else {
			BoBotGlobal.input_action = 0f;
		}	
		
		if (Input.GetKeyDown (KeyCode.Escape) )
		{	
			BoBotGlobal.input_menu = 1f;
		} else {
			BoBotGlobal.input_menu = 0f;
		}	
	}	
}
