using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class Render
    {
        private int maxX; // 가로
        private int maxY; // 세로

        public Render()
        {
            maxX = GameManager.Instance.GetSetting.GetWidth;
            maxY = GameManager.Instance.GetSetting.GetHeight;
        }
        public void RenderInfo()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("레벨 : " + GameManager.Instance.GetPlayer.Level);
            Console.Write("HP : " + GameManager.Instance.GetPlayer.HP + " / " + GameManager.Instance.GetPlayer.MaxHp + "  ");
            Console.WriteLine("EXP : " + GameManager.Instance.GetPlayer.Exp + " / " + GameManager.Instance.GetPlayer.MaxExp);
        }
        public void RenderScreen() // 화면 그리기
        {
            for(int i = 0; i < maxX; i+=2)
            {
                for(int j = 10; j < maxY; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write('□');
                }
            }
        }
        private void CheckPosition(int x, int y)
        { 
            
        }
    }
}
