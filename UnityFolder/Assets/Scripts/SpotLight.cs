using UnityEngine;
using System.Collections;

public class SpotLight : MonoBehaviour 
{
	public Vector3 forwardVector;
	
	void Start () 
	{
		forwardVector = transform.forward;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.forward = forwardVector;
	}
}
