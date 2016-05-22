using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;

public class PlayerScriptEnabler : MonoBehaviour {
	
	void Start () {
		
		NetworkIdentity ni = this.GetComponent<NetworkIdentity>();
		bool state = ni.isLocalPlayer;
		
		this.GetComponent<CarController>().enabled = state;
		this.GetComponent<CarUserControl>().enabled = state;
		this.GetComponent<CarAudio>().enabled = state;
		
	}
	
}
