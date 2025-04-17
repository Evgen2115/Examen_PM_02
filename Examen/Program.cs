using System;
using System.Globalization;
using System.IO;

class Program
{

    private static int n = 9; // количество точек маршрута
    private static double[,] distances = new double[n, n];
    private static double[,] speeds = new double[n, n];

    static void Main(string[] args)
    {
        ReadData("distances.csv");
        GenerateSpeeds();

        Console.WriteLine("Средние скорости на участках:");
        PrintSpeeds();

        Console.WriteLine("Введите номер стартовой точки (1-9):");
        int start = int.Parse(Console.ReadLine()) - 1; 

        Console.WriteLine("Введите номер конечной точки (1-9):");
        int end = int.Parse(Console.ReadLine()) - 1; 

        double[] result = Dijkstra(distances, start);
        double travelTimeInMinutes = (distances[start, end] / speeds[start, end]) * 60; // Получаем время в минутах
        int hours = (int)(travelTimeInMinutes / 60); // Полные часы
        int minutes = (int)(travelTimeInMinutes % 60); //  Минуты
        Console.WriteLine($"Время в пути от точки {start + 1} до точки {end + 1}: {hours} час(ов) {minutes} минут(ы)"); // Вывод в минутах и часах
    }

    public static void ReadData(string path)
    {
        var lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            int from = int.Parse(parts[0]) - 1; 
            int to = int.Parse(parts[1]) - 1; 
            double distance = double.Parse(parts[2], CultureInfo.InvariantCulture); 

            distances[from, to] = distance;
            distances[to, from] = distance;
        }
    }

    public static void GenerateSpeeds()
    {
        Random random = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (distances[i, j] > 0)
                {
                    speeds[i, j] = random.Next(30, 81); // скорость от 30 до 80 км/ч
                }
            }
        }
    }

    public static void PrintSpeeds()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (speeds[i, j] > 0)
                {
                    Console.WriteLine($"Скорость от {i + 1} до {j + 1}: {speeds[i, j]} км/ч"); 
                }
            }
        }
    }

    private static double[] Dijkstra(double[,] a, int v0)
    {
        double[] dist = new double[n];
        bool[] vis = new bool[n];
        int unvis = n;
        int v;

        for (int i = 0; i < n; i++)
            dist[i] = Double.MaxValue;
        dist[v0] = 0.0;

        while (unvis > 0)
        {
            v = -1;
            for (int i = 0; i < n; i++)
            {
                if (vis[i])
                    continue;
                if ((v == -1) || (dist[v] > dist[i]))
                    v = i;
            }
            vis[v] = true;
            unvis--;
            for (int i = 0; i < n; i++)
            {
                if (dist[i] > dist[v] + a[v, i])
                    dist[i] = dist[v] + a[v, i];
            }
        }
        return dist;
    }
}