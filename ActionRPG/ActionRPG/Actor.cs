using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class Actor
    {
        protected Random random = new Random();
        protected float hp; // 체력
        public float HP { get { return hp; } set { hp = value; } }
        protected float maxHp; // 최대 체력
        public float MaxHp { get { return maxHp; } set { maxHp = value; } }
        protected float range;
        public float Range { get { return range; } set { range = value; } } // 공격 범위
        protected float power;
        public float Power { get { return power; } set { power = value; } } // 공격력
        protected float exp;
        public float Exp { get { return exp; } set { exp = value; } } // 플레이어 에게는 현재 경험치 / 몬스터, 아이템 에게는 주는 경험치

        protected bool isDead;
        public bool IsDead { get { return isDead; } set { isDead = value; } } // 사망 여부

        protected int xPos;
        public int Position_X { get { return xPos; } set { xPos = value; } } // 위치 좌표

        protected int yPos;
        public int Position_Y { get { return yPos; } set { yPos = value; } } // 위치 좌표

        protected char myShape;
        public char Shape { get { return myShape; } set { myShape = value; } } // 액터의 모양

        protected ConsoleColor myColor;
        public ConsoleColor Color { get { return myColor; } set { myColor = value; } } // 액터의 색상
    }
}
