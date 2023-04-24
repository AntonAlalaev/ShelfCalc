using System.Collections.Generic;

namespace ShelfCalc
{
    internal class SeriesL125 : StellCalc
    {
        public SeriesL125(string ShelfDistance, string LowerShelf, string Amount) : base(ShelfDistance, LowerShelf, Amount)
        {
            // Шаг перфорации стойки
            Step = 12.5;

            MinimalShelfDistance = 20;

            // Смещение верха полки относительно перфорации
            Shift = 5;

            // Разница между высотой стойки и высотой самой верхней полки
            StandHeightShift = 114;

            // Приведение расстояния между полками к шагу перфорации
            this.ShelfDistance = DistanceToStep(this.ShelfDistance, Shift, Step);

            // Минимально возможная высота нижней полки
            MinimalLowerShelf = 107;

            SecondShelfPositionOverride = 153 - 7;

            // Толщина полки
            ShelfHeight = 30;

            // Создание экземпляра массива положений полок
            ShelfPosArray = new List<double>();
            if (this.Amount <= 0)
            {
                this.Amount = 1;
            }
            GetLowerShelfCalc();
            GetShelfCalc();

            BaseBlockName = "LSeriesOpora2";
            BaseShiftDistanceX = -2;
            BaseShiftDistanceX2 = -13;
            BaseShiftDistanceY = 0;
            BaseWidthIncrement = 12;
            BaseWidthIncrement2 = 25;

            StandBlockName = "Stand125";
            StandShiftDistanceX = 0;
            StandShiftDistanceX2 = -12;
            StandShiftDistanceY = 107;
            StandWidthIncrement = 8;

            ShelfBlockName = "ShelfSection";
            ShelfTopBlockName = ShelfBlockName;
            ShelfShiftDistanceY = -30;
            ShelfTopShiftDistanceY = ShelfShiftDistanceY;
            ShelfShiftDistanceX = 4;
            ShelfWidthIncrement = 0;
            ShelfShiftDistanceX2 = -8;

            StandFrontBlockName = "StandFront";
            StandFrontShiftX = -21.5;
            ShelfFrontBlockName = "ShelfFront";
            ShelfFrontShifX = 16.5;
            ShelfFrontLengthIncrement = -33;
            BaseFrontBlockName = "SeriesLFrontOpora";
            BaseFrontShiftX = -52.5;
            BaseFrontIcrement = 105;
            ShelfTopFrontBlockName = "ShelfTopFront";
            ShelfTopFrontShifX = ShelfFrontShifX;
            ShelfTopFrontLengthIncrement = ShelfFrontLengthIncrement;
            StandFrontShiftCoeff = 0;
            LowerShelfBlockName = ShelfBlockName;
            LowerShelfFrontBlockName = ShelfFrontBlockName;

        }

    }
}
