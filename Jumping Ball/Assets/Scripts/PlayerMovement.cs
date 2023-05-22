using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	bool jump = false;
	Rigidbody rb;
	Transform cameraHolder;

	[SerializeField] float forwardSpeed, maxSpeed;
	[SerializeField] float jumpFore;
	public Vector3 vec;
	bool isGameOver = false;

	[SerializeField] GameObject[] obstacle;
	[SerializeField] float obstacleDistance;
	[SerializeField]float obstaclePosY;
	[SerializeField] float numberObstacles;
	int length;

	public int Score = 0;
	public AudioSource Audiclick;
	public AudioSource AudigameOver;
	public ParticleSystem trailParticle, explodeParticle;
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		cameraHolder = Camera.main.transform.parent;
		BuildObstacles();
	}

	void Update()
    {
		rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
            rb.velocity = new Vector3(rb.velocity.x, 0, forwardSpeed);
            rb.AddForce(Vector3.up * jumpFore);
		}
	}

	void BuildObstacles()
	{
		length = obstacle.Length;
		vec.z = 56.4f;
		for (int i = 0; i < numberObstacles; i++)
		{
			vec.z += obstacleDistance;
			vec.y = Random.Range(-obstaclePosY, obstaclePosY);
			Instantiate(obstacle[Random.Range(0, length)], vec, Quaternion.identity);
		}
	}


	private void OnCollisionEnter()
	{
		//stop player move 
		rb.velocity = Vector3.zero;
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		GetComponent<MeshRenderer>().enabled = false;
		//effect
		AudigameOver.Play();
		explodeParticle.Play();

		if (GameController.instance != null)
		{
			GameController.instance.GameOverShowPanel();
		}
	}
}
