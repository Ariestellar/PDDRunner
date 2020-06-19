using GameParametrs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SignsCreator : MonoBehaviour
{
    [SerializeField] private GameObject _templateSign;    
    [SerializeField] private List<SignPriorityWay> _possibleRoadSign;
    [SerializeField] private List<SignsPlacementVariants> _fourWayCrossroad;//Пока реализованна генерация для 4ех стороннего перекрестка

    private List<SignPriorityWay> _arrangementValuesSigns;
    private List<Sign> _varietiesSigns;

    public List<SignPriorityWay> ArrangementValuesSigns => _arrangementValuesSigns;
       
    private void Awake()
    {
        _varietiesSigns = Resources.LoadAll<Sign>("Signs").ToList();
    }

    public void Create(List<Transform> positionSigns)
    {
        _arrangementValuesSigns = GenerateSignsArrangementValues();
        if (_arrangementValuesSigns[0] != SignPriorityWay.unsigned)
        {
            for (int i = 0; i < positionSigns.Count; i++)
            {
                GameObject currentSign = Instantiate(_templateSign, positionSigns[i]);
                currentSign.GetComponent<SignDisplay>().Init(GetValueSign(_arrangementValuesSigns[i]));
            }
        }
    }

    private Sign GetValueSign(SignPriorityWay signPriorityWay)
    {
        Sign sign = null;
        foreach (var signValue in _varietiesSigns)
        {
            if (signValue.SignPriorityWayValue == signPriorityWay)
            {
                sign = signValue;                
            }            
        }
        return sign;
    }

    private List<SignPriorityWay> GenerateSignsArrangementValues()
    {
        SignPriorityWay firstSignValue = GetRandomSign();
        return ArrangeSignsValues(firstSignValue, GetVariantArrangementSigns(firstSignValue));
    }

    private List<SignPriorityWay> ArrangeSignsValues(SignPriorityWay firstSignValue, List<SignPriorityWay> variantArrangementSigns)
    {
        List<SignPriorityWay> arrangementSigns = new List<SignPriorityWay>();

        int indexSignInPlacement = variantArrangementSigns.IndexOf(firstSignValue);

        for (int i = 0; i < variantArrangementSigns.Count; i++)
        {
            arrangementSigns.Add(variantArrangementSigns[indexSignInPlacement]);
            indexSignInPlacement = GetNextIndexSignInPlacement(indexSignInPlacement, variantArrangementSigns.Count);
        }
        return arrangementSigns;
    }

    private SignPriorityWay GetRandomSign()
    {
        int firstSignValueIndex = UnityEngine.Random.Range(0, _possibleRoadSign.Count);
        return _possibleRoadSign[firstSignValueIndex];
    }

    private int GetNextIndexSignInPlacement(int currentIndexSignInPlacement, int totalIndexSignInPlacement)
    {
        return (currentIndexSignInPlacement + 1) % totalIndexSignInPlacement;
    }

    private List<SignPriorityWay> GetVariantArrangementSigns(SignPriorityWay signValue)
    {
        List<SignPriorityWay> arrangementSignsVariant = null;
        foreach (var signsPlacementVariants in _fourWayCrossroad)
        {
            if (signsPlacementVariants.Arrangement.Contains(signValue) == true)
            {
                arrangementSignsVariant = signsPlacementVariants.Arrangement;
                break;
            }
        }        
        return arrangementSignsVariant;
    }    
}
