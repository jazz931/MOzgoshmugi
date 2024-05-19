using System.Windows;
using System;


namespace Method_Nelder_Mid_MOzgoshmugi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ////string expr = "x1^2 + x1*x2 + x2^2 - 6*x1 - 9*x2";    //Решение (1,4)  значение функции : -21;

            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(8);

            metod_Nelder_Mead.Generate_points(2 + 1);

            metod_Nelder_Mead.Method();

            Console.WriteLine($"Рассматриваем данное выражение: {Task} -> min");
            Console.WriteLine($"Лучшая точка: x1 = {metod_Nelder_Mead.X_l.point[0]} , x2 = {metod_Nelder_Mead.X_l.point[1]}");
            Console.WriteLine($"Минимальное значение функции: {metod_Nelder_Mead.f_l}");
            Console.WriteLine($"Кол-во циклов поиска экстремума функции {metod_Nelder_Mead.iter + 1}");
        }
    }
}