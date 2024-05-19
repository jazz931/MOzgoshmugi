//helped at the measure of my possibilities - Mazniker Anton (copyright MOzgoshmugi 2024)

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Mathos.Parser;

namespace Method_Nelder_Mid_MOzgoshmugi
{
    public class Metod_Nelder_Mead
    {
        public int iter = 0;
        private double betta = 0.5;//коэфициент сжатия
        private double alpha = 1.0;//коэфициент отражения
        private double gamma = 2.0;//коэфициент растяжения
        public List<Point> points = new List<Point>();//список точек
        public List<double> F_point_X_i = new List<double>();//спсиок значений фнкции в точке
        public double f_h, f_g, f_l, f_r;
        public Point X_R, X_h, X_g, X_l;
        int choice_task = 0;

        public void Generate_points(int count_points)
        {

            for (int i = 0; i < count_points; i++)
            {
                Point p = new Point(count_points - 1);
                for (int c = 0; c < p.count_mern; c++)
                {
                    if (c != i)
                    {
                        p.point[c] = 0;
                    }
                    else
                    {
                        p.point[c] = 1;
                    }

                }
                points.Add(p);
            }
        }

        public string Select_a_task(int choice)
        {
            choice_task = choice;
            switch (choice)
            {
                case 0:
                    return "x1^2 + x1*x2 + x2^2 - 6*x1 - 9*x2";

                case 1:
                    return "cos(x1 - 2*pi) + sin(x2 + pi) + 3";

                case 2:
                    return "20 + (x1 - 5)^2 + (x2 - 5)^2 - 10*cos(2*pi*x1) - 10*cos(2*pi*x2)";

                case 3:
                    return "0.5 + ((sin(x1^2 - x2^2)^2 - 0.5))/(1 + 0.001*(x1^2 + x2^2))^2";

                default:
                    return "x1^2 + x2^2 - 4*x1 + 100 - 8*x2";
            }            
        }

        public double Rashet_func(Point X_i)
        {
            Mathos.Parser.MathParser parser = new Mathos.Parser.MathParser();
            string expr = Select_a_task(choice_task);
            parser.LocalVariables.Add("x1", X_i.point[0]); // Значение x1
            parser.LocalVariables.Add("x2", X_i.point[1]); // Значение x2
            double result = parser.Parse(expr);
            return result;
        }

        public void Rashet_func()
        {
            F_point_X_i.Clear();
            for (int i = 0; i < points.Count; i++)
            {
                Mathos.Parser.MathParser parser = new Mathos.Parser.MathParser();
                string expr = Select_a_task(choice_task);
                parser.LocalVariables.Add("x1", points[i].point[0]); // Значение x1
                parser.LocalVariables.Add("x2", points[i].point[1]); // Значение x2
                double result = parser.Parse(expr);
                F_point_X_i.Add(result);
            }
        }

        public int Find_index_max_value_Func()//поиск с максимальным значением функции в точке
        {
            int max_index = 0;
            double max_value = -1000000;
            int i = 0;
            foreach (var Func_value in F_point_X_i)
            {
                if (Func_value > max_value)
                {
                    max_index = i;
                    max_value = Func_value;
                }
                i++;
            }
            return max_index;
        }

        public Point Find_max_value_Func()//поиск с максимальным значением функции в точке
        {
            int max_index = 0;
            double max_value = -1000000;
            int i = 0;
            foreach (var Func_value in F_point_X_i)
            {
                if (Func_value > max_value)
                {
                    max_index = i;
                    max_value = Func_value;
                }
                i++;
            }
            return points[max_index];
        }

        public Point Find_min_value_Func()//поиск инекса с минимальным значенем в точке
        {
            int min_index = 0;
            double min_value = 1000000;
            int i = 0;
            foreach (var Func_value in F_point_X_i)
            {
                if (Func_value < min_value)
                {
                    min_index = i;
                    min_value = Func_value;
                }
                i++;
            }
            return points[min_index];

        }

        public Point Find_centr_gravity()//поиск центра тяжести всех точек
        {
            Point Sum;
            Sum = new Point(points[0].count_mern);
            foreach (var point in points)
            {
                if (point != Find_max_value_Func())
                {
                    for (int c = 0; c < points.Count - 1; c++)
                    {
                        Sum.point[c] += point.point[c] / (points.Count - 1);
                    }
                }
            }

            return Sum;
        }

        public Point Find_Next_Max_value_Func()
        {
            int max_index = 0;
            double max_value = -1000000;
            int i = 0;
            foreach (var Func_value in F_point_X_i)
            {
                if (Func_value > max_value && Func_value < f_h)
                {
                    max_index = i;
                    max_value = Func_value;
                }
                i++;
            }
            return points[max_index];
        }

        public Point Mirror()//"отражение"
        {
            Point X_c = Find_centr_gravity();
            Point X_r = new Point(X_c.count_mern);
            for (int i = 0; i < X_c.count_mern; i++)
            {
                X_r.point[i] = (1 + alpha) * X_c.point[i] - alpha * X_h.point[i];
            }

            return X_r;
        }

        public Point Sokrachenie() //"сжатие"
        {
            Point X_s = new Point(points[0].count_mern);
            for (int i = 0; i < X_s.count_mern; i++)
            {
                X_s.point[i] = betta * Find_max_value_Func().point[i] + (1 - betta) * Find_centr_gravity().point[i];
            }

            return X_s;
        }

        public Point Rastyazhenie(Point X_r)
        {
            Point X_e = X_r;
            Point X_c = Find_centr_gravity();
            for (int i = 0; i < X_e.count_mern; i++)
            {
                X_e.point[i] = (1 - gamma) * X_c.point[i] + gamma * X_r.point[i];
            }
            return X_e;
        }

        //Для сценарныя
        public void Method()
        {
            do
            {
                Rashet_func();  //1

                X_h = Find_max_value_Func();    //2
                X_g = Find_Next_Max_value_Func();
                X_l = Find_min_value_Func();
                f_h = Rashet_func(X_h);
                f_g = Rashet_func(X_g);
                f_l = Rashet_func(X_l);

                X_R = Mirror();    //3
                f_r = Rashet_func(X_R);

                Compare();  //4

            } while (!Proverka_shodimosti());

        }

        //Для сценария
        void Compare()
        {
            if (f_r < f_l)
            {
                Point X_E = Rastyazhenie(X_R);
                double f_e = Rashet_func(X_E);

                if (f_e < f_r) { points[Find_index_max_value_Func()] = new Point(X_E); }
                else if (f_e >= f_r) { points[Find_index_max_value_Func()] = new Point(X_R); }
            }

            else if (f_l <= f_r && f_r < f_g)
            {
                points[Find_index_max_value_Func()] = new Point(X_R);
            }

            else
            {
                if (f_g <= f_r && f_r < f_h)
                {
                    Point tmp = new Point(X_h);
                    points[Find_index_max_value_Func()] = new Point(X_R);
                    X_h = new Point(X_R);
                    X_R = new Point(tmp);

                    double tmp2 = f_h;
                    f_h = f_r;
                    f_r = tmp2;
                }

                Point X_S = Sokrachenie();
                double f_s = Rashet_func(X_S);

                if (f_s < f_h)
                {
                    points[ind_search(X_h)] = new Point(X_S);
                }

                else if (f_s >= f_h)
                {
                    Point X_0 = Find_min_value_Func();
                    for (int i = 0; i < points.Count; i++)
                    {
                        for (int j = 0; j < points[i].count_mern; j++)
                        {
                            points[i].point[j] = X_0.point[j] + (points[i].point[j] - X_0.point[j]) / 2;
                        }
                    }
                }
            }
        }

        public int ind_search(Point X_H)
        {
            int index = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (X_H.equals(points[i])) index = i;
            }
            return index;
        }

        public bool Proverka_shodimosti(double eps = 0.0000001)
        {
            int Count_Points_eps = 0;
            int Count_to_compare = points.Count - 1; //to compare n other points with 1 chosen(min func value point)
            Point razn = new Point(X_l.count_mern);
            for (int i = 0; i < points.Count; i++)
            {
                int pass_count = 0;
                if (points[i] != X_l)
                {
                    for (int j = 0; j < points[i].count_mern; j++)
                    {
                        razn.point[j] = X_l.point[j] - points[i].point[j];
                        if (Math.Abs(razn.point[j]) <= eps && Math.Abs((f_l - Rashet_func(points[i]))) <= eps)
                        {
                            pass_count++;
                        }
                    }
                    if (pass_count == points[i].count_mern)
                    {
                        Count_Points_eps++;
                    }
                }
            }
            if (Count_Points_eps != Count_to_compare)
            {
                iter++;
                return false;
            }
            return true;
        }
    }
}
