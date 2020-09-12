using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotReel : MonoBehaviour
{
    [SerializeField] 
    private SlotReelStripItem[] slotReelStripItems;

    [SerializeField] private SlotReelItemImageController[] slotReelItemImageControllers;

    private int reelIndex;
    private bool isSpinning = false;
    
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
            slotReelItemImageControllers = GetComponentsInChildren<SlotReelItemImageController>();
        }

        if (slotReelItemImageControllers.Length == 0)
        {
            Debug.LogError(String.Format("No SlotReelItemImageController components assigned or found in children of {0}", gameObject.name));
        }

        reelIndex = Random.Range(0, slotReelStripItems.Length - 1);

        setReelImages(reelIndex);
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
                setReelImages(reelIndex);
            }
        }
    }

    public void SpinReel(float timeToSpin)
    {
        isSpinning = true;
        
        StartCoroutine(waitForSpin(timeToSpin));
    }

    private void setReelImages(int index)
    {
        for (int i = 0; i < slotReelItemImageControllers.Length; i++)
        {
            Sprite sprite = slotReelStripItems[(index + i) % slotReelStripItems.Length].sprite;
            slotReelItemImageControllers[i].SetItemImage(sprite);
        }
    }

    IEnumerator waitForSpin(float timeToSpin)
    {
        yield return new WaitForSeconds(timeToSpin);

        isSpinning = false;
        
        reelIndex = Random.Range(0, slotReelStripItems.Length - 1);

        setReelImages(reelIndex);
    }
}
