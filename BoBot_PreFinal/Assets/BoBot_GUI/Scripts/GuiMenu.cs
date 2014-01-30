using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class GuiMenu : MonoBehaviour {
	
	public GUISkin skin;
	
	private SceneFader sceneFader;
	string Level = "DemoLevel_01";
	public static int guiSwitcher =0;
	
	//Angabe der Standard DPI
	float dpi =160;
	
	//Angabe einer Ausgangröße
	static float HEIGHT = 1440.0f;
	static float WIDTH = 2560.0f;
	
	GameObject steu;
	
	// Use this for initialization
	void Awake () {			

#if UNITY_STANDALONE || UNITY_EDITOR		
		
		steu = GameObject.Find("SteuComp");
		
#elif UNITY_IPHONE || UNITY_ANDROID
		
		steu = GameObject.Find("SteuMobile");
#endif
		
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
		
		Save_Load.Save_Directory();
	}//Awake

	void OnGUI(){
		
		AspectUtility.SetCamera();
		
		GUI.skin = skin;
		
		//Anpassen der Schriftgröße in Abhängigkeit der DPI
		if(Screen.dpi >0){
			dpi = Screen.dpi;
		}//if
		skin.button.fontSize = (int)((dpi/160)*50);
		skin.label.fontSize = (int)((dpi/160)*80);
		
		
		Vector2 scale = new Vector2((float)AspectUtility.screenWidth / WIDTH,(float)AspectUtility.screenHeight / HEIGHT);
		GUI.matrix = Matrix4x4.TRS(new Vector3((float) AspectUtility.xOffset, (float) AspectUtility.yOffset, 0), Quaternion.identity, new Vector3(scale.x,scale.y,1.0f));
		
		GUILayout.BeginArea(new Rect((AspectUtility.xOffset/2)+300,(AspectUtility.yOffset/2)+600,Camera.main.rect.width*WIDTH,Camera.main.rect.height*HEIGHT));
		switch(guiSwitcher){
			case 0:
	
				GUILayout.Label("Menü");	
			
					if(GUILayout.Button("Spiel neu starten")){
						if(Save_Load.ar_Player.Count>0){
							Save_Load.Gamesave_Player_loeschen(Save_Load.ar_Player[3].ToString());
						}//if
						sceneFader.SwitchScene (Level);
						AudioFade.MenueIsActive(false);
					}//if
					
					if(Save_Load.ar_Player.Count>0){
						if(GUILayout.Button("Weiterspielen")){
							static_holder.file_to_load = Save_Load.ar_Player[3].ToString();
							sceneFader.SwitchScene (Save_Load.ar_Player[1].ToString());
							AudioFade.MenueIsActive(false);
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
			GUILayout.Label("Steuerung");
			
			activeSteuerung( steu,true);
			
			if(GUILayout.Button("Zurück zum Menü")){
				activeSteuerung( steu,false);
				guiSwitcher =0;
			}//if
			
			GUILayout.EndArea();
		break;

		}//switch		
	}//OnGUI
	
	void activeSteuerung(GameObject steu, bool active){
		
		steu.GetComponent<MeshRenderer>().enabled = active;
	}//activeSteuerung
}//class
