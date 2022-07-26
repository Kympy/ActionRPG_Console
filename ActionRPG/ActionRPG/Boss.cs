using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class Boss : Monster
    {
        public override void InitInfo() // 보스 정보 초기화
        {
            base.InitInfo();
            hp = GameManager.Instance.Stage * 5;
            maxHp = hp;
            power = GameManager.Instance.Stage * 5;
            exp = GameManager.Instance.Stage * 2;
            range = 3;
            myShape = '★';
            myColor = ConsoleColor.Magenta;
            isDead = false;
        }
        public override void Move()
        {
            base.Move();
        }
        public override void Attack()
        {
            base.Attack();
        }
        public override void DeadFlag()
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
                        GameManager.Instance.CurrentBossCount--;
                        myColor = ConsoleColor.DarkGray; // 회색
                    }
                }
            }
        }
        public override void DecreaseHP(float playerPower)
        {
            base.DecreaseHP(playerPower);
        }
    }
}
