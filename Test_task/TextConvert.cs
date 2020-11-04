using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TestTask
{
    class TextConvert
    {
        public static void StrToState(string str, out int S, out int count) //Преобразовение состояния в виде строки в номер состояния и количество моментов времени этого состояния
        {
            Regex regex = new Regex(@"(\d+)"); 
            MatchCollection matches = regex.Matches(str);
            if(matches.Count == 3)
            {
                S = Convert.ToInt32(matches[0].Value);
                count = Convert.ToInt32(matches[2].Value) - Convert.ToInt32(matches[1].Value) + 1;
            }
            else
            {
                S = -1; count = -1;
            }
        }
        public static List<int> StrToListState(string str) //Получение числового вектора из набора состояний для ресурса из строки
        {
            Regex regex = new Regex(@"S\d*{\d*,\d*}"); //Регулярное выражение для состояний
            MatchCollection matches = regex.Matches(str);
            List<int> resource = new List<int>();
            foreach(Match m in matches)
            {
                int S, count;
                StrToState(m.Value, out S, out count); //Преобразование из записи вида Sn{a,b} в вектор состояний
                for(int i = 0; i < count; i++)
                {
                    resource.Add(S);
                }
            }
            return resource;
        }
        public static string ListStateToStr(List<int> resultStates) //Анализ выходного вектора для получения списка общих состояний в виде Sn(a,b)
        {
            string result = "", temp = "";
            int previosState = 0;
            int startTime = 1, finishTime = 1;
            for (int i = 0; i < resultStates.Count; i++) //Проход по вектору для сбора идущих подряд состояний для последовательных моментов времени в вид Sn(a,b)
            {
                if (resultStates[i] != 0 && previosState == resultStates[i] && i != resultStates.Count - 1)
                {
                    finishTime++;
                    previosState = resultStates[i];
                }
                if (resultStates[i] != 0 && previosState == resultStates[i] && i == resultStates.Count - 1)
                {
                    temp = $"S{previosState}{{{startTime},{(finishTime + 1).ToString()}}} ";
                    result += temp;
                }
                if (previosState != resultStates[i] && resultStates[i] != 0)
                {
                    temp = $"S{previosState}{{{startTime},{finishTime}}} ";
                    result += temp;
                    startTime = i + 1; finishTime = i + 1; previosState = resultStates[i];
                }
                if (previosState != resultStates[i] && resultStates[i] == 0)
                {
                    temp = $"S{previosState}{{{startTime},{finishTime}}} ";
                    result += temp;
                    previosState = 0;
                }
                if (resultStates[i] != 0 && previosState == 0)
                {
                    startTime = i + 1; finishTime = i + 1; previosState = resultStates[i];
                }
            }
            Regex regex = new Regex(@"S0{\d+,\d+} "); //Регулярные выражения для нулевых состояний, означающие отстутвие общего состояния на данный момент времени
            MatchCollection matches = regex.Matches(result);
            foreach(Match match in matches)
            {
                result = result.Replace(match.Value, ""); //Удаление нулевых состояний
            }
            return result;
        }
    }
}
