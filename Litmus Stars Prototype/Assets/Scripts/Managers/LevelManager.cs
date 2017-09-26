using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	public Transform StarLocation;
	public GameObject PlanetPrefab;

	public int MaxPlanets = 10;
	public int MinPlanets = 3;

	private List<OrbitController> _planets = new List<OrbitController>();

	//Test
	public int Seed = 0;
	
	// Update is called once per frame
	void Update () 
	{

	}

	public List<Transform> GenerateLevel(int seed){
		Random.InitState (seed);
		List<Transform> planetTransforms = new List<Transform> ();
		planetTransforms.Add (StarLocation);
		int planetCount = Random.Range (MinPlanets, MaxPlanets);
		float lastX = 2f;
		float lastY = 2f;
		for(int i = 0; i < planetCount;i++){
			GameObject planetObject = GameObject.Instantiate (PlanetPrefab, StarLocation) as GameObject;
			float startProgress = Random.Range (0f, 1f);
			float orbitPeriod = Random.Range (10f, 40f);
			float x = Random.Range (lastX + 1f, lastX + 4f);
			float y = Random.Range (lastY + 1f, lastY + 4f);
			Vector2 orbit = new Vector2 (x, y);
			lastX = x;
			lastY = y;
			OrbitController planet = planetObject.GetComponent<OrbitController> ();
			planet.Initialise (startProgress, orbitPeriod, orbit, i);
			planetTransforms.Add (planet.GetPlanetTransform ());
			_planets.Add (planet);
		}
		Debug.Log (planetTransforms.Count);
		return planetTransforms;
	}

	public void StartLevel(){
		foreach(OrbitController planet in _planets){
			planet.StartOrbit ();
		}
	}
}
