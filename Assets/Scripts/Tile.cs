using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
        
    public bool Destroyable;

    private GameObject _prefab;

    private Vector3 _position;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //Instantiate(prefab, _position, Quaternion.identity);
	}
}
