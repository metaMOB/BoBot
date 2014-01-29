using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {
	
	private SceneFader sceneFader;
	public GUISkin skin;
	public float nativeVerticalResolution = 640.0f;
		
	// Use this for initialization
	void Awake () {
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
		Save_Load.Save_Directory();
		//Save_Load.Save_Directory();
	}//Awake
	
	void OnGUI(){
		
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));
		GUI.skin =skin;
		
		if(GUILayout.Button("Test")){
			Application.LoadLevel ("DemoLevel_01");
		}
		
//		if(GUI.Button(new Rect(100,100,100,100),"Test")){
//		}
	}
}
