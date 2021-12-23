using UnityEngine;
using System.Collections;

public class BoxcastTest : MonoBehaviour
{

	[SerializeField]
	bool isEnable = false;
	[SerializeField] private float xdistance = 0.5f;
	[SerializeField] private float ydistance;
	[SerializeField] private Vector3 scale = new Vector3(1.25f,0);
	[SerializeField] private Vector3 ascale = new Vector3(0,2.5f);
	Vector3 vector = new Vector3(1, 0);

	void OnDrawGizmos()
	{
		if (isEnable == false)
			return;
		vector.x = -transform.localScale.x;
		Gizmos.DrawWireCube(transform.position + Vector3.down * ydistance, scale);
		Gizmos.DrawWireCube(transform.position + Vector3.up * ydistance, scale);
		Gizmos.DrawWireCube(transform.position + vector * xdistance, ascale);

	}
}