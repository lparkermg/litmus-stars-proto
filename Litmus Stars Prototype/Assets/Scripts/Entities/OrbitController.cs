using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour 
{
	public Transform OrbitingObject;
	public Planet Planet;
	public Ellipse OrbitPath;

	[Range(0f,1f)]
	public float OrbitProgress = 0f;
	public float OrbitPeriod = 3f;
	public bool OrbitActive = false;

	public AnimationCurve SpeedCurve;
	private float _speedPoint = 0.0f;

	private bool _initialised = false;

	public void Initialise(float startProgress, float orbitPeriod,Vector2 orbitAxis,int planetId){
		OrbitPath.XAxis = orbitAxis.x;
		OrbitPath.YAxis = orbitAxis.y;
		OrbitProgress = startProgress;
		OrbitPeriod = orbitPeriod < 0.1f ? 0.1f : orbitPeriod;
		Planet.Initialise (planetId);
		_initialised = true;
	}

	public bool StartOrbit(){
		if (!_initialised)
			return false;

		SetOrbitingObjectPosition ();
		OrbitActive = true;
		StartCoroutine (AnimateOrbit());
		return true;
	}

	public void ZeroSpeedPoint(){
		_speedPoint = 0.0f;
	}

	public Transform GetPlanetTransform(){
		return Planet.GetTransform ();
	}

	public OrbitController GetOrbitController(){
		return this;
	}

	private void SetOrbitingObjectPosition(){
		Vector2 orbitPos = OrbitPath.Evaluate (OrbitProgress);
		OrbitingObject.localPosition = new Vector3 (orbitPos.x, 0.0f, orbitPos.y);
	}

	IEnumerator AnimateOrbit(){
		float orbitSpeed = 1f / OrbitPeriod;
		while(OrbitActive){
			float time = Time.deltaTime; //TODO: Use version from GM.
			if (time > 0.0f) {
				if (_speedPoint < 1.0f)
					_speedPoint += time / 4f;
				else
					_speedPoint = 1.0f;

				OrbitProgress += time * orbitSpeed * SpeedCurve.Evaluate (_speedPoint);
				OrbitProgress %= 1f;

				SetOrbitingObjectPosition ();
			}

			yield return null;
		}
	}
}
