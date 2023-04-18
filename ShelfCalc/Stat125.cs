using System.Collections.Generic;

namespace ShelfCalc
{
    internal class Stat125 : StellCalc
    {
        public Stat125(string ShelfDistance, string LowerShelf, string Amount) : base(ShelfDistance, LowerShelf, Amount)
        {
            Step = 12.5;
            Shift = 5;
            StandHeightShift = 10;
            this.ShelfDistance = DistanceToStep(this.ShelfDistance, Shift, Step);
            MinimalLowerShelf = 42;
            SecondShelfPositionOverride = MinimalLowerShelf;
            ShelfHeight = 30;
            GetLowerShelfCalc();
            ShelfPosArray = new List<double>();
            if (this.Amount <= 0)
            {
                this.Amount = 1;
            }
            GetShelfCalc();
            BaseBlockName = "StatOpora";
            BaseShiftDistanceX = -2;
            BaseShiftDistanceX2 = -13;
            BaseShiftDistanceY = 0;
            BaseWidthIncrement = 12;
            BaseWidthIncrement2 = 22;

            StandBlockName = "Stand125";
            StandShiftDistanceX = 0;
            StandShiftDistanceX2 = -12;
            StandShiftDistanceY = 3;
            StandWidthIncrement = 8;

            ShelfBlockName = "ShelfSection";
            ShelfTopBlockName = ShelfBlockName;
            ShelfTopShiftDistanceY = ShelfShiftDistanceY;
            ShelfShiftDistanceX = 4;
            ShelfShiftDistanceY = -30;
            ShelfWidthIncrement = 0;
            ShelfShiftDistanceX2 = -8;

            StandFrontBlockName = "StandFront";
            StandFrontShiftX = -21.5;

            ShelfFrontBlockName = "ShelfFront";
            ShelfFrontShifX = 16.5;
            ShelfFrontLengthIncrement = -33;
            BaseFrontBlockName = "StatOporaFront";
            BaseFrontShiftX = -21.5;
            BaseFrontIcrement = 43;
            ShelfTopFrontBlockName = "ShelfTopFront";

        }
    }
}
