using UnityEngine;
using System.Collections;

public abstract class StickyCharacter : Hitable
{
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
		StickyOnCollisionEnter(collision);
	}
}
