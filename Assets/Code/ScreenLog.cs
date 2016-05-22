using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenLog : MonoBehaviour {

	public static ScreenLog INSTANCE;
	
	void Awake() {
		
		INSTANCE = this;
		
	}
	
	public static void Log(string msg) {
		INSTANCE.GetComponentInChildren<Text>().text += msg + "\n<b>[======]</b>\n";
	}
	
}
