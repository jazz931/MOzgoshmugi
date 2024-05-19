using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Method_Nelder_Mid_MOzgoshmugi
{
    public class Point
    {
        public double[]? point;
        public int count_mern;

        public Point(int count_mern_prostranstva)
        {
            point = new double[count_mern_prostranstva];
            count_mern = count_mern_prostranstva;
        }

        public Point()
        {
            point = null;
        }

        public Point(Point copy)
        {
            point = new double[copy.count_mern];
            count_mern = copy.count_mern;
            for (int i = 0; i < copy.count_mern; i++)
            {
                point[i] = copy.point[i];
            }
        }

        public bool equals(Point tocompare)
        {   //somewhy just instead of rewriting an operator
            if (tocompare.count_mern != count_mern) return false;
            else
            {
                for (int i = 0; i < tocompare.count_mern; i++)
                {
                    if (point[i] != tocompare.point[i]) return false;
                }
                return true;
            }
        }

    }
}
