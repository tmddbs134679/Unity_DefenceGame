using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class EditMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI volumeText;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle volumeToggle;
    [SerializeField] Sprite volumeOn;
    [SerializeField] Sprite volumeMute;

    [SerializeField] AudioClip AC;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = 100;

        Camera camera = FindObjectOfType<Camera>();

        audioSource = camera.GetComponent<AudioSource>();
        volumeToggle.onValueChanged.AddListener(UpdateVolume);
        
    }

    // Update is called once per frame
    void Update()
    {
        VolumeControl();
    }

    void VolumeControl()
    {
        volumeText.text = volumeSlider.value.ToString();

        audioSource.volume = volumeSlider.value / 100f;
    }

    void UpdateVolume(bool isOn)
    {

        ChangeVolume(!isOn);     
    }

    void ChangeVolume(bool isOn)
    {
        if (volumeToggle.isOn)
        {
            audioSource.mute = true;
            volumeToggle.image.sprite = volumeMute;
        }

        else
        {
            audioSource.mute = false;
            volumeToggle.image.sprite = volumeOn;
        }
    }
}
