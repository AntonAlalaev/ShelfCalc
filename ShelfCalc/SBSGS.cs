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

            BaseBlockName = "SBSGSOpora";
            BaseShiftDistanceX = -4;
            BaseShiftDistanceX2 = BaseShiftDistanceX;
            BaseShiftDistanceY = 0;
            BaseWidthIncrement = 0;
            BaseWidthIncrement2 = BaseWidthIncrement;

            StandBlockName = "StandSBSGS";
            StandShiftDistanceX = 0;
            StandShiftDistanceX2 = StandShiftDistanceX;
            StandShiftDistanceY = 2;
            StandWidthIncrement = 0;

            ShelfBlockName = "ShelfSectionSBSGS";
            ShelfTopBlockName = "ShelfTopSectionSBSGS";
            ShelfTopShiftDistanceY = -50;
            ShelfShiftDistanceX = 0;
            ShelfShiftDistanceY = -50;

            ShelfWidthIncrement = 0;
            ShelfShiftDistanceX2 = -ShelfShiftDistanceX;



            StandFrontBlockName = "SBSGS_Stand_front";
            StandFrontShiftX = -55;

            ShelfFrontBlockName = "ShelfFrontSBSGS";
            ShelfFrontShifX = -15;

            ShelfFrontLengthIncrement = 54;
            BaseFrontBlockName = "SBSGSOporaFront";
            BaseFrontShiftX = -57.5;
            BaseFrontIcrement = 115;

            ShelfTopFrontBlockName = "ShelfFrontTopSBSGS";
            ShelfTopFrontShifX = ShelfFrontShifX;
            ShelfTopFrontLengthIncrement = ShelfFrontLengthIncrement;
            StandFrontShiftCoeff = -15;

            LowerShelfBlockName = "ShelfSectionBottomSBSGS";
            LowerShelfFrontBlockName = "ShelfFrontBottomSBSGS";

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
