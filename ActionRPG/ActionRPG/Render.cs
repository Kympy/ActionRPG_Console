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

        private char renderShape;
        private ConsoleColor renderColor;
        public void InitRender()
        {
            maxX = GameManager.Instance.GetSetting.GetWidth;
            maxY = GameManager.Instance.GetSetting.GetHeight - 10;
        }
        public void RenderInfo() // 정보 표시
        {
            // 좌측 상단에 표시
            Console.SetCursorPosition(0, 0); Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("레벨 : " + GameManager.Instance.GetPlayer.Level);
            Console.Write("HP : " + GameManager.Instance.GetPlayer.HP + " / " + GameManager.Instance.GetPlayer.MaxHp + "  ");
            Console.WriteLine("EXP : " + GameManager.Instance.GetPlayer.Exp + " / " + GameManager.Instance.GetPlayer.MaxExp + "  ");
            Console.WriteLine("공격력 : " + GameManager.Instance.GetPlayer.Power + "  " + "공격 범위 : " + GameManager.Instance.GetPlayer.Range + "  ");
            // 우측 상단에 표시
            Console.SetCursorPosition(maxX / 4 * 3, 0); // 4분의 3 지점부터 
            Console.Write("스테이지 : " + GameManager.Instance.Stage);
            Console.SetCursorPosition(maxX / 4 * 3, 1);
            Console.Write("몬스터 : " + GameManager.Instance.CurrentMonsterCount + " / " + GameManager.Instance.MaxMonsterCount + "  ");
            Console.SetCursorPosition(maxX / 4 * 3, 2);
            Console.Write("보스 : " + GameManager.Instance.CurrentBossCount + " / " + GameManager.Instance.MaxBossCount + "  ");
            // 아이템 획득 시 중간에 표시
            if(GameManager.Instance.GetPlayer.IsPowerUp)
            {
                Console.SetCursorPosition(maxX / 5 * 2, 0);
                Console.Write("공격력 3배 상승! " + GameManager.Instance.GetPlayer.PowerTime + " 초  ");
            }
            else
            {
                Console.SetCursorPosition(maxX / 5 * 2, 0);
                Console.Write("                        ");
            }
            if(GameManager.Instance.GetPlayer.IsInvincible)
            {
                Console.SetCursorPosition(maxX / 5 * 2, 1);
                Console.Write("무적! " + GameManager.Instance.GetPlayer.InvincibleTime + " 초  ");
            }
            else
            {
                Console.SetCursorPosition(maxX / 5 * 2, 1);
                Console.Write("                   ");
            }
        }
        public void RenderScreen() // 맵 화면 그리기
        {
            for (int j = 0; j < maxY; j++)
            {
                for (int i = 0; i < maxX; i+=2) // 특수문자라 2칸씩 그림
                {
                    CheckPosition(i, j); // 현재 위치에 그려야 할 것이 무엇이니?
                    Console.SetCursorPosition(i, j + 4); // 정보 표시줄 때문에 4줄 밑에서 그리기 시작
                    Console.ForegroundColor = renderColor; // 렌더링 할 대상의 색깔
                    Console.Write(renderShape); // 렌더링 할 대상의 모양
                }
            }
        }
        private void CheckPosition(int x, int y) // 포지션에 그려야 할 대상이 무엇인지?
        {
            if (x == GameManager.Instance.GetPlayer.Position_X && y == GameManager.Instance.GetPlayer.Position_Y) // 플레이어 위치라면
            {
                renderShape = GameManager.Instance.GetPlayer.Shape; // 플레이어 렌더링
                renderColor = GameManager.Instance.GetPlayer.Color;
            }
            else if(x == GameManager.Instance.GetPlayer.RangePosition_X && y == GameManager.Instance.GetPlayer.RangePosition_Y
                && GameManager.Instance.GetPlayer.IsAttack) // 플레이어가 공격중이고, 공격범위라면
            {
                renderShape = GameManager.Instance.GetPlayer.AttackShape; // 공격 애니메이션 렌더링
                renderColor = GameManager.Instance.GetPlayer.AttackColor;
            }
            else if(x == GameManager.Instance.Item.Position_X && y == GameManager.Instance.Item.Position_Y) // 아이템이라면
            {
                renderShape = GameManager.Instance.Item.Shape; // 아이템 모양과 색깔 가져오기~
                renderColor = GameManager.Instance.Item.Color;
            }
            else // 플레이어가 아니면
            {
                for(int i = 0; i < GameManager.Instance.MaxMonsterCount; i++) // 몬스터
                {
                    if (x == GameManager.Instance.GetMonsterList[i].Position_X && y == GameManager.Instance.GetMonsterList[i].Position_Y)
                    {
                        renderShape = GameManager.Instance.GetMonsterList[i].Shape;
                        renderColor = GameManager.Instance.GetMonsterList[i].Color;
                        return;
                    }
                }
                for(int  i = 0; i < GameManager.Instance.MaxBossCount; i++) // 보스
                {
                    if (x == GameManager.Instance.GetBossList[i].Position_X && y == GameManager.Instance.GetBossList[i].Position_Y)
                    {
                        renderShape = GameManager.Instance.GetBossList[i].Shape;
                        renderColor = GameManager.Instance.GetBossList[i].Color;
                        return;
                    }
                }
                renderShape = '　'; // 아무것도 아니면 공백
                renderColor = ConsoleColor.White;
            }
        }
    }
}
