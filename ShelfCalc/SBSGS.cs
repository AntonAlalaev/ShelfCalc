using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfCalc
{
    internal class SBSGS : StellCalc
    {
        public SBSGS(string ShelfDistance, string LowerShelf, string Amount) : base(ShelfDistance, LowerShelf, Amount)
        {
            Step = 50;
            Shift = 25;
            StandHeightShift = 182;
            MinimalLowerShelf = 282;
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

            BaseBlockName = "SBSGSOporaSide";
            BaseShiftDistanceX = -12;
            BaseShiftDistanceX2 = BaseShiftDistanceX;
            BaseShiftDistanceY = 0;
            BaseWidthIncrement = 24;
            BaseWidthIncrement2 = BaseWidthIncrement;

            StandBlockName = "StandSGS";
            StandShiftDistanceX = 0;
            StandShiftDistanceX2 = StandShiftDistanceX;
            StandShiftDistanceY = 182;
            StandWidthIncrement = 0;

            ShelfBlockName = "ShelfSectionSGS";
            ShelfTopBlockName = "ShelfTopSectionSGS";
            ShelfTopShiftDistanceY = -50;
            ShelfShiftDistanceX = 0;
            ShelfShiftDistanceY = -50;

            ShelfWidthIncrement = 0;
            ShelfShiftDistanceX2 = -ShelfShiftDistanceX;



            StandFrontBlockName = "SGS_Stand_front";
            StandFrontShiftX = -55;

            ShelfFrontBlockName = "ShelfFrontSGS";
            ShelfFrontShifX = -15;

            ShelfFrontLengthIncrement = 54;
            BaseFrontBlockName = "SBSGSOporaFront";
            BaseFrontShiftX = -57.5;
            BaseFrontIcrement = 115;

            ShelfTopFrontBlockName = "ShelfFrontTopSGS";
            ShelfTopFrontShifX = ShelfFrontShifX;
            ShelfTopFrontLengthIncrement = ShelfFrontLengthIncrement;
            StandFrontShiftCoeff = -15;

            LowerShelfBlockName = "ShelfSectionBottomSGS";
            LowerShelfFrontBlockName = "ShelfFrontBottomSGS";

            //рассчет положения полок
            GetShelfCalc();

        }





        /// <summary>
        /// Рассчитиывает положение полок по заданному шагу перфорации
        /// </summary>
        public override void GetShelfCalc()
        {
            // Высота расстояние между поверхностями полок (учитывая толщину полки)
            double ShelfDistanceWithHeight = ShelfDistance + ShelfHeight;

            for (int i = 0; i < Amount; i++)
            {
                if (i == 0)
                {
                    ShelfPosArray.Add(StepPosition(LowerShelf + ShelfDistanceWithHeight, LowerShelf, Step));
                }
                else
                {
                    // положения обычных полок
                    ShelfPosArray.Add(StepPosition(ShelfPosArray[i - 1] + ShelfDistanceWithHeight, LowerShelf, Step));

                }
            }

            UpperShelf = ShelfPosArray[ShelfPosArray.Count - 1];
            TotalHeight = UpperShelf;
            StandHeight = UpperShelf - StandHeightShift;
        }
    }
}
