using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class Monster : Actor
    {
        //private char attackShape = '●';
        //public char AttackShape { get { return attackShape; } }
        //private ConsoleColor attackColor = ConsoleColor.Red;
        //public ConsoleColor AttackColor { get { return attackColor; } }
        public virtual void InitInfo()
        {
            hp = GameManager.Instance.Stage;
            maxHp = hp;
            power = GameManager.Instance.Stage;
            exp = GameManager.Instance.Stage;
            range = 2;
            myShape = '◆';
            myColor = ConsoleColor.DarkMagenta;
            isDead = false;
            xPos = random.Next(0, GameManager.Instance.GetSetting.GetWidth);
            if(xPos % 2 != 0) // 2로 나누어 떨어지지 않는다면 >> 2단위로 X 좌표가 이루어져있기 때문
            {
                xPos -= 1;
            }
            yPos = random.Next(0, GameManager.Instance.GetSetting.GetHeight - 11);
        }
        public virtual void Move()
        {
            if(isDead == false) // 살아있다면
            {
                // 상하 이동 ? 좌우 이동?
                if (random.Next(0, 2) == 0)  // 상하 이동
                {
                    // 위로? 아래로?
                    if (random.Next(0, 2) == 0) // 위로
                    {
                        yPos -= 1;
                        if (yPos < 0) yPos = 0;
                    }
                    else
                    {
                        yPos += 1; // 아래로
                        if(yPos > GameManager.Instance.GetSetting.GetHeight - 11) yPos = GameManager.Instance.GetSetting.GetHeight - 11;
                    }
                }
                else // 좌우 이동
                {
                    // 왼쪽? 오른쪽?
                    if (random.Next(0, 2) == 0) // 좌로
                    {
                        xPos -= 2;
                        if (xPos < 0) xPos = 0;
                    }
                    else
                    {
                        xPos += 2; // 우로
                        if(xPos > GameManager.Instance.GetSetting.GetWidth - 2) xPos = GameManager.Instance.GetSetting.GetWidth - 2;
                    }
                }
            }
        }
        public virtual void Attack()
        {
            if(isDead == false)
            {
                if (MathF.Abs(GameManager.Instance.GetPlayer.Position_X - xPos) <= range) // 플레이어가 x 축 사거리 이내
                {
                    if (MathF.Abs(GameManager.Instance.GetPlayer.Position_Y - yPos) <= range) // 플레이어가 y축 사거리 이내
                    {
                        GameManager.Instance.GetPlayer.DecreaseHP(power); // 플레이어 체력 감소
                    }
                }
            }
        }
        public virtual void DeadFlag() // 사망 판정과 체력 업데이트
        {
            if(isDead == false)
            {
                if (hp > maxHp * 0.8) // 80퍼 초과 시
                {
                    myColor = ConsoleColor.DarkMagenta; // 기본
                }
                else if (hp > maxHp * 0.4) // 40퍼 초과 시
                {
                    myColor = ConsoleColor.Yellow; // 노란색
                }
                else
                {
                    myColor = ConsoleColor.Red; // 40퍼 이하 빨간색
                    if (hp <= 0) // 0 이되면
                    {
                        isDead = true; // 죽음
                        GameManager.Instance.CurrentMonsterCount--;
                        GameManager.Instance.GetPlayer.Exp += exp;
                        myColor = ConsoleColor.DarkGray; // 회색
                    }
                }
            }
        }
        public virtual void DecreaseHP(float playerPower)
        {
            hp -= playerPower;
        }
    }
}
