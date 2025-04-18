using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPGApp2
{
    public class Item
    {
        public enum ItemType
        {
            Weapon,
            Armor,
            Potion
        }

        public enum ConsumeItemType
        {
            Heal,
            Damage,
            Atk,
            Def
        }

        public string Name { get; set; }  //     아이템 이름
        public ItemType Type { get; set; } //    아이템의 유형
        public string Description { get; set; }  //    아이템 설명
        public int Value { get; set; }  //    아이템 가치 (가격)
        public int Attack { get; set; }  //    공격력 증가
        public int Defense { get; set; }  //    방어력 증가
        public int ValueIndex { get; set; } //    특정값 증가 또는 감소할 값
        public bool IsEquip { get; set; }  //    아이템을 장착했는지 안했는지 확인하는 값

        public ConsumeItemType PotionType { get; set; } //    물약의 타입

        public int ItemID { get; set; } //    아이템 ID
        //    무기    100~
        //    방어구 200~
        //    포션    300~


        // 장비 생성자
        public Item(string name, ItemType type, string description, int value, int attack, int defense, int valueIndex, int itemID, bool isEquip)
        {
            Name = name;
            Type = type;
            Description = description;
            Value = value;
            Attack = attack;
            Defense = defense;
            ValueIndex = valueIndex;
            ItemID = itemID;
            IsEquip = isEquip; 
        }
        //    포션 생성자
        public Item(string name, ItemType type, string description, int value, int attack, int defense, int valueIndex, ConsumeItemType potionType, int itemID)
        {
            Name = name;
            Type = type;
            Description = description;
            Value = value;
            Attack = attack;
            Defense = defense;
            ValueIndex = valueIndex;
            PotionType = potionType;
            ItemID = itemID;
        }

        //    포션 사용하기
        public static void UsePotion(Item potion)
        {
            if (potion.Type != ItemType.Potion)
            {
                Console.WriteLine("이 아이템은 사용할 수 있는 포션이 아닙니다!");
                return;
            }

            switch (potion.PotionType)
            {
                case ConsumeItemType.Heal:
                    int healAmount = potion.ValueIndex;
                    Player.hp += healAmount;

                    if (Player.hp > Player.maxHp)
                    {
                        Player.hp = Player.maxHp;
                    }

                    Player.SortingInventory();

                    if (Player.inven.Contains(potion))
                    {
                        Player.inven.Remove(potion);
                    }

                    break;

                    // 다른 포션 타입은 나중에 추가해도 돼!
            }

        }

        public string ToSaveString()
        {
            if (Type == ItemType.Potion)
            {
                // 포션 저장 (isEquip 대신 potionType 사용)
                return $"{ItemID}|{Name}|{(int)Type}|{Description}|{Value}|{Attack}|{Defense}|{ValueIndex}|{(int)PotionType}";
            }
            else
            {
                // 장비 저장
                return $"{ItemID}|{Name}|{(int)Type}|{Description}|{Value}|{Attack}|{Defense}|{ValueIndex}|{IsEquip}";
            }
        }



        //    무기
        static public Item Sword = new Item("칼", ItemType.Weapon,"크고 날카로운 검 입니다.", 100, 5, 0, 0, 101, false);
        static public Item ShortSword = new Item("숏소드", ItemType.Weapon, "작지만 그만큼 자신을 지킬 때 좋은 칼 입니다.", 100, 3, 0, 0, 102, false);
        static public Item falcata = new Item("팔카타", ItemType.Weapon, "호플리테스들이 쓰며 그리스군의 검으로 굳어진 칼 입니다.", 100, 7, 0, 0, 103, false);
        static public Item Khopesh = new Item("코피스", ItemType.Weapon, "이집트의 대표적인 검으로 낫처럼 생긴 모양이 특징 입니다.", 100, 10, 0, 0, 104, false);
        static public Item Gladius = new Item("글라디우스", ItemType.Weapon, "로마군의 대표적인 한손검 입니다.", 100, 10, 0, 0, 105, false);
        static public Item Spatha = new Item("스파타", ItemType.Weapon, "글라디우스의 뒤를 이은 로마군의 검 입니다.", 100, 12, 0, 0, 106, false);


        //    방어구
        static public Item LightArmor = new Item("방어구", ItemType.Armor, "기초적인 방어구다.", 100, 0, 3, 0, 201, false);
        static public Item Linothorax = new Item("리노토락스", ItemType.Armor, "헬레니즘 시대를 대표하는 갑옷 입니다.", 100, 0, 5, 0, 202, false);
        static public Item Gambeson = new Item("누비 갑옷", ItemType.Armor, "아마포와 천을 이용해 만든 갑옷 입니다.", 100, 0, 6, 0, 203, false);
        static public Item HeavyArmor = new Item("중갑", ItemType.Armor, "무겁지만 강력한 방어구 입니다.", 100, 0, 10, 0, 204, false);
        static public Item Lorica = new Item("로리카", ItemType.Armor, "로마군의 상징적인 갑옷 입니다.", 100, 0, 7, 0, 205, false);

        //    포션
        static public Item crudePotion = new Item("조잡한 포션", ItemType.Potion, "누군가 야메로 제작한 포션 입니다.", 10, 0, 0, 10, ConsumeItemType.Heal , 301);
        static public Item potion = new Item("포션", ItemType.Potion, "통상적으로 유통되는 포션 입니다.", 20, 0, 0, 25, ConsumeItemType.Heal, 302);
        static public Item greatPotion = new Item("그레이트 포션", ItemType.Potion, "더 많은 치유가 필요하다면 그저 양을 늘리면 그만입니다.", 40, 0, 0, 50, ConsumeItemType.Heal, 303);
        static public Item amazingtPotion = new Item("어메이징 포션", ItemType.Potion, "어떤 재료가 들어갔길래 이 적은 양으로도 충분할까요?", 100, 0, 0, 100, ConsumeItemType.Heal, 303);
    }
}
