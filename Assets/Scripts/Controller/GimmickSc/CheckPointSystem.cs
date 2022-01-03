using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointSystem : MonoBehaviour
{
    public Vector3 _checkPoint = default;

    [SerializeField]
    private GameObject player = default;

    

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Respawn()
    {
        player.transform.position = _checkPoint;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}