using System;
using UnityEngine;
using UnityEngine.UI;

public class SlotReelItemController : MonoBehaviour
{
    [SerializeField] 
    private Image itemImage;
    
    private SlotReelStripItem item;
    
    public SlotReelStripItem SlotReelStripItem
    {
        get => item;
    }
    
    void Start()
    {
        if (!itemImage)
        {
            Debug.LogError(String.Format("ItemImage Image component not assigned on {0}", gameObject.name));
        }
    }

    public void SetItem(SlotReelStripItem item)
    {
        this.item = item;
        
        itemImage.sprite = item.sprite;
    }
}
