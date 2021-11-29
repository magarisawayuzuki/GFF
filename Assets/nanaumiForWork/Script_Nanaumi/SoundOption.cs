using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{

	//　SoundOptionキャンバスを設定
	[SerializeField]
	private GameObject soundUI;
	//　GameSoundShot
	[SerializeField]
	private AudioMixerSnapshot gameSoundShot;
	//　OptionSoundShot
	[SerializeField]
	private AudioMixerSnapshot optionSoundShot;

	[SerializeField]
	private AudioMixer audioMixer;

	private InputController inputs;

    private void Awake()
    {
		inputs = new InputController();
		SetMaster(0);
		SetBGM(0);
		SetSE(0);
    }

    void Update()
	{
		//　Pauseが押されたらUIをオン・オフ
		if (inputs.UI.Pause.triggered)
		{
			soundUI.SetActive(!soundUI.activeSelf);

			if (soundUI.activeSelf)
			{
				optionSoundShot.TransitionTo(0.01f);
			}
			else
			{
				gameSoundShot.TransitionTo(0.01f);
			}
		}
	}


	public void SetMaster(float volume)
	{
		audioMixer.SetFloat("MasterVolume", volume);
	}

	public void SetBGM(float volume)
	{
		audioMixer.SetFloat("BGMVolume", volume);
	}

	public void SetSE(float volume)
	{
		audioMixer.SetFloat("SEVolume", volume);
	}
}

