﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public static class BasketManager
    {
        public static ObservableCollection<BasketItem> BasketItems = new ObservableCollection<BasketItem>();

        public static ObservableCollection<BasketItem> GetBasketItems() { return BasketItems; }

        public static void AddBasketItem(BasketItem basketItem)
        {
            BasketItems.Add(basketItem);
        }

        public static void DeleteBasketItem(int roomId)
        {
            var basketItem = BasketItems.FirstOrDefault(i => i.RoomId == roomId);
            BasketItems.Remove(basketItem);
        }

        public static decimal CalculateTotalPrice()
        {
            var totalPrice = 0m;
            foreach(var item in BasketItems)
            {
                var numberOfDays = (item.StartDate - item.EndDate).Days;
                totalPrice += item.Price * numberOfDays;
            }
            return totalPrice;
        }
    }
}
