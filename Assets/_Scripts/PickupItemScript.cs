using UnityEngine;
using System.Collections;

public class PickupItemScript : MonoBehaviour {

	[SerializeField] private string itemName = "item";
	[SerializeField] AudioClip itemPickupSound;
	private PlayerControllerScript playerController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// method called when player clicks on a GameObject within range
	void Activate () {
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerScript> ();
		playerController.AddInventoryItem (itemName);
		AudioSource.PlayClipAtPoint (itemPickupSound, this.transform.position);
		Destroy (this.gameObject);
	}
}
