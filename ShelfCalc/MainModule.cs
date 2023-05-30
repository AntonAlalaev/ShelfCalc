using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using System.Globalization;
using System.Linq;

namespace ShelfCalc
{
    public class MainModule
    {
        #region Глобальные переменные

        /// <summary>
        /// Текущий путь к сборке
        /// </summary>
        public static string Path
        {
            get; set;
        }

        public static Document CurrentDocument;
        #endregion

        [CommandMethod("ShelfStart")]
        public static void ShelfStart()
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
            Path = path1.Substring(0, Index) + "\\";
            string PathToSourceFile = Path + "ShelfBlocks.dwg";

            var oldCult = System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru");
            CurrentDocument = Application.DocumentManager.MdiActiveDocument;
            MainWindow wind = new MainWindow();
            //Application.ShowModalWindow(wind);
            Application.ShowModelessWindow(wind);

        }
    }
}
