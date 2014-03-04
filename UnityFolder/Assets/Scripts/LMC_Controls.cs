using UnityEngine;
using System.Collections;
using Leap;


public class LMC_Controls : MonoBehaviour 
{
	Controller controller;

	public GameObject pointerPrefab;

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

		HandleSphereSpawn();
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

	void HandleSphereSpawn()
	{
		float previousFloor = Mathf.Floor(previousCircleProgress);
		float currentFloor = Mathf.Floor(currentCircleProgress);

		if(currentFloor > previousFloor)
		{
			Debug.Log("SPAWN CIRCLE)");
		}

	}



}
