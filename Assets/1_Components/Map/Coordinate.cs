using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Map
{
    [System.Serializable]
    public struct Coordinate
    {
        public float x;
        public float y;

        public Coordinate(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        


        public static Coordinate zero = new (0, 0);

        public static Coordinate Lerp(Coordinate a, Coordinate b, float t)
        {
            t = Mathf.Clamp01(t);
            return LerpUnclamped(a, b, t);
        }
        public static Coordinate LerpUnclamped(Coordinate a, Coordinate b, float t)
        {
            return new Coordinate(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }


        public override string ToString() => $"({x}, {y})";

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2);
        }

        public override bool Equals(object other)
        {
            if (!(other is Coordinate))
            {
                return false;
            }

            return Equals((Coordinate)other);
        }
        public bool Equals(Coordinate other)
        {
            return x == other.x && y == other.y;
        }



        public static Coordinate operator +(Coordinate a, Coordinate b) => new (a.x + b.x, a.y + b.y);

        public static Coordinate operator -(Coordinate a, Coordinate b) => new (a.x - b.x, a.y - b.y);

        public static bool operator ==(Coordinate lhs, Coordinate rhs)
        {
            float num = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            return num * num + num2 * num2 < 9.99999944E-11f;
        }

        public static bool operator !=(Coordinate lhs, Coordinate rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator Coordinate(Vector2 v)
        {
            return new Coordinate(v.x, v.y);
        }

        public static implicit operator Vector2(Coordinate v)
        {
            return new Vector2(v.x, v.y);
        }
    }

}