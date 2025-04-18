using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MyTextRPGApp2.Item;

namespace MyTextRPGApp2
{
    public enum Job
    {
        Thorakitai = 0,   //    방패와 창을 쓰는 보병
        Hoplite,            //     방패와 창을 쓰는 근접보병
        Peltast,             //    투창을 사용하는 보병
        Praetorian,        //    근위 보병대
        Pezhetairoi        //    전우 보병대
    }

    
    internal class Player
    {
        static public string name;
        static public Job job;

        static public int level;
        static public float exp;

        static public int maxHp;
        static public int hp;
        static public int dmg;
        static public int def;
        static public int maxJavelins;
        static public int javelins; //    전투 당 사용 가능한 투창 수

        public static int gold;

        static public Location nowLocation = Location.Shelter;

        static public Item weapon = null;
        static public Item armor = null;

        static public List<Item> inven = new List<Item>();

        public const string path = @"C:\Users\power\OneDrive\Desktop\MyTextRPGApp2\save.txt";

        static public string JobName => job switch
        {
            Job.Thorakitai => "토라키타이",
            Job.Hoplite => "호플리테스",
            Job.Peltast => "펠타스트",
            Job.Praetorian => "프레토리안",
            Job.Pezhetairoi => "전우 보병대",
            _ => "알 수 없음"
        };

        static public void PrintPlayerData()
        {
            Console.Clear();
            SortingInventory();
            Console.WriteLine("========== 플레이어 정보 ==========");
            Console.WriteLine($"이름: {name}", 10);
            Console.WriteLine($"직책: {JobName}\n", 10);
            Console.WriteLine($"레벨: {level}", 10);
            Console.WriteLine($"경험치: {exp}\n", 10);
            Console.WriteLine($"체력: {maxHp} / {hp}", 10);
            Console.WriteLine($"공격력: {dmg}", 10);
            Console.WriteLine($"방어력: {def}\n", 10);
            Console.WriteLine($"투창 수: {maxJavelins}\n", 10);
            Console.WriteLine($"소지금: {gold}", 10);
            Console.WriteLine("==================================");
            Console.WriteLine($"착용중인 무기: {weapon?.Name}");
            Console.WriteLine($"착용중인 장비: {armor?.Name}");
            Console.WriteLine("==================================");
            Console.WriteLine("----------------------------------------------------------------------\n");
        }

        static void EquipWeapon(Item item)
        {
            if (weapon != null)
            {
                dmg -= weapon.Attack;
                def -= weapon.Defense;
                weapon.IsEquip = false;
            }

            weapon = item;
            dmg += item.Attack;
            def += item.Defense;
            item.IsEquip = true;
        }

        static void EquipArmor(Item item)
        {
            if (armor != null)
            {
                dmg -= armor.Attack;
                def -= armor.Defense;
                armor.IsEquip = false;
            }

            armor = item;
            dmg += item.Attack;
            def += item.Defense;
            item.IsEquip = true;
        }

        static public void SortingInventory()
        {
            //    인벤토리 아이템 정렬하기
            inven.Sort((item1, item2) => item1.ItemID.CompareTo(item2.ItemID));
        }

        static public void ShowInventoryByType(Item.ItemType? filterType = null)
        {
            SortingInventory();
            Console.Clear();
            PrintPlayerData();

            List<Item> filteredList = filterType == null
                ? inven
                : inven.Where(item => item.Type == filterType).ToList();
            //    만약 필터 타입이 없다면 인벤 전체를 보여주고
            //    특정한 필터가 존재한다면 해당 타입의 아이템만 보여준다.

            if (filteredList.Count == 0)
            {
                Console.WriteLine("해당 항목의 아이템이 없습니다!");
                return;
            }

            int page = 0;
            int itemsPerPage = 10;

            while (true)
            {
                Console.Clear();
                Console.Clear();
                PrintPlayerData();
                Console.WriteLine("[ 인벤토리 ]");
                Console.WriteLine();

                //    필터링된 리스트 출력
                for (int i = 0; i < itemsPerPage; i++)
                {
                    int index = page * itemsPerPage + i;
                    if (index >= filteredList.Count) break;

                    var item = filteredList[index];

                    //    장착 여부에 따라 표시
                    string equipMarker = (item.Type != Item.ItemType.Potion && item.IsEquip) ? "[E] " : "";

                    if(item.Type == Item.ItemType.Potion)
                    {
                        Console.WriteLine($"{i + 1}. {equipMarker}[{item.Type}]{item.Name}\n[회복량: {item.Value,3}]\n" +
                       $"{item.Description}\n-------------------------------------------\n");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {equipMarker}[{item.Type}]{item.Name}\n[공격력: {item.Attack,3}][방어력: {item.Defense,3}]\n" +
                      $"{item.Description}\n-------------------------------------------\n");
                    }
                   
                }

                Console.WriteLine();
                Console.WriteLine("8. 다음 페이지  9. 이전 페이지  0. 나가기");
                Console.Write("\n> ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int selected))
                {
                    if (selected == 0) break; // 나가기
                    else if (selected == 8 && page > 0) page--;
                    else if (selected == 9 && (page + 1) * itemsPerPage < filteredList.Count) page++;
                    else if (selected >= 1 && selected <= itemsPerPage)
                    {
                        int actualIndex = page * itemsPerPage + (selected - 1);
                        if (actualIndex < filteredList.Count)
                        {
                            Item selectedItem = filteredList[actualIndex];
                            //    만약 아이템 타입이 무기나 방어구라면?
                            if (selectedItem.Type == Item.ItemType.Weapon || selectedItem.Type == Item.ItemType.Armor)
                            {
                                //    모든 아이템 목록 확인
                                foreach (var item in inven)
                                {
                                    //    만약 착용한 아이템이 있다면 착용 해제
                                    //    근데 이거 그냥 착용중인거 가져와서 하면 되지 않나?
                                    if (item.Type == selectedItem.Type)
                                        item.IsEquip = false;
                                }

                                //    선택한 아이템 장착상태 true로 만들어 주기
                                //    확인용 이기에 여기에서 장착은 안됨
                                selectedItem.IsEquip = true;

                                //    만약 선택한 아이템 타입이 무기라면 무기에 장착하고
                                //    아니라면 방어구로 장착하기
                                if (selectedItem.Type == Item.ItemType.Weapon) EquipWeapon(selectedItem);
                                else EquipArmor(selectedItem);

                                Console.WriteLine($"{selectedItem.Name}을(를) 장착했습니다!");
                            }
                            //    만약 아이템 타입이 소비 아이템이라면?
                            else if (selectedItem.Type == Item.ItemType.Potion)
                            { 
                                //    포션 사용하기
                                Item.UsePotion(selectedItem);

                                //    리스트 갱신 시키기
                                //    포션을 사용해도 남아있는 문제를 해결
                                filteredList = inven.Where(item => item.Type == Item.ItemType.Potion).ToList();

                                continue; // 다시 화면 갱신
                            }
                        }
                        else
                        {
                            Console.WriteLine("해당 번호에 아이템이 존재하지 않습니다!");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("유효하지 않은 선택 입니다!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("숫자만 입력할 수 있습니다!");
                    Console.ReadKey();
                }

            }
        }

        //    이미 만들어진 아이템 데이터를 복사해서 가져오기
        public static void AddItem(Item selectedItem)
        {
            Item boughtItem = new Item(
                selectedItem.Name,
                selectedItem.Type,
                selectedItem.Description,
                selectedItem.Value,
                selectedItem.Attack,
                selectedItem.Defense,
                selectedItem.ValueIndex,
                selectedItem.ItemID,
                false); //    장착 상태 false로 추가
            inven.Add(boughtItem); //    인벤토리에 추가
        }


        //    플레이어 데이터 저장하기
        public static void SavePlayerData()
        {

            //    아래의 내용을 메모장에 적을 수 있게 해주는 클래스 사용하기
            //    using을 사용하면 사용 후 완전히 닫을 수 있게 도와줌!
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(name);
                writer.WriteLine((int)job);
                writer.WriteLine(level);
                writer.WriteLine(exp);
                writer.WriteLine(maxHp);
                writer.WriteLine(hp);
                writer.WriteLine(dmg);
                writer.WriteLine(def);
                writer.WriteLine(maxJavelins);
                writer.WriteLine(javelins);
                writer.WriteLine(gold);
                writer.WriteLine((int)nowLocation);

                //    가지고 있는 무기 및 방어구 저장
                //    왜 병합 연산자 쓰는건가??
                //    weapon이랑 armor는 null일 가능성이 높기 때문에
                //    일반적으로 삼항을 사용하게 될 경우 예외가 발생하기에
                //    null이여도 상관 없으니 병합 연산자를 사용함!
                writer.WriteLine(weapon?.ItemID ?? -1);
                writer.WriteLine(armor?.ItemID ?? -1);

                //    인벤토리 저장
                writer.WriteLine(inven.Count);
                //    모든 인벤토리를 저장해야하니 foreach사용!
                foreach (var item in inven)
                {
                    writer.WriteLine(item.ToSaveString()); //    아이템 정보를 한 줄로 저장
                }
            }

            Console.WriteLine("플레이어 데이터를 성공적으로 저장했습니다!");
        }

        public static void LoadPlayerData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("저장 파일이 존재하지 않습니다.");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            try
            {
                if (lines.Length < 15)
                {
                    Console.WriteLine("파일에 저장된 데이터가 부족합니다.");
                    return;
                }

                // 플레이어 데이터
                name = lines[0];
                job = (Job)Enum.Parse(typeof(Job), lines[1]);
                level = int.Parse(lines[2]);
                exp = float.Parse(lines[3]);
                maxHp = int.Parse(lines[4]);
                hp = int.Parse(lines[5]);
                dmg = int.Parse(lines[6]);
                def = int.Parse(lines[7]);
                maxJavelins = int.Parse(lines[8]);
                javelins = int.Parse(lines[9]);
                gold = int.Parse(lines[10]);
                nowLocation = (Location)Enum.Parse(typeof(Location), lines[11]);

                // 무기/방어구 ID
                int weaponID = int.Parse(lines[12]);
                int armorID = int.Parse(lines[13]);

                // 인벤토리 수
                int inventoryCount = int.Parse(lines[14]);

                if (lines.Length < 15 + inventoryCount)
                {
                    Console.WriteLine("인벤토리 데이터가 불완전합니다.");
                    return;
                }

                inven.Clear();

                for (int i = 15; i < 15 + inventoryCount; i++)
                {
                    string[] itemData = lines[i].Split('|');
                    if (itemData.Length < 9)
                    {
                        Console.WriteLine($"아이템 데이터가 잘못되었습니다: {lines[i]}");
                        continue;
                    }

                    int itemID = int.Parse(itemData[0]);
                    string itemName = itemData[1];
                    Item.ItemType type = (Item.ItemType)Enum.Parse(typeof(Item.ItemType), itemData[2]);
                    string description = itemData[3];
                    int value = int.Parse(itemData[4]);
                    int attack = int.Parse(itemData[5]);
                    int defense = int.Parse(itemData[6]);
                    int valueIndex = int.Parse(itemData[7]);

                    Item item;

                    if (type == Item.ItemType.Potion)
                    {
                        // 포션: 마지막 필드 = potionType (int)
                        ConsumeItemType potionType = (ConsumeItemType)int.Parse(itemData[8]);
                        item = new Item(itemName, type, description, value, attack, defense, valueIndex, potionType, itemID);
                    }
                    else
                    {
                        // 장비: 마지막 필드 = isEquip (bool)
                        bool isEquip = bool.Parse(itemData[8]);
                        item = new Item(itemName, type, description, value, attack, defense, valueIndex, itemID, isEquip);
                    }

                    inven.Add(item);
                }

                // 장비 아이템 할당
                weapon = inven.FirstOrDefault(x => x.ItemID == weaponID);
                armor = inven.FirstOrDefault(x => x.ItemID == armorID);

                Console.WriteLine("플레이어 및 인벤토리 데이터를 성공적으로 불러왔습니다!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("저장된 데이터를 불러오는 데 실패했습니다.");
                Console.WriteLine($"에러 내용: {ex.Message}");
            }
        }
    }
}
