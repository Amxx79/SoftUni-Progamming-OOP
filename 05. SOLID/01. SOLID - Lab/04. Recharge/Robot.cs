﻿using System;
using _04._Recharge.Interfaces;

namespace P04.Recharge
{
    public class Robot : RobotWorker
    {
        private int capacity;
        private int currentPower;

        public Robot(string id, int capacity) : base(id)
        {
            this.capacity = capacity;
        }

        public int Capacity
        {
            get { return this.capacity; }
        }

        public int CurrentPower
        {
            get { return this.currentPower; }
            set { this.currentPower = value; }
        }

        public void Work(int hours)
        {
            if (hours > this.currentPower)
            {
                hours = currentPower;
            }

            base.Work(hours);
            this.currentPower -= hours;
        }

        public override void Recharge()
        {
            this.currentPower = this.capacity;
        }
    }
}