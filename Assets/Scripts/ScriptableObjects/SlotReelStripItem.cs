using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SlotReelStripItem : ScriptableObject
{
    /// <summary>
    /// Sprite that displays on strip
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// Award for getting 3 of the same item in a row
    /// </summary>
    public int lineAward;
}
