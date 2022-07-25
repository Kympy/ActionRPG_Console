using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class Player : Actor
    {
        private int level; // 레벨
        public int Level { get { return level; }}
        private int maxExp; // 최대 경험치
        public int MaxExp { get { return maxExp; } }
        public Player()
        {
            InitPlayer();
        }
        public void InitPlayer()
        {
            hp = 10;
            MaxHp = hp;
            level = 1;
            exp = 0;
            maxExp = 10;
            power = 1;
            range = 1;
        }
    }
}
