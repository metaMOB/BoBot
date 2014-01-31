using UnityEngine;
using System.Collections;
using System.IO;

public class Save_Load : MonoBehaviour {

	
	static string savings_Datei = Application.persistentDataPath + "//boBot//Gamesave//Player//savings.hvt";
	public static ArrayList ar_Player = new ArrayList();
	public static ArrayList ar_newPlayer = new ArrayList();
	public static ArrayList ar = new ArrayList();
	
	public static void Save_Directory(){
		
		Debug.Log(Application.persistentDataPath);
		
		if(!Directory.Exists(Application.persistentDataPath + "//boBot//Gamesave")){
			Directory.CreateDirectory(Application.persistentDataPath + "//boBot//Gamesave");
			Directory.CreateDirectory(Application.persistentDataPath + "//boBot//Gamesave//Player");
			Directory.CreateDirectory(Application.persistentDataPath + "//boBot//Gamesave//Game");
		}
		else{
			if(File.Exists(savings_Datei)){
				Gamesave_player_lesen();
			}
		}
	}
	
	//Auslesen der Savings-Datei und Inhalt in eine ArrayList hinzufügen
	static void Gamesave_player_lesen(){
		
		ar_Player.Clear();
		StreamReader sr = new StreamReader(savings_Datei);
		
		string sLine ="";
		
		while(sLine != null){
			sLine = sr.ReadLine();
			if(sLine !=null){
				ar_Player.Add(sLine);
			}
		}
		sr.Close();
		
		if(ar_Player.Count == 0){
			GuiMenu.guiSwitcher = 0;
		}
		
	}
	
	//Ermitteln der Information für die saving-Datei
	public static void Gamesave_Player_schreiben(string myName, string meinLevel, string meinTimestamp, string file_to_load, float x, float y, float z){
		
		if(File.Exists(savings_Datei)){
			
			Gamesave_player_lesen();
			ar_newPlayer.Clear();
			
			for(int i =0; i<ar_Player.Count;i++){
				ar_newPlayer.Add(ar_Player[i]);
			}
			ar_newPlayer.Add(myName);
			ar_newPlayer.Add(meinLevel);
			ar_newPlayer.Add(meinTimestamp);
			ar_newPlayer.Add(file_to_load);
			
			Write_Data_Player(file_to_load,x,y,z);
		}
		else{
			ar_newPlayer.Add(myName);
			ar_newPlayer.Add(meinLevel);
			ar_newPlayer.Add(meinTimestamp);
			ar_newPlayer.Add(file_to_load);
			
			Write_Data_Player(file_to_load,x,y,z);
		}
	}
	
	//Schreiben in die saving-Datei und der Player-Datei
	public static void Write_Data_Player(string file_to_load, float x, float y, float z){
		
		if(ar_newPlayer.Count != 0){
			StreamWriter sw = new StreamWriter(savings_Datei);
		
			for(int i = 0; i<ar_newPlayer.Count;i++){
				sw.WriteLine(ar_newPlayer[i]);
			}
			sw.Flush();
			sw.Close();
		
			ar_newPlayer.Clear();
		}
		
		StreamWriter sw1 = new StreamWriter(Application.persistentDataPath + file_to_load);
		
		sw1.WriteLine("myPlayer");
		sw1.WriteLine(x);
		sw1.WriteLine(y);
		sw1.WriteLine(z);
		
		sw1.Flush();
		sw1.Close();
		
		Gamesave_player_lesen();
	}

	
	public static void Gamesave_Player_loeschen(string del_Player){
		Gamesave_player_lesen();
		
		ar_newPlayer.Clear();
		
		for(int i =0;i<ar_Player.Count;i=i+4){
			if(ar_Player[i+3].ToString() != del_Player){
				ar_newPlayer.Add(ar_Player[i]);
				ar_newPlayer.Add(ar_Player[i+1]);
				ar_newPlayer.Add(ar_Player[i+2]);
				ar_newPlayer.Add(ar_Player[i+3]);
			}
		}
		StreamWriter sw = new StreamWriter(savings_Datei);
		for(int j =0;j<ar_newPlayer.Count;j++){
			sw.WriteLine(ar_newPlayer[j]);
		}
		sw.Flush();
		sw.Close();
		
		ar_newPlayer.Clear();
		
		File.Delete(Application.persistentDataPath + del_Player);
		
		static_holder.file_to_load = "";
		
		Gamesave_player_lesen();
		
		
	}
	
}
