using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour 
{
	public float rotation;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(transform.forward, rotation * Time.deltaTime, Space.World);
	}

}
