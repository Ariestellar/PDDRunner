using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int _countActionsGiveWay;

    public int CountActionsGiveWay => _countActionsGiveWay;

    public void IncreaseCountActionsGiveWay()
    {
        _countActionsGiveWay += 1;
    }

    public void ResetCountActionsGiveWay()//DistanceMeter -> UnityEvent DroveCrossroad
    {
        _countActionsGiveWay = 0;
    }
}
