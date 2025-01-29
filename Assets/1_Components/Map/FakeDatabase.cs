
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{
    public class FakeDatabase
    {
        private static List<ArmyData> armyDataList;

        static FakeDatabase()
        {
            armyDataList = new List<ArmyData>
            {
                new ArmyData("Karamanoðlu Ordu 1", "Ýbrahim Bey", 5),
                new ArmyData("Karamanoðlu Ordu 2", "Mehmet Bey", 7),
                new ArmyData("Karamanoðlu Ordu 3", "Ahmet Bey", 3),
                new ArmyData("Karamanoðlu Ordu 4", "Ali Bey", 6),
                new ArmyData("Karamanoðlu Ordu 5", "Hüseyin Bey", 4)
            };
        }
        public static ArmyData GetRandomArmyData()
        {
            int randomIndex = UnityEngine.Random.Range(0, armyDataList.Count);
            return armyDataList[randomIndex];
        }
        private static readonly string[] castleNames = new string[]
        {
            "Karamanoðlu Kalesi",
            "Sultan Gazi Kalesi",
            "Kütahya Kalesi",
            "Beylik Kalesi",
            "Dumanlý Kale"
        };

        private static readonly string[] castleOwners = new string[]
        {
            "Sultan Mehmet Bey",
            "Süleyman Bey",
            "Hüdavendigâr Bey",
            "Ali Paþa",
            "Beyazýt Bey",
            "Osman Gazi"
        };

        private static readonly string[] resources = new string[]
        {
            "Altýn",
            "Zeytin",
            "Çiftlik",
            "Göller",
            "Kervansaray"
        };

        public static CastleData GetRandomCastleData()
        {
            CastleData data = new CastleData
            {
                castleName = castleNames[UnityEngine.Random.Range(0, castleNames.Length)],
                castleOwner = castleOwners[UnityEngine.Random.Range(0, castleOwners.Length)],
                castleLevel = UnityEngine.Random.Range(1, 101),
                resourcesAvailable = UnityEngine.Random.Range(100, 1000)
            };

            return data;
        }

        [System.Serializable]
        public class ArmyData
        {
            public string armyName;
            public string armyLeader;
            public int armyLevel;

            public ArmyData(string name, string leader, int level)
            {
                armyName = name;
                armyLeader = leader;
                armyLevel = level;
            }
        }
    }
}