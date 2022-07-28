using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class Player : Actor
    {
        public enum PlayerState // 플레이어 이동 방향 상태
        {
            idle,
            up,
            down,
            left,
            right,
        }
        private int level; // 레벨
        public int Level { get { return level; }}
        private float maxExp; // 최대 경험치
        public float MaxExp { get { return maxExp; } }
        private float tempPower; // 임시 공격력 >> 아이템 적용 시 사용
        public float TempPower { get { return tempPower; } }

        // ========================================================== 공격
        private PlayerState direction; // 공격 애니메이션을 위한 방향
        public PlayerState Direction { get { return direction; }}

        private float rangePositionX; // 공격범위 렌더링 좌표
        public float RangePosition_X { get { return rangePositionX; }}
        private float rangePositionY;
        public float RangePosition_Y { get { return rangePositionY; }}

        private char attackShape = '■'; // 공격 렌더링 모양
        public char AttackShape { get { return attackShape; }}

        private ConsoleColor attackColor = ConsoleColor.Cyan; // 공격 렌더링 색깔
        public ConsoleColor AttackColor { get { return attackColor; }}

        private bool isAttack = false; // 공격 중 인지 변수 (애니메이션과 공격판정에 사용)
        public bool IsAttack { get { return isAttack; } set { isAttack = value; } }

        private bool isPowerup = false; // 파워 업 상태 인지?
        public bool IsPowerUp { get { return isPowerup; } set { isPowerup = value; } }

        private bool isInvincible = false; // 무적 상태인지?
        public bool IsInvincible { get { return isInvincible; } set { isInvincible = value; } }

        private int powerTime = 10; // 파워업 시간
        public int PowerTime { get { return powerTime; }}
        private int invincibleTime = 5; // 무적 시간
        public int InvincibleTime { get { return invincibleTime; }}

        public void InitPlayer()
        {
            hp = 10;
            MaxHp = hp;
            level = 1;
            exp = 0;
            maxExp = 10;
            power = 1;
            range = 1;
            isDead = false;
            tempPower = power * 3;
            myShape = '▲'; // 내 모양
            myColor = ConsoleColor.White; // 내 색상
            xPos = GameManager.Instance.GetSetting.GetWidth / 2; // 중앙에서 시작
            yPos = (GameManager.Instance.GetSetting.GetHeight - 10) / 2;
            direction = PlayerState.up;
            rangePositionX = xPos - 2;
            rangePositionY = yPos;
        }
        public void DeadFlag() // 사망 판정과 체력에 따른 색상 업데이트
        {
            if(isInvincible == false && isPowerup == false) // 아이템 효과 상태가 아닐 때만 색상 지정
            {
                if (hp > maxHp * 0.8) // 80퍼 초과
                {
                    myColor = ConsoleColor.White;
                }
                else if (hp > maxHp * 0.4) // 40퍼 초과
                {
                    myColor = ConsoleColor.Yellow;
                }
                else // 40퍼 이하
                {
                    myColor = ConsoleColor.Red;
                }
            }
            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
            }
        }
        public void DecreaseHP(float monsterPower) // 몬스터 공격력만큼 체력감소
        {
            hp -= monsterPower;
        }
        public void LevelUP() // 레벨 업
        {
            if(exp >= maxExp) // 레벨 업 시 모든 스텟 1.5배
            {
                maxHp = maxHp * 1.5f;
                hp = maxHp;
                level++;
                exp = 0;
                maxExp = maxExp * 1.5f;
                power = power * 1.5f;
                range = range * 1.5f;
                isDead = false;
                tempPower = power * 3f;
                myColor = ConsoleColor.White; // 내 색상
            }
        }
        public void SwitchDirection(PlayerState state) // 이동에 따른 방향 저장 및 캐릭터 모양 변경
        {
            direction = state;
            switch(direction)
            {
                default:
                    {
                        break;
                    }
                case PlayerState.up:
                    {
                        myShape = '▲';
                        yPos -= 1;
                        if(yPos < 0) yPos = 0; // 맵 외부로 벗어나지 못하도록 제한
                        break;
                    }
                case PlayerState.down:
                    {
                        myShape = '▼';
                        yPos += 1;
                        if (yPos > GameManager.Instance.GetSetting.GetHeight - 11) yPos = GameManager.Instance.GetSetting.GetHeight - 11;
                        break;
                    }
                case PlayerState.left:
                    {
                        myShape = '◀';
                        xPos -= 2;
                        if(xPos < 0) xPos = 0;
                        break;
                    }
                case PlayerState.right:
                    {
                        myShape = '▶';
                        xPos += 2;
                        if (xPos > GameManager.Instance.GetSetting.GetWidth - 2) xPos = GameManager.Instance.GetSetting.GetWidth - 2;
                        break;
                    }
            }
        }
        public async void AttackStart() // 마지막 이동 위치(바라보는 방향) 에 따른 공격 시작
        {
            isAttack = true;
            switch (direction) // 마지막 이동 방향에 따라 칼을 휘두르는 모션의 출발점이 바뀜
            {
                default:
                    {
                        break;
                    }
                case PlayerState.up:
                    {
                        rangePositionX = xPos - 2;
                        rangePositionY = yPos;
                        break;
                    }
                case PlayerState.down:
                    {
                        rangePositionX = xPos + 2;
                        rangePositionY = yPos;
                        break;
                    }
                case PlayerState.left:
                    { 
                        rangePositionX = xPos;
                        rangePositionY = yPos + 1;
                        break;
                    }
                case PlayerState.right:
                    {
                        rangePositionX = xPos;
                        rangePositionY = yPos - 1;
                        break;
                    }
            }
            await Task.Delay(100);
            AttackMid();
        }
        public async void AttackMid() // 공격 중
        {
            switch (direction)
            {
                default:
                    {
                        break;
                    }
                case PlayerState.up:
                    {
                        rangePositionX += 2;
                        rangePositionY -= 1;
                        break;
                    }
                case PlayerState.down:
                    {
                        rangePositionX -= 2;
                        rangePositionY += 1;
                        break;
                    }
                case PlayerState.left:
                    {
                        rangePositionX -= 2;
                        rangePositionY -= 1;
                        break;
                    }
                case PlayerState.right:
                    {
                        rangePositionX += 2;
                        rangePositionY += 1;
                        break;
                    }
            }
            await Task.Delay(100);
            AttackEnd();
        }
        public async void AttackEnd() // 공격 종료
        {
            switch (direction)
            {
                default:
                    {
                        break;
                    }
                case PlayerState.up:
                    {
                        rangePositionX += 2;
                        rangePositionY += 1;
                        break;
                    }
                case PlayerState.down:
                    {
                        rangePositionX -= 2;
                        rangePositionY -= 1;
                        break;
                    }
                case PlayerState.left:
                    {
                        rangePositionX += 2;
                        rangePositionY -= 1;
                        break;
                    }
                case PlayerState.right:
                    {
                        rangePositionX -= 2;
                        rangePositionY += 1;
                        break;
                    }
            }
            await Task.Delay(100);
            isAttack = false;
        }
        public void CheckCollision() // 검 휘두르는거에 맞았으면 >> 공격범위 연산때문에 휘두르는건 일정하고 그냥 xy 거리차 범위로 공격을 계산함
        {
            if (isAttack)
            {
                for (int i = 0; i < GameManager.Instance.MaxMonsterCount; i++)
                {
                    if(MathF.Abs(xPos - GameManager.Instance.GetMonsterList[i].Position_X) <= range 
                        && MathF.Abs(yPos - GameManager.Instance.GetMonsterList[i].Position_Y) <= range) // 공격범위 내에 있다면
                    {
                        GameManager.Instance.GetMonsterList[i].DecreaseHP(power); // 해당 몬스터의 체력을 내 공격력만큼 감소시킴
                    }
                }
                for(int i = 0; i < GameManager.Instance.MaxBossCount; i++) // 보스
                {
                    if (MathF.Abs(xPos - GameManager.Instance.GetBossList[i].Position_X) <= range
                         && MathF.Abs(yPos - GameManager.Instance.GetBossList[i].Position_Y) <= range)
                    {
                        GameManager.Instance.GetBossList[i].DecreaseHP(power); // 해당 몬스터의 체력을 내 공격력만큼 감소시킴
                    }
                }
            }
        }
        public void PowerTimer() // 파워업
        {
            if (isPowerup) // 아이템 먹었으면
            {
                powerTime--; // 1초 마다 카운트
                myColor = ConsoleColor.Blue;
                if (powerTime <= 0) // 10초가 넘어가면
                {
                    power = TempPower / 3; // 원래 공격력으로 돌아오고
                    isPowerup = false; // 상태 종료
                }
            }
            else
            {
                powerTime = 10;
            }
        }
        public void InvincibleTimer()
        {
            if (isInvincible) // 무적템 먹었으면
            {
                invincibleTime--;
                myColor = ConsoleColor.Blue;
                if (invincibleTime <= 0)
                {
                    isInvincible = false;
                }
            }
            else
            {
                invincibleTime = 5;
            }
        }
    }
}
