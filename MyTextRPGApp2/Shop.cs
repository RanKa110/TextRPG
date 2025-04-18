using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPGApp2
{


    public static class Shop
    {
        public static List<Item> weaponShopItems = new List<Item>
        {
            Item.Sword,
            Item.ShortSword,
            Item.falcata,
            Item.Khopesh,
            Item.Gladius,
            Item.Spatha
        };

        public static List<Item> armorShopItems = new List<Item>
        {
            Item.LightArmor,
            Item.Linothorax,
            Item.Gambeson,
            Item.HeavyArmor,
            Item.Lorica
        };



        

        public static void BuyItem(Item selectedItem)
        {
            if (Player.gold >= selectedItem.Value)
            {
                Player.gold -= selectedItem.Value;
                Player.AddItem(selectedItem); // 인벤토리에 복사해서 추가
                Console.WriteLine($"{selectedItem.Name}을(를) 구매했습니다!");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다!");
            }
        }

        public static void ShowItemShop(List<Item> shopItems, string title)
        {
            int currentPage = 0;
            const int itemsPerPage = 7;

            while (true)
            {
                Console.Clear();
                Player.PrintPlayerData();
                Console.WriteLine($"=== [ {title} ] ===");

                int start = currentPage * itemsPerPage;
                int end = Math.Min(start + itemsPerPage, shopItems.Count);

                for (int i = start; i < end; i++)
                {
                    Item item = shopItems[i];
                    Console.WriteLine($"{i - start + 1}. [{item.Type}] {item.Name} - 가격: {item.Value}\n[공격력: {item.Attack,3}] [방어력: {item.Defense,3}]\n" +
                                      $"{item.Description}\n-------------------------------------------\n");
                }

                Console.WriteLine();
                Console.WriteLine("8. 이전 페이지");
                Console.WriteLine("9. 다음 페이지");
                Console.WriteLine("0. 돌아가기");
                Console.WriteLine("구매하려면 번호를 입력하세요.");
                Console.Write("\n>");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    break;
                }
                else if (input == "8" && currentPage > 0)
                {
                    currentPage--;
                }
                else if (input == "9" && end < shopItems.Count)
                {
                    currentPage++;
                }
                else
                {
                    if (int.TryParse(input, out int selected))
                    {
                        int itemIndex = start + selected - 1;
                        if (itemIndex >= start && itemIndex < end)
                        {
                            Item selectedItem = shopItems[itemIndex];
                            Shop.BuyItem(selectedItem);
                            Console.WriteLine("계속하려면 아무 키나 누르세요...");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }



        public static void SellItem(Item selectedItem)
        {
            if (selectedItem.IsEquip)
            {
                Console.WriteLine($"{selectedItem.Name}은(는) 현재 장착 중이라 판매할 수 없습니다!");
                return;
            }

            if (Player.inven.Contains(selectedItem))
            {
                int sellValue = selectedItem.Value / 2;

                Player.gold += sellValue;
                Player.inven.Remove(selectedItem);

                Console.WriteLine($"{selectedItem.Name}을(를) 판매했습니다! (+{sellValue} G)");
            }
            else
            {
                Console.WriteLine("판매할 수 없는 아이템입니다!");
            }
        }

        public static void ShowSellMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[ 아이템 판매 ]");
                Console.WriteLine("보유 골드: " + Player.gold);
                Console.WriteLine();

                // 판매 가능한 아이템 보여주기
                for (int i = 0; i < Player.inven.Count; i++)
                {
                    Item item = Player.inven[i];
                    string equipTag = item.IsEquip ? "[장착중] " : "";
                    string sellableTag = item.IsEquip ? "(판매불가)" : $"(판매가: {item.Value / 2}G)";
                    Console.WriteLine($"{i + 1}. {equipTag}{item.Name} {sellableTag}");
                }

                Console.WriteLine("0. 나가기");
                Console.Write("판매할 아이템 번호를 선택하세요: ");
                string input = Console.ReadLine();

                if (input == "0") break;

                if (int.TryParse(input, out int index) && index >= 1 && index <= Player.inven.Count)
                {
                    SellItem(Player.inven[index - 1]);
                    Console.WriteLine("계속하려면 아무 키나 누르세요...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }
            }
        }







    }
}
