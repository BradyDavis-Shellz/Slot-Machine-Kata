using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotReelCollectionController : MonoBehaviour
{
    [SerializeField] private SlotReel[] slotReels;
    // Start is called before the first frame update
    void Start()
    {
        if (slotReels == null || slotReels.Length == 0)
        {
            slotReels = GetComponentsInChildren<SlotReel>();
        }

        if (slotReels.Length == 0)
        {
            Debug.LogError(String.Format("No SlotReel components found in children of {0}", gameObject.name));
        }
    }

    public void SpinReels(float timeToSpin)
    {
        for (int i = 0; i < slotReels.Length; i++)
        {
            // slots stop in order, with a uniform time to spin between stops
            float reelSpinTime = timeToSpin * (i + 1) / slotReels.Length;

            slotReels[i].SpinReel(reelSpinTime);
        }
    }
}
