﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (BoBot_BasicPhysicsComponent))]

public class BoBot_SoundComponent : MonoBehaviour {
	
	public AudioClip horizontalMoveSound;
	public AudioClip horizontalHitSound;
	public AudioClip verticalHitSound;
	
	public AudioClip horizontalMoveBeginSound;
	public AudioClip horizontalMoveEndSound;
	
	public float verticalHitGate = 0.3f;
	public float horizontalMovingGate = 0.01f;
	public float horizontalHitGate = 0.01f;
	
	public float mainVolume = 0.5f;
	
	public bool horizontalIsVertical = false;
	
	private AudioSource audioSourceHorizontalMoveLoop;
	private AudioSource audioSourceHorizontalMoveEnd;
	private AudioSource audioSourceHorizontalMoveBegin;
	
	private AudioSource audioSourceHorizontalHit;
	private AudioSource audioSourceVerticalHit;
	
	private bool isHorizontalMoving;
	private bool isVerticalMoveing;	
	private List<AudioClip> currentHorizontalMoveSound = new List<AudioClip>();	
	private BoBot_BasicPhysicsComponent basicPhysics;
	private BoBot_DebugComponent debugInfo;
	
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		
		audioSourceHorizontalMoveLoop = gameObject.AddComponent<AudioSource>();
		audioSourceHorizontalMoveEnd = gameObject.AddComponent<AudioSource>();
		audioSourceHorizontalMoveBegin = gameObject.AddComponent<AudioSource>();
		audioSourceHorizontalHit = gameObject.AddComponent<AudioSource>();
		audioSourceVerticalHit = gameObject.AddComponent<AudioSource>();
		
		audioSourceHorizontalMoveLoop.clip = horizontalMoveSound;
		audioSourceHorizontalMoveEnd.clip = horizontalMoveEndSound;
		audioSourceHorizontalMoveBegin.clip = horizontalMoveBeginSound;
		
		
		
		audioSourceHorizontalMoveLoop.playOnAwake = false;
		audioSourceHorizontalMoveEnd.playOnAwake = false;
		audioSourceHorizontalMoveBegin.playOnAwake = false;
		
		/*audioSourceHorizontalMoveLoop.minDistance = 1f;
		audioSourceHorizontalMoveEnd.minDistance = 1f;
		audioSourceHorizontalMoveBegin.minDistance = 1f;
		
		audioSourceHorizontalMoveLoop.maxDistance = 10f;
		audioSourceHorizontalMoveEnd.maxDistance = 10f;
		audioSourceHorizontalMoveBegin.maxDistance = 10f;*/
		
		audioSourceHorizontalHit.clip = horizontalHitSound;
		audioSourceVerticalHit.clip = verticalHitSound;
		audioSourceHorizontalHit.playOnAwake = false;
		audioSourceVerticalHit.playOnAwake = false;
		audioSourceHorizontalHit.minDistance = 1f;
		audioSourceVerticalHit.minDistance = 1f;
		audioSourceHorizontalHit.maxDistance = 1f;
		audioSourceVerticalHit.maxDistance = 1f;
		
		audioSourceVerticalHit.volume = mainVolume;
		
		audioSourceHorizontalMoveLoop.volume = mainVolume;
		audioSourceHorizontalMoveEnd.volume = mainVolume;
		audioSourceHorizontalMoveBegin.volume = mainVolume;
		
		audioSourceHorizontalMoveLoop.dopplerLevel = 0;
		audioSourceHorizontalMoveEnd.dopplerLevel = 0;
		audioSourceHorizontalMoveBegin.dopplerLevel = 0;
		
		audioSourceHorizontalMoveLoop.loop = true;
		audioSourceHorizontalMoveEnd.loop = false;
		audioSourceHorizontalMoveBegin.loop = false;
		
		audioSourceHorizontalHit.dopplerLevel = 0;
		audioSourceVerticalHit.dopplerLevel = 0;
				
		basicPhysics = gameObject.GetComponent<BoBot_BasicPhysicsComponent>();
		if (!horizontalMoveBeginSound || !horizontalMoveEndSound){
			audioSourceHorizontalMoveLoop.loop = true;
		}
	}
	
	// Update is called once per frame
	void Update(){
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("SoundComponent");			
			debugInfo.addText ("> Ply Move "+audioSourceHorizontalMoveLoop.isPlaying);
			debugInfo.addText ("> Vol Move "+(audioSourceHorizontalMoveLoop.volume).ToString("0.00"));
			debugInfo.addText ("> Ply Move "+audioSourceVerticalHit.isPlaying);
			debugInfo.addText ("> Vol Move "+(audioSourceVerticalHit.volume).ToString("0.00"));
		}
	}
	
	void FixedUpdate () {
		float absDeltaX;
		float deltaTwoX;
		float deltaTwoY;
		
		if (!horizontalIsVertical){
			absDeltaX = Mathf.Abs(basicPhysics.delta.x);
			deltaTwoX = basicPhysics.deltaTwo.x;
			deltaTwoY = basicPhysics.deltaTwo.y;
		} else {
			absDeltaX = Mathf.Abs(basicPhysics.delta.y);
			deltaTwoX = basicPhysics.deltaTwo.y;
			deltaTwoY = basicPhysics.deltaTwo.x;
		}
			
		if (deltaTwoY > verticalHitGate && !audioSourceVerticalHit.isPlaying){			
			audioSourceVerticalHit.loop = false;
			audioSourceVerticalHit.Play();
		} 
		
		if ( Mathf.Abs(deltaTwoX) > horizontalHitGate && !audioSourceHorizontalHit.isPlaying){			
			audioSourceHorizontalHit.loop = false;
			audioSourceHorizontalHit.Play();
		}
		
		if (absDeltaX > horizontalMovingGate){
			if (!isHorizontalMoving){
				isHorizontalMoving = true;
				if (horizontalMoveBeginSound && horizontalMoveEndSound){
					audioSourceHorizontalMoveBegin.Play();
					audioSourceHorizontalMoveLoop.PlayDelayed(horizontalMoveBeginSound.length);
				} else {
					audioSourceHorizontalMoveLoop.loop = true;
					audioSourceHorizontalMoveLoop.Play();
				}
			}
		} else if (absDeltaX < horizontalMovingGate){
			if (isHorizontalMoving){
				isHorizontalMoving = false;
				if (horizontalMoveBeginSound && horizontalMoveEndSound){
					audioSourceHorizontalMoveLoop.Stop ();
					audioSourceHorizontalMoveEnd.Play();
				} else {
					audioSourceHorizontalMoveLoop.loop = false;
				}
			} 			
		}
	}	
}
