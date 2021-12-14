using UnityEngine;
using System.Collections;

public class BoxcastTest : MonoBehaviour
{

	[SerializeField]
	bool isEnable = false;
	public float distance;
	private float jumpdistance = 1.1f;
	public float xdistance = 0;
	public Vector3 scale;
	public Vector3 ascale = new Vector3(0,2);

	void OnDrawGizmos()
	{
		if (isEnable == false)
			return;

		Gizmos.DrawWireCube(transform.position + Vector3.down * distance, scale);
		Gizmos.DrawWireCube(transform.position + Vector3.up * jumpdistance, scale);
		Gizmos.DrawWireCube(transform.position + Vector3.right * xdistance, ascale);
		Gizmos.DrawWireCube(transform.position + Vector3.left * xdistance, ascale);

	}
}