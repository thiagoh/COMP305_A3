using UnityEngine;
using System.Collections;

public class DoorControllerScript : MonoBehaviour {
	
	[SerializeField] private AudioClip doorOpenSound, doorCloseSound, doorLockedSound;
	[SerializeField] private bool locked = false;
	[SerializeField] public string keyName = "key";
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
			if (!locked) {
				OpenDoor ();
				doorOpen = true;
			} else {
				AudioSource.PlayClipAtPoint (doorLockedSound, this.transform.position);
				UnlockDoor ();
			}
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

	void UnlockDoor() {
		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerScript> ().CheckInventoryForItem (keyName)) {
			locked = false;
			GameObject.FindGameObjectWithTag ("GUIController").GetComponent<GUIControllerScript> ().SetMessage ("Door opened with " + keyName);
			Activate ();
		} else {
			GameObject.FindGameObjectWithTag ("GUIController").GetComponent<GUIControllerScript> ().SetMessage ("Door is locked");
		}
	}
}
