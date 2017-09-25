using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour 
{
	private int _id; //So we don't get dupes.

	public void Initialise(int id){
		_id = id;
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (_id + ": IM IN");
		//TODO: Update GM accordingly (With is it the star or another planet)
	}

	void OnTriggerExit(Collider other){
		Debug.Log(_id + ": IM OUT");
		//TODO: Update GM accordingly (Let it handle if it's already been removed.)
	}

	public int GetId(){
		return _id;
	}
}
