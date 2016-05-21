using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ForceCollector : NetworkBehaviour {

	private Rigidbody rb;
	
	void Start() {
		this.rb = this.GetComponent<Rigidbody>();
	}
	
	[ClientRpc]
	public void RpcAddForce(Vector3 vec) {
		
		Debug.Log("Adding force of " + vec + " to " + this.gameObject);
		this.rb.AddForce(vec);
		
	}
	
}
