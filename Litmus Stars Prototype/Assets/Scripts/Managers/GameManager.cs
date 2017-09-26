using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour 
{
	private bool _paused = false;
	public float DeltaTime { get; private set; }
	public CinemachineVirtualCamera Cam;

	private InputManager _input;
	private bool _handling = false;

	private LevelManager _levelManager;

	private bool _gameStarted = false;

	//Gameplay Stuff
	private int _selectedIndex = 0;
	private List<Transform> _selectables;
	private Transform _currentlySelected = null;
	// Use this for initialization

	void Start () 
	{
		_input = GetComponent<InputManager> ();
		_levelManager = GetComponent<LevelManager> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		PauseCheck ();
		InputCheck ();
	}

	public void StartGame(){
		_selectables = _levelManager.GenerateLevel (0);
		_currentlySelected = _selectables [0];
		_levelManager.StartLevel ();
		_gameStarted = true;
	}

	private void PauseCheck(){
		DeltaTime = _paused ? 0.0f : Time.deltaTime;
	}

	private void InputCheck(){
		//TODO: Check inputs here.
		if (_input.SelectPressed && !_gameStarted)
			StartGame ();

		if (_input.SelectPressed && _gameStarted)
			CheckSelecting ();

		if (_input.VerticalAxis >= 0.1f && _gameStarted && !_handling)
			ChangeSelection (true);
		else if (_input.VerticalAxis <= -0.1f && _gameStarted && !_handling)
			ChangeSelection (false);
		else if (_input.VerticalAxis < 0.1f && _input.VerticalAxis > -0.1f && _gameStarted && _handling)
			_handling = false;
			
	}

	private void ChangeSelection(bool up){
		_selectedIndex = up ? ++_selectedIndex : --_selectedIndex;
		_selectedIndex = _selectedIndex >= _selectables.Count ? 0 : _selectedIndex;
		_selectedIndex = _selectedIndex < 0 ? _selectables.Count : _selectedIndex;
		_currentlySelected = _selectables [_selectedIndex];
		Debug.Log (_selectedIndex);
		Debug.Log (_selectables.Count);
		UpdateCamera ();
		_handling = true;
	}

	private void UpdateCamera(){
		Cam.LookAt = _currentlySelected;
		Cam.m_Lens.FieldOfView = _currentlySelected.CompareTag("Planet") ? 15.0f : 60.0f;
	}

	private void CheckSelecting(){
		if (_currentlySelected != null && _currentlySelected.CompareTag ("Planet")) {
			OrbitController controller = _currentlySelected.GetComponent<OrbitController> ();
			controller.ZeroSpeedPoint ();
		}
		
	}
}
