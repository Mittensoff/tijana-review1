using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodDelivery.DBModel;

namespace FoodDelivery.Models
{
    public class RestaurantDistance
    {
        private double _distance;
        private Restaurant _restaurant;

        public double Distance { get { return _distance; } set { this._distance = value; } }
        public Restaurant Restaurant { get { return _restaurant; } set { this._restaurant = value; } }

        public RestaurantDistance(double dist, Restaurant rest)
        {
            this._distance = dist;
            this._restaurant = rest;
        }
    }
}