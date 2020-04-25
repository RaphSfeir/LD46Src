using UnityEngine;
using System.Collections;

public class PickUpSpirit : MonoBehaviour {


	public bool following = false;

	public float limitSpeed = 1.0f; 
	public bool runningAway = false; 
	public Vector3 currentSpeed ; 
	public GameObject targetSteel;
	public bool onSteel = false ;
	public Vector3 initialPosition ;
	public bool collectable = false; 

	public Transform followingTarget; 
	public GameManager gameManager;
	private Rigidbody2D _rigidbody;
	private BoxCollider2D _collider;


    void Awake()
    {
		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<BoxCollider2D>();
		_rigidbody.bodyType = RigidbodyType2D.Kinematic;
	}
	void Start() {

		this.initialPosition = this.transform.position; 
		this.gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

	}

	void Update() {

		if (this.collectable && this.following == false) {
			Vector3 diffPosInit =  this.transform.position - this.initialPosition;
			if (diffPosInit.magnitude > 1) {
				this.transform.position = this.initialPosition ; 
			}
		}


		if (followingTarget != null) {
			if (!this.onSteel) {
				this.following = true; 
				this.moveToTarget(); 
				if (this.targetSteel != null) {
					this.placeSteel(); 
				}
			}
			else {
				this.following = true; 
				this.moveToSteel(); 
			}
		}

		if (transform.position.y > -0.9f && _rigidbody.bodyType == RigidbodyType2D.Kinematic && gameObject.tag != "Spirit") {
			_rigidbody.bodyType = RigidbodyType2D.Dynamic;
		} else if (transform.position.y < -0.9f && _rigidbody.bodyType == RigidbodyType2D.Dynamic && gameObject.tag != "Spirit") {
			_rigidbody.bodyType = RigidbodyType2D.Kinematic;
			_rigidbody.velocity = Vector3.zero;
		}
	}

	public void placeSteel() {
		Vector3 diffPos = targetSteel.transform.position - this.transform.position; 
		Debug.Log (diffPos.magnitude); 
		if (diffPos.magnitude < 0.3f) {
			this.followingTarget = this.targetSteel.transform; 
			Debug.Log ("near it "); 
			this.onSteel = true ;
			if (this.GetComponent<AudioSource>() != null)
				this.GetComponent<AudioSource>().Play(); 
			
			/*GameObject DOOROUT =  GameObject.FindGameObjectWithTag("DOORTOOPEN");
			DoorOut DO = (DoorOut) DOOROUT.transform.GetComponent("DoorOut"); 
			DO.lumCount++ ; 
			if (DO.lumCount == 2) 
				DO.openDoor(); 
			this.light.intensity = 8.0f; 
			this.light.range = 3;*/

		}
	}

	void moveToTarget() {
		Vector3 directionToGo ; 
		if (!this.runningAway) {
			directionToGo = this.transform.position - followingTarget.position ;
		}
		else {
			directionToGo = - ( this.transform.position - followingTarget.position ) ;
		}
		this.currentSpeed = - directionToGo ; 
		this.translate () ;
	}
	
	void moveToSteel() {
		Vector3 directionToGo ; 
		if (!this.runningAway) {
			directionToGo = this.transform.position - (followingTarget.position) ;
		}
		else {
			directionToGo = - ( this.transform.position - (followingTarget.position) );
		}
		this.currentSpeed = directionToGo ; 
		this.translate () ;
	}

	private void translate() {
		
		this.currentSpeed.x = Mathf.Clamp(this.currentSpeed.x, - this.limitSpeed, this.limitSpeed) ; 
		this.currentSpeed.y = Mathf.Clamp(this.currentSpeed.y, - this.limitSpeed, this.limitSpeed) ; 
		Vector3 speedVector = new Vector3 (this.currentSpeed.x * Time.deltaTime, this.currentSpeed.y * Time.deltaTime, 0); 
		this.transform.Translate(speedVector ); 

	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player" && followingTarget == null) {
			this.following = true; 
			GameObject player = gameManager.getPlayer();
			PlayerController PC = player.GetComponent<PlayerController>();
			PC.carryingSpirits.Add(this);
			PC.spiritCount++;
			PC.pickUpSound();
			this.followingTarget = gameManager.getPlayer().transform; 
		}
	}

	public void mutateToFallingSpirit() {
		Debug.Log("Mutate start");
		_collider.size = new Vector2(0.044f, 0.040f);
		_rigidbody.bodyType = RigidbodyType2D.Dynamic;
		gameObject.tag = "Spirit";
		Debug.Log("Mutate End");

	}
}
