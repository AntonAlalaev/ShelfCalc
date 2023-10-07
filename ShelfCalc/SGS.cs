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
            MinimalShelfDistance = 50;
            SecondShelfPositionOverride = MinimalLowerShelf;

            //рассчет нижней полки
            GetLowerShelfCalc();

            ShelfPosArray = new List<double>();
            this.ShelfDistance = DistanceToStep(this.ShelfDistance, Shift, Step);

            if (this.Amount <= 0)
            {
                this.Amount = 1;
            }

            BaseBlockName = "SlideOpora";
            BaseShiftDistanceX = -19;
            BaseShiftDistanceX2 = BaseShiftDistanceX;
            BaseShiftDistanceY = 0;
            BaseWidthIncrement = 81;
            BaseWidthIncrement2 = BaseWidthIncrement;

            StandBlockName = "StandSlide";
            StandShiftDistanceX = 0;
            StandShiftDistanceX2 = StandShiftDistanceX;
            StandShiftDistanceY = 4;
            StandWidthIncrement = 0;

            ShelfBlockName = "ShelfSectionSlide1000";
            ShelfTopBlockName = "ShelfTopSection";
            ShelfTopShiftDistanceY = -80;
            ShelfShiftDistanceX = 0;
            ShelfShiftDistanceY = -80;
            ShelfWidthIncrement = 0;
            ShelfShiftDistanceX2 = -ShelfShiftDistanceX;



            StandFrontBlockName = "StandSlideFront";
            StandFrontShiftX = -140; //was 140
            ShelfFrontBlockName = "ShelfFrontSlide1000";
            ShelfFrontShifX = -73.5; // was -73.5
            ShelfFrontLengthIncrement = 6 + 14; //was 6
            BaseFrontBlockName = "SlideOporaFront1400";
            BaseFrontShiftX = -199.5; // was -100
            BaseFrontIcrement = 399;

            ShelfTopFrontBlockName = "ShelfTopFrontSlide1400";
            ShelfTopFrontShifX = -73.5;
            ShelfTopFrontLengthIncrement = 6;
            StandFrontShiftCoeff = -18.5; // was -18.5;

            LowerShelfBlockName = "ShelfSectionBottomSlide1000";
            LowerShelfFrontBlockName = "ShelfFrontBottomSlide1000";

            //рассчет положения полок
            GetShelfCalc();

        }
    }
}
