﻿using UnityEngine;
using System.Collections;

public class DoorControllerScript : MonoBehaviour {
	
	[SerializeField]private AudioClip doorOpenSound, doorCloseSound;
	private Animator doorAnimator;
	private bool doorOpen = false;

	// Use this for initialization
	void Start () {
		doorAnimator = GetComponentInParent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called when the player activates the door
	void Activate () {
		
		if (doorOpen) {
			CloseDoor ();
			doorOpen = false;
		} else {
			OpenDoor ();
			doorOpen = true;
//			Debug.Log ("activate");
		}
	}

	void OpenDoor() {
		doorAnimator.SetTrigger ("Open");
		AudioSource.PlayClipAtPoint (doorOpenSound, this.transform.position);
	}

	void CloseDoor() {
		doorAnimator.SetTrigger ("Close");
		AudioSource.PlayClipAtPoint (doorCloseSound, this.transform.position);
	}
}
