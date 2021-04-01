using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HandCursor : MonoBehaviour
{
    //[SerializeField] private Sprite _beforeTapHand;
    //[SerializeField] private Sprite _tapHand;
    
    private Image _image;

    public Image Image => _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
}
