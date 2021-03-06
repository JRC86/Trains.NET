﻿using System.Diagnostics.CodeAnalysis;

namespace Trains.NET.Engine.Tracks
{
    public class BridgeFactory : IStaticEntityFactory<Track>
    {
        private readonly ITerrainMap _terrainMap;

        public BridgeFactory(ITerrainMap terrainMap)
        {
            _terrainMap = terrainMap;
        }

        public bool TryCreateEntity(int column, int row, [NotNullWhen(returnValue: true)] out Track? entity)
        {
            if (!_terrainMap.Get(column, row).IsWater)
            {
                entity = null;
                return false;
            }

            entity = new Bridge();
            return true;
        }
    }
}
