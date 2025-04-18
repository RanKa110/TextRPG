using System.IO;
using System.Reflection.Emit;
using System.Xml.Linq;
using System;

namespace MyTextRPGApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //    아무거나

            try
            {
                if (!Event.FirstScene())
                {
                    Event.Start();
                }
                Event.ShowMenu();
            }
            catch { }

            //    구현된 기능
            //    1. 게임 시작 화면
            //    2. 상태보기(상시로 띄움)
            //    3. 인벤토리
            //    4. 장착관리(인벤에서 관리)
            //    5. 상점(사고팔기 가능)
            
            //    6. 아이템 정보 클래스로 만들어서 관리
            //    7. 나만의 아이템추가?
            //    8. 휴식 기능
            //    9. 장착 개선
            //    10. 게임 저장/불러오기

            
            //    구현 안된 기능
            //    1. 레벨업 기능 추가
            //    2. 던전 탐험 기능 추가
            //    3. 몬스터 제작
            //    4. 전투 구현
        }

    }
}