using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class Item : Actor
    {
        private int nowEffect; // 현재 아이템 효과
        public enum ItemEffect
        {
            recover,
            powerUp,
            invincible,
        }
        public void InitItem() // 아이템 정보 초기화 : 초기화 하면서 랜덤한 효과 획득
        {
            xPos = random.Next(1, GameManager.Instance.GetSetting.GetWidth - 2);
            if (xPos % 2 != 0) xPos -= 1; // 2의 배수가 아니면 1 빼주기
            yPos = random.Next(1, GameManager.Instance.GetSetting.GetHeight - 11);

            myShape = '♥';
            myColor = ConsoleColor.Green;
            nowEffect = random.Next((int)ItemEffect.recover, (int)ItemEffect.invincible + 1);
        }
        public void CheckItemCollision() // 아이템 충돌 체크하고 해당 효과 플레이어에게 적용
        {
            if(xPos == GameManager.Instance.GetPlayer.Position_X && yPos == GameManager.Instance.GetPlayer.Position_Y)
            {
                switch(nowEffect)
                {
                    case (int)ItemEffect.recover: // 회복 아이템이면
                        {
                            GameManager.Instance.GetPlayer.HP = GameManager.Instance.GetPlayer.MaxHp; // 체력 100 % 회복
                            InitItem(); // 먹었으면 위치 초기화
                            break;
                        }
                    case (int)ItemEffect.powerUp: // 파워 업이면
                        {
                            GameManager.Instance.GetPlayer.Power = GameManager.Instance.GetPlayer.TempPower; // 공격력 상승
                            GameManager.Instance.GetPlayer.IsPowerUp = true; // 상태 변경
                            InitItem();
                            break;
                        }
                    case (int)ItemEffect.invincible: // 무적이면
                        {
                            GameManager.Instance.GetPlayer.IsInvincible = true; // 상태 변경
                            InitItem();
                            break;
                        }
                }
            }
        }
    }
}
