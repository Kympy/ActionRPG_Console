using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class InputManager : SingleTon<InputManager>
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int myKey);
        private short myKey = 0;
        public void GetKey()
        {
            myKey = 0;
            if (Console.KeyAvailable) // 키입력이 존재한다면
            {
                myKey = GetAsyncKeyState((int)ConsoleKey.W); // 상
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameManager.Instance.GetPlayer.SwitchDirection(Player.PlayerState.up);
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.A); // 좌
                if ((myKey & 0x8000) == 0x8000) // 좌
                {
                    GameManager.Instance.GetPlayer.SwitchDirection(Player.PlayerState.left);
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.S); // 하
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameManager.Instance.GetPlayer.SwitchDirection(Player.PlayerState.down);
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.D); // 우
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameManager.Instance.GetPlayer.SwitchDirection(Player.PlayerState.right);
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.Spacebar); // 공격
                if ((myKey & 0x8000) == 0x8000)
                {
                    if(GameManager.Instance.GetPlayer.IsAttack == false)
                    {
                        GameManager.Instance.GetPlayer.AttackStart(); // 공격 시작
                    }
                }
                else GameManager.Instance.GetPlayer.IsAttack = false;

                myKey = GetAsyncKeyState((int)ConsoleKey.F); // 스테이지 업 치트키
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameManager.Instance.StageUP();
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.R); // 레벨 업 치트키
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameManager.Instance.GetPlayer.Exp++;
                }
            }
        }
    }
}
