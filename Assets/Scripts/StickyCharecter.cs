using UnityEngine;
using System.Collections;

public abstract class StickyCharacter : Hitable
{
	public float maxStickyForce;
	public float stickyDistance;
	public float stickyDamingRatio;
	public float stickyFrequency;

	public bool isSticky;
	//ArrayList contactJoints;
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
	
	//use it in order to don't override Start
	protected virtual void StickyStart ()	{}
	protected override void HitableStart ()
	{
		contactJoints = new Hashtable();
		contactPoints = new Hashtable();
		StickyStart();
	}
	
	//use it in order to don't override OnColisionEnter
	protected virtual void StickyOnCollisionEnter (Collision2D collision){}
	override protected void HitableOnCollisionEnter (Collision2D collision)
	{
		if (IsSticky && !contactJoints.Contains(collision.rigidbody)) {
			foreach (ContactPoint2D contactPoint in collision.contacts) {
				Vector3 point = contactPoint.point;
				SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D>();
				joint.connectedBody = collision.rigidbody;
				Vector3 anchor = point - transform.position;
				Vector3 connectedAnchor = point - collision.transform.position;
				joint.anchor = new Vector2(anchor.x, anchor.y);
				joint.connectedAnchor = new Vector2(connectedAnchor.x , connectedAnchor.y);
				joint.collideConnected = true;
				joint.distance = stickyDistance;
				contactJoints.Add(collision.rigidbody, joint);
				contactPoints.Add(collision, contactPoint);
			}
		}
		StickyOnCollisionEnter(collision);
	}
}
