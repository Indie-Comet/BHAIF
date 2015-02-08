using System.Collections;
using UnityEngine;


public class MouseFollower : MonoBehaviour
{

	public float speed;
	public Camera mainCamera;
	
	void Update ()
	{
		Vector3 newPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		newPos.z = 0;
		newPos -= transform.position;
		//newPos.Normalize ();

		transform.Translate(newPos * speed * Time.deltaTime);
	}
}
