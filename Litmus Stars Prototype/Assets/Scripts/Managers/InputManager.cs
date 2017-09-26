using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
	public float VerticalAxis { get; private set;}
	public float HorizontalAxis {get; private set;}

	public bool SelectPressed { get; private set;}
	public bool CancelPressed { get; private set;}
		
	// Update is called once per frame
	void Update () 
	{
		InputCheck ();
	}

	private void InputCheck(){
		VerticalAxis = Input.GetAxis ("Vertical");
		HorizontalAxis = Input.GetAxis ("Horizontal");
		SelectPressed = Input.GetButtonDown ("Submit");
		CancelPressed = Input.GetButtonDown ("Cancel");
	}
}
