using UnityEngine;

public class GroundChecker : MonoBehaviour
{
	[Header("Boxcast Property")]
	[SerializeField] private Vector3 boxSize;
	[SerializeField] private float maxDistance;
	[SerializeField] private LayerMask groundLayer;

	[Header("Debug")]
	[SerializeField] private bool drawGizmo;

	private void OnDrawGizmos()
	{
		if (!drawGizmo) return;

		Gizmos.color = Color.cyan;
		Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
	}

	public bool IsGrounded()
	{
		//RaycastHit m_Hit = new();
		//if (Physics.BoxCast(transform.position, boxSize, -transform.up, out m_Hit, transform.rotation, maxDistance, groundLayer))
		//	Debug.Log(m_Hit.collider.name);
		return Physics.CheckBox(transform.position - transform.up * maxDistance, boxSize, transform.rotation, groundLayer);
		//return Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, groundLayer);
	}

}