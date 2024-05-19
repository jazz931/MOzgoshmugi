using Method_Nelder_Mid_MOzgoshmugi;
namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Правильность выборки выражения(задания)
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(7);
            string true_choice = "x1^2 + x2^2 - 4*x1 + 100 - 8*x2";

            Assert.AreEqual(true_choice, Task);
        }

        [TestMethod]
        public void TestMethod2()
        {
            //Проверка на успешность создания класса
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(1);
            metod_Nelder_Mead.Generate_points(3);

            Assert.IsNotNull(metod_Nelder_Mead);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //Проверка на правильный расчёт в формуле
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(2);

            Point X = new Point();
            X = new Point(2);
            X.point[0] = 0.0;
            X.point[1] = 0.0;

            double F = metod_Nelder_Mead.Rashet_func(X);

            Assert.AreEqual(50, F);
        }

        [TestMethod]
        public void TestMethod4()
        {
            //Проверка на совпадение решений функций Rashet_func
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();
            string Task = metod_Nelder_Mead.Select_a_task(1);

            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Rashet_func();

            Point X0, X1, X2 = new Point();

            X0 = new Point(2);
            X0.point[0] = 1.0;
            X0.point[1] = 0.0;

            X1 = new Point(2);
            X1.point[0] = 0.0;
            X1.point[1] = 1.0;

            X2 = new Point(2);
            X2.point[0] = 0.0;
            X2.point[1] = 0.0;

            Assert.AreEqual(metod_Nelder_Mead.F_point_X_i[0], metod_Nelder_Mead.Rashet_func(X0));
            Assert.AreEqual(metod_Nelder_Mead.F_point_X_i[1], metod_Nelder_Mead.Rashet_func(X1));
            Assert.AreEqual(metod_Nelder_Mead.F_point_X_i[2], metod_Nelder_Mead.Rashet_func(X2));
        }

        [TestMethod]
        public void TestMethod5()
        {
            //Проверка на нахождение максимального максимальным значением функции в точке,
            //с максимальным значением функции в точке,
            //поиск инжекса с минимальным значением в точке
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(1);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Select_a_task(1);
            metod_Nelder_Mead.Rashet_func();
            int index = metod_Nelder_Mead.Find_index_max_value_Func();

            Assert.AreEqual(index, metod_Nelder_Mead.F_point_X_i.Count - 1);
            Assert.AreEqual(metod_Nelder_Mead.points[index], metod_Nelder_Mead.Find_max_value_Func());
            Assert.AreEqual(metod_Nelder_Mead.points[1], metod_Nelder_Mead.Find_min_value_Func());
        }

        [TestMethod]
        public void TestMethod6()
        {
            //Проверка на то что Find_max_value_Func() не эквивалентен Find_Next_Max_value_Func()
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(1);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Select_a_task(1);
            metod_Nelder_Mead.Rashet_func();

            Assert.AreNotEqual(metod_Nelder_Mead.Find_max_value_Func(), metod_Nelder_Mead.Find_Next_Max_value_Func());
        }

        [TestMethod]
        public void TestMethod7()
        {
            //Проверка ind_search
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(1);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Select_a_task(0);
            metod_Nelder_Mead.Rashet_func();

            Point X0 = new Point();
            X0 = new Point(2);
            X0.point[0] = 0.0;
            X0.point[1] = 0.0;

            Assert.AreEqual(metod_Nelder_Mead.ind_search(X0), 2);
        }


        [TestMethod]
        public void TestMethod8()
        {
            //Проверка Sokrachenie
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(2);
            metod_Nelder_Mead.Generate_points(2 + 1);

            Point X = new Point();
            X = new Point(2);
            X.point[0] = 0.5;
            X.point[1] = 0.25;


            Point check = metod_Nelder_Mead.Sokrachenie();

            Assert.IsTrue(X.equals(check));
        }

        [TestMethod]
        public void TestMethod9()
        {
            //Проверка  Mirror, Find_centr_gravity, Rastyazhenie
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(2);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Method();

            Point X = new Point();
            X = new Point(2);
            X.point[0] = 5.0;
            X.point[1] = 5.0;

            //Для удобства округляем
            Point check = metod_Nelder_Mead.Mirror();
            check.point[0] = Math.Round(check.point[0]);
            check.point[1] = Math.Round(check.point[1]);
            Assert.IsTrue(X.equals(check));

            Point check1 = metod_Nelder_Mead.Find_centr_gravity();
            check1.point[0] = Math.Round(check1.point[0]);
            check1.point[1] = Math.Round(check1.point[1]);
            Assert.IsTrue(X.equals(check1));

            Point check2 = metod_Nelder_Mead.Rastyazhenie(metod_Nelder_Mead.X_R);
            check2.point[0] = Math.Round(check2.point[0]);
            check2.point[1] = Math.Round(check2.point[1]);
            Assert.IsTrue(X.equals(check2));
        }

        [TestMethod]
        public void TestMethod_1()
        {
            //    //Математический тест #1
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(1);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Method();

            Console.WriteLine($"Лучшая точка: x1 = {metod_Nelder_Mead.X_l.point[0]} , x2 = {metod_Nelder_Mead.X_l.point[1]}");
            Console.WriteLine($"Минимальное значение функции: {metod_Nelder_Mead.f_l}");

            string expr = "cos(x1 - 2*pi) + sin(x2 + pi) + 3";

            Point X = new Point();
            X = new Point(2);
            X.point[0] = 3.0;
            X.point[1] = 2.0;

            Point check = new Point();
            check = new Point(2);
            check.point[0] = Math.Round(metod_Nelder_Mead.X_l.point[0]);
            check.point[1] = Math.Round(metod_Nelder_Mead.X_l.point[1]);


            Assert.AreEqual(expr, Task);
            Assert.IsTrue(X.equals(check));
            Assert.AreEqual(1, Math.Round(metod_Nelder_Mead.f_l));
        }

        [TestMethod]
        public void TestMethod_2()
        {
            //    //Математический тест #2
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(2);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Method();

            Console.WriteLine($"Лучшая точка: x1 = {metod_Nelder_Mead.X_l.point[0]} , x2 = {metod_Nelder_Mead.X_l.point[1]}");
            Console.WriteLine($"Минимальное значение функции: {metod_Nelder_Mead.f_l}");

            string expr = "20 + (x1 - 5)^2 + (x2 - 5)^2 - 10*cos(2*pi*x1) - 10*cos(2*pi*x2)";

            Point X = new Point();
            X = new Point(2);
            X.point[0] = 5.0;
            X.point[1] = 5.0;

            Assert.AreEqual(expr, Task);
            Assert.IsTrue(X.equals(metod_Nelder_Mead.X_l));
            Assert.AreEqual(0, metod_Nelder_Mead.f_l);
        }

        [TestMethod]
        public void TestMethod_3()
        {
            //    //Математический тест #3
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(3);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Method();

            Console.WriteLine($"Лучшая точка: x1 = {metod_Nelder_Mead.X_l.point[0]} , x2 = {metod_Nelder_Mead.X_l.point[1]}");
            Console.WriteLine($"Минимальное значение функции: {metod_Nelder_Mead.f_l}");

            string expr = "0.5 + ((sin(x1^2 - x2^2)^2 - 0.5))/(1 + 0.001*(x1^2 + x2^2))^2";

            Point X = new Point();
            X = new Point(2);
            X.point[0] = 0.0;
            X.point[1] = 0.0;

            Point check = new Point();
            check = new Point(2);
            check.point[0] = Math.Round(metod_Nelder_Mead.X_l.point[0]);
            check.point[1] = Math.Round(metod_Nelder_Mead.X_l.point[1]);

            Assert.AreEqual(expr, Task);
            Assert.IsTrue(X.equals(check));
            Assert.AreEqual(0, metod_Nelder_Mead.f_l);
        }

        [TestMethod]
        public void TestMethod_4_1()
        {
            //Математический тест #4.2
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(4);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Method();

            Console.WriteLine($"Лучшая точка: x1 = {metod_Nelder_Mead.X_l.point[0]} , x2 = {metod_Nelder_Mead.X_l.point[1]}");
            Console.WriteLine($"Минимальное значение функции: {metod_Nelder_Mead.f_l}");

            string expr = "x1^2 + x2^2 - 4*x1 + 100 - 8*x2";

            Point X = new Point();
            X = new Point(2);
            X.point[0] = 2.0;
            X.point[1] = 4.0;

            Point check = new Point();
            check = new Point(2);
            check.point[0] = Math.Round(metod_Nelder_Mead.X_l.point[0]);
            check.point[1] = Math.Round(metod_Nelder_Mead.X_l.point[1]);

            Assert.AreEqual(expr, Task);
            Assert.IsTrue(X.equals(check));
            Assert.AreEqual(80, Math.Round(metod_Nelder_Mead.f_l));
        }

        [TestMethod]
        public void TestMethod_4_2()
        {
            //Математический тест #4.2
            Metod_Nelder_Mead metod_Nelder_Mead = new Metod_Nelder_Mead();

            string Task = metod_Nelder_Mead.Select_a_task(8);
            metod_Nelder_Mead.Generate_points(2 + 1);
            metod_Nelder_Mead.Method();

            Console.WriteLine($"Лучшая точка: x1 = {metod_Nelder_Mead.X_l.point[0]} , x2 = {metod_Nelder_Mead.X_l.point[1]}");
            Console.WriteLine($"Минимальное значение функции: {metod_Nelder_Mead.f_l}");

            string expr = "x1^2 + x2^2 - 4*x1 + 100 - 8*x2";

            Point X = new Point();
            X = new Point(2);
            X.point[0] = 2.0;
            X.point[1] = 4.0;

            Point check = new Point();
            check = new Point(2);
            check.point[0] = Math.Round(metod_Nelder_Mead.X_l.point[0]);
            check.point[1] = Math.Round(metod_Nelder_Mead.X_l.point[1]);

            Assert.AreEqual(expr, Task);
            Assert.IsTrue(X.equals(check));
            Assert.AreEqual(80, Math.Round(metod_Nelder_Mead.f_l));
        }
    }
}
