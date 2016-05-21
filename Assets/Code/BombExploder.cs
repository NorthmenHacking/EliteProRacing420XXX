using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BombExploder : NetworkBehaviour {
	
	[SyncVar]
	public float timeRemaining = 0F;
	
	public float maxDist = 1F;
	public float power = 1F;
	public float lift = 0F;
	public GameObject postEffects;
	
	void Update() {
		
		if (!this.isServer) return;
		
		// Try to explode.
		timeRemaining -= Time.deltaTime;
		if (timeRemaining <= 0) {
			this.CmdExplode();
		}
		
	}
	
	[Command]
	private void CmdExplode() {
		
		Collider[] cols = Physics.OverlapSphere(this.transform.position, this.maxDist);
		
		foreach (Collider hit in cols) {
			
			if (hit.attachedRigidbody != null) {
				
				Debug.Log("hit " + hit.gameObject);
				hit.attachedRigidbody.AddExplosionForce(this.power, this.transform.position, this.maxDist, this.lift);
				
			}
			
		}
		
		NetworkServer.Destroy(this.gameObject);
		GameObject.Destroy(this.gameObject);
		
		// Pretty!
		if (this.postEffects == null) return;
		GameObject effects = (GameObject) GameObject.Instantiate(this.postEffects, this.transform.position, Quaternion.identity);
		NetworkServer.Spawn(effects);
		
	}
	
}
