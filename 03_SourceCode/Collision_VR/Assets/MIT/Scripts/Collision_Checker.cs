using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;


//[System.Serializable]
//public class Transform_Offset
//{
//	public Vector3 pos;
//	public Quaternion rot;
//	public Vector3 sca;
//}

public class Collision_Checker : MonoBehaviour
{
	//[Header("Gizmo")]
	//public bool drawGizmo;
	//public Mesh mesh;
	//public Color gizmo_color;

	//[Space(10f), Header("Physics Check")]
	//public Transform_Offset physics_checkbox;
	//public LayerMask layerMask;

	//public BoxCollider boxCollider;

	[Header("Ragdoll")]
	public GameObject body;
	public List<Rigidbody> rigidbodys;
	public List<BoxCollider> ragdoll_boxCollider;
	public List<SphereCollider> ragdoll_sphereCollider;
	public List<CapsuleCollider> ragdoll_capsuleCollider;

	public Rig rig;
	public RiggingManager riggingManager;
	public Animator body_ani;

	public Transform target;
	public AnimationCurve curve;

	public GameObject OVRrig;
	public GameObject head_cam;

	[Header("Animator")]
	public Animator ani_forklift;

	public BoxBlur boxBlur;
	private OVRScreenFade fade;
	private Scene_Loader scene_Loader;
	public string load_scene_name;

	public bool safety;
	//public List<Animator> ani_worker;
	//public bool[] default_facing;

	//private void OnDrawGizmos()
	//{
	//	if (!drawGizmo) return;

	//	Gizmos.color = gizmo_color;
	//	Quaternion qu = physics_checkbox.rot;
	//	qu.eulerAngles += transform.eulerAngles;
	//	Gizmos.DrawWireMesh(mesh, physics_checkbox.pos + transform.position, qu, physics_checkbox.sca);
	//	Gizmos.DrawMesh(mesh, physics_checkbox.pos + transform.position, qu, physics_checkbox.sca);
	//}

	private void Awake()
	{
		boxBlur = FindObjectOfType<BoxBlur>();
		fade = FindObjectOfType<OVRScreenFade>();
		scene_Loader = FindObjectOfType<Scene_Loader>();

		rigidbodys = body.GetComponentsInChildren<Rigidbody>().ToList();
		ragdoll_boxCollider = body.GetComponentsInChildren<BoxCollider>().ToList();
		ragdoll_capsuleCollider = body.GetComponentsInChildren<CapsuleCollider>().ToList();

		rig = GetComponentInChildren<Rig>();
		riggingManager = GetComponent<RiggingManager>();
		body_ani = GetComponentInChildren<Animator>();

		foreach (var ragdoll in rigidbodys)
		{
			ragdoll.useGravity = false;
			ragdoll.isKinematic = true;
		}
		foreach (var ragdoll in ragdoll_boxCollider)
		{
			ragdoll.enabled = false;
		}
		foreach (var ragdoll in ragdoll_capsuleCollider)
		{
			ragdoll.enabled = false;
		}
		foreach (var ragdoll in ragdoll_sphereCollider)
		{
			ragdoll.enabled = false;
		}
	}

	void Update()
	{
		//Gizmos.color = gizmo_color;
		//Quaternion qu = physics_checkbox.rot;
		//qu.eulerAngles += transform.eulerAngles;
		//Physics.CheckBox(physics_checkbox.pos + transform.position, physics_checkbox.sca, qu, layerMask);

		//boxCollider.center = physics_checkbox.pos;
		//boxCollider.size = physics_checkbox.sca;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Collision_Trigger")
		{
			StartCoroutine(Collision());
		}
	}

	IEnumerator Collision()
	{
		OVRrig.SetActive(false);
		//head_cam.SetActive(true);

		//target.transform.position = new Vector3(target.transform.position.x, head_cam.transform.position.y, transform.position.z);

		//Vector3 relativePosition = target.transform.position - transform.position;
		//Quaternion targetRotation = Quaternion.LookRotation(relativePosition);

		float t = 0f;
		while (t < 1)
		{
			OVRrig.transform.rotation = Quaternion.Lerp(OVRrig.transform.rotation, target.rotation, curve.Evaluate(t));
			t += Time.deltaTime;
			if (t >= 0.9999f)
				t = 1f;
			yield return null;
		}
		OVRrig.transform.rotation = target.rotation;
		yield return null;
		if (safety)
		{
			OVRrig.SetActive(true);
		}

		//yield return new WaitForSeconds(1f);

		//foreach (var ani in ani_worker)
		//{
		//	ani.SetTrigger("Forklift");
		//}

		if (!safety)
		{
			ani_forklift.SetTrigger("Forklift");

			yield return new WaitForSeconds(0.6f);

			rig.weight = 0;
			riggingManager.enabled = false;
			body_ani.enabled = false;

			foreach (var ragdoll in rigidbodys)
			{
				ragdoll.useGravity = true;
				ragdoll.isKinematic = false;
				ragdoll.interpolation = RigidbodyInterpolation.Extrapolate;
			}
			foreach (var ragdoll in ragdoll_boxCollider)
			{
				ragdoll.enabled = true;
			}
			foreach (var ragdoll in ragdoll_capsuleCollider)
			{
				ragdoll.enabled = true;
			}
			foreach (var ragdoll in ragdoll_sphereCollider)
			{
				ragdoll.enabled = true;
			}
		}

		if (!safety)
			boxBlur.Blur();

		yield return new WaitForSeconds(10f);

		if (!safety)
			scene_Loader.Scene_Loading(load_scene_name);
		else
		{
			fade.FadeOut();
			yield return new WaitForSeconds(fade.fadeTime + 1.5f);
			new Exit_Program().Exit();
		}

		enabled = false;
	}
}
