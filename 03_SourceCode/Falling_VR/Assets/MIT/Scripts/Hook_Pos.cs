using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_Pos : MonoBehaviour
{
	public GameObject Hook;
	private Connector connector;

	private void Start()
	{
		connector = Hook.GetComponent<Connector>();

		Hook.transform.position = transform.position;
	}

	void Update()
    {
		if (Vector3.Distance(Hook.transform.position, transform.position) <= 1f && Vector3.Distance(connector.transform.position, connector.target.transform.position) > connector.distance_by_target)
		{
			Hook.transform.position = transform.position;
		}
    }
}
