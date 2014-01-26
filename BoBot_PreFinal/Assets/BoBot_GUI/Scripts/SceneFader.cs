using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public Texture2D texture;
	public float fadeSpeed = 2.0f;

	//private int nextLevel =1;
	private string nextLevel ="DemoLevel_01";
	private Rect screenRect;
	private Color currentColor;
	private bool isStarting = true;
	private bool isEnding = false;

	void Awake(){
		screenRect = new Rect (0, 0, Screen.width, Screen.height);
		currentColor = Color.black;
	}

	// Update is called once per frame
	void Update () {

		if (isStarting) {
			FadeIn ();
		}
		if (isEnding) {
			FadeOut();
		}
	
	}
	void OnGUI(){

		if (isStarting || isEnding) {

			GUI.depth = 0;
			GUI.color = currentColor;
			GUI.DrawTexture(screenRect, texture,ScaleMode.StretchToFill);
		}
	}

	void FadeIn(){

		currentColor = Color.Lerp (currentColor, Color.clear, fadeSpeed * Time.deltaTime);

		if (currentColor.a <= 0.05f) {
			currentColor = Color.clear;
			isStarting = false;
				}
		}

	void FadeOut(){

		currentColor = Color.Lerp (currentColor, Color.black, fadeSpeed * Time.deltaTime);
		
		if (currentColor.a >= 0.95f) {
			currentColor.a = 1;
			Application.LoadLevel(nextLevel);
			//Application.LoadLevelAsync(nextLevel);
		}
	}

	public void SwitchScene( string nextSceneIndex){

		nextLevel = nextSceneIndex;
		isEnding = true;
		isStarting = false;

		}


}
