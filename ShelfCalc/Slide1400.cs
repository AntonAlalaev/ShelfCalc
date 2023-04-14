using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfCalc
{
    internal class Slide1400 : StellCalc
    {
        /// <summary>
        /// Высота верхней полки (зазор от рабочей поверхности до нижней части
        /// </summary>
        public double TopShelfHeight;

        public Slide1400(string ShelfDistance, string LowerShelf, string Amount) : base(ShelfDistance, LowerShelf, Amount)
        {
            Step = 50;
            Shift = 28.5;
            StandHeightShift = 6.5;
            this.ShelfDistance = DistanceToStep(this.ShelfDistance, Shift, Step);
            TopShelfHeight = 80;
            MinimalLowerShelf = 223.5;
            ShelfHeight = 171.5;
            SecondShelfPositionOverride = MinimalLowerShelf;
            GetLowerShelfCalc();
            ShelfPosArray = new List<double>();


            if (this.Amount <= 0)
            {
                this.Amount = 1;
            }
            GetShelfCalc();
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

            ShelfBlockName = "ShelfSectionSlide1400";
            ShelfShiftDistanceX = 0;
            ShelfShiftDistanceY = -176.5;
            ShelfWidthIncrement = 140;
            ShelfShiftDistanceX2 = -ShelfShiftDistanceX;

            StandFrontBlockName = "StandSlideFront";
            StandFrontShiftX = -140; //was 140
            ShelfFrontBlockName = "ShelfFrontSlide1400";
            ShelfFrontShifX = -73.5;
            ShelfFrontLengthIncrement = 6;
            BaseFrontBlockName = "SlideOporaFront1400";
            BaseFrontShiftX = -100;
            BaseFrontIcrement = 399;

            ShelfTopFrontBlockName = "ShelfTopFrontSlide1400";
            //ShelfTopShiftDistanceX;
            //ShelfTopShiftDistanceY;



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
                    if (i != Amount - 1)
                        // положения обычных полок
                        ShelfPosArray.Add(StepPosition(ShelfPosArray[i - 1] + ShelfDistanceWithHeight, LowerShelf, Step));
                    else
                        // положение верхней полки, т.к. у нее другая толщина
                        ShelfPosArray.Add(StepPosition(ShelfPosArray[i - 1] + ShelfDistance + TopShelfHeight, LowerShelf, Step));
                }
            }
            
            UpperShelf = ShelfPosArray[ShelfPosArray.Count - 1];
            TotalHeight = UpperShelf;
            StandHeight = UpperShelf - StandHeightShift;
        }

    }

}
