using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpinReelButton : MonoBehaviour
{
    [SerializeField] 
    private Button spinButton;

    [SerializeField] private SlotReelCollectionController slotReelCollectionController;

    [SerializeField]
    private float timeToSpin = 3f;

    [SerializeField] private AudioSource audioSource;

    void onSpinButtonClick()
    {
        slotReelCollectionController.SpinReels(timeToSpin);
        audioSource.Play();

        StartCoroutine(waitForSpin());
    }

    IEnumerator waitForSpin()
    {
        spinButton.interactable = false;

        yield return new WaitForSeconds(timeToSpin);

        spinButton.interactable = true;
        audioSource.Stop();
    }
    
    void Start()
    {
        if (!spinButton)
        {
            spinButton = GetComponent<Button>();
        }
        
        if (!spinButton)
        {
            Debug.LogError(String.Format("Button component not found on {0}", gameObject.name));
        }

        if (!slotReelCollectionController)
        {
            slotReelCollectionController = FindObjectOfType<SlotReelCollectionController>();
        }

        if (!slotReelCollectionController)
        {
            Debug.LogError(String.Format("No SlotReelCollectionController assigned to {0} or in scene.", gameObject.name));
        }
        
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (!audioSource)
        {
            Debug.LogError(String.Format("Audio source not assigned or found on {0}", gameObject.name));
        }

        audioSource.loop = true;

        spinButton.onClick.AddListener(onSpinButtonClick);
    }
}
