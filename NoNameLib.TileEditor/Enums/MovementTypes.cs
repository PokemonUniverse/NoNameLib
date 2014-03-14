using System;

namespace NoNameLib.TileEditor.Enums
{
    [Flags]
    public enum MovementTypes
    {
        BlockedNorth = 1,
        BlockedEast = 2,
        BlockedNorthEast = BlockedNorth | BlockedEast, // 3
        BlockedSouth = 4,
        BlockedSouthEast = BlockedSouth | BlockedEast, // 5
        BlockedWest = 6,
        BlockedNorthWest = BlockedNorth | BlockedWest, // 7
        BlockedSouthWest = BlockedSouth | BlockedWest, // 10

        Blocked = BlockedNorth | BlockedEast | BlockedSouth | BlockedWest, // 13

        Walk = 20,
        Surf = 30,
        Bike = 40,
    }
}
