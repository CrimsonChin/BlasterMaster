using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
        Instantiate(Player, new Vector3(1, 1, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
