using UnityEngine;
using System.Collections;
using System.IO;

public class load_scene : MonoBehaviour {
	
	
	private bool isPause = false;
	private SceneFader sceneFader;
	public GUISkin menu;
	string Level = "DemoLevel_01";
	
	public Texture2D background;
	
	string myName = "PlayerOne";
	string meinTimestamp = "";
	string meinLevel ="";
	
	private string file_to_load ="";
	ArrayList ar = new ArrayList();
	
	float dpi =160;
	
	// Use this for initialization
	void Start () {
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
			
		file_to_load = static_holder.file_to_load;
		
		if(file_to_load !=""){
			
			Read_Data(file_to_load);
		
		}//if
		else{
			meinTimestamp = System.DateTime.Now.ToString();
			meinLevel = Level;
			file_to_load = "//boBot//Gamesave//Game//" + myName + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + ".sav";
			Save_Load.Gamesave_Player_schreiben(myName,meinLevel,meinTimestamp,file_to_load,GameObject.Find("Player").transform.localPosition.x,GameObject.Find("Player").transform.localPosition.y,GameObject.Find("Player").transform.localPosition.z);
			static_holder.file_to_load = file_to_load;
			Read_Data(file_to_load);
		}//else
		
	}//start
	
	//Lesen der Player Datei und Inhalt in eine ArrayList hinterlegen
	void Read_Data(string yourFile){
		StreamReader sr = new StreamReader(Application.persistentDataPath + yourFile);
		
		string sLine = "";
		
		while(sLine != null){
			sLine = sr.ReadLine();
			if(sLine != null){
				ar.Add(sLine);
			}
		}
		sr.Close();
		instance_Data();
	}
	
	//Auslesen der ArrayList und setzen des Players an die ausgelesene Position
	void instance_Data(){
		
		for(int i =0; i < ar.Count; i=i+4){
			
			string my_x = ar[i+1].ToString();
			float myx = float.Parse(my_x);
			string my_y = ar[i+2].ToString();
			float myy = float.Parse(my_y);
			string my_z = ar[i+3].ToString();
			float myz = float.Parse(my_z);
			
			GameObject.Find("Player").transform.localPosition = new Vector3(myx,myy,myz);
		}
	}	
	
	//Pausenmenü
	void OnGUI(){

		if (isPause) {
			
			GUI.DrawTexture(new Rect (0,0,Screen.width,Screen.height),background);
			GUI.skin = menu;
			
			if(Screen.dpi >0){
				dpi = Screen.dpi;
			}
			menu.button.fontSize = (int)((dpi/160)*30);
			menu.label.fontSize = (int)((dpi/160)*60);
			
			GUILayout.BeginArea(new Rect((Screen.width/2)-150,(Screen.height/2)-150,512,512));
				
			GUILayout.Label("Pause");
			
			if(GUILayout.Button("Weiter")){
				ToggleTimeScale();
			}

			if(GUILayout.Button("Spiel beenden")){
				
				AudioFade.MenueIsActive(true);
				
				ToggleTimeScale();
				sceneFader.SwitchScene("StartMenu");
			}
			GUILayout.EndArea ();
		}

	}

	void ToggleTimeScale(){

		if (!isPause) {
			Time.timeScale = 0;
		} 
		else {
			Time.timeScale = 1;
		}
		isPause = !isPause;
	}
	
	// Update is called once per frame
	void Update () {
	
		#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_ANDROID		
		
			if (Input.GetKeyDown (KeyCode.Escape)) {
				ToggleTimeScale();
			}//if
		
		#elif UNITY_IPHONE 
		
		#endif
		
	}
}//class
