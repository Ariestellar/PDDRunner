using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignsCreator : MonoBehaviour
{
    [SerializeField] private GameObject _templateSign;
    [SerializeField] private List<SignPriorityWay> _playerRoadSign;   
     
    [SerializeField] private Sprite _signLabelMainWay;
    [SerializeField] private Sprite _signLabelMainWayLeftTurn;
    [SerializeField] private Sprite _signLabelMainWayRightTurn;

    [SerializeField] private Sprite _signLabelMinorWay;
    [SerializeField] private Sprite _signLabelMinorWayLeftTurn;
    [SerializeField] private Sprite _signLabelMinorWayRightTurn;

    [SerializeField] private List<SignPriorityWay> _possibleRoadSign;

    [SerializeField] private List<SignPriorityWay> _arrangementSignsVariantWhereMainWayStraight;
    [SerializeField] private List<SignPriorityWay> _arrangementSignsVariantWhereMainWayCurve;

    public List<SignPriorityWay> Create(List<Transform> positionSigns)
    {
        List<SignPriorityWay> signPriorityWay = GenerateRoadSign();
        
        if (signPriorityWay[0] != SignPriorityWay.unsigned)//Создаем знаки (если они подразумеваются на перекрестке)
        {
            PrioritizeRoadSigns(CreateListSign(positionSigns), signPriorityWay);
        }

        return signPriorityWay;
    }

    private List<Sign> CreateListSign(List<Transform> positionSigns)
    {
        List<Sign> signs = new List<Sign>();
        for (int i = 0; i < positionSigns.Count; i++)
        {
            GameObject signGO = Instantiate(_templateSign, positionSigns[i]);
            signs.Add(signGO.GetComponent<Sign>());
        }
        return signs;
    }
        
    private void PrioritizeRoadSigns(List<Sign> signs, List<SignPriorityWay> playerRoadSign)
    {       
        for (int i = 0; i < signs.Count; i++)
        {
            LabelRoadSign(signs[i], playerRoadSign[i]);
        }
    }

    private List<SignPriorityWay> GenerateRoadSign()
    {
        List<SignPriorityWay> signPriorityWay;
        int firstSignValueIndex = Random.Range(0, _possibleRoadSign.Count);
        SignPriorityWay firstSignValue = _possibleRoadSign[firstSignValueIndex];
        List<SignPriorityWay> variantArrangementSigns = GetVariantArrangementSigns(firstSignValue);
        int indexSignInPlacement = variantArrangementSigns.IndexOf(firstSignValue);
        signPriorityWay = new List<SignPriorityWay>(variantArrangementSigns.Count);
        for (int i = 0; i < variantArrangementSigns.Count; i++)
        {
            if (firstSignValue != SignPriorityWay.unsigned)
            {
                signPriorityWay.Add(variantArrangementSigns[indexSignInPlacement]);
                indexSignInPlacement = GetNextIndexSignInPlacement(indexSignInPlacement, variantArrangementSigns.Count);
            }
            else
            {
                signPriorityWay.Add(firstSignValue);
            }
        }        
        return signPriorityWay;
    }

    private int GetNextIndexSignInPlacement(int currentIndexSignInPlacement, int totalIndexSignInPlacement)
    {
        return (currentIndexSignInPlacement + 1) % totalIndexSignInPlacement;
    }

    private List<SignPriorityWay> GetVariantArrangementSigns(SignPriorityWay signValue)
    {
        List<SignPriorityWay> arrangementSignsVariant = _arrangementSignsVariantWhereMainWayCurve.IndexOf(signValue) == -1 ? _arrangementSignsVariantWhereMainWayStraight : _arrangementSignsVariantWhereMainWayCurve;
        return arrangementSignsVariant;
    }

    //Временное решение "в лоб", отрефакторить
    private void LabelRoadSign(Sign sign, SignPriorityWay playerRoadSign)
    {

        if ((int)playerRoadSign == 1 || ((int)playerRoadSign > 2 && (int)playerRoadSign < 5))
        {
            sign.SetLabel(_signLabelMainWay);
            if (playerRoadSign == SignPriorityWay.mainLeftTurn)
            {
                sign.SetPriorityLabel(_signLabelMainWayLeftTurn);
            }
            else if (playerRoadSign == SignPriorityWay.mainRightTurn)
            {
                sign.SetPriorityLabel(_signLabelMainWayRightTurn);
            }
        }
        else if ((int)playerRoadSign == 2 || ((int)playerRoadSign > 2 && (int)playerRoadSign > 4))
        {
            sign.SetLabel(_signLabelMinorWay);
            if (playerRoadSign == SignPriorityWay.minorLeftTurn)
            {
                sign.SetPriorityLabel(_signLabelMinorWayLeftTurn);
            }
            else if (playerRoadSign == SignPriorityWay.minorRightTurn)
            {
                sign.SetPriorityLabel(_signLabelMinorWayRightTurn);
            }
        }
    }
}
