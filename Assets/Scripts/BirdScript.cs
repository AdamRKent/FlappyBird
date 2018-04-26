using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

	public static BirdScript instance;

	[SerializeField]
	private Rigidbody2D myRigidBody;

	[SerializeField]
	private Animator anim;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip flapClick, pointClip, diedClip;

	public int score;

	private float forwardSpeed = 4f;
 	private float bounceSpeed = 4f;
	private bool didFlap;
	public bool isAlive;
	public Button flapButton;

	// //myRigidBody = GetComponent<RigidBody2D> (); ...This is the less efficient way to script it. Which essentially says that the myRigidBody function will grab any component that is attached to the body


	void Awake () {
		if (instance == null) {
			instance = this;
		}

		isAlive = true;
		score = 0;

		flapButton = GameObject.FindGameObjectWithTag ("FlapButton").GetComponent<Button>();
		flapButton.onClick.AddListener (() => FlapTheBird());
		 //if you do this, make sure that NO OTHER OBJECTS reference the 'flapButton' Tag...especially Canvas

		 	SetCamerasX ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (isAlive) {
			Vector3 tempPos = transform.position;
			tempPos.x += forwardSpeed * Time.deltaTime;
			transform.position = tempPos;

			if (didFlap) {
				didFlap = false;
				myRigidBody.velocity = new Vector2(0, bounceSpeed);
				audioSource.PlayOneShot(flapClick);
				anim.SetTrigger("Flap");
			}

			if(myRigidBody.velocity.y >= 0) {
				transform.rotation = Quaternion.Euler(0, 0, 0);
			} else {
				float angle = 0;
				angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 20);
					//this function takes the angle from angle 0, to max angle 90, at a given time (in this case, its velocity in the y direction divided by any integer)
				transform.rotation = Quaternion.Euler(0, 0, angle);
			}

			//Debug.Log(forwardSpeed); //check if the forward speed is updating based on our code
		}
	}
	void SetCamerasX() {
		CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
	}

	public float GetPositionX() {
		return transform.position.x;
	}

	public void FlapTheBird () {
		didFlap = true;
	}

	void OnCollisionEnter2D(Collision2D target) {
		if(target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe") {
			if(isAlive) {
				isAlive = false;
				anim.SetTrigger("BirdDied");
				audioSource.PlayOneShot(diedClip);
				GameplayController.instance.PlayerDiedShowScore(score);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target) {
		if(target.tag == "PipeHolder") {
			score++;
			GameplayController.instance.SetScore(score);
			audioSource.PlayOneShot(pointClip);
		}
	}

}
