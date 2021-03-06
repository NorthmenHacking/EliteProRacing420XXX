﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ForceCollector : NetworkBehaviour {

	private Rigidbody rb;
	
	void Start() {
		this.rb = this.GetComponent<Rigidbody>();
	}
	
	[ClientRpc]
	public void RpcExplode(float power, Vector3 pos, float dist, float lift) {
		this.rb.AddExplosionForce(power, pos, dist, lift);
	}
	
}
