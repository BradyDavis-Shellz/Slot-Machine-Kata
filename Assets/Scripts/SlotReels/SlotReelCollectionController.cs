using System;
using UnityEngine;
using System.Linq;

public class SlotReelCollectionController : MonoBehaviour
{
    [SerializeField] 
    private SlotReel[] slotReels;

    [SerializeField] 
    private ScoreTextDisplay scoreTextDisplay;

    [SerializeField] private SoundEffectsController sfxController;
    
    [SerializeField] private ParticleSystem winParticleSystem;

    private bool isSpinning = false;

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

        if (!scoreTextDisplay)
        {
            scoreTextDisplay = FindObjectOfType<ScoreTextDisplay>();
        }

        if (!scoreTextDisplay)
        {
            Debug.LogError(String.Format("No ScoreTextDisplay component assigned or found in scene for {0}", gameObject.name));
        }

        if (!sfxController)
        {
            sfxController = FindObjectOfType<SoundEffectsController>();
        }

        if (!sfxController)
        {
            Debug.LogError(String.Format("No SoundEffectsController component assigned or found in scene for {0}", gameObject.name));
        }

        if (!winParticleSystem)
        {
            Debug.LogError(String.Format("No particleSystem component assigned for {0}", gameObject.name));
        }
    }

    void Update()
    {
        if (isSpinning)
        {
            if (slotReels.Where(reel => reel.IsSpinning).ToArray().Length == 0)
            {
                isSpinning = false;

                AwardScoreForPayLines();
            }
        }
    }
    
    public void SpinReels(float timeToSpin)
    {
        isSpinning = true;
        for (int i = 0; i < slotReels.Length; i++)
        {
            // slots stop in order, with a uniform time to spin between stops
            float reelSpinTime = timeToSpin * (i + 1) / slotReels.Length;

            slotReels[i].SpinReel(reelSpinTime);
        }
    }

    private void AwardScoreForPayLines()
    {
        int score = GetScoreForHorizontalPayLines();

        score += GetScoreForSingles();
        
        if (score > 0)
        {
            sfxController.PlayWinSoundEffect();
            winParticleSystem.Play();
        }

        scoreTextDisplay.AddPoints(score);
        
    }

    private int GetScoreForSingles()
    {
        int score = 0;
        foreach (var reel in slotReels)
        {
            score += reel.SlotReelItemControllers.Select(x => x.SlotReelStripItem.singleAward).Sum();
        }

        return score;
    }

    private int GetScoreForHorizontalPayLines()
    {
        int score = 0;

        SlotReel initialReel = slotReels[0];
        
        for (int i = 0; i < initialReel.SlotReelItemControllers.Length; i++)
        {
            bool hasMatch = true;
            SlotReelStripItem item = initialReel.SlotReelItemControllers[i].SlotReelStripItem;
            for (int j = 1; j < slotReels.Length; j++)
            {
                if (!item.Equals(slotReels[j].SlotReelItemControllers[i].SlotReelStripItem))
                {
                    hasMatch = false;
                    break;
                }
            }

            if (hasMatch)
            {
                score += item.lineAward;
            }
        }

        return score;
    }
}
