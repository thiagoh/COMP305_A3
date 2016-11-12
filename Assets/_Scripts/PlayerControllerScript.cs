using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerScript : MonoBehaviour {

	private RaycastHit activate;
	private float targetDistance;
	private GameObject targetObject;
	[SerializeField] float allowedRange = 1.5f;
	private GameObject flashlight;
	private Light [] lights;
	[SerializeField] AudioClip flashlightSwitchSound;
	private GUIControllerScript guiController;
	public GameObject particle_hit;

	private List<string> inventoryItems = new List<string> ();

	// Use this for initialization
	void Start () {
		
		flashlight = GameObject.Find ("Flashlight");
		lights = flashlight.GetComponentsInChildren<Light> ();
		guiController = GameObject.FindGameObjectWithTag ("GUIController").GetComponent<GUIControllerScript> ();
	}

	// Update is called once per frame
	void Update () {

		// We only need to raycast once per frame, and use the RaycastHit object to perform all the control logic
		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out activate)) {
			targetDistance = activate.distance;
			targetObject = activate.transform.gameObject;
			if (targetDistance < allowedRange) {
				
				if (targetObject.CompareTag ("Door")) {
					guiController.SendMessage ("SetTooltip", "Click to use door", SendMessageOptions.DontRequireReceiver);
					guiController.RotateCursor ();
				} else if (targetObject.CompareTag ("Item")) {
					guiController.SendMessage ("SetTooltip", "Click to pick up item", SendMessageOptions.DontRequireReceiver);
					guiController.RotateCursor ();
				} else {
					guiController.SendMessage ("SetTooltip", "", SendMessageOptions.DontRequireReceiver);
					guiController.StopCursor ();
				}

				if (Input.GetButtonDown ("Fire1")) {
					Debug.Log (targetObject.tag);
					targetObject.SendMessage ("Activate", null, SendMessageOptions.DontRequireReceiver);
					Instantiate (particle_hit, activate.point, Quaternion.identity);
//					guiController.RotateCursor();
//					guiController.SendMessage ("SetMessage", "LMB clicked", SendMessageOptions.DontRequireReceiver);
				}
			} else {
				guiController.SendMessage ("SetTooltip", "", SendMessageOptions.DontRequireReceiver);
				guiController.StopCursor();
			}
		}


		// Switch the flashlight on/off
		if (Input.GetButtonDown ("Fire2")) {
			AudioSource.PlayClipAtPoint (flashlightSwitchSound, this.transform.position);
			foreach (Light light in lights) {
				light.enabled = !light.enabled;
			}
		}
	}

	public void AddInventoryItem (string itemName) {
		inventoryItems.Add (itemName);
	}

	public void DeleteInventoryItem (string itemName) {
		inventoryItems.Remove (itemName);
	}

	public bool CheckInventoryForItem (string itemName) {
		return inventoryItems.Contains (itemName);
	}
}
