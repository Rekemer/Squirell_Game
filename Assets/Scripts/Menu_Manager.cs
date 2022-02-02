using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu_Manager : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;

	public Animator transition;
	public float transitionTime = 1f;

	public Slider volumeSliders;
	
	public Toggle[] resolutionToggles;
	public Toggle fullscreenToggle;
	public int[] screenWidths;
	int activeScreenResIndex;

    private void Update()
    {
        if (volumeSliders == null)
        {
			print("null");
        }
    }
    void Start()
	{
		activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
		bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;

		for (int i = 0; i < resolutionToggles.Length; i++)
		{
			resolutionToggles[i].isOn = i == activeScreenResIndex;
		}

		fullscreenToggle.isOn = isFullscreen;
		volumeSliders.value = AudioManager.instance.musicVolumePercent;
	}
	public void Play()
    {
        Loader.Load(2, transition, transitionTime);
    }
    public void Quit()
    {
		
        Application.Quit();
    }

     public void OptionsMenu()
    {
        mainMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(true);
    }
	 
    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
    }

	public void SetScreenResolution(int i)
	{
		if (resolutionToggles[i].isOn)
		{
			activeScreenResIndex = i;
			float aspectRatio = 16 / 9f;
			Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
			PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
			PlayerPrefs.Save();
		}
	}

	public void SetFullscreen(bool isFullscreen)
	{
		for (int i = 0; i < resolutionToggles.Length; i++)
		{
			resolutionToggles[i].interactable = !isFullscreen; // deactivate other resolution toggles
		}

		if (isFullscreen)
		{
			Resolution[] allResolutions = Screen.resolutions; // to geat all resolutions monitor supports 
			Resolution maxResolution = allResolutions[allResolutions.Length - 1]; // max resolution monitor can handle
			Screen.SetResolution(maxResolution.width, maxResolution.height, true); // setting resolution
		}
		else
		{
			SetScreenResolution(activeScreenResIndex); // get last chosen resolutioun
		}

		PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
		PlayerPrefs.Save();
	}

	public void SetMasterVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
	}

	public void SetMusicVolume(float value)
	{
		
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
	}

	public void SetSfxVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
	}
}
