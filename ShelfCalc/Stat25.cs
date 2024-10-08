﻿using System.Collections.Generic;

namespace ShelfCalc
{
    public class Stat25 : StellCalc
    {
        public Stat25(string ShelfDistance, string LowerShelf, string Amount) : base(ShelfDistance, LowerShelf, Amount)
        {
            Step = 25;
            Shift = 5;
            StandHeightShift = 10;
            MinimalShelfDistance = 20;
            this.ShelfDistance = DistanceToStep(this.ShelfDistance, Shift, Step);
            MinimalLowerShelf = 42;
            ShelfHeight = 30;
            SecondShelfPositionOverride = MinimalLowerShelf;
            GetLowerShelfCalc();
            ShelfPosArray = new List<double>();


            if (this.Amount <= 0)
            {
                this.Amount = 1;
            }
            GetShelfCalc();
            StoikaHeightCalc();

            BaseBlockName = "StatOpora";
            BaseShiftDistanceX = -2;
            BaseShiftDistanceX2 = -13;
            BaseShiftDistanceY = 0;
            BaseWidthIncrement = 12;
            BaseWidthIncrement2 = 22;

            StandBlockName = "Stand25";
            StandShiftDistanceX = 0;
            StandShiftDistanceX2 = -12;
            StandShiftDistanceY = 3;
            StandWidthIncrement = 8;

            ShelfBlockName = "ShelfSection";
            ShelfTopBlockName = ShelfBlockName;
            ShelfShiftDistanceX = 4;
            ShelfShiftDistanceY = -30;
            ShelfTopShiftDistanceY = ShelfShiftDistanceY;
            ShelfWidthIncrement = 0;
            ShelfShiftDistanceX2 = -8;

            StandFrontBlockName = "StandFront";
            StandFrontShiftX = -21.5;
            //StandFrontShiftY = 
            ShelfFrontBlockName = "ShelfFront";
            ShelfFrontShifX = 16.5;
            ShelfFrontLengthIncrement = -33;
            BaseFrontBlockName = "StatOporaFront";
            BaseFrontShiftX = -21.5;
            BaseFrontIcrement = 43;

            ShelfTopFrontBlockName = "ShelfTopFront";
            ShelfTopFrontShifX = ShelfFrontShifX;
            ShelfTopFrontLengthIncrement = ShelfFrontLengthIncrement;
            StandFrontShiftCoeff = 0;
            LowerShelfBlockName = ShelfBlockName;
            LowerShelfFrontBlockName = ShelfFrontBlockName;
        }


    }
}
