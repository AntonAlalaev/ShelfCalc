using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfCalc
{
    internal class SGS : StellCalc
    {
        public SGS(string ShelfDistance, string LowerShelf, string Amount) : base(ShelfDistance, LowerShelf, Amount)
        {
            Step = 50;
            Shift = 25;
            StandHeightShift = 2;
            MinimalLowerShelf = 102;
            ShelfHeight = 50;
            MinimalShelfDistance = 100;
            SecondShelfPositionOverride = MinimalLowerShelf;

            //рассчет нижней полки
            GetLowerShelfCalc();

            ShelfPosArray = new List<double>();
            this.ShelfDistance = DistanceToStep(this.ShelfDistance, Shift, Step);

            if (this.Amount <= 0)
            {
                this.Amount = 1;
            }

            BaseBlockName = "SGSOpora";
            BaseShiftDistanceX = 0;
            BaseShiftDistanceX2 = BaseShiftDistanceX;
            BaseShiftDistanceY = -2;
            BaseWidthIncrement = 0;
            BaseWidthIncrement2 = BaseWidthIncrement;

            StandBlockName = "StandSGS";
            StandShiftDistanceX = 0;
            StandShiftDistanceX2 = StandShiftDistanceX;
            StandShiftDistanceY = 0;
            StandWidthIncrement = 0;

            ShelfBlockName = "ShelfSectionSGS";
            ShelfTopBlockName = "ShelfTopSectionSGS";
            ShelfTopShiftDistanceY = 0;
            ShelfShiftDistanceX = 0;
            ShelfShiftDistanceY = 0;
            ShelfWidthIncrement = 0;
            ShelfShiftDistanceX2 = -ShelfShiftDistanceX;



            StandFrontBlockName = "SGS_Stand_front";
            StandFrontShiftX = 0; //was 140

            ShelfFrontBlockName = "ShelfFrontSGS";
            ShelfFrontShifX = 0; // was -73.5
            ShelfFrontLengthIncrement = 0; //was 6
            BaseFrontBlockName = "SGSOporaFront";
            BaseFrontShiftX = -28; // was -100
            BaseFrontIcrement = 100;

            ShelfTopFrontBlockName = "ShelfFrontTopSGS";
            ShelfTopFrontShifX = 0;
            ShelfTopFrontLengthIncrement = 0;
            StandFrontShiftCoeff = -18.5; // was -18.5;

            LowerShelfBlockName = "ShelfSectionBottomSGS";
            LowerShelfFrontBlockName = "ShelfFrontBottomSGS";

            //рассчет положения полок
            GetShelfCalc();

        }
    }
}
