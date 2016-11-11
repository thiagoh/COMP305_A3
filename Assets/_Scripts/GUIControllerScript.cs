using UnityEngine;
using System.Collections;

// required to change UI elements
using UnityEngine.UI;

public class GUIControllerScript : MonoBehaviour {

	[SerializeField] private float messageFadeTime = 2.0f;
	private Animator animator;
	private Text cursorTooltip, cursorMessage;
	private float messageTimer;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator> ();
		cursorTooltip = GameObject.Find ("CursorTooltip").GetComponent<Text>();
		cursorMessage = GameObject.Find ("CursorMessage").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > messageTimer) {
			cursorMessage.text = "";
		} else {
			
		}
	}

	public void RotateCursor () {
		animator.SetTrigger ("RotateCursor");
	}

	public void SetTooltip (string text) {
		cursorTooltip.text = text;
	}

	public void SetMessage (string text) {
		messageTimer = Time.time + messageFadeTime;
		cursorMessage.canvasRenderer.SetAlpha(1.0f);
		cursorMessage.text = text;
		cursorMessage.CrossFadeAlpha (0, messageFadeTime, true);
	}
}
