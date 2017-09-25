using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour 
{
	private LineRenderer _lineRenderer;

	[Range(6,64)]
	public int Segments;
	public Ellipse Ellipse;

	void Awake(){
		_lineRenderer = GetComponent<LineRenderer> ();
		CalculateEllipse ();
	}

	private void CalculateEllipse(){
		Vector3[] points = new Vector3[Segments + 1];
		for(int i = 0; i < Segments; i++){
			Vector2 position2D = Ellipse.Evaluate ((float)i/(float)Segments);
			points [i] = new Vector3 (position2D.x, position2D.y, 0.0f);
		}

		points [Segments] = points [0];

		_lineRenderer.positionCount = Segments + 1;
		_lineRenderer.SetPositions (points);

	}

	void OnValidate(){
		if (Application.isPlaying && _lineRenderer != null)
			CalculateEllipse ();
	}
}
