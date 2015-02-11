using UnityEngine;
using System.Collections;

public class StickyCharacter : MonoBehaviour {
	public float maxStickyForce;
	public float stickyElasticity;
	public bool isSticky;
	Hashtable contactJoints;
	Hashtable contactPoints;
	
	public bool IsSticky {
		get {
			return isSticky;
		} 
		set {
			if (!value) {
				foreach (SpringJoint2D joint in contactJoints) {
					Destroy(joint);
				}
				contactJoints = new Hashtable();
				contactPoints = new Hashtable();
			}
			isSticky = value;
		}
	}
	
	void Start ()
	{
		contactJoints = new Hashtable();
		contactPoints = new Hashtable();
	}
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (IsSticky && (!contactJoints.Contains(collision.rigidbody) || contactJoints[collision.rigidbody].Equals(null))) {
			if (contactJoints.Contains(collision.rigidbody))
				contactJoints.Remove(collision.rigidbody);
			foreach (ContactPoint2D contactPoint in collision.contacts) {
				Vector3 point = contactPoint.point;
				GlueJoint joint = gameObject.AddComponent<GlueJoint>();
				joint.connectedBody = collision.rigidbody;
				joint.anchor = rigidbody2D.GetPoint(point);
				joint.connectedAnchor = collision.rigidbody.GetPoint(point);
				joint.elasticity = stickyElasticity;
				joint.maxForce = maxStickyForce;
				contactJoints.Add(collision.rigidbody, joint);
				contactPoints.Add(collision, contactPoint);
			}
		}
	}
}
