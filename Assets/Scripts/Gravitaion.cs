using System.Collections;
using UnityEngine;

public class Gravitaion : MonoBehaviour
{	
	public BlackHole blackHole;
	
	void FixedUpdate ()
	{
		Vector2 tmp = (blackHole.transform.position - transform.position);
		float dist = Mathf.Max(tmp.sqrMagnitude, blackHole.minDist);
		tmp.Normalize();
		tmp *= blackHole.acceleration * rigidbody2D.mass;
		if (blackHole.useCanonicalPhysics) {
			tmp /= dist;
		}
		rigidbody2D.AddForce(tmp, ForceMode2D.Force);
	}
}
