using Common.Models;

namespace Dal
{
    static class DummyData
    {
        public static readonly ControlTower[] ControlTowers =
        {
            new ControlTower{Id=1,},
        };

        public static readonly Station[] Stations =  {
            new Station{ Id = 1,},
            new Station{ Id = 2,},
            new Station{ Id = 3,},
            new Station{ Id = 4,},
            new Station{ Id = 5,},
            new Station{ Id = 6,},
            new Station{ Id = 7,},
            new Station{ Id = 8,},
            new Station{ Id = 9,},
        };

        public static readonly StationRelation[] StationRelations = {
            new StationRelation {StationFromId=1,StationToId=2,Direction=DirectionEnum.Landing},  
            new StationRelation {StationFromId=2,StationToId=3,Direction=DirectionEnum.Landing},  
            new StationRelation {StationFromId=3,StationToId=4,Direction=DirectionEnum.Landing},  
            new StationRelation {StationFromId=4,StationToId=5,Direction=DirectionEnum.Landing},  
            new StationRelation {StationFromId=5,StationToId=6,Direction=DirectionEnum.Landing},  
            new StationRelation {StationFromId=5,StationToId=7,Direction=DirectionEnum.Landing},
            new StationRelation {StationFromId=5,StationToId=9,Direction=DirectionEnum.Landing},
            
            new StationRelation {StationFromId=6,StationToId=8,Direction=DirectionEnum.Takeoff},  
            new StationRelation {StationFromId=7,StationToId=8,Direction=DirectionEnum.Takeoff}, 
            new StationRelation {StationFromId=8,StationToId=4,Direction=DirectionEnum.Takeoff}, 
        };

        public static readonly ControlTowerStationRelation[] ControlTowerStationRelations =
        {
            new ControlTowerStationRelation{ControlTowerId=1,StationToId=1,Direction = DirectionEnum.Landing},

            new ControlTowerStationRelation{ControlTowerId=1,StationToId=6,Direction = DirectionEnum.Takeoff},
            new ControlTowerStationRelation{ControlTowerId=1,StationToId=7,Direction = DirectionEnum.Takeoff},
        };

    }
}
