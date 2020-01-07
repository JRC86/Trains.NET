﻿namespace Trains.NET.Engine
{
    public interface IGameBoard
    {
        int Columns { get; set; }
        int Rows { get; set; }

        void AddTrack(int column, int row);
        void RemoveTrack(int column, int row);
        Track? GetTrackAt(int column, int row);
        System.Collections.Generic.IEnumerable<(int, int, Track)> GetTracks();
    }
}
