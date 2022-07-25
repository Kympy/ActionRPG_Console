using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class Actor
    {
        protected int hp; // 체력
        public int HP { get { return hp; } set { hp = value; } }
        protected int maxHp; // 최대 체력
        public int MaxHp { get { return maxHp; } set { maxHp = value; } }
        protected int range;
        public int Range { get { return range; } set { range = value; } } // 공격 범위
        protected int power;
        public int Power { get { return power; } set { power = value; } } // 공격력
        protected int exp;
        public int Exp { get { return exp; } set { exp = value; } } // 플레이어 에게는 현재 경험치 / 몬스터에게는 주는 경험치
    }
}
