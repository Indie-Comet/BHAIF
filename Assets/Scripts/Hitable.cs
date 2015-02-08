using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class Hitable : MonoBehaviour
{
	
	public float collisionDamageCoefficient;
	public float minDamageToDeal;
	public float startHp;
	float hp;
	
	public float Hp {
		get {
			return hp;
		}
	}
	
	protected Vector2 GetImpulse() {
		return rigidbody2D.velocity * rigidbody2D.mass;
	}
	
	protected float GetRotationImpulse() {
		return rigidbody2D.inertia * rigidbody2D.angularVelocity;
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
	void OnCollisionEnter2D (Collision2D collision)
	{
		float damage = 0;		
		if (damage > minDamageToDeal) {
			damage *= collisionDamageCoefficient;
			hp -= damage;
			if (hp <= 0) {
				Die();
			}
		}
		Debug.Log(damage.ToString() + ' ' + hp.ToString());
		HitableOnCollisionEnter(collision);
	}
}

