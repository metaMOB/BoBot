using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class GuiMenu : MonoBehaviour {
	
	public Texture2D background;
	public Texture2D first;
	public Texture2D second;
	public Texture2D third;
	
//	public GameObject rain;
//	GameObject shower;
	
	private SceneFader sceneFader;
	string Level = "DemoLevel_01";
	public GUISkin skin;
	static float HEIGHT = 1440.0f;
	static float WIDTH = 2560.0f;
	public static int guiSwitcher =0;
	
	
	
	// Use this for initialization
	void Awake () {
		
		
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
//		shower = GameObject.Find("Regen");
//		if(!shower){
//			shower= (GameObject)Instantiate(rain,transform.position,transform.rotation);
//			shower.name = "Regen";
//		}
		Save_Load.Save_Directory();
	}//Awake

	void OnGUI(){
		
		GUI.skin = skin;
		Vector2 scale = new Vector2((float)Screen.width / WIDTH,(float)Screen.height / HEIGHT);
	
		float scaledResolutionWidth = 2560.0f;//Screen.width / Screen.height * WIDTH;
		float scaledResolutionHeight =  1440.0f;//Screen.height / Screen.height * HEIGHT;
		
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(scale.x,scale.y,1.0f));
		
		GUI.DrawTexture(new Rect(0,0,scaledResolutionWidth,scaledResolutionHeight), background);
		GUI.DrawTexture(new Rect(0,0,scaledResolutionWidth,scaledResolutionHeight), third);
		GUI.DrawTexture(new Rect(0,0,scaledResolutionWidth,scaledResolutionHeight), second);		
		GUI.DrawTexture(new Rect(0,0,scaledResolutionWidth,scaledResolutionHeight), first);
		
//		Transform[] list = GameObject.Find("Regen").GetComponentsInChildren<Transform>();
//		
//		for(int i = 0; i< list.Length;i++){
//			Debug.Log (list[i].name + " " + list[i].transform.localPosition);
//			list[i].transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
//		}
		 
		
		switch(guiSwitcher){
			case 0:
				GUILayout.BeginArea(new Rect((scaledResolutionWidth/2)-550,(scaledResolutionHeight/2)-75,512,512));
				
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
