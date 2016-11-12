using UnityEngine;
using System.Collections;

public class PlaySoundAtPlayer : MonoBehaviour {

	[SerializeField] private AudioClip [] randomSounds;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayRandomClipAtPlayer () {
		PlayClipAtPlayer (randomSounds [Random.Range(0, randomSounds.Length - 1)]);
	}

	public void PlayClipAtPlayer (AudioClip clip) {
		audioSource.PlayOneShot (clip);
	}


}
