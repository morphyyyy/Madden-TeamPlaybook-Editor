﻿using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MaddenTeamPlaybookEditor.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static int RemoveAll<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }
    }
}
