﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public abstract class ChoiceItemBase<TSource>
        : IChoiceItem, INotifyPropertyChanged
        where TSource : class, ISource
    {
        protected ChoiceItemBase(string text, TSource source)
        {
            this.itemId = Guid.NewGuid();
            this.text = text;
            this.source = source;
        }

        private readonly Guid itemId;
        private readonly string text;
        protected readonly TSource source;
        
        private bool isChosen;
        private bool isExpanded = true;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid ItemId
        {
            get { return itemId; }
        }

        public string Text
        {
            get { return text; }
        }

        public bool IsChosen
        {
            get { return isChosen; }
            set
            {
                if (isChosen == value)
                    return;

                isChosen = value;
                OnPropertyChanged("IsChosen");
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded == value)
                    return;

                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return text;
        }
    }
}
