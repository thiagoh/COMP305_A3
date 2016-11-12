using UnityEngine;
using System.Collections;

// required to change UI elements
using UnityEngine.UI;

public class GUIControllerScript : MonoBehaviour {

	[SerializeField] private float messageFadeTime = 2.0f;
	private Animator animator;
	private Text cursorTooltip, cursorMessage;
	private float messageTimer;
	private GameObject batteryInner;
	private float batteryInnerWidth;
	private float batteryOriginalX;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator> ();
		cursorTooltip = GameObject.Find ("CursorTooltip").GetComponent<Text>();
		cursorMessage = GameObject.Find ("CursorMessage").GetComponent<Text>();

		batteryInner = GameObject.Find ("BatteryInner");
		RectTransform rt = (RectTransform)batteryInner.transform;
		batteryInnerWidth = rt.rect.width;
		batteryOriginalX = batteryInner.transform.position.x;
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

	public void StopCursor () {
		animator.SetTrigger ("StopCursor");
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

	public void UpdateBattery (float fraction) {
		Debug.Log (batteryInner.transform.position);
		batteryInner.transform.localScale = new Vector3(fraction, 1, 0);
		batteryInner.transform.position = new Vector3 (batteryOriginalX + (1 - fraction) / 2 * batteryInnerWidth, batteryInner.transform.position.y, 0);
	}
}
