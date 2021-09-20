using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private int _xCoord;
    public int xCoord
    {
        get
        {
            return _xCoord;
        }
        set
        {
            _xCoord = value;
            UpdatePosition();
        }
    }

    [SerializeField]
    private int _yCoord;
    public int yCoord
    {
        get
        {
            return _yCoord;
        }
        set
        {
            _yCoord = value;
            UpdatePosition();
        }
    }

    [SerializeField]
    private int _z;
    public int z
    {
        get
        {
            return _z;
        }
        set
        {
            _z = value;
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        transform.position = new Vector3(_xCoord * 10, _yCoord * 10, 0);
    }

    public bool CoordinateMatch(Tile t)
    {
        return this.xCoord == t.xCoord && this.yCoord == t.yCoord;
    }

    override public string ToString()
    {
        return "[" + (_xCoord + 1) + ", " + (_yCoord + 1) + "]";
    }
}
