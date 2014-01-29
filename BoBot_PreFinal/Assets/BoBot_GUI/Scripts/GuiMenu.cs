using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class GuiMenu : MonoBehaviour {
	
//	public GameObject rain;
//	GameObject shower;
	
	private SceneFader sceneFader;
	string Level = "DemoLevel_01";
	public GUISkin skin;
	static float HEIGHT = 1440.0f;
	static float WIDTH = 2560.0f;
	public static int guiSwitcher =0;
	float dpi =160;
	
	
	
	// Use this for initialization
	void Awake () {		
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
		Save_Load.Save_Directory();
	}//Awake

	void OnGUI(){
		
		AspectUtility.SetCamera();
		
		GUI.skin = skin;
		
		if(Screen.dpi >0){
			dpi = Screen.dpi;
		}
		//skin.button.fontSize = (int)((dpi/160)*50);
		skin.button.fontSize = 50;
		Vector2 scale = new Vector2((float)AspectUtility.screenWidth / WIDTH,(float)AspectUtility.screenHeight / HEIGHT);
		GUI.matrix = Matrix4x4.TRS(new Vector3((float) AspectUtility.xOffset, (float) AspectUtility.yOffset, 0), Quaternion.identity, new Vector3(scale.x,scale.y,1.0f));
		
		switch(guiSwitcher){
			case 0:
				//GUILayout.BeginArea(AspectUtility.screenRect);
				GUILayout.BeginArea(new Rect((AspectUtility.xOffset/2)+300,(AspectUtility.yOffset/2)+600,Camera.main.rect.width*WIDTH,Camera.main.rect.height*HEIGHT));
				
				GUILayout.Label("Menü");	
			
					if(GUILayout.Button("Spiel neu starten")){
						if(Save_Load.ar_Player.Count>0){
							Save_Load.Gamesave_Player_loeschen(Save_Load.ar_Player[3].ToString());
						}//if
						sceneFader.SwitchScene (Level);
					}//if
					
					if(Save_Load.ar_Player.Count>0){
						if(GUILayout.Button("Weiterspielen")){
							static_holder.file_to_load = Save_Load.ar_Player[3].ToString();
							sceneFader.SwitchScene (Save_Load.ar_Player[1].ToString());
						}//if
					}//if
			
					if(GUILayout.Button("Steuerung")){
						guiSwitcher =1;
					}//if
			
					if (GUILayout.Button("Spiel beenden")) {
						Application.Quit();
					}//if
			
			GUILayout.EndArea();	
				break;
		case 1:
			break;

		}//switch		
	}//OnGUI
}//class
