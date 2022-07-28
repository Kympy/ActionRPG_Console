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

        private int currentTimer;
        private int gameLoopTimer = 0; // 렌더링 타이머
        private int inputTimer = 0; // 입력 타이머
        private int monsterMoveTimer = 0; // 몬스터 이동 타이머
        private int HPTimer = 0; // 체력 체크 타이머

        private int stage; // 게임 스테이지
        public int Stage { get { return stage; } set { stage = value; } }

        private int currentMonsterCount; // 현재 몬스터 수
        public int CurrentMonsterCount { get { return currentMonsterCount; } set { currentMonsterCount = value; } }
        private int maxMonsterCount; // 최대 몬스터 수
        public int MaxMonsterCount { get { return maxMonsterCount; } set { maxMonsterCount = value; } }
        private int currentBossCount; // 현재 보스 수
        public int CurrentBossCount { get { return currentBossCount; } set { currentBossCount = value; } }
        private int maxBossCount; // 최대 보스 수
        public int MaxBossCount { get { return maxBossCount; } set { maxBossCount = value; } }

        private Monster[] monsterList; // 몬스터 배열
        public Monster[] GetMonsterList { get { return monsterList; } }
        private Boss[] bossList; // 보스 배열
        public Boss[] GetBossList { get { return bossList; } }

        private Item item = new Item(); // 아이템
        public Item Item { get { return item; } }
        private bool isClear = false; // 스테이지 클리어
        public void Awake() // 초기화
        {
            setting.InitWindow();
            render.InitRender();
            player.InitPlayer();
            item.InitItem();
            stage = 1; // 현재 스테이지
            currentMonsterCount = 20; // 현재 몬스터 수
            maxMonsterCount = currentMonsterCount; // 최대 몬스터 수
            currentBossCount = 1; // 현재 보스 수
            maxBossCount = currentBossCount; // 최대 보스 수
        }
        public void Start()
        {
            Console.CursorVisible = false; // 커서 숨김
            CreateMonster(); // 몬스터 생성
        }
        public void Update()
        {
            while(true)
            {
                currentTimer = Environment.TickCount & Int32.MaxValue;
                if(currentTimer - gameLoopTimer > 1000 / 30) // 렌더링 타이머
                {
                    gameLoopTimer = currentTimer;
                    player.CheckCollision(); // 충돌은 렌더링과 동일하게 체크
                    item.CheckItemCollision(); // 아이템 충돌 체크
                    player.LevelUP(); // 레벨 업
                    render.RenderInfo(); // 정보 그리기
                    render.RenderScreen(); // 화면 그리기
                    CheckStageUP(); // 다 죽였는지 체크
                    if(isClear)
                    {
                        Thread.Sleep(1000);
                        Console.Clear();
                        StageUP();// 다음 스테이지 변수값 변경 함수 호출
                    }
                    if(GameManager.Instance.GetPlayer.IsDead)
                    {
                        Thread.Sleep(1000);
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("\t ======= Game Over =======");
                        Console.WriteLine();
                        return;
                    }
                }
                if(currentTimer - inputTimer > 1000 / 10) // 키 입력 타이머
                {
                    inputTimer = currentTimer;
                    InputManager.Instance.GetKey();
                }
                if(currentTimer - monsterMoveTimer > 1000) // 1초마다 몬스터 이동
                {
                    monsterMoveTimer = currentTimer;
                    MoveMonster(); // 몬스터 이동 & 공격
                    player.PowerTimer(); // 플레이어 파워 업 체크 ( 같은 1초 타이머라 이곳에 배치)
                    player.InvincibleTimer(); // 무적 체크
                }
                if(currentTimer - HPTimer > 500) // 0.5 초마다 체력 색상 update
                {
                    HPTimer = currentTimer;
                    CheckHPMonster();
                    player.DeadFlag();
                }
            }
        }
        private void CreateMonster() // 몬스터 생성
        {
            monsterList = new Monster[maxMonsterCount]; // 몬스터 초기화
            for(int i = 0; i < maxMonsterCount; i++)
            {
                monsterList[i] = new Monster();
                monsterList[i].InitInfo();
            }
            bossList = new Boss[maxBossCount]; // 보스 초기화
            for(int i = 0; i < maxBossCount; i++)
            {
                bossList[i] = new Boss();
                bossList[i].InitInfo();
            }
        }
        private void MoveMonster() // 몬스터 이동 및 공격
        {
            for(int i = 0; i < monsterList.Length; i++)
            {
                monsterList[i].Move();
                monsterList[i].Attack();
            }
            for(int i = 0; i < bossList.Length; i++) // 보스 이동
            {
                bossList[i].Move();
                bossList[i].Attack();
            }
        }
        private void CheckHPMonster() // 몬스터 사망 플래그 체크
        {
            for(int i = 0; i < monsterList.Length; i++)
            {
                monsterList[i].DeadFlag();
            }
            for(int i = 0; i < bossList.Length; i++)
            {
                bossList[i].DeadFlag();
            }
        }
        private void CheckStageUP() // 몬스터 다 죽었는지 체크
        {
            if(currentBossCount <= 0 && currentMonsterCount <= 0)
            {
                isClear = true;
            }
        }
        public void StageUP() // 스테이지 상승
        {
            isClear = false;
            stage++;
            currentMonsterCount = maxMonsterCount;
            currentBossCount = maxBossCount;
            CreateMonster(); // 몬스터 재생성
        }
    }
}
