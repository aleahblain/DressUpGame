using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public GameObject audioSource;
    bool soundToggle = true;

    void Start()
    {
        OnGUI();

    }

    void OnGUI()
    {
        soundToggle = !soundToggle;
        if (soundToggle)
        {
            audioSource.SetActive(true);

        }
        else
        {
            audioSource.SetActive(false);

        }
    }

}
