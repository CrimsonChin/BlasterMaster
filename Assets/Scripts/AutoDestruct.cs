using UnityEngine;
using System.Collections;

public class AutoDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
