using System;
using System.Collections.Generic;

namespace ShelfCalc
{
    /// <summary>
    /// Базовый класс для рассчета полок и высот стеллажей
    /// </summary>
    public class StellCalc
    {
        public StellCalc(string ShelfDistance, String LowerShelf, string Amount)
        {
            try
            {
                this.ShelfDistance = double.Parse(ShelfDistance, System.Globalization.CultureInfo.InvariantCulture);
                this.LowerShelf = double.Parse(LowerShelf, System.Globalization.CultureInfo.InvariantCulture);
                this.Amount = int.Parse(Amount, System.Globalization.CultureInfo.InvariantCulture);
                ShelfPosArray = new List<double>();
            }
            catch
            {
                //MessageBox.Show("Ошибка ввода!");
                // Ошибка ввода
            }
        }

        /// <summary>
        /// Конструктор класса для создания стационарного стеллажа
        /// </summary>
        /// <param name="ShelfDistance">Расстояние между полками</param>
        /// <param name="LowerShelf">Положение нижней полки</param>
        /// <param name="Amount">Количесво полок</param>
        public StellCalc(double ShelfDistance, double LowerShelf, int Amount)
        {
            this.ShelfDistance = ShelfDistance;
            this.LowerShelf = LowerShelf;
            this.Amount = Amount;
        }

        /// <summary>
        /// Наименование блока для полки
        /// </summary>
        public string ShelfBlockName;

        /// <summary>
        /// Наименование блока для стойки
        /// </summary>
        public string StandBlockName;

        /// <summary>
        /// Наименование блока для стойки вид спереди
        /// </summary>
        public string StandFrontBlockName;

        /// <summary>
        /// Наименование блока для вида полки спереди
        /// </summary>
        public string ShelfFrontBlockName;


        /// <summary>
        /// Наименование блока для верхней полки на виде сбоку
        /// </summary>
        public string ShelfTopBlockName;

        /// <summary>
        /// Наименование блока для опоры
        /// </summary>
        public string BaseBlockName;

        /// <summary>
        /// Наименование блока опоры для вида спереди
        /// </summary>
        public string BaseFrontBlockName;

        /// <summary>
        /// Наименование блока верхней полки
        /// </summary>
        public string ShelfTopFrontBlockName;

        /// <summary>
        /// Наименование блока нижней полки
        /// </summary>
        public string LowerShelfBlockName;

        /// <summary>
        /// Наименование блока нижней полки вид спереди
        /// </summary>
        public string LowerShelfFrontBlockName;

        /// <summary>
        /// Смещение координат блока опоры по X
        /// </summary>
        public double BaseShiftDistanceX;

        /// <summary>
        /// Смещение координат 2-х блока опоры по X
        /// </summary>
        public double BaseShiftDistanceX2;

        /// <summary>
        /// Смещение координат блока опоры по Y
        /// </summary>
        public double BaseShiftDistanceY;

        /// <summary>
        /// Приращение глубины опоры
        /// </summary>
        public double BaseWidthIncrement;

        /// <summary>
        /// Приращение глубины опоры для 2-х сторонней
        /// </summary>
        public double BaseWidthIncrement2;

        /// <summary>
        /// Смещение координат блока стойки по X
        /// </summary>
        public double StandShiftDistanceX;

        /// <summary>
        /// Смещение координат блока стойки по X
        /// </summary>
        public double StandShiftDistanceX2;

        /// <summary>
        /// Смещение координат блока стойки по Y
        /// </summary>
        public double StandShiftDistanceY;

        /// <summary>
        /// Смещение координат блока полки по X
        /// </summary>
        public double ShelfShiftDistanceX;

        /// <summary>
        /// Смещение координат блока полки по X
        /// </summary>
        public double ShelfShiftDistanceX2;

        /// <summary>
        /// Смещение координат блока полки по Y
        /// </summary>
        public double ShelfShiftDistanceY;


        /// <summary>
        /// Смещение координат блока верхней полки по Y
        /// </summary>
        public double ShelfTopShiftDistanceY;


        /// <summary>
        /// Смещение вида стойки спереди относительно Х от оси полки
        /// </summary>
        public double StandFrontShiftX;

        /// <summary>
        /// Смещение полки на виде спереди относительно X от оси реальной полки
        /// </summary>
        public double ShelfFrontShifX;

        /// <summary>
        /// Инкремент длины полки на виде спереди от реальной длины
        /// </summary>
        public double ShelfFrontLengthIncrement;


        /// <summary>
        /// Смещение координат опоры по X вид спереди
        /// </summary>
        public double BaseFrontShiftX;

        /// <summary>
        /// Удлинение опоры по X вид спереди
        /// </summary>
        public double BaseFrontIcrement;


        /// <summary>
        /// Приращение глубины полки
        /// </summary>
        public double ShelfWidthIncrement;

        /// <summary>
        /// Приращение Глубины стойки
        /// </summary>
        public double StandWidthIncrement;

        /// <summary>
        /// Список с позициями полок
        /// </summary>
        public List<double> ShelfPosArray;

        /// <summary>
        /// Смещение грани относительно перфорации
        /// </summary>
        internal double Shift;

        /// <summary>
        /// Расстояние между полками
        /// </summary>
        internal double ShelfDistance;

        /// <summary>
        /// Положение нижней полки
        /// </summary>
        internal double LowerShelf;

        /// <summary>
        /// Количество полок
        /// </summary>
        internal int Amount;

        /// <summary>
        /// Шаг перфорации
        /// </summary>
        internal double Step;

        /// <summary>
        /// Смещение высоты стойки от высоты стеллажа
        /// </summary>
        internal double StandHeightShift;

        /// <summary>
        /// Положение второй полки, если оно отличается от первой измененным шагом перфорации
        /// </summary>
        internal double SecondShelfPositionOverride;


        // Выходные цифры
        #region 

        /// <summary>
        /// Высота верхней полки крышки
        /// </summary>
        internal double UpperShelf;

        /// <summary>
        /// Высота стойки
        /// </summary>
        internal double StandHeight;

        /// <summary>
        /// Общая высота стеллажа
        /// </summary>
        internal double TotalHeight;

        /// <summary>
        /// Минимально возможное положение нижней полки
        /// </summary>
        internal double MinimalLowerShelf;

        /// <summary>
        /// Высота полки
        /// </summary>
        internal double ShelfHeight;

        /// <summary>
        /// Минимальное расстояние между полками
        /// </summary>
        internal double MinimalShelfDistance;


        #endregion


        // Общие расчетные процедуры

        /// <summary>
        /// Определеяет расстояние между полками согласно шагу
        /// </summary>
        /// <param name="Distance">расстояние между полками</param>
        /// <param name="Shift">Смещение</param>
        /// <param name="Step">Шаг</param>
        /// <returns></returns>
        public virtual double DistanceToStep(double Distance, double Shift, double Step)
        {
            if (Distance <= MinimalShelfDistance)
            {
                return MinimalShelfDistance;
            }
            double CalcStepToRet = MinimalShelfDistance; // was 0

            while (true)
            {
                CalcStepToRet += Step;
                if (Math.Round(CalcStepToRet , 1) >= Distance)
                {
                    return CalcStepToRet;
                }
            }
        }



        /// <summary>
        /// Приводит заданное значение к шагу перфорации
        /// </summary>
        /// <param name="Position">Позиция</param>
        /// <param name="FirstShelfPosition">Положение верхней полки</param>
        /// <param name="Step">Шаг перфорации</param>
        /// <returns>Вычисленное положение полки</returns>
        public virtual double StepPosition(double Position, double FirstShelfPosition, double Step)
        {
            if (Math.Round(Position, 1) <= Math.Round(FirstShelfPosition, 1))
            {
                return FirstShelfPosition;
            }

            // Если вычисленное положение находится между совсем нижней полкой, которая прикручивается к
            // каркасу рамы и первой полкой, которую можно установить на перфорацию

            if (Math.Round(Position, 1) <= SecondShelfPositionOverride && Math.Round(Position, 1) > MinimalLowerShelf)
            {
                return SecondShelfPositionOverride;
            }

            double CurrentPosition = 0;
            while (true)
            {
                if (Math.Round(SecondShelfPositionOverride + CurrentPosition, 1) >= Math.Round(Position, 1))
                {
                    return (Math.Round(SecondShelfPositionOverride + CurrentPosition, 1));
                }
                CurrentPosition += Step;
            }
        }

        /// <summary>
        /// Рассчитиывает положение полок по заданному шагу перфорации
        /// </summary>
        public virtual void GetShelfCalc()
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
                    ShelfPosArray.Add(StepPosition(ShelfPosArray[i - 1] + ShelfDistanceWithHeight, LowerShelf, Step));
                }
            }
            UpperShelf = ShelfPosArray[ShelfPosArray.Count - 1];
            TotalHeight = UpperShelf;
            StandHeight = UpperShelf - StandHeightShift;
        }

        /// <summary>
        /// Расчет положения нижней полки
        /// </summary>
        public virtual void GetLowerShelfCalc()
        {
            //SecondShelfPositionOverride


            // сначала проверим минимальную высоту полки на допустимость
            // если она ниже или равна нижнему положению полки, то возвращем нижнее положение полки
            if (Math.Round(LowerShelf, 1) <= Math.Round(MinimalLowerShelf, 1))
            {
                LowerShelf = MinimalLowerShelf;
                return;
            }

            // Условие выполняется только когда есть отклонение в положении второй полки
            // Вернее когда положение первой полки не совсем стандартное, а вторая полка расположена нормально согласно перфорации
            // Если нет отклонения в перфорации при установке первой полки то при инициализации класса
            // необходимо уравнять SecondShelfPositionOverride и MinimalLowerShelf
            if (Math.Round(LowerShelf, 1) <= SecondShelfPositionOverride && Math.Round(LowerShelf, 1) > MinimalLowerShelf)
            {
                LowerShelf = SecondShelfPositionOverride;
            }

            // текущее расчетное положение полки
            double CurrentPosition = 0;

            // повторяем цикл с заданным шагом перфорации до тех пор пока расчетное положение будет больше или равно заданному положению
            while (true)
            {
                if (Math.Round((SecondShelfPositionOverride + CurrentPosition), 1) >= Math.Round(LowerShelf, 1))
                {
                    LowerShelf = Math.Round((SecondShelfPositionOverride + CurrentPosition), 1);
                    return;
                }
                CurrentPosition += Step;
            }
        }

        /// <summary>
        /// Смещение блока верхней полки вида спереди по Х
        /// </summary>
        public double ShelfTopFrontShifX;

        /// <summary>
        /// Приращение длины блока верхней полки вид спереди
        /// </summary>
        public double ShelfTopFrontLengthIncrement;

        /// <summary>
        /// Сещение второй стойки на виде спереди
        /// </summary>
        public double StandFrontShiftCoeff;
    }

}
