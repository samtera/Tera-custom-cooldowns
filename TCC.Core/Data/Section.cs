﻿using System;
using System.Windows;

namespace TCC.Data
{
    public class Section
    {
        public uint Id { get; }
        public uint NameId { get; }
        public string MapId { get; }
        public double Top { get; }
        public double Left { get; }
        public double Width { get; }
        public double Height { get; }
        public bool IsDungeon { get; }
        public double Scale
        {
            get
            {
                return Width / (double)Application.Current.FindResource("MapWidth");
            }
        }
        public Section(uint sId, uint sNameId, string mapId, bool dg, double top = 0, double left = 0, double width = 0, double height = 0)
        {
            Id = sId;
            NameId = sNameId;
            MapId = mapId;
            Top = top;
            Left = left;
            Width = width;
            Height = height;
            IsDungeon = dg;
        }

        public bool ContainsPoint(float x, float y)
        {
            var matchesY = y > Left && y < Width + Left;
            var matchesX = x < Top && x > Top - Height;
            if (matchesX & matchesY)
            {
            }
            return matchesX && matchesY;
        }
    }
}
