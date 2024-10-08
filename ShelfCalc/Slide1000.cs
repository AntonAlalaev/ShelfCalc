﻿using System.Collections.Generic;

namespace ShelfCalc
{
    internal class Slide1000 : StellCalc
    {
        /// <summary>
        /// Высота верхней полки (зазор от рабочей поверхности до нижней части
        /// </summary>
        public double TopShelfHeight;

        /// <summary>
        /// Введенное расстояние между полками - необходимо для расчета положения верхней полки
        /// </summary>
        public double EnterredShelfDistance;

        /// <summary>
        /// Признак тали
        /// </summary>
        public bool Tall;

        /// <summary>
        /// Приращение высоты стоек при добавлении тали
        /// </summary>
        private const int TallIncrement = 700;

        public Slide1000(string ShelfDistance, string LowerShelf, string Amount, bool tall = false) : base(ShelfDistance, LowerShelf, Amount)
        {
            Tall = tall;
            Step = 50;
            Shift = 21.5; // was 28.5
            StandHeightShift = 5;
            TopShelfHeight = 80;
            MinimalLowerShelf = 243;
            ShelfHeight = 80;
            MinimalShelfDistance = 120;
            SecondShelfPositionOverride = MinimalLowerShelf;
            EnterredShelfDistance = this.ShelfDistance;

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

            SideTallBlockName = "Tall_side";
            FrontTallBlockName = "TallFront";

            TallLengthIncrementX = 280;
            TallWidthIncrementX = 0;
            TallLengthShiftX = -140;

            GetShelfCalc();
            //ShelfTopShiftDistanceX;
            //ShelfTopShiftDistanceY;



        }

        /*
        public override double DistanceToStep(double Distance, double Shift, double Step)
        {
            if (Distance <= MinimalShelfDistance)
            {
                return MinimalShelfDistance;
            }
            double CalcStepToRet = MinimalShelfDistance; // was 0

            while (true)
            {
                CalcStepToRet += Step;
                if (Math.Round(CalcStepToRet, 1) >= Distance)
                {
                    return CalcStepToRet;
                }
            }
        }
        */

        /// <summary>
        /// Вылет балки тали
        /// </summary>
        /// <param name="WidthClear"></param>
        /// <returns></returns>
        public override double TallLedge(double WidthClear = 800)
        {
            return 350;
        }

        //TallLedge = 350;

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
                    {
                        // положение верхней полки, т.к. у нее другая толщина и позиционирование
                        // позиция верхней полки относительно стандартной полки с первой установки равна 51,5 мм + толщина верхней полки 80 мм
                        double UpperShelfIncrease = 25 + 7 + 80;
                        //double CurrentPos = ShelfPosArray[i - 1];
                        while (true)
                        {
                            if (UpperShelfIncrease >= EnterredShelfDistance + TopShelfHeight)
                                break;
                            UpperShelfIncrease += Step;
                        }
                        ShelfPosArray.Add(UpperShelfIncrease + ShelfPosArray[i - 1]);

                        //ShelfPosArray.Add(StepPosition(ShelfPosArray[i - 1] + ShelfDistance + TopShelfHeight, LowerShelf, Step));
                    }
                }
            }

            UpperShelf = ShelfPosArray[ShelfPosArray.Count - 1];
            TotalHeight = UpperShelf;
            StandHeight = UpperShelf - StandHeightShift;

            // если есть таль то высота стоек и высота стеллажа прирастает
            if (Tall == true)
            {
                StandHeight = StandHeight + TallIncrement;
                TotalHeight = UpperShelf + TallIncrement;
            }
        }

    }
}
