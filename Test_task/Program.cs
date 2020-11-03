using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Test_task
{
    class Program
    {
        static void Main(string[] args)
        {
            StateMatrix matrix = new StateMatrix(); //Создание объекта для матрицы состояний
            using (StreamReader sr = new StreamReader(@"input.txt", System.Text.Encoding.Default))
            {
                string str;
                while((str = sr.ReadLine()) != null)
                {
                    List<int> test = TextConvert.strToListState(str); //Конвертация из строки в вектор для матрицы в виде List<int>
                    matrix.addResource(test);
                }
            }
            Result result = new Result(matrix.lsize); //Создание объекта для выходного вектора с общими состояниями ресурсов
            List<Thread> threads = new List<Thread>(); //Создание списка объектов потоков для анализа столбцов матрицы
            for(int i = 0; i < matrix.lsize; i++) //Создание потока для каждого столбца матрицы
            {
                Thread temp = new Thread(new ParameterizedThreadStart(result.checkColumn)); //Используется делегат ParameterizedThreadStart для передачи столбца матрицы в поток
                Column column = matrix.getColumn(i); //Получение стоблца по индексу из матрцы
                temp.Start(column); //Старт потока
                temp.Join(); //Блокировка главного потока до завершения проверки столбцов
            }
            using (StreamWriter sw = new StreamWriter(@"output.txt", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(TextConvert.listStateToStr(result.results)); //Конвертация из List<int> в нужный формат на выходе в файл; запись в файт
            }
            //Console.WriteLine(TextConvert.listStateToStr(result.results));
        }
    }
}
