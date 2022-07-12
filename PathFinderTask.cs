using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		static double res = double.MaxValue;//переменная для сравнения текущего пути
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints) //инициализация массивов и вызов рекурсивного метода
		{
			int position = 1;
			var resultAll = new int[checkpoints.Length];
			var bestOrder = new int[checkpoints.Length];
			for (int i = 0; i < bestOrder.Length; i++)
            { 
				bestOrder[i] = i;
				resultAll[i] = i;
			}
			var massRes = new double[checkpoints.Length];
			MakeTrivialPermutation(checkpoints, position, bestOrder, massRes, resultAll);
			res = double.MaxValue;
			return resultAll;
		}

		private static void MakeTrivialPermutation(Point[] checkpoints, int position,
 int[] bestOrder, double[] massRess, int[] resultAll) //метод построения маршрута
		{
			if (position == checkpoints.Length)
			{
				res = massRess[massRess.Length - 1];
				bestOrder.CopyTo(resultAll, 0); //запись оптимального маршрута 
				return;
			}
			for (int i = 1; i < checkpoints.Length; i++)
			{
				var index = Array.IndexOf(bestOrder, i, 1, position-1);
				if (index == -1)//если в массиве отсутствует элемент i
				{
					bestOrder[position] = i;
					massRess[position] = massRess[position-1] + 
Make(checkpoints[bestOrder[position]], checkpoints[bestOrder[position-1]]); //вычисление пути от точки к точки
					if (res > massRess[position])
					{
						MakeTrivialPermutation(checkpoints, position + 1, bestOrder, massRess, resultAll);
					}
				}
			}
		}

		public static double Make(Point a, Point b)// подсчёт расстояния
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
	}
}
