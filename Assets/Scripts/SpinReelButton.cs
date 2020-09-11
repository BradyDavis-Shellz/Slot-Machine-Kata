using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpinReelButton : MonoBehaviour
{
    [SerializeField]
    private Text spinButtonText;

    [SerializeField] 
    private Button spinButton;

    [SerializeField]
    private float timeToSpin = 5f;

    [SerializeField] 
    private const string SPINNING_TEXT = "Spinning!";
    
    [SerializeField] 
    private const string READY_TO_SPIN_TEXT = "Spin!";

    void onSpinButtonClick()
    {
        // TODO: Call for reel object to spin

        StartCoroutine(waitForSpin());
    }

    IEnumerator waitForSpin()
    {
        spinButtonText.text = SPINNING_TEXT;
        spinButton.interactable = false;

        yield return new WaitForSeconds(timeToSpin);

        spinButton.interactable = true;
        spinButtonText.text = READY_TO_SPIN_TEXT;
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
        
        if (!spinButtonText)
        {
            spinButtonText = GetComponentInChildren<Text>();
        }

        if (!spinButtonText)
        {
            Debug.LogError(String.Format("Text component not found in children of {0}", gameObject.name));
        }

        spinButtonText.text = READY_TO_SPIN_TEXT;
        spinButton.onClick.AddListener(onSpinButtonClick);
    }
}
