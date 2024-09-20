using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	public GameObject bulletPrefab;
	public Transform firePoint;
	public float bulletForce = 20f;

	[SerializeField] private LayerMask groundLayer;
	private Rigidbody rb;
	private Camera mainCamera;
	private Vector3 Iso_movement;

 // Isometric movement vectors
	private Vector3 isoRight = new Vector3(1f, 0f, 1f).normalized;
	private Vector3 isoForward = new Vector3(-1f, 0f, 1f).normalized;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		mainCamera = Camera.main;
	}

	void Update()
	{
		// ternary expression example
	
		// Movement
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = (isoRight * moveHorizontal + isoForward * moveVertical).normalized;

        // Apply movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

 		// Rotation towards mouse pointer
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
		{
			Vector3 targetPosition = hit.point;
			targetPosition.y = transform.position.y; // Keep the y-position the same as the player
			Vector3 direction = (targetPosition - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
		}

		// Shooting
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}

	void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
		bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
	}

	
}
