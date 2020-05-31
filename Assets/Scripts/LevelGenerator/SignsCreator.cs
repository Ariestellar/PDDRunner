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

    public List<SignPriorityWay> Create(List<Transform> positionSigns)
    {
        List<SignPriorityWay> signPriorityWay = GeneratePlayerRoadSign();
        
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

    //Временное решение "в лоб", отрефакторить
    private List<SignPriorityWay> GeneratePlayerRoadSign()
    {
        int signMainOption = Random.Range(0, 7);
        List<SignPriorityWay> signsPriorityWay = new List<SignPriorityWay>(4);

        signsPriorityWay.Insert(0, (SignPriorityWay)signMainOption);
        if (signsPriorityWay[0] == SignPriorityWay.main)
        {
            signsPriorityWay.Insert(1, SignPriorityWay.minor);
            signsPriorityWay.Insert(2, SignPriorityWay.main);
            signsPriorityWay.Insert(3, SignPriorityWay.minor);            
        }
        else if (signsPriorityWay[0] == SignPriorityWay.minor)
        {
            signsPriorityWay.Insert(1, SignPriorityWay.main);
            signsPriorityWay.Insert(2, SignPriorityWay.minor);
            signsPriorityWay.Insert(3, SignPriorityWay.main);
        }
        else if (signsPriorityWay[0] == SignPriorityWay.mainRightTurn)
        {
            signsPriorityWay.Insert(1, SignPriorityWay.mainLeftTurn);
            signsPriorityWay.Insert(2, SignPriorityWay.minorRightTurn);
            signsPriorityWay.Insert(3, SignPriorityWay.minorLeftTurn);
        }
        else if (signsPriorityWay[0] == SignPriorityWay.mainLeftTurn)
        {
            signsPriorityWay.Insert(1, SignPriorityWay.minorRightTurn);
            signsPriorityWay.Insert(2, SignPriorityWay.minorLeftTurn);
            signsPriorityWay.Insert(3, SignPriorityWay.mainRightTurn);
        }
        else if (signsPriorityWay[0] == SignPriorityWay.minorLeftTurn)
        {
            signsPriorityWay.Insert(1, SignPriorityWay.mainRightTurn);
            signsPriorityWay.Insert(2, SignPriorityWay.mainLeftTurn);
            signsPriorityWay.Insert(3, SignPriorityWay.minorRightTurn);
        }
        else if (signsPriorityWay[0] == SignPriorityWay.minorRightTurn)
        {
            signsPriorityWay.Insert(1, SignPriorityWay.minorLeftTurn);
            signsPriorityWay.Insert(2, SignPriorityWay.mainRightTurn);
            signsPriorityWay.Insert(3, SignPriorityWay.mainLeftTurn);
        }
        else
        {
            signsPriorityWay.Insert(1, SignPriorityWay.unsigned);
            signsPriorityWay.Insert(2, SignPriorityWay.unsigned);
            signsPriorityWay.Insert(3, SignPriorityWay.unsigned);
        }
        return signsPriorityWay;
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
