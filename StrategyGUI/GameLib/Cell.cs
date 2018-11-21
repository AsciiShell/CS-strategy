﻿// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
namespace GameLib
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
            {
                return false;
            }

            var point = (Point)obj;
            return X == point.X &&
                   Y == point.Y;
        }

        public double GetDistance(Point p)
        {
            return Math.Sqrt((X - p.X) ^ 2 + (Y - p.Y) ^ 2);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public bool IsNearForClose(Point p)
        {
            return GetDistance(p) < 1.5;
        }
        public bool IsNearForLongRange(Point p) 
        {
            return GetDistance(p) <= 4;
        }
        public static bool operator ==(Point a, Point b)
        {
            return a != null && b != null && a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
    }
    public abstract class Cell
    {
        public uint BaseDamage { get; internal set; }
        public Item.Kind kind;
        public Point Location { get; internal set; }
        public uint HP { get; internal set; }
        public Cell(Item.Kind kind, Point location)
        {
            this.kind = kind;
            Location = location;
            BaseDamage = 0;
        }

        public void GetDamage(Cell sender)
        {
            float damage = Item.GetRate(sender.kind, kind) * sender.BaseDamage;
            if (damage > HP)
                HP = 0;
            else
                HP -= (uint)damage;
        }
        public abstract void Attack(Cell target);
        public bool IsAlive() => HP != 0;
    }
}
