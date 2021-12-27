using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontAudioDestory : MonoBehaviour
{
    private DontAudioDestory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
