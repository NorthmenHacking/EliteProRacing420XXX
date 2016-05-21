using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TimeoutDestroy : NetworkBehaviour {
	
	public float timeout = 1F;
	
	void Update() {
		
		if (!this.isServer) return;
		
		// Try to explode.
		timeout -= Time.deltaTime;
		if (timeout <= 0) {
			this.CmdDestroy();
		}
		
	}
	
	[Command]
	private void CmdDestroy() {
		
		NetworkServer.Destroy(this.gameObject);
		GameObject.Destroy(this.gameObject);
		
	}
	
}
