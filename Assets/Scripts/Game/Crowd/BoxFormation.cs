using System.Collections.Generic;
using UnityEngine;

public class BoxFormation : FormationBase {
    [SerializeField] private int _unitWidth = 5;
    [SerializeField] private int _unitDepth = 5;
    [SerializeField] private bool _hollow = true;
    [SerializeField] private float _nthOffset = 2;

    public override List<Vector3> EvaluatePoints()
    {
        var tempList = new List<Vector3>();
        var middleOffset = new Vector3(_unitWidth * 0.5f, 0, _unitDepth * 0.5f);

        for (var x = 0; x < _unitWidth; x++) 
        {
            for (var z = 0; z < _unitDepth; z++) 
            {
                if (_hollow && x != 0 && x != _unitWidth - 1 && z != 0 && z != _unitDepth - 1) continue;
                var pos = new Vector3(x + (z % 2 == 0 ? 0 : _nthOffset), 0, z);

                pos -= middleOffset;

                pos += GetNoise(pos);

                pos *= Spread;
                tempList.Add(pos);
            }
        }

        return tempList;
    }

    public void SetWidthAndDepth(int width, int depth)
    {
        _unitWidth = width;
        _unitDepth = depth;
    }
}