using System.Collections;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
	public float horizontalSpeed;
	public float verticalSpeed;
	Vector3 velocity;
	
	//Horizontal Movement
	public void StartMoveRight ()
	{
		velocity.x = horizontalSpeed;
	}
	
	public void StopMoveHorizontal ()
	{
		velocity.x = 0;
	}
	
	public void StartMoveLeft ()
	{
		velocity.x = -horizontalSpeed;
	}
	
	//Vertical Movement
	public void StartMoveUp ()
	{
		velocity.y = verticalSpeed;
	}
	
	public void StopMoveVertical ()
	{
		velocity.y = 0;
	}
	
	public void StartMoveDown ()
	{
		velocity.y = -verticalSpeed;
	}
	
	void Update ()
	{
		transform.Translate(velocity * Time.deltaTime);
	}
}
