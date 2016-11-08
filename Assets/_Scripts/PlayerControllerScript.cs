using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

//	public GameObject activateEffect;

	private RaycastHit activate;
	private float targetDistance;
	[SerializeField] float allowedRange = 1.5f;
	private GameObject flashlight;
	private Light [] lights;
	[SerializeField] AudioClip flashlightSwitchSound;

	// Use this for initialization
	void Start () {
		flashlight = GameObject.Find ("Flashlight");
		lights = flashlight.GetComponentsInChildren<Light> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out activate)) {
				targetDistance = activate.distance;
				if (targetDistance < allowedRange) {
//					Debug.Log (activate.transform.gameObject.tag);
					activate.transform.gameObject.SendMessage ("Activate", null, SendMessageOptions.DontRequireReceiver);
//					Instantiate (activateEffect, activate.point, Quaternion.identity);
				}
			}
		}

		if (Input.GetButtonDown ("Fire2")) {
			AudioSource.PlayClipAtPoint (flashlightSwitchSound, this.transform.position);
			foreach (Light light in lights) {
				light.enabled = !light.enabled;
			}
		}
	}
}
