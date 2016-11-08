using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

//	public GameObject activateEffect;

	private RaycastHit activate;
	private float targetDistance;
	[SerializeField] float allowedRange = 1.5f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out activate)) {
				targetDistance = activate.distance;
				if (targetDistance < allowedRange) {
					Debug.Log (activate.transform.gameObject.tag);
					activate.transform.gameObject.SendMessage ("Activate", null, SendMessageOptions.DontRequireReceiver);
//					Instantiate (activateEffect, activate.point, Quaternion.identity);
				}
			}
		}
	}
}
