using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCheck : MonoBehaviour
{
    [SerializeField] private GameObject _audio = default;
    [SerializeField] private GameObject _audioParent = default;

    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Audio"))
        {
            Instantiate(_audio, _audioParent.transform);
        }
    }
}
