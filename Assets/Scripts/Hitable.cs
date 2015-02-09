using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class Hitable : MonoBehaviour
{
	
	public float collisionDamageCoefficient;
	public float minDamageToDeal;
	public float startHp;
	float hp;
	Vector2 lastVelocity = new Vector2();
	
	public float Hp {
		get {
			return hp;
		}
	}

	void LateUpdate() { lastVelocity = rigidbody2D.velocity;}
			
	protected Vector2 GetMomentum () {
		return rigidbody2D.velocity * rigidbody2D.mass;
	}
		
	//use it in order to don't override Start
	protected virtual void HitableStart () {}
	void Start ()
	{
		hp = startHp;
		HitableStart();
	}
	
	protected abstract void Die();
	
	//use it in order to don't override OnColisionEnter
	protected virtual void HitableOnCollisionEnter (Collision2D collision) {}
	void OnCollisionEnter2D (Collision2D collision) //TODO: Different Physics Materials.
	{	
		Vector2 momentum = lastVelocity * rigidbody2D.mass;  //Momentum before collision
		float damage = (GetMomentum() - momentum).magnitude; //Momentum change
		if (damage > minDamageToDeal) {
			damage *= collisionDamageCoefficient;
			hp -= damage;
			if (hp <= 0) {
				Die();
			}
		}
		HitableOnCollisionEnter(collision);
	}
}

