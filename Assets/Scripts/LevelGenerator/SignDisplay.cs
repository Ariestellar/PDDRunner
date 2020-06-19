using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _signLabel;
    [SerializeField] private SpriteRenderer _signLabelMask;
    [SerializeField] private SpriteRenderer _signPriorityLabel;
    [SerializeField] private SpriteRenderer _signPriorityLabelMask;
        
    public void Init(Sign sign)
    {
        SetLabel(sign.SignLabel);
        if (sign.SignPriorityLabel != null)
        {
            SetPriorityLabel(sign.SignPriorityLabel);
        }        
    }

    private void SetLabel(Sprite signLabel)
    {
        _signLabel.sprite = signLabel;
        _signLabelMask.sprite = signLabel;
    }

    private void SetPriorityLabel(Sprite signPriorityLabel)
    {
        _signPriorityLabel.sprite = signPriorityLabel;
        _signPriorityLabelMask.sprite = signPriorityLabel;
    }
}
