using UnityEngine;
using System.Collections;

public class GlueJoint : MonoBehaviour {
	public float elasticity;
	public Rigidbody2D connectedBody; //If connectedBody == null then we jointed to point in world space
	public Vector2 anchor;
	public Vector2 connectedAnchor;
	public float maxForce;
	
	void FixedUpdate() {
		Vector2 myPos = rigidbody2D.GetRelativePoint(anchor);
		Vector2 bodyPos = connectedAnchor;
		if (connectedBody) bodyPos = connectedBody.GetRelativePoint(connectedAnchor);
		float distance = (bodyPos - myPos).magnitude;
		Vector2 direction = (bodyPos - myPos);
		direction.Normalize();
		rigidbody2D.AddForceAtPosition(direction * distance * elasticity, myPos);
		if (connectedBody != null) {
		    connectedBody.rigidbody2D.AddForceAtPosition(direction * -1 * distance * elasticity, bodyPos);
		}
		Debug.DrawLine(myPos, bodyPos, Color.red);
		//Debug.Log("Glue Force = " + (distance * elasticity).ToString());
		if (distance * elasticity > maxForce) {
			Destroy(this);
		}
	}
}