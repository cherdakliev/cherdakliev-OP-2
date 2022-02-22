using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Prac_class
{
    public static class global
    {
        public static List<double> y = new List<double>();
        public static List<double> temp = new List<double>();
        public static List<List<double>> intervals = new List<List<double>>(); // для розрахунку мат сподівання під час навчання(з вилученям i-го елемента)
        public static List<double> List_of_M = new List<double>();
        public static List<List<double>> global_intervals = new List<List<double>>();
        public static List<double> List_of_Avar_M_e = new List<double>(); // еталонні мат сподівання
        public static List<double> List_of_Avar_M_i = new List<double>(); // мат сподівання режиму автентифікації
        public static List<double> List_of_Avar_D_e = new List<double>(); // еталонні дисперсіїї
        public static List<double> List_of_Avar_D_i = new List<double>(); // дисперсії режиму автентифікації
        public static List<double> List_of_D = new List<double>();
        public static double Avarage_M = 0; // середнє мат спрдівання
        public static double Avarage_D = 0; // середня диспрерсія
        public static List<double> tmplist;
        public static double temp_stu; // критерій стьюдента
        public static double tT = 2.31; // критерій стьюдента табличне значення
        public static double Ft = 3.18; // критерій фішера табличне значення
        public static double M = 0;
        public static double Dx = 0; // дисперсія
        public static string CodeWord = "длагнитор";
        public static int n = CodeWord.Length;
        public static StreamWriter sw;
    }

}
