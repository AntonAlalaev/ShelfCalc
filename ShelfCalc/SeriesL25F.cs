using System.Collections.Generic;

namespace ShelfCalc
{
    internal class SeriesL25F : StellCalc
    {
        public SeriesL25F(string ShelfDistance, string LowerShelf, string Amount) : base(ShelfDistance, LowerShelf, Amount)
        {
            // Шаг перфорации стойки
            Step = 25;

            // Смещение верха полки относительно перфорации
            Shift = 5;

            MinimalShelfDistance = 20;

            // Разница между высотой стойки и высотой самой верхней полки
            StandHeightShift = 114 + 20;

            // Приведение расстояния между полками к шагу перфорации
            this.ShelfDistance = DistanceToStep(this.ShelfDistance, Shift, Step);

            // Минимально возможная высота нижней полки
            MinimalLowerShelf = 107 + 20;

            SecondShelfPositionOverride = 153 - 7 + 20;

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

            BaseBlockName = "LSeriesOporaF2";
            BaseShiftDistanceX = -2;
            BaseShiftDistanceX2 = -13;
            BaseShiftDistanceY = 0;
            BaseWidthIncrement = 12;
            BaseWidthIncrement2 = 25;

            StandBlockName = "Stand25";
            StandShiftDistanceX = 0;
            StandShiftDistanceX2 = -12;
            StandShiftDistanceY = 127;
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
            BaseFrontBlockName = "SeriesLFrontOporaF";
            BaseFrontShiftX = -43.5;
            BaseFrontIcrement = 87;
            ShelfTopFrontBlockName = "ShelfTopFront";
            ShelfTopFrontShifX = ShelfFrontShifX;
            ShelfTopFrontLengthIncrement = ShelfFrontLengthIncrement;
            StandFrontShiftCoeff = 0;
        }
    }
}
