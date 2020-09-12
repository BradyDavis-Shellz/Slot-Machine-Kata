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

    public void SpinReel(float timeToSpin)
    {
        Debug.Log("Spinning reel" + gameObject.name + " for " + timeToSpin + " seconds");

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
        
        reelIndex = Random.Range(0, slotReelStripItems.Length - 1);

        setReelImages(reelIndex);
    }
}
