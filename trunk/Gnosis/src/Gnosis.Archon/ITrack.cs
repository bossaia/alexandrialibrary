﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public interface ITrack : INotifyPropertyChanged
    {
        string Path { get; set; }
        string ImagePath { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        string Album { get; set; }
        int Number { get; set; }
        DateTime ReleaseDate { get; set; }
        bool IsSelected { get; set; }
    }
}