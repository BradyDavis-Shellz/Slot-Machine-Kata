using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotReel : MonoBehaviour
{
    [SerializeField] 
    private SlotReelStripItem[] slotReelStripItems;

    [SerializeField] 
    private SlotReelItemController[] slotReelItemImageControllers;
    
    [SerializeField] private SoundEffectsController sfxController;

    public SlotReelItemController[] SlotReelItemControllers
    {
        get => slotReelItemImageControllers;
    }

    private int reelIndex;
    private bool isSpinning;

    public bool IsSpinning
    {
        get => isSpinning;
    }

    // Times per second the reel will change its index
    private int spinSpeed = 30;
    private float timeSinceLastRotation = 0f;

    void Start()
    {
        if (slotReelStripItems == null || slotReelStripItems.Length == 0) 
        {
            Debug.LogError(String.Format("No slot reel strip items found on {0}", gameObject.name));
        }

        if (slotReelItemImageControllers == null || slotReelItemImageControllers.Length == 0)
        {
            slotReelItemImageControllers = GetComponentsInChildren<SlotReelItemController>();
        }

        if (slotReelItemImageControllers.Length == 0)
        {
            Debug.LogError(String.Format("No SlotReelItemImageController components assigned or found in children of {0}", gameObject.name));
        }
        
        if (!sfxController)
        {
            sfxController = FindObjectOfType<SoundEffectsController>();
        }

        if (!sfxController)
        {
            Debug.LogError(String.Format("No SoundEffectsController component assigned or found in scene for {0}", gameObject.name));
        }

        reelIndex = Random.Range(0, slotReelStripItems.Length - 1);

        setReelItems(reelIndex);
    }

    void Update()
    {
        if (isSpinning)
        {
            timeSinceLastRotation += Time.deltaTime;
            if (timeSinceLastRotation >= 1f / spinSpeed )
            {
                timeSinceLastRotation = 0;
                reelIndex++;
                setReelItems(reelIndex);
            }
        }
    }

    public void SpinReel(float timeToSpin)
    {
        isSpinning = true;
        
        StartCoroutine(waitForSpin(timeToSpin));
    }

    private void setReelItems(int index)
    {
        for (int i = 0; i < slotReelItemImageControllers.Length; i++)
        {
            SlotReelStripItem item = slotReelStripItems[(index + i) % slotReelStripItems.Length];
            slotReelItemImageControllers[i].SetItem(item);
        }
    }

    IEnumerator waitForSpin(float timeToSpin)
    {
        yield return new WaitForSeconds(timeToSpin);

        sfxController.playReelStopSoundEffect();
        isSpinning = false;

        reelIndex = Random.Range(0, slotReelStripItems.Length - 1);

        setReelItems(reelIndex);
    }
}
