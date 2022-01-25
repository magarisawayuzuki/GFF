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
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame),LoadSceneMode.Additive);
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().ResetLife();
        player.transform.position = _checkPoint;

    }
}