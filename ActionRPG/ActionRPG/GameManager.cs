using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class GameManager : SingleTon<GameManager>
    {
        private GameSetting setting = new GameSetting(); // 게임 설정
        public GameSetting GetSetting { get { return setting; } }

        private Player player = new Player(); // 플레이어
        public Player GetPlayer { get { return player; } }
        private Render render = new Render(); // 렌더
        public void Awake()
        {
            setting.InitWindow();
        }
        public void Start()
        {
            //player.InitPlayer();
        }
        public void Update()
        {
            //render.RenderInfo();
            //render.RenderScreen();
        }
    }
}
