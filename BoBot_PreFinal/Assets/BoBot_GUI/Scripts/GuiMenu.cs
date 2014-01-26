using UnityEngine;
using System.Collections;
using System.IO;

public class GuiMenu : MonoBehaviour {

	private SceneFader sceneFader;
	public int max_speicher = 4;
	string Level = "DemoLevel_01";
	
	public static int guiSwitcher =0;
	
	public int hoehe_Speicherstand = 90;
	public int breite_Spielladen = 400;
	
	public static string myName = "";
	
	
	// Use this for initialization
	void Awake () {
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
		
		Save_Load.Save_Directory();
	}

	void OnGUI(){
		
		//Wenn gespeicherte Dateien vorhanden sind
		if(Save_Load.ar_Player.Count > 0) {
			switch(guiSwitcher){
			case 0:
				GUI.BeginGroup(new Rect((Screen.width/2)-150,200,300,400),"");
				
				//Wenn nicht mehr als Speicherstände angegeben
				if((Save_Load.ar_Player.Count/4) < max_speicher){
					
					if (GUI.Button ((new Rect(30,25,240,50)),"Spiel starten")) {
						guiSwitcher =2;
					}
					if (GUI.Button (new Rect(30,100,240,50),"Spiel laden")) {
						guiSwitcher =1;
					}	
				}
				//Wenn maximale Anzahl an Speicherständen erreicht
				else{
					if (GUI.Button ((new Rect(30,100,240,50)),"Spiel laden")) {
						guiSwitcher =1;
					}
				}
				if (GUI.Button (new Rect(30,175,240,50),"Exit")) {
					Application.Quit();
				}
				GUI.EndGroup();	
				break;
			case 1:
				GUI.BeginGroup(new Rect((Screen.width/2)-(breite_Spielladen/2),200,breite_Spielladen,120+((Save_Load.ar_Player.Count/4)*hoehe_Speicherstand)));
					GUI.Box(new Rect(0,0,breite_Spielladen,200+((Save_Load.ar_Player.Count/4)*hoehe_Speicherstand)),"Spiel laden");
				
				for(int i = 0;i< Save_Load.ar_Player.Count;i=i+4){
					GUI.BeginGroup(new Rect(0,30+(i*25),breite_Spielladen,90),"");
					GUI.Box(new Rect(0,0,breite_Spielladen,90),"");
					GUI.Label(new Rect(20,5,50,25),"Name: ");
					GUI.Label(new Rect(100,5,120,25),Save_Load.ar_Player[i].ToString());
					GUI.Label(new Rect(20,35,170,25),"Erstellt am: ");
					GUI.Label(new Rect(100,35,170,25),Save_Load.ar_Player[i+2].ToString());
					GUI.Label(new Rect(20,65,70,25),"Scene: ");
					GUI.Label(new Rect(100,65,100,25),Save_Load.ar_Player[i+1].ToString());
					if(GUI.Button(new Rect(320,5,70,35),"Play")){
						
						static_holder.file_to_load = Save_Load.ar_Player[i+3].ToString();
						sceneFader.SwitchScene (Save_Load.ar_Player[i+1].ToString());
					}
					if(GUI.Button(new Rect(320,45,70,35),"Delete")){
						Save_Load.Gamesave_Player_loeschen(Save_Load.ar_Player[i+3].ToString());
					}
					GUI.EndGroup();	
				}
				if(GUI.Button(new Rect(20,70+((Save_Load.ar_Player.Count/4)*hoehe_Speicherstand),70,35),"Zurück")){
						guiSwitcher =0;
				}
				GUI.EndGroup();
				break;
			case 2:
				GUI.BeginGroup(new Rect((Screen.width/2)-165,200,330,180),"");
				GUI.Box(new Rect(0,0,340,180),"Spielstart");
				GUI.Label(new Rect(10,30,200,25),"Gebe deinen Namen an: ");
				myName = GUI.TextField(new Rect(10,60,200,25),myName);
				
				if(GUI.Button(new Rect(230,100,100,50),"Okay")){
					sceneFader.SwitchScene (Level);
				}
				if(GUI.Button(new Rect(120,100,100,50),"Abbrechen")){
					myName ="";
				}
				if(GUI.Button(new Rect(10,100,100,50),"Zurück")){
						guiSwitcher =0;
				}
				GUI.EndGroup();
				break;
			}
		}
		
		//Wenn noch keine gespeicherten Daten vorhanden sind
		else{
			switch(guiSwitcher){
				
			case 0:
				GUI.BeginGroup(new Rect((Screen.width/2)-150,200,300,400),"");
				if (GUI.Button ((new Rect(30,25,240,50)),"Spiel starten")) {
					guiSwitcher =1;
				}
				if (GUI.Button (new Rect(30,100,240,50),"Exit")) {
					Application.Quit();
				}
				GUI.EndGroup();	
				break;
			case 1:
				GUI.BeginGroup(new Rect((Screen.width/2)-165,200,330,180),"");
				GUI.Box(new Rect(0,0,340,180),"Spielstart");
				GUI.Label(new Rect(10,30,200,25),"Gebe deinen Namen an: ");
				myName = GUI.TextField(new Rect(10,60,200,25),myName);
				
				if(GUI.Button(new Rect(230,100,100,50),"Okay")){
					sceneFader.SwitchScene (Level);
				}
				if(GUI.Button(new Rect(120,100,100,50),"Abbrechen")){
					myName ="";
				}
				if(GUI.Button(new Rect(10,100,100,50),"Zurück")){
						guiSwitcher =0;
				}
				GUI.EndGroup();
				break;
			}
		}
		/*
		if (GUILayout.Button ("Spiel starten")) {
						sceneFader.SwitchScene ("Level_01");
				}
		if (GUILayout.Button ("Level auswählen")) {
						sceneFader.SwitchScene("loadLevel");
				}

		if (GUILayout.Button ("Exit")) {
			Application.Quit();
		}*/

		//GUILayout.EndArea ();

	}
}
