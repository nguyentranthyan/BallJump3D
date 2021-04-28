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

	[SerializeField] float playerSpeed;
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
	public Text hscore;
	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		cameraHolder = Camera.main.transform.parent;
		buildObstacles();
	}

	// Update is called once per frame
	void Update()
    {
		hscore.text= "HIGH SCORE: " + GameManager.instance.GetHighScore();
		Score++;
		if (Input.GetMouseButtonDown(0))
		{
			jump = true;
			Audiclick.Play();
		}
		if (!isGameOver)
		{
			float playerY = transform.position.y;
			if(playerY > 80f || playerY  <-80f)
			{
				Time.timeScale = 0;
				isGameOver = true;
				if (GameController.instance != null)
				{
					GameController.instance._GameOverShowPanel(Score);
				}
				
			}
		}
	}

	private void FixedUpdate()
	{
		
		rb.AddForce(Vector3.forward * playerSpeed * Time.fixedDeltaTime);
		if (GameController.instance != null)
		{
			GameController.instance._setScore(Score);
		}
		if (jump)
		{
			rb.AddForce(Vector3.up * jumpFore*1000 * Time.fixedDeltaTime);
			jump = false;
		}
	
	}

	//camerafollow 
	private void LateUpdate()
	{
		vec.x = cameraHolder.transform.position.x;
		vec.y = cameraHolder.transform.position.y;
		vec.z = cameraHolder.position.z;
		cameraHolder.transform.position = vec;
	}

	void buildObstacles()
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
		transform.GetChild(1).GetComponent<ParticleSystem>().Play();

		if (GameController.instance != null)
		{
			GameController.instance._GameOverShowPanel(Score);
		}
	}
}
