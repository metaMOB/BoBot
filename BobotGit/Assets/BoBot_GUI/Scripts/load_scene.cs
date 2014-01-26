using UnityEngine;
using System.Collections;
using System.IO;

public class load_scene : MonoBehaviour {
	
	
	private bool isPause = false;
	private Rect butRect;
	private float ctrlWidth = 200.0f;
	private float ctrlHeight = 350.0f;
	private SceneFader sceneFader;
	public GUISkin menu;
	string Level = "DemoLevel_01";
	
	string myName = "";
	string meinTimestamp = "";
	string meinLevel ="";
	
	private string file_to_load ="";
	ArrayList ar = new ArrayList();
	
	// Use this for initialization
	void Start () {
		sceneFader = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SceneFader> ();
		butRect = new Rect ((Screen.width - ctrlWidth)/2, 100, ctrlWidth, ctrlHeight);
		
		myName = GuiMenu.myName;
		
		file_to_load = static_holder.file_to_load;
		if(file_to_load !=""){
			
		Read_Data(file_to_load);
		
		}
		else{
			meinTimestamp = System.DateTime.Now.ToString();
			meinLevel = Level;
			file_to_load = "/boBot/Gamesave/Game/" + myName + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + ".sav";
			Save_Load.Gamesave_Player_schreiben(myName,meinLevel,meinTimestamp,file_to_load,GameObject.Find("Player").transform.localPosition.x,GameObject.Find("Player").transform.localPosition.y,GameObject.Find("Player").transform.localPosition.z);
			
			Read_Data(file_to_load);
		}
		
	}
	
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
	
	void OnGUI(){


		if (isPause) {
			GUI.skin = menu;
			GUILayout.BeginArea (butRect);

			if(GUILayout.Button("Weiter")){
				ToggleTimeScale();
			}
			
			if(GUILayout.Button("Speichern")){
				
				Save_Load.Write_Data_Player(file_to_load,GameObject.Find("Player").transform.localPosition.x,GameObject.Find("Player").transform.localPosition.y,GameObject.Find("Player").transform.localPosition.z);
			}

			if(GUILayout.Button("Spiel beenden")){
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
	
		if (BoBotGlobal.input_menu == 1f) {
			ToggleTimeScale();
		}
	}
}
