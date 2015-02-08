using System.Collections;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
	public float acceleration;
	public float accelerationChangeSpeed;
	public bool useCanonicalPhysics;	 //Canonical Physics means : F ~ m/R^2, else F ~ m.
	public float minDist = 8;			 
	//When using Canonical Physics F ~ 1/R^2, so if R = 0 then F = INF. 
	//To prevent this, let's assume that R >= minDist

	void Update ()
	{
		acceleration += Input.GetAxis("Mouse ScrollWheel") * accelerationChangeSpeed;
	}
}
