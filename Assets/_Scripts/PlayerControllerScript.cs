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
	private bool flashlightOn = true;
	private GUIControllerScript guiController;
	public GameObject particle_hit;
	[SerializeField]private float batteryMaxTime = 10f;
	private float batteryRemainingTime;

	private Dictionary<string, string> tooltipDictionary = new Dictionary<string, string>();

	private List<string> inventoryItems = new List<string> ();

	// Use this for initialization
	void Start () {
		
		flashlight = GameObject.Find ("Flashlight");
		lights = flashlight.GetComponentsInChildren<Light> ();
		ToggleFlashlight ();

		guiController = GameObject.FindGameObjectWithTag ("GUIController").GetComponent<GUIControllerScript> ();
		batteryRemainingTime = batteryMaxTime;

		tooltipDictionary.Add("Door", "Click to use door");
		tooltipDictionary.Add("Item", "Click to pick up item");
	}

	// Update is called once per frame
	void Update () {

		// We only need to raycast once per frame, and use the RaycastHit object to perform all the control logic
		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out activate)) {
			targetDistance = activate.distance;
			targetObject = activate.transform.gameObject;
			if (targetDistance < allowedRange) {

				UpdateTooltip ();

				if (Input.GetButtonDown ("Fire1")) {
					ActivateObject ();
				}
			
			} else {
				ClearTooltip ();
			}
		}

		// Switch the flashlight on/off
		if (Input.GetButtonDown ("Fire2")) {
			AudioSource.PlayClipAtPoint (flashlightSwitchSound, this.transform.position);
			if (batteryRemainingTime <= 0f) {
				guiController.SendMessage ("SetMessage", "Flashlight is out of battery", SendMessageOptions.DontRequireReceiver);
				guiController.RotateCursor ();
			}


			ToggleFlashlight ();
		}

		// Update battery indicator
		if (flashlightOn) {
			batteryRemainingTime = Mathf.Max(batteryRemainingTime - 1f / 60f, 0f);
			guiController.UpdateBattery (batteryRemainingTime / batteryMaxTime);
			if (batteryRemainingTime <= 0f)
				ToggleFlashlight ();
		}
	}

	void UpdateTooltip () {
		if (targetObject.CompareTag ("Untagged")) {
			ClearTooltip ();
		} else {
			foreach (string key in tooltipDictionary.Keys) {
				if (targetObject.CompareTag (key)) {
					guiController.SendMessage ("SetTooltip", tooltipDictionary[key], SendMessageOptions.DontRequireReceiver);
					guiController.RotateCursor ();
				}
			}
		}
	}

	void ClearTooltip () {
		guiController.SendMessage ("SetTooltip", "", SendMessageOptions.DontRequireReceiver);
		guiController.StopCursor();
	}

	void ActivateObject () {
		Debug.Log (targetObject.tag);
		targetObject.SendMessage ("Activate", null, SendMessageOptions.DontRequireReceiver);
		Instantiate (particle_hit, activate.point, Quaternion.identity);
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

	void ToggleFlashlight () {
		if (!flashlightOn && batteryRemainingTime > 0) {
			flashlightOn = true;
		} else {
			flashlightOn = false;
		}

		foreach (Light light in lights) {
			light.enabled = flashlightOn;
		}
	}
}
