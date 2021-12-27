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

	[SerializeField] private Slider[] sliders;

	private bool _isChange = default;
    private void Awake()
    {
		inputs = new InputController();
        audioMixer.GetFloat("MasterVolume", out float a);
		sliders[0].value = a;
		audioMixer.GetFloat("BGMVolume", out float b);
		sliders[1].value = b;
		audioMixer.GetFloat("SEVolume", out float c);
		sliders[2].value = c;
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


	public void SetMaster()
	{
		audioMixer.SetFloat("MasterVolume", sliders[0].value);
	}

	public void SetBGM(float volume)
	{
		audioMixer.SetFloat("BGMVolume", sliders[1].value);
	}

	public void SetSE(float volume)
	{
		audioMixer.SetFloat("SEVolume", sliders[2].value);
	}
}

