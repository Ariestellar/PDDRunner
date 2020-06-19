using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SignsPlacementVariants")]
public class SignsPlacementVariants : ScriptableObject
{
    [SerializeField] private List<SignPriorityWay> _arrangement; 
    
    public List<SignPriorityWay> Arrangement => _arrangement;    
}
