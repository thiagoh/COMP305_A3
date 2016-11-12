using UnityEngine;
using System.Collections;

public class PickupItemScript : MonoBehaviour {

	[SerializeField] private string itemName = "item";
	[SerializeField] AudioClip itemPickupSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// method called when player clicks on a GameObject within range
	void Activate () {
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerScript> ().AddInventoryItem (itemName);
		GameObject.FindGameObjectWithTag ("GUIController").GetComponent<GUIControllerScript> ().SetMessage ("Picked up " + itemName);
		AudioSource.PlayClipAtPoint (itemPickupSound, this.transform.position);
		Destroy (this.gameObject);
	}
}
