using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPGApp2
{
    public enum Location
    {
        Shelter = 0,
        Dungeon0,
        Dungeon1,
        Dungeon2,
        Dungeon3,
        Dungeon4
    }



    internal class Event
    {
        //    초기 화면
        static public bool FirstScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("안녕하세요! 모험가님! 혹시 처음이신가요?");
                Console.WriteLine("1. 그렇다.");
                Console.WriteLine("2. 아니다.");
                Console.Write("\n>");

                string select = Console.ReadLine();

                if (int.TryParse(select, out int choice) && (choice >= 1 && choice <= 2))
                {
                    switch (choice)
                    {
                        case 1:
                            return false; 
                        case 2:
                            Console.WriteLine("불러오기를 진행합니다!");
                            Player.LoadPlayerData(Player.path);
                            Console.ReadKey();
                            return true;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 접근 입니다!");
                }
            }
        }

        //    처음 시작 고르기
        static public void Start()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신걸 환영합니다!");
            Console.Write("우선 당신의 이름을 입력해 주세요!\n>");
            Player.name = Console.ReadLine();

            //    테스트용 아이템 지급
            Player.AddItem(Item.Sword);
            Player.AddItem(Item.ShortSword);
            Player.AddItem(Item.Gladius);
            Player.AddItem(Item.Khopesh);

            Player.AddItem(Item.crudePotion);
            Player.AddItem(Item.potion);
            Player.AddItem(Item.greatPotion);
            Player.AddItem(Item.amazingtPotion);

            SelectJob();
            
            SelectGod();

            Console.Clear();
            Player.PrintPlayerData();
            Console.WriteLine($"신께서 {Player.name}님을 축복하기를!");
            Console.WriteLine($"앞으로의 여정에 신의 가호가 함께하기를 바라며 즐거운 게임 되세요!");
            Console.Write("아무 키나 눌러서 진행하기! >");
            Console.ReadKey();
        }

        //    직업을 고르는 초기 이벤트
        static public void SelectJob()
        {
            string answer;

            while (true) // 선택이 확정될 때까지 반복
            {
                // 직업 선택 화면 출력
                Console.Clear();
                Console.WriteLine($"환영합니다! {Player.name}!");
                Console.WriteLine("당신은 스파르타 던전에 들어왔습니다!.");
                Console.WriteLine("하지만 그 전에 당신의 이야기를 들어보고 싶습니다!");

                Console.WriteLine($"{Player.name}님의 직책은 무엇입니까?\n");

                Console.WriteLine("1. 토라키타이");
                Console.WriteLine("2. 호플리테스");
                Console.WriteLine("3. 펠타스트");
                Console.WriteLine("4. 프레토리안");
                Console.WriteLine("5. 전우 보병대");
                Console.Write("\n>");

                string select = Console.ReadLine();

                if (int.TryParse(select, out int choice) && (choice >= 1 && choice <= 5 || choice == 256))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("1. 토라키타이");
                            Console.WriteLine("토라키타이는 고대 헬레니즘 시대의 중보병으로,");
                            Console.WriteLine("강한 방어력과 유연한 전투 대처 능력을 가진 병사입니다.");
                            Console.WriteLine("사슬 갑옷과 둥근 방패를 착용하고, 창과 검을 사용하여");
                            Console.WriteLine("근접 전투에서 뛰어난 성과를 자랑하며, 다양한 전술에 적응할 수 있습니다.\n");

                            Console.WriteLine("======스탯======");
                            Console.WriteLine("체력: 100");
                            Console.WriteLine("공격력: 10");
                            Console.WriteLine("방어력: 10");
                            Console.WriteLine("투창: 2\n");
                            Console.WriteLine("소지금: 1000");
                            Console.WriteLine("==============\n");

                            Console.WriteLine($"\n{Player.name}님의 직업이 맞나요?");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.job = Job.Thorakitai;
                                Player.maxHp = 100;
                                Player.hp = 100;
                                Player.dmg = 10;
                                Player.def = 10;
                                Player.maxJavelins = 2;
                                Player.javelins = 2;
                                Player.gold = 1000;
                                return;
                            }
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("2. 호플리테스");
                            Console.WriteLine("호플리테스는 고대 그리스의 중보병으로, 강력한 방어력과 조직적인 전투 방식으로 유명합니다.");
                            Console.WriteLine("호플리테스는 두꺼운 청동 갑옷과 큰 원형 방패를 착용하며, 긴 창을 능숙하게 다룹니다.");
                            Console.WriteLine("고대 그리스의 보병 하면 떠오르는 가장 스트레오타입인 병종으로 300의 주인공 진영 또한 전원이 호플리테스 입니다.");
                            Console.WriteLine("그들의 강점은 강력한 방어와 협력적인 전술, 그리고 전투 중 '적의 전선을 부수는' 능력입니다.\n");
                            
                            Console.WriteLine("======스탯======");
                            Console.WriteLine("체력: 110");
                            Console.WriteLine("공격력: 15");
                            Console.WriteLine("방어력: 10");
                            Console.WriteLine("투창: 1\n");
                            Console.WriteLine("소지금: 800");
                            Console.WriteLine("==============\n");

                            Console.WriteLine($"\n{Player.name}님의 직업이 맞나요?");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.job = Job.Hoplite;
                                Player.maxHp = 110;
                                Player.hp = 110;
                                Player.dmg = 15;
                                Player.def = 10;
                                Player.maxJavelins = 1;
                                Player.javelins = 1;
                                Player.gold = 800;
                                return;
                            }
                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("3. 펠타스트");
                            Console.WriteLine("펠타스트는 고대 그리스와 로마의 경보병으로, 기동성 높은 전투를 선호합니다.");
                            Console.WriteLine("그들은 가벼운 갑옷과 둥근 방패를 착용하고, 주로 던질 수 있는 창과 검을 사용했습니다.");
                            Console.WriteLine("펠타스트는 투창이 기본인 병종으로 그만큼 가장 많은 투창을 소지하고 있습니다.");
                            Console.WriteLine("그들의 장점은 빠른 기동력과 원거리에서의 저격 능력으로, 적을 혼란에 빠뜨리거나 약화시키는 역할을 합니다.\n");

                            Console.WriteLine("======스탯======");
                            Console.WriteLine("체력: 100");
                            Console.WriteLine("공격력: 10");
                            Console.WriteLine("방어력: 5");
                            Console.WriteLine("투창: 5\n");
                            Console.WriteLine("소지금: 1000");
                            Console.WriteLine("==============\n");

                            Console.WriteLine($"\n{Player.name}님의 직업이 맞나요?");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.job = Job.Peltast;
                                Player.maxHp = 100;
                                Player.hp = 100;
                                Player.dmg = 10;
                                Player.def = 5;
                                Player.maxJavelins = 5;
                                Player.javelins = 5;
                                Player.gold = 1000;
                                return;
                            }
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine("4. 프레토리안");
                            Console.WriteLine("프레토리안은 고대 로마 황제의 근위대입니다. 황제의 신변을 보호하며, 정치적 안정과 군사적 권력을 유지하는 역할을 맡았습니다.");
                            Console.WriteLine("그들은 일반적인 군대보다 훨씬 더 높은 수준의 훈련을 받았고, 황제를 지키는 임무 외에도 때때로 정치적 역할을 하기도 했습니다.");
                            Console.WriteLine("그들의 장점은 뛰어난 방어력과 강력한 공격력이며, 전장에서 강력한 힘을 발휘할 수 있습니다.\n");

                            Console.WriteLine("======스탯======");
                            Console.WriteLine("체력: 120");
                            Console.WriteLine("공격력: 15");
                            Console.WriteLine("방어력: 15");
                            Console.WriteLine("투창: 0\n");
                            Console.WriteLine("소지금: 500\n");
                            Console.WriteLine("==============\n");

                            Console.WriteLine($"\n{Player.name}님의 직업이 맞나요?");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.job = Job.Praetorian;
                                Player.maxHp = 120;
                                Player.hp = 120;
                                Player.dmg = 15;
                                Player.def = 15;
                                Player.maxJavelins = 0;
                                Player.javelins = 0;
                                Player.gold = 500;
                                return;
                            }
                            break;

                        case 5:
                            Console.Clear();
                            Console.WriteLine("5. 전우 보병대");
                            Console.WriteLine("페제타이로이(Pezhetairoi)라고도 불리는 전우 보병대는 그리스식 최정예 장창병 입니다.");
                            Console.WriteLine("전우 보병대는 경량화 덕분에 빠른 기동력을 확보할 수 있었고 장창을 이용하여 적의 접근을 차단하였습니다.");
                            Console.WriteLine("멀리서는 장창을, 근거리에서는 검을 이용하여 어떤 거리든 적을 완벽히 통제하는 완벽에 가까운 보병 입니다.");
                            Console.WriteLine("어떤 매니저 님이 제일 좋아하시는 병종 입니다.\n");

                            Console.WriteLine("======스탯======");
                            Console.WriteLine("체력: 100");
                            Console.WriteLine("공격력: 20");
                            Console.WriteLine("방어력: 5");
                            Console.WriteLine("투창: 1\n");
                            Console.WriteLine("소지금: 500\n");
                            Console.WriteLine("==============\n");

                            Console.WriteLine($"\n{Player.name}님의 직업이 맞나요?");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.job = Job.Pezhetairoi;
                                Player.maxHp = 100;
                                Player.hp = 100;
                                Player.dmg = 20;
                                Player.def = 5;
                                Player.maxJavelins = 1;
                                Player.javelins = 1;
                                Player.gold = 500;
                                return;
                            }
                            break;

                        case 256:
                            Console.Clear();
                            Console.WriteLine("비밀!. 노아");
                            Console.WriteLine("당신은 귀여운 코딩 도우미 노아 입니다!");
                            Console.WriteLine("로봇임에도 불구하고 재빠르고 날렵한 움직임이 특징 입니다.");
                            Console.WriteLine("은밀함을 위해 투창을 소지하고 있진 않지만 기본 능력이 충실한 편 입니다.\n");

                            Console.WriteLine("======스탯======");
                            Console.WriteLine("체력: 150");
                            Console.WriteLine("공격력: 15");
                            Console.WriteLine("방어력: 15");
                            Console.WriteLine("투창: 0\n");
                            Console.WriteLine("소지금: 9999\n");
                            Console.WriteLine("==============\n");
                            Console.WriteLine("안내: 노아를 고르면 당신의 이름이 \"노아\" 로 고정됩니다!");

                            Console.WriteLine($"\n{Player.name}님의 직업이 맞나요?");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.job = Job.Peltast;
                                Player.maxHp = 150;
                                Player.hp = 150;
                                Player.dmg = 15;
                                Player.def = 15;
                                Player.maxJavelins = 0;
                                Player.javelins = 0;
                                Player.gold = 9999;
                                Player.name = "노아";
                                return;
                            }
                            break;

                        default:
                            Console.WriteLine("잘못된 입력 입니다!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다! 1~4 사이의 숫자를 입력하세요.");
                }
            }
        }

        //    처음 좋아하는 신 고르기
        static public void SelectGod()
        {
            string answer;
            while(true)
            {
                Console.Clear();
                Player.PrintPlayerData();
                Console.WriteLine($"{Player.name}님의 직책은 {Player.JobName}입니다!\n정말 멋지네요!");
                Console.WriteLine($"혹시 {Player.name}님이 제일 마음에 들어하는 신이 있을까요?");
                Console.WriteLine("\n1. 기이아");
                Console.WriteLine("2. 아레스");
                Console.WriteLine("3. 아테나");
                Console.WriteLine("3. 헤르메스");
                Console.Write("\n>");

                string select = Console.ReadLine();

                if (int.TryParse(select, out int choice) && choice >= 1 && choice <= 4)
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("1. 가이아");
                            Console.WriteLine("가이아는 창조와 대지의 여신으로 생명의 근원 입니다.");
                            Console.WriteLine("당신이 가이아를 고른다면 그 축복으로 당신의 최대 체력이 올라갈 것 입니다.");
                            Console.WriteLine("\n효과: 최대체력 + 10\n");

                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.hp += 10;
                                Player.maxHp += 10;
                                return;
                            }
                            break;

                        case 2:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("2. 아레스");
                            Console.WriteLine("아레스는 전쟁과 투쟁, 군인의 신 입니다.");
                            Console.WriteLine("당신이 아레스를 고른다면 그 축복으로 당신의 공격력과 방어력이 올라갈 것 입니다.");
                            Console.WriteLine("\n효과: 공격력 + 3");
                            Console.WriteLine("효과: 방어력 + 3\n");

                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.dmg += 3;
                                Player.def += 3;
                                return;
                            }
                            break;

                        case 3:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("3. 아테나");
                            Console.WriteLine("아레스는 전쟁과 지혜의 신 입니다.");
                            Console.WriteLine("당신이 아테나를 고른다면 그 축복으로 당신의 투창 소지량이 증가할 것 입니다.");
                            Console.WriteLine("\n효과: 투창 소지량 + 2\n");

                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.maxJavelins += 2;
                                Player.javelins += 2;
                                return;
                            }
                            break;

                        case 4:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("4. 헤르메스");
                            Console.WriteLine("헤르메스는 도둑과 상인의 신 입니다.");
                            Console.WriteLine("당신이 헤르메스를 고른다면 그 축복으로 당신의 기본 소지금이 증가할 것 입니다.");
                            Console.WriteLine("\n효과: 소지금 + 500\n");

                            Console.WriteLine("1. 네!");
                            Console.WriteLine("2. 아니요!\n");
                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Player.gold += 500;
                                return;
                            }
                            break;

                        default:
                            Console.WriteLine("잘못된 입력 입니다!");
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다! 1~4 사이의 숫자를 입력하세요.");
                }
            }
        }

        //    메인 메뉴
        static public void ShowMenu()
        {
            while (true)
            {
                if (Player.nowLocation == Location.Shelter)
                {
                    Player.PrintPlayerData();
                    Console.WriteLine("1. 던전으로 이동하기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 휴식");
                    Console.WriteLine("4. 상점");
                    Console.WriteLine("5. 대화하기(미구현)");

                    Console.WriteLine("\n7. 디버그용 체력 깎기!(10으로 만들기)");

                    Console.WriteLine("\n8. 저장하기");
                    Console.WriteLine("9. 불러오기");
                    Console.WriteLine("0. 종료하기\n");
                    Console.Write(">");
                    string select = Console.ReadLine();

                    if(int.TryParse(select, out int choice) && (choice >= 0 && choice <= 4 || choice >= 7 && choice <= 9))
                    {
                        switch (choice)
                        {
                            case 1:
                                MoveToDungeon();
                                break;

                            case 2:
                                ShowInven();
                                Console.ReadKey();
                                break;

                            case 3:
                                Console.WriteLine("휴식을 취합니다.");
                                Rest();
                                Console.WriteLine("모든 체력이 회복되었습니다!");
                                Console.ReadKey();
                                break;

                            case 4:
                                ShowShopMenu();
                                break;


                            case 7:
                                Player.hp = 10;
                                break;

                            case 8:
                                Player.SavePlayerData();
                                Console.WriteLine("저장을 완료했습니다!");
                                Console.ReadKey();
                                break;
                            case 9:
                                Console.WriteLine("불러오기를 진행합니다!");
                                Player.LoadPlayerData(Player.path);
                                Console.ReadKey();
                                break;
                            case 0:
                                Console.WriteLine("게임을 종료합니다!");
                                Console.ReadKey();
                                Environment.Exit(0);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 접근 입니다!");
                    }
                }
            }

        }

        //    휴식
        public static void Rest()
        {
            Player.hp = Player.maxHp;
            Console.WriteLine($"{Player.hp} -> {Player.maxHp}");
        }

        //    던전으로 이동
        public static void MoveToDungeon()
        {
            string answer;

            while (true)
            {
                Console.Clear();
                Player.PrintPlayerData();
                Console.WriteLine("어디로 이동하시겠습니까?");
                Console.WriteLine("1. 1계층: 테르모필레 동굴");
                Console.WriteLine("2. 2계층: 발길이 끊긴 테르모필레의 적막");
                Console.WriteLine("3. 3계층: 에우리오타스의 근원");
                Console.WriteLine("4. 4계층: 장엄한 바닥 \'타이게토스\'");
                Console.WriteLine("5. 5계층: 라케다이몬의 심장");

                Console.WriteLine("\n0. 이동하지 않는다.");
                Console.Write("\n>");
                string select = Console.ReadLine();
                if (int.TryParse(select, out int choice) && (choice >= 0 && choice <= 5))
                {
                    switch(choice)
                    {
                        case 0:
                            Console.WriteLine("다시 던전의 입구 근처로 돌아갑니다!");
                            Player.nowLocation = Location.Shelter;
                            return;

                        case 1:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("==============================");
                            Console.WriteLine("\"테르모필레 동굴\"");
                            Console.WriteLine("\"The Cavern of Thermopylae\"");
                            Console.WriteLine("==============================\n");

                            Console.WriteLine("테르모필레 동굴은 과거 페르시아 전사들과 스파르타 전사들이"
                                + " 치열한 전투를 치른 곳의 지명을 따온 동굴 입니다.");
                            Console.WriteLine("동굴 안에는 죽어서도 싸움을 갈망하는 길을 잃은 전사들의 영혼으로 가득 차 있고");
                            Console.WriteLine("아직까지도 그 전투는 이어져 오고 있어 \'테르모필레 동굴\'로 불리게 되었습니다.");
                            Console.WriteLine("\n==============================");

                            Console.WriteLine("\n이곳으로 이동하시겠습니까>");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("1. 아니요!");

                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Console.WriteLine("테르모필레 동굴로 향합니다.");
                                Player.nowLocation = Location.Dungeon0;
                                Console.WriteLine("미구현!");
                                Console.ReadKey();
                                return;
                            }
                            Player.nowLocation = Location.Shelter;
                            break;

                        case 2:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("==============================");
                            Console.WriteLine("\"발길이 끊긴 테르모필레의 적막\"");
                            Console.WriteLine("\"The Silence of Forsaken Thermopylae\"");
                            Console.WriteLine("==============================\n");

                            Console.WriteLine("테르모필레 동굴 깊숙한 곳으로 아무런 발소리도, " +
                                "\n치열한 전투의 함성과 칼이 부딛히는 소리 조차 들리지 않는 곳 입니다.");
                            Console.WriteLine("어떤 존재가, 어떤 것이 이 적막한 곳에 존재하는지는 아무도 알 수 없습니다.");
                            Console.WriteLine("알고 있던 자들은 모두 그 적막과 하나가 되었기 때문입니다.");
                            Console.WriteLine("\n==============================");

                            Console.WriteLine("\n이곳으로 이동하시겠습니까>");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("1. 아니요!");

                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Console.WriteLine("발길이 끊긴 테르모필레의 적막으로 향합니다.");
                                Player.nowLocation = Location.Dungeon1;
                                Console.WriteLine("미구현!");
                                Console.ReadKey();
                                return;
                            }
                            Player.nowLocation = Location.Shelter;
                            break;

                        case 3:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("==============================");
                            Console.WriteLine("\"에우리오타스의 종단\"");
                            Console.WriteLine("\"The Final Flow of Eurotas\"");
                            Console.WriteLine("==============================\n");

                            Console.WriteLine("에우리오타스 강의 끝 지점으로, 바다가 아닌 지하 깊숙한 곳에 위치해 있습니다.");
                            Console.WriteLine("일생을 살며 노환이 아닌 전장에서 전사한 스파르타 전사들 처럼 땅에 스며드는 운명을 맞이한");
                            Console.WriteLine("물방울들이 모여 형성한 곳으로, 수천년에 걸쳐 형성된 것으로 보입니다.");
                            Console.WriteLine("이곳에 생명이 종종 나타난다고는 하지만, 무엇을 상상하던간에 우리가 알던 모습은 절대 아닐겁니다.");
                            Console.WriteLine("\n==============================");

                            Console.WriteLine("\n이곳으로 이동하시겠습니까>");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("1. 아니요!");

                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Console.WriteLine("에우리오타스의 종단으로 향합니다.");
                                Player.nowLocation = Location.Dungeon2;
                                Console.WriteLine("미구현!");
                                Console.ReadKey();
                                return;
                            }
                            Player.nowLocation = Location.Shelter;
                            break;

                        case 4:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("==============================");
                            Console.WriteLine("\"장엄한 바닥 \'타이게토스\'\"");
                            Console.WriteLine("\"The Majestic Floor of Taygetus\"");
                            Console.WriteLine("==============================\n");

                            Console.WriteLine("타이게토스 산의 이름을 그대로 가져온 이곳은 마치 경기장 같은 모양을 하고 있습니다.");
                            Console.WriteLine("관중석이 없고, 그저 광활한 경기장이 펼쳐진 이곳에는 ");
                            Console.WriteLine("오직 새로운 상대를 기다리는 미지의 투사들만이 조용히 잠들어 있습니다.");
                            Console.WriteLine("그들은 잠들어 있는 듯하지만, 언제든지 이곳에 도전하는 자가 있다면 깨어나 싸움을 시작할 것입니다.");
                            Console.WriteLine("살육과 도전은 그들의 일상이며, 그들에게 그것은 단순한 의무가 아니라, 존재의 이유입니다.");
                            Console.WriteLine("\n==============================");

                            Console.WriteLine("\n이곳으로 이동하시겠습니까>");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("1. 아니요!");

                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Console.WriteLine("장엄한 바닥 \'타이게토스\'로 향합니다.");
                                Player.nowLocation = Location.Dungeon3;
                                Console.WriteLine("미구현!");
                                Console.ReadKey();
                                return;
                            }
                            Player.nowLocation = Location.Shelter;
                            break;

                        case 5:
                            Console.Clear();
                            Player.PrintPlayerData();
                            Console.WriteLine("==============================");
                            Console.WriteLine("\"라케다이몬의 심장\"");
                            Console.WriteLine("\"The Heart of Lacedaemon\"");
                            Console.WriteLine("==============================\n");

                            Console.WriteLine("무엇이 이곳에 있는지 알고 싶지 않습니다.");
                            Console.WriteLine("왜 이름이 이런지도 알고 싶지 않습니다.");
                            Console.WriteLine("알고 싶지도, 이해하고 싶지도 않습니다.");
                            Console.WriteLine("아니, \"알 수도, 이해할 수도 없습니다.\"");
                            Console.WriteLine("\n==============================");

                            Console.WriteLine("\n이곳으로 이동하시겠습니까>");
                            Console.WriteLine("1. 네!");
                            Console.WriteLine("1. 아니요!");

                            Console.Write(">");
                            answer = Console.ReadLine();
                            if (int.Parse(answer) == 1)
                            {
                                Console.WriteLine("라케다이몬의 심장으로 향합니다.");
                                Player.nowLocation = Location.Dungeon4;
                                Console.WriteLine("미구현!");
                                Console.ReadKey();
                                return;
                            }
                            Player.nowLocation = Location.Shelter;
                            break;
                    }
                }

             }
        }

        //    인벤토리 확인하기
        public static void ShowInven()
        {
            while(true)
            {
                Player.PrintPlayerData();
                Console.WriteLine("1.아이템 장착하기");
                Console.WriteLine("2.아이템 사용하기");
                Console.WriteLine("3.아이템 확인하기");
                Console.WriteLine("\n0.돌아가기");
                Console.Write("\n>");

                string select = Console.ReadLine();

                if (int.TryParse(select, out int choice) && choice >= 0 && choice <= 3)
                {
                    switch(choice)
                    {
                        case 1:
                            EquipItem();
                            Console.ReadKey();
                            break;

                        case 2:
                            UseItem();
                            break;

                        case 3:
                            Player.ShowInventoryByType();
                            break;

                        case 0:
                            return;
                    }
                }
            }
        }

        public static void EquipItem()
        {
            while (true)
            {
                Console.Clear();
                Player.PrintPlayerData();
                Console.WriteLine("장착할 아이템을 골라주세요");
                Console.WriteLine("1. 무기");
                Console.WriteLine("2. 방어구");
                Console.WriteLine("\n0. 돌아간다");
                Console.Write("\n>");

                string select = Console.ReadLine();
                if (int.TryParse(select, out int choice) && choice >= 0 && choice <= 3)
                {
                    switch (choice)
                    {
                        case 1:
                            Player.ShowInventoryByType(Item.ItemType.Weapon);
                            break;

                        case 2:
                            Player.ShowInventoryByType(Item.ItemType.Armor);
                            break;

                        case 0:
                            return;
                    }
                }

            }
        }

        public static void UseItem()
        {
            Player.ShowInventoryByType(Item.ItemType.Potion);
        }


        public static void ShowShopMenu()
        {
            while (true)
            {
                Player.PrintPlayerData();
                Console.WriteLine("1.무기 아이템 구매하기");
                Console.WriteLine("2.방어구 아이템 구매하기");
                Console.WriteLine("3.아이템 판매하기");
                Console.WriteLine("\n0.돌아가기");
                Console.Write("\n>");

                string select = Console.ReadLine();

                if (int.TryParse(select, out int choice) && choice >= 0 && choice <= 2)
                {
                    switch (choice)
                    {
                        case 1:
                            Shop.ShowItemShop(Shop.weaponShopItems, "무기 상점");
                            break;

                        case 2:
                            Shop.ShowItemShop(Shop.armorShopItems, "방어구 상점");
                            break;

                        case 3:
                            Shop.ShowSellMenu();
                            break;

                        case 0:
                            return;
                    }
                }
            }
        }







    }
}
