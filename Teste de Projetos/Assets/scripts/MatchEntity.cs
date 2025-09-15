using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchEntity : MonoBehaviour
{
    public MatchFeedBack _feedback;
    public MoveWithMouseDrag _movablePair;
    public ScaleWithPivots _scaledObject;
    public Renderer _fixedPairRenderer;
    public MatchSystemManager _matchSystemManager;
    
    private bool _matched;
    public Vector3 GetMovablePairPosition()
    {
        return _movablePair.GetPosition();
    }

    public void SetMovablePairPosition(Vector3 NewMovablePairPosition)
    {
        _movablePair.SetInitialPosition(NewMovablePairPosition);
    }

    public void SetMaterialToPairs(Material PairMaterial)
    {
        _scaledObject.GetComponent<Renderer>().material = PairMaterial;
        _fixedPairRenderer.material = PairMaterial;
    }

    public void PairObjetctInteraction(bool IsEnter, MoveWithMouseDrag movable)
    {
        if(IsEnter && !_matched)
        {
            _matched = (movable == _movablePair);
            if(_matched)
            {
                _matchSystemManager.NewMatchRecord(_matched);
                _feedback.ChangeMaterialWithMatch(_matched);
            }
        }
        else if (!IsEnter && _matched)
        {
            _matched = !(movable == _movablePair);
            if(!_matched)
            {
                _matchSystemManager.NewMatchRecord(_matched);
                _feedback.ChangeMaterialWithMatch(_matched);
            }
        }
    }
}
