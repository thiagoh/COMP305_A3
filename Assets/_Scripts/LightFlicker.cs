using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	[SerializeField] private float flickerFrequency = 10f;
	[SerializeField] private int numFlickers = 5;
	[SerializeField] private float dimTime = 0.05f;
	[SerializeField] private float brightTime = 0.2f;
	private bool flickTrigger = true;
	private float flickTimer;
	private Light light;
	private float initialIntensity;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		initialIntensity = light.intensity;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > flickTimer + flickerFrequency) {
			StartCoroutine ("Flicker");
			flickTimer = Time.time;
		}
	}

	// Make the light flicker periodically
	IEnumerator Flicker () {
		if (flickTrigger) {
			for (int i = 0; i < numFlickers; i++) {
				light.intensity = initialIntensity * Random.Range(0.1f, 0.5f);
				yield return new WaitForSeconds(dimTime * Random.Range(0.1f, 2f));
				light.intensity = initialIntensity;
				yield return new WaitForSeconds(brightTime* Random.Range(0.1f, 2f));
			}
			StopCoroutine ("Flicker");
		}
	}
}
