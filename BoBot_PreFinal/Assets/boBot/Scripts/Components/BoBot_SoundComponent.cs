using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoBot_BasicPhysicsComponent))]
public class BoBot_SoundComponent : MonoBehaviour {
	
	public AudioClip horizontalMoveSound;
	public AudioClip horizontalHitSound;
	public AudioClip verticalHitSound;
	
	public float verticalHitGate = 0.3f;
	public float horizontalMovingGate = 0.01f;
	public float horizontalHitGate = 0.01f;
	
	private AudioSource audioSourceHorizontalMove;
	private AudioSource audioSourceHorizontalHit;
	private AudioSource audioSourceVerticalHit;
	
	private BoBot_BasicPhysicsComponent basicPhysics;
	
//	private Transform rigid;
//	
//	private Vector3 lastPos = Vector3.zero;
//	private Vector3 lastDelta = Vector3.zero;
//	private Vector3 deltaTwo;
	private BoBot_DebugComponent debugInfo;

	// Use this for initialization
		
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		//rigid = gameObject.rigidbody.transform;
		//lastPos = rigid.position;
		
		audioSourceHorizontalMove = gameObject.AddComponent<AudioSource>();
		audioSourceHorizontalHit = gameObject.AddComponent<AudioSource>();
		audioSourceVerticalHit = gameObject.AddComponent<AudioSource>();
		
		audioSourceHorizontalMove.clip = horizontalMoveSound;
		audioSourceHorizontalHit.clip = horizontalHitSound;
		audioSourceVerticalHit.clip = verticalHitSound;
		audioSourceVerticalHit.volume = 10;
	//	Debug.Log ("ssadsd "+gameObject.name);
		basicPhysics = gameObject.GetComponent<BoBot_BasicPhysicsComponent>();
		//Debug.Log (basicPhysics);
	}
	
	// Update is called once per frame
	void Update(){
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("SoundComponent");
			//debugInfo.addText ("> Delta2 "+(deltaTwo.x).ToString("#0.##")+ "/"+(deltaTwo.y).ToString("#0.##"));
			
			debugInfo.addText ("> Ply Move "+audioSourceHorizontalMove.isPlaying);
			debugInfo.addText ("> Vol Move "+(audioSourceHorizontalMove.volume).ToString("0.00"));
			debugInfo.addText ("> Ply Move "+audioSourceVerticalHit.isPlaying);
			debugInfo.addText ("> Vol Move "+(audioSourceVerticalHit.volume).ToString("0.00"));
		}
	}
	
	void FixedUpdate () {
		//Vector3 delta = rigid.position - lastPos;
		//deltaTwo = delta - lastDelta;
		try {
			//audioSourceHorizontalMove.volume = Mathf.Abs(basicPhysics.delta.x*50);
		}
		
		catch {
			Debug.Log ("error "+gameObject.name);	
		}
			
		if (basicPhysics.deltaTwo.y > verticalHitGate && !audioSourceVerticalHit.isPlaying){			
			audioSourceVerticalHit.loop = false;
			audioSourceVerticalHit.Play();
		} 
		
		if ( Mathf.Abs(basicPhysics.deltaTwo.x) > horizontalHitGate && !audioSourceHorizontalHit.isPlaying){			
			audioSourceHorizontalHit.loop = false;
			audioSourceHorizontalHit.Play();
		}
		//Debug.Log ("vh "+ gameObject.name +"  "+basicPhysics.deltaTwo.y);
		
		if (basicPhysics.delta.x < -horizontalMovingGate || basicPhysics.delta.x > horizontalMovingGate){
			if (!audioSourceHorizontalMove.isPlaying ){ //&& gameObject.rigidbody.useGravity){
			//Debug.Log ("play");
				audioSourceHorizontalMove.loop = true;				
				audioSourceHorizontalMove.Play();
			} 
		} else if (audioSourceHorizontalMove.isPlaying){
			//Debug.Log ("stop");
			//audioSourceHorizontalMove.Stop();
			audioSourceHorizontalMove.loop = false;			
		}
		//lastPos = rigid.position;
		//lastDelta = delta;		
	}
	
	
}
