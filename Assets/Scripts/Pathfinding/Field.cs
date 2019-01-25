using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    public int Height;
    public int Width;

    [SerializeField]
    private Field[] _neighbors = new Field[8];
    public Field[] Neighbors { get { return _neighbors; } set { _neighbors = value; } }

    [SerializeField]
    private int _x;
    public int X { get { return _x; } set { _x = value; } }
    [SerializeField]
    private int _y;
    public int Y { get { return _y; } set { _y = value; } }
    

    public Field GetNeighbor(Direction direction)
    {
        return _neighbors[(int)direction];
    }

    public Direction GetDirection(Field neighbor)
    {
        for (int direction = 0; ; direction++) if (_neighbors[direction] == neighbor) return (Direction)direction;
    }
}
