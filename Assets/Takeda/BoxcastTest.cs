using UnityEngine;
using System.Collections;

public class BoxcastTest : MonoBehaviour
{

	[SerializeField]
	bool isEnable = false;
	[SerializeField] private float attackdistance = 1f;
	[SerializeField] private float grounddistance = 1.3f;
	[SerializeField] private Vector3 scale = new Vector3(1.25f,0);
	[SerializeField] private Vector3 ascale = new Vector3(0,2.5f);
	private Vector3 _attackDirection = new Vector3(1, 0);


	void OnDrawGizmos()
	{
		if (isEnable == false)
			return;
		_attackDirection.x = -transform.localScale.x;
		Gizmos.DrawWireCube(transform.position + Vector3.down * grounddistance, scale);
		Gizmos.DrawWireCube(transform.position + _attackDirection * attackdistance, ascale);

	}
}