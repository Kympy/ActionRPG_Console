using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class GameSetting
    {
        private int width = 80;
        public int GetWidth { get { return width; } }
        private int height = 50;
        public int GetHeight { get { return height; } }

        public void InitWindow() // 윈도우 크기 설정
        {
            Console.SetWindowSize(width, height);
            Console.BufferWidth = width;
            Console.BufferHeight = height;
            Console.Title = "Action RPG";
        }
    }
}
