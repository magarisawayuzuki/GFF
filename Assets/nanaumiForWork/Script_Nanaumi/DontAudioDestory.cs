using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontAudioDestory : MonoBehaviour
{
    private DontAudioDestory instance = default;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("AudioDestroy");
            Destroy(gameObject);
        }
    }
}
