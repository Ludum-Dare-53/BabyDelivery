using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public TextMeshProUGUI volumeText;
    public Slider volumeSlider;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeVolume();
    }

    void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
}
