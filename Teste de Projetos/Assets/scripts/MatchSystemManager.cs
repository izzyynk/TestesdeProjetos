using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;

public class MatchSystemManager : MonoBehaviour
{
    public List<Material> _colorMaterials;
    private List<MatchEntity> _matchEntities;
    private int _targeMatchCount;
    private int _currentMatchCount;
    void Start()
    {
        _matchEntities = transform.GetComponentsInChildren<MatchEntity>().ToList();
        _targeMatchCount = _matchEntities.Count;
        SetEntityColors();
        RandomizeMovablePairPlacement();
    }

    void SetEntityColors()
    {
        Shuffle(_colorMaterials);

        for(int i = 0; i < _matchEntities.Count; i++)
        {
            _matchEntities[i].SetMaterialToPairs(_colorMaterials[i]);
        }
    }

    void RandomizeMovablePairPlacement()
    {
        List<Vector3> movablePairPositions = new List<Vector3>();

        for(int i = 0; i < _matchEntities.Count; i++)
        {
            movablePairPositions.Add(_matchEntities[i].GetMovablePairPosition());
        }

        Shuffle(movablePairPositions);

        for(int i = 0; i< _matchEntities.Count; i++)
        {
            _matchEntities[i].SetMovablePairPosition(movablePairPositions[i]);
        }
    }

    public void NewMatchRecord(bool MatchConnected)
    {
        if(MatchConnected)
        {
            _currentMatchCount++;
        }
        else
        {
            _currentMatchCount--;
        }
        Debug.Log("Currently, there are" + _currentMatchCount + " matches.");

        if(_currentMatchCount == _targeMatchCount)
        {
            Debug.Log("WEll DOne! All PAIRED");
        }
    }


    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
