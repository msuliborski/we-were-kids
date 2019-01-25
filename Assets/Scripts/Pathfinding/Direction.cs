

public enum Direction
{
    N, NE, E, SE, S, SW, W, NW
}

public static class DirectionExtensions
{
    public static Direction Opposite(this Direction direction)
    {
        return (int)direction < 4 ? (direction + 4) : (direction - 4);
    }

    public static int SmallestDifference(this Direction direction, Direction next)
    {
        int i = 0, j = 0;
        Direction temp = direction;
        while (temp != next)
        {
            i++;
            temp = temp.Next();
        }
        temp = direction;
        while (temp != next)
        {
            j--;
            temp = temp.Previous();
        }

        if (-j < i) return j;
        return i;
    }

    public static Direction Previous(this Direction direction)
    {
        return direction == Direction.N ? Direction.NW : (direction - 1);
    }

    public static Direction Next(this Direction direction)
    {
        return direction == Direction.NW ? Direction.N : (direction + 1);
    }
}
