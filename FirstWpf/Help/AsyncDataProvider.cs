using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

namespace Infrastructure.Shared.DataProviders
{
    /// <summary>
    /// Emulates a long items getting process using some delay of getting of each item
    /// </summary>
    public static class AsyncDataProvider
    {
        /// <summary>
        /// Default delay for each item getting
        /// </summary>
        private const int _DefaultDelayTime = 300;
        
        public static List<string> GetItems()
        {
            return GetItems(_DefaultDelayTime);
        }
        
        public static List<string> GetItems(int delayTime)
        {            
            List<string> items = new List<string>();            
            foreach (string item in Enum.GetNames(typeof(AttributeTargets)).OrderBy(item => item.ToLower()))
            {
                items.Add(item);
                // Syntetic delay to emulate a long items getting process
                Thread.Sleep(delayTime);
            }

            return items;
        }        
    }    
}
