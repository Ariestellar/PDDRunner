using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Sign")]
public class Sign : ScriptableObject
{  
    [SerializeField] private SignPriorityWay _signPriorityWay;    
    [SerializeField] private Sprite _signLabel;   
    [SerializeField] private Sprite _signPriorityLabel;

    public SignPriorityWay SignPriorityWayValue => _signPriorityWay;
    public Sprite SignPriorityLabel => _signPriorityLabel;
    public Sprite SignLabel => _signLabel;
}
