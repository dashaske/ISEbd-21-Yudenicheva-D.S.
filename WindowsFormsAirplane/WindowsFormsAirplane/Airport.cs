﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsAirplane
{
    public class Airport<T, U> where T : class, ITransport
                               where U : class, IDopElements
    {
        //массив объектов
        private T[] _places;

        private int PictureWidth { get; set; }

        private int PictureHeight { get; set; }

        //высота парковочного места
        private const int _placeSizeWidth = 210;

        //ширина парковочного места
        private const int _placeSizeHeight = 133;

        public Airport(int sizes, int pictureWidth, int pictureHeight)
        {
            _places = new T[sizes];
            PictureWidth = pictureWidth;
            PictureHeight = pictureHeight;
            for (int i = 0; i < _places.Length; i++)
            {
                _places[i] = null;
            }
        }

        public static bool operator +(Airport<T, U> p, T bomber)
        {
            for (int i = 0; i < p._places.Length; i++)
            {
                if (p._places[i] == null)
                {
                    p._places[i] = bomber;
                    p._places[i].SetPosition(3 + i / 3 * _placeSizeWidth + 3,
                     i % 3 * _placeSizeHeight + 15, p.PictureWidth,
                    p.PictureHeight);
                    return true;
                }
            }
            return false;
        }
        public static T operator -(Airport<T, U> p, int index)
        {
            if (index >= 0 && index < p._places.Length)
            {
                T warplane = p._places[index];
                p._places[index] = null;
                return warplane;
            }
            return null;
        }

        private int CompareHelper()
        {
            int cnt = 0;
            for (int i = 0; i < _places.Length; ++i)
            {
                if (_places[i] == null)
                {
                    cnt++;
                }
            }
            return cnt;
        }

        public static bool operator ==(Airport<T, U> a, int bom)
        {
            return (a.CompareHelper() == bom);
        }

        public static bool operator !=(Airport<T, U> a, int bom)
        {
            return (a.CompareHelper() != bom);
        }

        //метод отрисовки парковки
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Length; i++)
            {
                {
                    _places[i]?.DrawFly(g);
                }
            }
        }

        //метод отрисовки разметки
        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            //границы праковки
            g.DrawRectangle(pen, 0, 0, (_places.Length / 15) * _placeSizeWidth, 480);
            for (int i = 0; i < _places.Length / 3; i++)
            {
                for (int j = 0; j < 4; ++j)
                {//линия рамзетки места
                    g.DrawLine(pen, i * _placeSizeWidth, j * _placeSizeHeight,
                    i * _placeSizeWidth + 120, j * _placeSizeHeight);

                    g.DrawLine(pen, i * _placeSizeWidth, 0, i * _placeSizeWidth, 400);
                }
            }
        }
    }
}
