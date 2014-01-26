using UnityEngine;
using System.Collections;

public class loadLevel : MonoBehaviour {

	private SceneFader sceneFader;
	private Rect butRect;
	private Rect butRect2;
	private float ctrlWidth = 200.0f;
	private float ctrlHeight = 350.0f;
	public GUISkin menu;
	
	// Use this for initialization
	void Awake () {
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
		butRect = new Rect ((Screen.width - ctrlWidth)/2, 100, ctrlWidth, ctrlHeight);
		PlayerPrefs.SetInt("unlockedLevel1", 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		GUI.skin = menu;
		GUILayout.BeginArea (butRect);
		GUILayout.Label ("Level auswählen");

		GUILayout.FlexibleSpace();
		
		for (int i= 1; i<99; i++){
			if(PlayerPrefs.GetInt("unlockedLevel_0" + i, 0)== 1){
				if (GUILayout.Button ("Level " + i)) {
					sceneFader.SwitchScene ("Level_0" + i);
				}
			}
		}
	
		
		if (GUILayout.Button ("Hauptmenü")) {
						sceneFader.SwitchScene("StartMenu");
				}

		GUILayout.FlexibleSpace();
		GUILayout.EndArea ();

	}
}