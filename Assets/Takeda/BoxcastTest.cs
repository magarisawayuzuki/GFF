using UnityEngine;
using System.Collections;

public class BoxcastTest : MonoBehaviour
{

	[SerializeField]
	bool isEnable = false;
	[SerializeField] private float xdistance = 0.5f;
	[SerializeField] private float ydistance;
	[SerializeField] private Vector3 scale = new Vector3(0.25f,0);
	[SerializeField] private Vector3 ascale = new Vector3(0,2.5f);

	void OnDrawGizmos()
	{
		if (isEnable == false)
			return;

		Gizmos.DrawWireCube(transform.position + Vector3.down * ydistance, scale);
		Gizmos.DrawWireCube(transform.position + Vector3.up * ydistance, scale);
		Gizmos.DrawWireCube(transform.position + Vector3.right * xdistance, ascale);
		Gizmos.DrawWireCube(transform.position + Vector3.left * xdistance, ascale);

	}
}