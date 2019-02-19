﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.ReactiveCollections
{
    public delegate void ReactiveCollectionEventHandler<T>(object sender, ReactiveCollectionEventArgs<T> e);

    public class ReactiveCollectionEventArgs<T> : EventArgs
    {
        public IEnumerable<T> AddedItems { get; private set; }
        public IEnumerable<T> RemovedItems { get; private set; }

        public ReactiveCollectionEventArgs() : base()
        {
            AddedItems = new List<T>();
            RemovedItems = new List<T>();
        }
    }

    public class ReactiveCollectionItemEventArgs : EventArgs
    {
        public string PropertyName { get; set; }
        public object Collection { get; set; }
        public object CollectionNode { get; set; }
    }

    public class ReactiveCollection<T> : Collection<T>
    {
        #region Methods

        public new void Add(T item)
        {
            base.Add(item);

            if (item is IReactiveItem)
            {
                (item as IReactiveItem).PropertyChanged += ReactiveCollection_PropertyChanged;
            }

            List<T> addedItems = new List<T> { item };
            OnItemAdded(addedItems);
            OnCollectionChanged(addedItems, new List<T> { });
        }

        public new bool Remove(T item)
        {
            bool sucess = base.Remove(item);

            if (item is IReactiveItem)
            {
                (item as IReactiveItem).PropertyChanged -= ReactiveCollection_PropertyChanged;
            }

            List<T> removedItems = new List<T> { item };
            OnItemRemoved(removedItems);
            OnCollectionChanged(new List<T> { }, removedItems);
            return sucess;
        }

        private void ReactiveCollection_PropertyChanged(object sender, EventArgs e)
        {
            var eArgs = e as ReactiveCollectionItemEventArgs;
            eArgs.Collection = this;
            ItemPropertyChanged?.Invoke(sender, eArgs);
        }

        #endregion

        #region Events

        public event ReactiveCollectionEventHandler<T> ItemAdded;
        public event ReactiveCollectionEventHandler<T> ItemRemoved;
        public event ReactiveCollectionEventHandler<T> CollectionChanged;
        public event EventHandler ItemPropertyChanged;

        private void OnItemAdded(IEnumerable<T> addedItems)
        {
            if (ItemAdded != null)
            {
                ReactiveCollectionEventArgs<T> e = new ReactiveCollectionEventArgs<T>();
                (e.AddedItems as List<T>).AddRange(addedItems);
                ItemAdded(this, e);
            }
        }

        private void OnItemRemoved(IEnumerable<T> removedItems)
        {
            if (ItemRemoved != null)
            {
                ReactiveCollectionEventArgs<T> e = new ReactiveCollectionEventArgs<T>();
                (e.RemovedItems as List<T>).AddRange(removedItems);
                ItemRemoved(this, e);
            }
        }

        private void OnCollectionChanged(IEnumerable<T> addedItems, IEnumerable<T> removedItems)
        {
            if (CollectionChanged != null)
            {
                ReactiveCollectionEventArgs<T> e = new ReactiveCollectionEventArgs<T>();
                (e.AddedItems as List<T>).AddRange(addedItems);
                (e.RemovedItems as List<T>).AddRange(removedItems);
                CollectionChanged(this, e);
            }
        }

        #endregion
    }
}