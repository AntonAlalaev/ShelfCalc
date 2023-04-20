using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;
using System.Linq;


namespace ShelfCalc
{
    internal class DrawStell
    {
        /// <summary>
        /// Рисует виды стеллажа
        /// </summary>
        /// <param name="Stellar">экземпляр класса стеллажа</param>
        /// <param name="ShelfWidthClear">Глубина полки</param>
        /// <param name="ShelfLength">Длина полки</param>
        /// <param name="LowerShelfDraw">Рисовать нижнюю полку или нет</param>
        /// <param name="DoubleSide">Двухсторонний или односторонний стеллаж</param>
        public static void Draw(StellCalc Stellar, double ShelfWidthClear, double ShelfLength, bool LowerShelfDraw = true, bool DoubleSide = true)
        {
            // получаем путь к сборке
            System.IO.FileStream str = System.Reflection.Assembly.GetExecutingAssembly().GetFile("ShelfCalc.dll");
            string path1 = str.Name;
            int Index = 0;
            for (int i = path1.Length - 1; i >= 0; i--)
            {
                if (path1.ElementAt(i).ToString() == "\\")
                {
                    //Console.WriteLine("\n Index of : " + i + " \n");
                    Index = i;
                    break;
                }
            }
            // присваиваем переменной Path путь к каталогу сборки
            string BlockTemplatePath = path1.Substring(0, Index) + "\\";
            string PathToSourceFile = BlockTemplatePath + "ShelfBlocks.dwg";

            //var oldCult = System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru");
            Document CurrentDocument = Application.DocumentManager.MdiActiveDocument;

            // Устанавливаем точку вставки
            Point3d InsertionPoint = new Point3d(0, 0, 0);

            // Запрашиваем на экране
            InsertionPoint = BlockOperation.getPoint(CurrentDocument);

            if (DoubleSide)
            {
                InsertionPoint = new Point3d(InsertionPoint.X + ShelfWidthClear, InsertionPoint.Y, InsertionPoint.Z);
            }

            DrawSection(Stellar, ShelfWidthClear, LowerShelfDraw, DoubleSide, PathToSourceFile, CurrentDocument, InsertionPoint);

            InsertionPoint = new Point3d(InsertionPoint.X + ShelfWidthClear + 1000, InsertionPoint.Y, InsertionPoint.Z);
            DrawFront(Stellar, LowerShelfDraw, ShelfLength, PathToSourceFile, CurrentDocument, InsertionPoint);

        }

        /// <summary>
        /// Рисует вид стеллажа спереди
        /// </summary>
        /// <param name="Stellar">Стеллаж</param>
        /// <param name="LowerShelfDraw">Рисовать нижнюю полку или нет - для серии Л</param>
        /// <param name="ShelfLength">длина полки</param>
        /// <param name="PathToSourceFile">Путь к файлу с блоками</param>
        /// <param name="CurrentDocument">Текущий документ автокада</param>
        /// <param name="InsertionPoint">Точка вставки</param>
        private static void DrawFront(StellCalc Stellar, bool LowerShelfDraw, double ShelfLength, string PathToSourceFile, Document CurrentDocument, Point3d InsertionPoint)
        {
            // ------------------------
            // Копируем описание блоков
            // ------------------------

            // Стойка
            BlockOperation.CloneBlockToDocument(CurrentDocument, PathToSourceFile, Stellar.StandFrontBlockName);

            // Полка
            BlockOperation.CloneBlockToDocument(CurrentDocument, PathToSourceFile, Stellar.ShelfFrontBlockName);

            //Верхняя полка 
            BlockOperation.CloneBlockToDocument(CurrentDocument, PathToSourceFile, Stellar.ShelfTopFrontBlockName);

            // Опора
            BlockOperation.CloneBlockToDocument(CurrentDocument, PathToSourceFile, Stellar.BaseFrontBlockName);

            // Вставляем стойки
            BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.StandFrontBlockName, "Height", Stellar.StandHeight,
                InsertionPoint.X + Stellar.StandFrontShiftX, InsertionPoint.Y + Stellar.StandShiftDistanceY, InsertionPoint.Z);
            BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.StandFrontBlockName, "Height", Stellar.StandHeight,
                InsertionPoint.X + ShelfLength - Stellar.ShelfFrontShifX + Stellar.StandFrontShiftCoeff,
                InsertionPoint.Y + Stellar.StandShiftDistanceY, InsertionPoint.Z);

            // Вставляем полки
            if (LowerShelfDraw)
                BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfFrontBlockName, "Length", ShelfLength + Stellar.ShelfFrontLengthIncrement,
                    InsertionPoint.X + Stellar.ShelfFrontShifX, InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Stellar.LowerShelf, InsertionPoint.Z);

            for (int i = 0; i < Stellar.ShelfPosArray.Count; i++)
            {
                double Item = Stellar.ShelfPosArray[i];
                if (i == Stellar.ShelfPosArray.Count - 1)
                {
                    BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfTopFrontBlockName, "Length", ShelfLength+Stellar.ShelfTopFrontLengthIncrement,
                        InsertionPoint.X+Stellar.ShelfTopFrontShifX, InsertionPoint.Y + Stellar.ShelfTopShiftDistanceY + Item, InsertionPoint.Z);
                }
                else
                {
                    BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfFrontBlockName, "Length", ShelfLength + Stellar.ShelfFrontLengthIncrement,
                        InsertionPoint.X + Stellar.ShelfFrontShifX, InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Item, InsertionPoint.Z);
                }

            }

            // Рисуем опору
            BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.BaseFrontBlockName, "Length", ShelfLength + Stellar.BaseFrontIcrement,
                InsertionPoint.X + Stellar.BaseFrontShiftX, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);

            // --------------
            // ставим размеры на виде спереди 
            // --------------

            // длина полки сверху

            Point3d pt1 = new Point3d(InsertionPoint.X, InsertionPoint.Y + Stellar.ShelfPosArray[Stellar.ShelfPosArray.Count - 1], InsertionPoint.Z);
            Point3d pt2 = new Point3d(InsertionPoint.X + ShelfLength, InsertionPoint.Y + Stellar.ShelfPosArray[Stellar.ShelfPosArray.Count - 1], InsertionPoint.Z);
            DrawHorizontalDimension(CurrentDocument, pt1, pt2, pt1.Y + 200);

            // размеры расстояния между полками


            // рисуем размеры между полками
            pt1 = new Point3d(InsertionPoint.X + ShelfLength / 2 - 200,
                InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Stellar.LowerShelf + Stellar.ShelfHeight, InsertionPoint.Z);


            for (int i = 0; i < Stellar.ShelfPosArray.Count; i++)
            {
                double Item = Stellar.ShelfPosArray[i];
                if (i != Stellar.ShelfPosArray.Count - 1)
                {
                    pt2 = new Point3d(InsertionPoint.X + ShelfLength / 2 - 200,
                    InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Item, InsertionPoint.Z);
                    DrawVerticalDimension(CurrentDocument, pt1, pt2, pt1.X + 200);
                    pt1 = new Point3d(pt2.X, pt2.Y + Stellar.ShelfHeight, pt2.Z);
                }
                else
                {
                    pt2 = new Point3d(InsertionPoint.X + ShelfLength / 2 - 200,
                    InsertionPoint.Y + Stellar.ShelfTopShiftDistanceY + Item, InsertionPoint.Z);
                    DrawVerticalDimension(CurrentDocument, pt1, pt2, pt1.X + 200);
                }
            }

            /*
            foreach (double Item in Stellar.ShelfPosArray)
            {
                pt2 = new Point3d(InsertionPoint.X + ShelfLength / 2 - 200,
                    InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Item, InsertionPoint.Z);
                DrawVerticalDimension(CurrentDocument, pt1, pt2, pt1.X + 200);
                pt1 = new Point3d(pt2.X, pt2.Y + Stellar.ShelfHeight, pt2.Z);
            }
            */

        }

        /// <summary>
        /// Рисует вид стеллажа сбоку
        /// </summary>
        /// <param name="Stellar">экземпляр класса стеллажа</param>
        /// <param name="ShelfWidthClear">глубина полки</param>
        /// <param name="LowerShelfDraw">Рисовать нижнюю полку или нет</param>
        /// <param name="DoubleSide">Двухсторонний или односторонний</param>
        /// <param name="PathToSourceFile">Путь к файлу с блоками</param>
        /// <param name="CurrentDocument">Документ Автокада в котором надо рисовать</param>
        /// <param name="InsertionPoint">Точка вставки</param>
        private static void DrawSection(StellCalc Stellar, double ShelfWidthClear, bool LowerShelfDraw, bool DoubleSide, string PathToSourceFile, Document CurrentDocument, Point3d InsertionPoint)
        {
            // ------------------------
            // Копируем описание блоков
            // ------------------------

            // База
            BlockOperation.CloneBlockToDocument(CurrentDocument, PathToSourceFile, Stellar.BaseBlockName);

            // Полка
            BlockOperation.CloneBlockToDocument(CurrentDocument, PathToSourceFile, Stellar.ShelfBlockName);

            // Верхняя пролка вид в разрезе
            BlockOperation.CloneBlockToDocument(CurrentDocument, PathToSourceFile, Stellar.ShelfTopBlockName);

            // Стойка
            BlockOperation.CloneBlockToDocument(CurrentDocument, PathToSourceFile, Stellar.StandBlockName);


            // Создаем коллекцию с параметрами блока
            Dictionary<string, double> StandParametrs = new Dictionary<string, double>();
            StandParametrs.Add("Width", ShelfWidthClear + Stellar.StandWidthIncrement);
            StandParametrs.Add("Height", Stellar.StandHeight);

            // ---------------
            // Стойка
            // ---------------

            // Вставляем стойку с параметрами
            BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.StandBlockName, StandParametrs,
                InsertionPoint.X + Stellar.StandShiftDistanceX, InsertionPoint.Y + Stellar.StandShiftDistanceY, InsertionPoint.Z);

            if (DoubleSide)
            {
                // Вставляем стойку с параметрами
                BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.StandBlockName, StandParametrs,
                    InsertionPoint.X + Stellar.StandShiftDistanceX2 - ShelfWidthClear, InsertionPoint.Y + Stellar.StandShiftDistanceY, InsertionPoint.Z);
            }

            // --------------
            // Опора
            // --------------

            if (DoubleSide)
            {
                // Вставляем опору с параметрами
                BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.BaseBlockName, "Width", ShelfWidthClear * 2 + Stellar.BaseWidthIncrement2,
                    InsertionPoint.X + Stellar.BaseShiftDistanceX2 - ShelfWidthClear, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);

            }
            else
            {
                // Вставляем опору с параметрами
                BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.BaseBlockName, "Width", ShelfWidthClear + Stellar.BaseWidthIncrement,
                    InsertionPoint.X + Stellar.BaseShiftDistanceX, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);

            }

            // ------------
            // Полки
            // ------------

            // Если надо вставляем нижнюю полку 
            if (LowerShelfDraw)
            {
                BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfBlockName, "Width", ShelfWidthClear + Stellar.ShelfWidthIncrement,
                   InsertionPoint.X + Stellar.ShelfShiftDistanceX, InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Stellar.LowerShelf, InsertionPoint.Z);
                if (DoubleSide)
                {
                    BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfBlockName, "Width", ShelfWidthClear + Stellar.ShelfWidthIncrement,
                   InsertionPoint.X + Stellar.ShelfShiftDistanceX2 - ShelfWidthClear, InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Stellar.LowerShelf, InsertionPoint.Z);
                }
            }

            // Вставляем полки
            for (int i = 0; i < Stellar.ShelfPosArray.Count; i++)
            {
                double Item = Stellar.ShelfPosArray[i];
                if (i == Stellar.ShelfPosArray.Count - 1)
                {
                    BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfTopBlockName, "Width", ShelfWidthClear + Stellar.ShelfWidthIncrement,
                        InsertionPoint.X + Stellar.ShelfShiftDistanceX, InsertionPoint.Y + Stellar.ShelfTopShiftDistanceY + Item, InsertionPoint.Z);

                }
                else
                {
                    BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfBlockName, "Width", ShelfWidthClear + Stellar.ShelfWidthIncrement,
                        InsertionPoint.X + Stellar.ShelfShiftDistanceX, InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Item, InsertionPoint.Z);
                }
            }

            // если двухсторонний
            if (DoubleSide)
            {
                // Вставляем полки для второй секции
                for (int i = 0; i < Stellar.ShelfPosArray.Count; i++)
                {
                    double Item = Stellar.ShelfPosArray[i];
                    if (i == Stellar.ShelfPosArray.Count - 1)
                    {
                        BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfTopBlockName, "Width", ShelfWidthClear + Stellar.ShelfWidthIncrement,
                            InsertionPoint.X + Stellar.ShelfShiftDistanceX2 - ShelfWidthClear, InsertionPoint.Y + Stellar.ShelfTopShiftDistanceY + Item, InsertionPoint.Z);
                    }
                    else
                    {
                        BlockOperation.bRefInsertDynamic(CurrentDocument, Stellar.ShelfBlockName, "Width", ShelfWidthClear + Stellar.ShelfWidthIncrement,
                            InsertionPoint.X + Stellar.ShelfShiftDistanceX2 - ShelfWidthClear, InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Item, InsertionPoint.Z);
                    }
                }
            }

            // -------------
            // Размеры
            // -------------

            // рисуем размеры между полками
            Point3d pt1 = new Point3d(InsertionPoint.X + Stellar.ShelfShiftDistanceX + ShelfWidthClear + Stellar.ShelfWidthIncrement,
                InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Stellar.LowerShelf + Stellar.ShelfHeight, InsertionPoint.Z);

            Point3d pt2;

            for (int i = 0; i < Stellar.ShelfPosArray.Count; i++)
            {
                double Item = Stellar.ShelfPosArray[i];

                if (i != Stellar.ShelfPosArray.Count - 1)
                {
                    pt2 = new Point3d(InsertionPoint.X + Stellar.ShelfShiftDistanceX + ShelfWidthClear + Stellar.ShelfWidthIncrement,
                    InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Item, InsertionPoint.Z);
                    DrawVerticalDimension(CurrentDocument, pt1, pt2, pt1.X + 200);
                    pt1 = new Point3d(pt2.X, pt2.Y + Stellar.ShelfHeight, pt2.Z);
                }
                else
                {
                    pt2 = new Point3d(InsertionPoint.X + Stellar.ShelfShiftDistanceX + ShelfWidthClear + Stellar.ShelfWidthIncrement,
                   InsertionPoint.Y + Stellar.ShelfTopShiftDistanceY + Item, InsertionPoint.Z);
                    DrawVerticalDimension(CurrentDocument, pt1, pt2, pt1.X + 200);
                }
            }
            /*
            foreach (double Item in Stellar.ShelfPosArray)
            {
                pt2 = new Point3d(InsertionPoint.X + Stellar.ShelfShiftDistanceX + ShelfWidthClear + Stellar.ShelfWidthIncrement,
                    InsertionPoint.Y + Stellar.ShelfShiftDistanceY + Item, InsertionPoint.Z);
                DrawVerticalDimension(CurrentDocument, pt1, pt2, pt1.X + 200);
                pt1 = new Point3d(pt2.X, pt2.Y + Stellar.ShelfHeight, pt2.Z);
            }
            */
            // рисуем размеры стойки 
            pt1 = new Point3d(InsertionPoint.X + Stellar.StandShiftDistanceX + ShelfWidthClear + Stellar.StandWidthIncrement,
                InsertionPoint.Y + Stellar.StandShiftDistanceY, InsertionPoint.Z);

            pt2 = new Point3d(InsertionPoint.X + Stellar.StandShiftDistanceX + ShelfWidthClear + Stellar.StandWidthIncrement,
                InsertionPoint.Y + Stellar.StandShiftDistanceY + Stellar.StandHeight, InsertionPoint.Z);
            DrawVerticalDimension(CurrentDocument, pt1, pt2, pt1.X + 400);

            // рисуем размер высоты стеллажа
            if (DoubleSide)
                pt1 = new Point3d(InsertionPoint.X + Stellar.BaseShiftDistanceX2 - ShelfWidthClear + ShelfWidthClear * 2 + Stellar.BaseWidthIncrement2, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);
            else
                pt1 = new Point3d(InsertionPoint.X + Stellar.BaseShiftDistanceX + ShelfWidthClear + Stellar.BaseWidthIncrement, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);

            pt2 = new Point3d(pt1.X, Stellar.ShelfPosArray[Stellar.ShelfPosArray.Count - 1] + InsertionPoint.Y, InsertionPoint.Z);
            DrawVerticalDimension(CurrentDocument, pt1, pt2, pt1.X + 600);

            // рисуем горизонтальные размеры по базе
            if (DoubleSide)
                pt1 = new Point3d(InsertionPoint.X + Stellar.BaseShiftDistanceX2 - ShelfWidthClear, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);
            else
                pt1 = new Point3d(InsertionPoint.X + Stellar.BaseShiftDistanceX, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);

            if (DoubleSide)
                pt2 = new Point3d(InsertionPoint.X + Stellar.BaseShiftDistanceX2 - ShelfWidthClear + ShelfWidthClear * 2 + Stellar.BaseWidthIncrement2, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);
            else
                pt2 = new Point3d(InsertionPoint.X + Stellar.BaseShiftDistanceX + ShelfWidthClear + Stellar.BaseWidthIncrement, InsertionPoint.Y + Stellar.BaseShiftDistanceY, InsertionPoint.Z);
            DrawHorizontalDimension(CurrentDocument, pt1, pt2, pt1.Y - 200);
        }

        /// <summary>
        /// Устанавливает размер по двум 2D точкам горизонтальный
        /// </summary>
        /// <param name="doc">Документ</param>
        /// <param name="First">Первая точка</param>
        /// <param name="Second">Вторая точка</param>
        /// <param name="Ypos">положение по Y размерной линии</param>
        public static void DrawHorizontalDimension(Document doc, Point3d First, Point3d Second, double Ypos)
        {
            Point3d FirstPoint = new Point3d(First.X, First.Y, 0);
            Point3d SecondPoint = new Point3d(Second.X, Second.Y, 0);
            Point3d ThirdPoint = new Point3d((First.X + Second.X) / 2, Ypos, 0);
            DrawDimension(doc, FirstPoint, SecondPoint, ThirdPoint);
        }


        /// <summary>
        /// Устанавливает размер по двум точкам вертикальный
        /// </summary>
        /// <param name="doc">Документ Автокада</param>
        /// <param name="First">Первая точка</param>
        /// <param name="Second">Втора точка</param>
        /// <param name="Xpos">пололжение по X размерной линии</param>
        public static void DrawVerticalDimension(Document doc, Point3d First, Point3d Second, double Xpos)
        {
            Point3d FirstPoint = new Point3d(First.X, First.Y, 0);
            Point3d SecondPoint = new Point3d(Second.X, Second.Y, 0);
            Point3d ThirdPoint = new Point3d(Xpos, (First.Y + Second.Y) / 2, 0);
            DrawDimension(doc, FirstPoint, SecondPoint, ThirdPoint);
        }


        /// <summary>
        /// Устанавливает размер по трем точкам
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="FirstPoint"></param>
        /// <param name="SecondPoint"></param>
        /// <param name="ThirdPoint"></param>
        /// <param name="Distance"></param>
        public static void DrawDimension(Document doc, Point3d FirstPoint, Point3d SecondPoint, Point3d ThirdPoint, string Distance = "")
        {
            Database db = doc.Database;
            using (DocumentLock acLckDoc = doc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    // создаем размер
                    AlignedDimension odim = new AlignedDimension(FirstPoint, SecondPoint, ThirdPoint, Distance, db.Dimstyle);
                    odim.Dimscale = 20;
                    //BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                    // открываем запись в таблице блоков
                    BlockTableRecord btr = SymbolUtilityServices.GetBlockModelSpaceId(db).GetObject(OpenMode.ForWrite) as BlockTableRecord;
                    btr.AppendEntity(odim);
                    tr.AddNewlyCreatedDBObject(odim, true);
                    // фиксируем транзакцию
                    tr.Commit();
                } // using
            } // LockDocument
        }
    }
}
