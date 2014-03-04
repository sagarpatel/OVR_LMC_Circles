using UnityEngine;
using System.Collections;
using Leap;


public class LMC_Controls : MonoBehaviour 
{
	Controller controller;

	public GameObject pointerPrefab;
	public GameObject bulletPrefab;

	GameObject pointer;

	public float currentCircleProgress = 0;
	float previousCircleProgress;
	// Use this for initialization
	void Start () 
	{
		// Leap stuff
		controller = new Controller();
		controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
		controller.Config.SetFloat("Gesture.Circle.MinArc", Mathf.PI );
		controller.Config.Save();	

		pointer = (GameObject) GameObject.Instantiate(pointerPrefab, transform.position, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () 
	{
		Frame currentFrame = controller.Frame();

		DisplayFingerTips(currentFrame);
		TrackCircleProgress(currentFrame);

		HandleSphereSpawn(currentFrame);
	}

	void DisplayFingerTips(Frame frame)
	{

		Pointable pointable = frame.Pointables.Frontmost;
		Vector3 tipPosition = pointable.StabilizedTipPosition.ToUnity();
		pointer.transform.position = tipPosition;	

		//Debug.Log(tipPosition);
	}

	void TrackCircleProgress(Frame frame)
	{
		if(!frame.Gestures().IsEmpty)
		{
			previousCircleProgress = currentCircleProgress;
			Gesture gesture = frame.Gestures()[0];
			CircleGesture circleGesture = new CircleGesture(gesture);
			currentCircleProgress = circleGesture.Progress;
		}
	}

	void HandleSphereSpawn(Frame frame)
	{
		float previousFloor = Mathf.Floor(previousCircleProgress);
		float currentFloor = Mathf.Floor(currentCircleProgress);

		if(currentFloor > previousFloor)
		{
			Debug.Log("SPAWN CIRCLE)");
			Gesture gesture = frame.Gestures()[0];
			CircleGesture circleGesture = new CircleGesture(gesture);
			
			float direction = 1.0f;
			if (circleGesture.Pointable.Direction.AngleTo(circleGesture.Normal) <= Mathf.PI/2) 
				direction *= 1.0f;
			else
				direction *= -1.0f;


			Vector3 sNormal = direction * circleGesture.Normal.ToUnity();
			Vector3 sVelocity = sNormal * 100.0f;
			Vector3 sPosition = circleGesture.Center.ToUnity();
			Vector3 sScale = new Vector3(circleGesture.Radius, circleGesture.Radius, circleGesture.Radius);

			GameObject bullet = (GameObject)Instantiate(bulletPrefab, sPosition, Quaternion.LookRotation(sNormal));
			bullet.transform.localScale = sScale;
			bullet.GetComponent<PVA>().intialVelocity = sVelocity;

		}

	}



}
