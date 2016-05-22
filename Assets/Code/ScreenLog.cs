using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class ScreenLog : MonoBehaviour {

	public static ScreenLog INSTANCE;
	
	private Text text;
	
	void Awake() {
		
		INSTANCE = this;
		
	}
	
	void Start() {
		this.text = this.GetComponentInChildren<Text>();
	}
	
	public void LogMessage(string message) {
		this.text.text += message + "\n";
	}
	
	public static void Log(string msg) {
		INSTANCE.LogMessage("<i>" + msg + "</i>");
	}
	
}
