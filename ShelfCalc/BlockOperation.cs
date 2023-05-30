using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace ShelfCalc
{
    class BlockOperation
    {
        /// <summary>
        /// Возвращает указанную точку
        /// </summary>
        /// <param name="doc">Текущий документ Автокада</param>
        /// <returns>точка</returns>
        public static Point3d GetPoint(Document doc)
        {
            Editor ed = doc.Editor;
            PromptPointResult ptRes = ed.GetPoint("Укажите точку вставки");
            if (ptRes.Status == PromptStatus.OK)
            {
                return ptRes.Value;
            }
            return new Point3d(0, 0, 0);
        }


        /// <summary>
        /// Вставка обычного блока
        /// </summary>
        /// <param name="doc">Document ACADDocument</param>
        /// <param name="blockName">Имя блока</param>
        /// <param name="X">Координата X</param>
        /// <param name="Y">Координата Y</param>
        /// <param name="Z">Координата Z</param>
        public static void BRefInsert(Document doc, string blockName, int X, int Y, int Z = 0)
        {
            Database db = doc.Database;
            Point3d InsertionPoint = new Point3d(X, Y, Z);
            using (DocumentLock acLckDoc = doc.LockDocument())
            {
                // начинаем транзакцию
                Transaction tr = db.TransactionManager.StartTransaction();
                using (tr)
                {
                    // открываем таблицу блоков на чтение
                    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                    // проверяем, нет ли в таблице блока с таким именем
                    //ObjectId btrId;
                    if (!bt.Has(blockName))
                        return;
                    // получем ObjectID определения блока
                    ObjectId btrId = bt[blockName];

                    // открываем пространство модели на запись
                    BlockTableRecord ms = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                    BlockReference br = new BlockReference(InsertionPoint, btrId);

                    // добавляем вхождение блока на пространство модели и в транзакцию
                    ms.AppendEntity(br);
                    tr.AddNewlyCreatedDBObject(br, true);

                    // фиксируем транзакцию
                    tr.Commit();
                }
            }
        }

        /// <summary>
        /// Вставка динамического блока
        /// </summary>
        /// <param name="doc">Document AutoCAD</param>
        /// <param name="blockName">Имя блока</param>
        /// <param name="PropName">Имя параметра блока</param>
        /// <param name="PropValue">Значения параметра блока</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        public static void BRefInsertDynamic(Document doc, string blockName, string PropName, double PropValue, double X, double Y, double Z = 0)
        {
            Database db = doc.Database;
            Editor ed = doc.Editor;
            Point3d InsertionPoint = new Point3d(X, Y, Z);
            using (DocumentLock acLckDoc = doc.LockDocument())
            {
                using (var tr = db.TransactionManager.StartTransaction())
                {
                    var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                    // if the bloc table has the block definition 
                    if (bt.Has(blockName))
                    {
                        // create a new block reference 
                        var br = new BlockReference(InsertionPoint, bt[blockName]);

                        // add the block reference to the curentSpace and the transaction 
                        var curSpace = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                        curSpace.AppendEntity(br);
                        tr.AddNewlyCreatedDBObject(br, true);

                        // set the dynamic property value 
                        foreach (DynamicBlockReferenceProperty prop in br.DynamicBlockReferencePropertyCollection)
                        {
                            if (prop.PropertyName == PropName)
                            {
                                prop.Value = PropValue;
                            }
                        }
                    }
                    // save changes 
                    tr.Commit();
                } // <- end using: disposing the transaction and all objects opened with it (block table) or added to it (block reference) 
            }
        }

        /// <summary>
        /// Вставка динамического блока
        /// </summary>
        /// <param name="doc">Document AutoCAD</param>
        /// <param name="blockName">Имя блока</param>
        /// <param name="Parametrs">Коллекция с параметерами <string> - Имя параметра, <double> - Значение параметра</param>
        /// <param name="InsertionPoint">Точка вставки </param>
        public static void BRefInsertDynamic(Document doc, string blockName, Dictionary<string, double> Parametrs, Point3d InsertionPoint)
        {
            BRefInsertDynamic(doc, blockName, Parametrs, InsertionPoint.X, InsertionPoint.Y, InsertionPoint.Z);
        }


        /// <summary>
        /// Вставка динамического блока
        /// </summary>
        /// <param name="doc">Document AutoCAD</param>
        /// <param name="blockName">Имя блока</param>
        /// <param name="Parametrs">Коллекция с параметерами <string> - Имя параметра, <double> - Значение параметра</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        public static void BRefInsertDynamic(Document doc, string blockName, Dictionary<string, double> Parametrs, double X, double Y, double Z = 0)
        {
            Database db = doc.Database;
            Editor ed = doc.Editor;
            Point3d InsertionPoint = new Point3d(X, Y, Z);
            using (DocumentLock acLckDoc = doc.LockDocument())
            {
                using (var tr = db.TransactionManager.StartTransaction())
                {
                    var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                    // if the bloc table has the block definition 
                    if (bt.Has(blockName))
                    {
                        // create a new block reference 
                        var br = new BlockReference(InsertionPoint, bt[blockName]);

                        // add the block reference to the curentSpace and the transaction 
                        var curSpace = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                        curSpace.AppendEntity(br);
                        tr.AddNewlyCreatedDBObject(br, true);

                        // set the dynamic property value 
                        foreach (DynamicBlockReferenceProperty prop in br.DynamicBlockReferencePropertyCollection)
                        {
                            foreach (string Item in Parametrs.Keys)
                            {
                                if (prop.PropertyName == Item)
                                {
                                    prop.Value = Parametrs[Item];
                                }
                            }
                            /*
                            if (prop.PropertyName == PropName)
                            {
                                prop.Value = PropValue;
                            }
                            */
                        }
                    }
                    // save changes 
                    tr.Commit();
                } // <- end using: disposing the transaction and all objects opened with it (block table) or added to it (block reference) 
            }
        }



        /// <summary>
        /// Клонирует указанный блок из указанного файла
        /// </summary>
        /// <param name="CurrentDoc">текущий документ Application.DocumentManager.MdiActiveDocument</param>
        /// <param name="PathToSourceFile">Путь к файлу источнику блока</param>
        /// <param name="blockName">Имя блока</param>
        static public void CloneBlockToDocument(Document CurrentDoc, string PathToSourceFile, string blockName)
        {
            //Document doc = Application.DocumentManager.MdiActiveDocument;
            Document doc = CurrentDoc;
            using (DocumentLock acLckDoc = doc.LockDocument())
            {
                using (Database OpenDb = new Database(false, true))
                {
                    OpenDb.ReadDwgFile(PathToSourceFile, System.IO.FileShare.ReadWrite, true, "");
                    ObjectIdCollection ids = new ObjectIdCollection();
                    using (Transaction tr = OpenDb.TransactionManager.StartTransaction())
                    {

                        //For example, Get the block by name "TEST"
                        BlockTable bt;
                        bt = (BlockTable)tr.GetObject(OpenDb.BlockTableId, OpenMode.ForRead);
                        if (bt.Has(blockName))
                        {
                            ids.Add(bt[blockName]);
                        }
                        tr.Commit();
                    }
                    //if found, add the block
                    if (ids.Count != 0)
                    {
                        //get the current drawing database
                        Database destdb = doc.Database;
                        IdMapping iMap = new IdMapping();
                        destdb.WblockCloneObjects(ids, destdb.BlockTableId, iMap, DuplicateRecordCloning.Ignore, false);
                    }
                }
            }
            //doc.LockMode()
        }  //InsertBlock
    }
}
