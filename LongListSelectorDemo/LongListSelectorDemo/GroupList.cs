using Microsoft.Phone.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Copyright (C) Author All rights reserved
/*******************************************************************************************
 * Creation:
 *   Author:          军
 *   Date:            2013/12/23 星期一 10:50:53
 *   Description: 
 *   ClrVersion       4.0.30319.18046        
 * Modification:
 *  Version:       Author:            Date:                    Description:
 *  
*******************************************************************************************/
#endregion

namespace LongListSelectorDemo
{
    /// <summary>
    /// 分组列表类
    /// </summary>
    public class GroupList<T>:List<T>
    {
        //定义一个获得key的委托
        public delegate string GetKeyDelegate(T item);
        public string Key { get; private set; }
        public GroupList(string key)
        {
            this.Key = key;
        }
        //根据不同的文化生成分组里表的Key列表
        public static List<GroupList<T>> CreateGroups(SortedLocaleGrouping slg)
        {
            List<GroupList<T>> list = new List<GroupList<T>>();
            foreach (string key in slg.GroupDisplayNames)
            {
                list.Add(new GroupList<T>(key));
            }
            return list;
        }
        public static List<GroupList<T>> CreateGroups(IEnumerable<T> items, CultureInfo ci, GetKeyDelegate getKey, bool sort)
        {
            SortedLocaleGrouping slg = new SortedLocaleGrouping(ci);
            List<GroupList<T>> list = CreateGroups(slg);
            foreach (T item in items)
            {
                int index = 0;
                if (slg.SupportsPhonetics)
                {

                }
                else
                {
                    index=slg.GetGroupIndex(getKey(item));
                }
                if (index >= 0 && index < list.Count)
                {
                    list[index].Add(item);
                }
            }
            if (sort)
            {
                foreach (GroupList<T> group in list)
                {
                    group.Sort((c0, c1) => { return ci.CompareInfo.Compare(getKey(c0), getKey(c1)); });
                }
            }
            return list;
        }
    }
}
