using UnityEngine;
using System.Collections;

public class Hero : StickyCharacter {
	override protected void Die() {
		Debug.Log("Hero Dead");
		Destroy(gameObject);
	}
}
