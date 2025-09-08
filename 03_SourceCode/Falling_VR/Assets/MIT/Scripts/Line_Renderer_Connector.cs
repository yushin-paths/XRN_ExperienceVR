using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Renderer_Connector : MonoBehaviour
{
	public GameObject start;
	public GameObject end;
	public LineRenderer line;

	void Update()
    {
		line.SetPosition(0, start.transform.position);
		line.SetPosition(1, end.transform.position);

	}
}
