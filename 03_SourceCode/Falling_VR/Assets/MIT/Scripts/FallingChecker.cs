using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FallingChecker : MonoBehaviour
{
	public GroundChecker groundChecker;
	public OVRPlayerController OVRController;
	public RiggingManager riggingManager;
	public Rig rig;

	public Camera OVR_Camera;
	//public GameObject Falling_Camera_Rig;

	[SerializeField] private float time;
	[SerializeField] private float curTime;

	[SerializeField] private GameObject player_Model;
	//[SerializeField] private Rigidbody player_main_rigidbody;
	[SerializeField] private List<CapsuleCollider> player_ragdoll_Capsule_colliders;
	[SerializeField] private List<SphereCollider> player_ragdoll_Sphere_colliders;
	[SerializeField] private List<BoxCollider> player_ragdoll_Box_colliders;
	[SerializeField] private List<Rigidbody> player_rigidbody;

	[SerializeField] private Animator ani;

	[SerializeField] private bool safety;
	private OVRScreenFade fade;
	//[SerializeField] private Avatar avatar;

	//[SerializeField] private TextMeshProUGUI txt;

	bool isFalling = false;

	private void Awake()
	{
		riggingManager = GetComponent<RiggingManager>();

		player_ragdoll_Capsule_colliders = player_Model.GetComponentsInChildren<CapsuleCollider>().ToList();
		player_ragdoll_Sphere_colliders = player_Model.GetComponentsInChildren<SphereCollider>().ToList();
		player_ragdoll_Box_colliders = player_Model.GetComponentsInChildren<BoxCollider>().ToList();

		fade = FindObjectOfType<OVRScreenFade>();

		foreach (var collider in player_ragdoll_Capsule_colliders)
		{
			player_rigidbody.Add(collider.GetComponent<Rigidbody>());
			player_rigidbody[player_rigidbody.Count - 1].useGravity = false;
			collider.enabled = false;
		}
		foreach (var collider in player_ragdoll_Sphere_colliders)
		{
			player_rigidbody.Add(collider.GetComponent<Rigidbody>());
			player_rigidbody[player_rigidbody.Count - 1].useGravity = false;
			collider.enabled = false;
		}
		foreach (var collider in player_ragdoll_Box_colliders)
		{
			player_rigidbody.Add(collider.GetComponent<Rigidbody>());
			player_rigidbody[player_rigidbody.Count - 1].useGravity = false;
			collider.enabled = false;
		}
	}

	private void Update()
	{
		//txt.text = $"{transform.position.x} : {transform.position.y} : {transform.position.z} \n{groundChecker.IsGrounded()}";

		if (!groundChecker.IsGrounded() && !isFalling)
		{
			StartCoroutine(StartTimer());
		}
	}

	IEnumerator StartTimer()
	{
		curTime = time;
		while (curTime > 0 && !groundChecker.IsGrounded() && !isFalling)
		{
			curTime -= Time.deltaTime;

			yield return null;

			if (curTime <= 0)
			{
				curTime = 0;

				riggingManager.enabled = false;
				OVRController.enabled = false;
				OVRController.gameObject.SetActive(false);
				//OVRController.GetComponent<CharacterController>().enabled = false;
				//OVRController.enabled = false;


				OVR_Camera.gameObject.SetActive(false);
				//Falling_Camera_Rig.gameObject.SetActive(true);

				//ani.avatar = avatar;

				rig.weight = 0;
				ani.SetTrigger("T_Pose");

				foreach (var collider in player_ragdoll_Capsule_colliders)
				{
					collider.enabled = true;
				}
				foreach (var collider in player_ragdoll_Sphere_colliders)
				{
					collider.enabled = true;
				}
				foreach (var collider in player_ragdoll_Box_colliders)
				{
					collider.enabled = true;
				}
				foreach (var rigidbody in player_rigidbody)
				{
					rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
					rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
					rigidbody.useGravity = true;
					rigidbody.isKinematic = false;
				}

				isFalling = true;
				ani.enabled = false;

				if (safety)
				{
					yield return new WaitForSeconds(5f);

					fade.FadeOut();
					yield return new WaitForSeconds(fade.fadeTime + 1.5f);

					new Exit_Program().Exit();
				}

				yield break;
			}
		}
	}
}
