using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotReelItemImageController : MonoBehaviour
{
    [SerializeField] 
    private Image itemImage;
    
    void Start()
    {
        if (!itemImage)
        {
            Debug.LogError(String.Format("ItemImage Image component not assigned on {0}", gameObject.name));
        }
    }
    
    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
}
