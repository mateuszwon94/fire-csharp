using System;
using System.Text;

public class Program {
    public Program() {
        _width = Console.WindowWidth;
        _height = Console.WindowHeight;

        for (int i = 0; i < _height; ++i) {
            _fire.Add(Enumerable.Repeat((byte)0,_width).ToList());
        }
    }

    public void Seed() {
        for (int i = 0; i < _width; ++i) {
            _fire.Last()[RandomIndex()] = RandomChar();
        }
        for (int i = 0; i < _width; ++i) {
            _fire.Last()[RandomIndex()] = 0;
        }
    }

    public void Calculate() {
        for (int i = 0; i < _height - 1; ++i) {
            for (int j = 0; j < _width - 1; ++j) {
                _fire[i][j] = Average(_fire[i][j], _fire[i+1][j], _fire[i][j+1], _fire[i+1][j+1]);
            }
        }

        for (int i = 0; i < _height - 1; ++i) {
            for (int j = _width - 1; j > 0; --j) {
                _fire[i][j] = Average(_fire[i][j], _fire[i+1][j], _fire[i][j-1], _fire[i+1][j-1]);
            }
        }
    }

    public void Resize() {
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;

        if (height > _height) {
            _fire.Insert(0, Enumerable.Repeat((byte)0,_width).ToList());
        } else if (height < _height) {
            _fire.RemoveAt(0);
        }

        if (width > _width) {
            foreach (var row in _fire) {
                row.InsertRange(0, Enumerable.Repeat((byte)0, width - _width));
            }
        } else if (width < _width) {
            foreach (var row in _fire) {
                row.RemoveRange(0, _width - width);
            }
        }

        _width = width;
        _height = height;
    }

    public void Print() {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _height - 1; ++i) {
            for (int j = 0; j < _width; ++j) {
                sb.Append(fireChars[_fire[i][j]]);
            }
            sb.AppendLine();
        }
        Console.Write(sb.ToString());
    }

    private byte Average(params int[] bytes) {
        return (byte)(bytes.Sum() / bytes.Length);
    }

    private byte RandomChar() {
        return (byte)random.Next(1, fireChars.Length);
    }

    private int RandomIndex() {
        return random.Next(_width);
    }

    private List<List<byte>> _fire = [];

    private int _width = 100;
    private int _height = 50;

    private Random random = new Random();

    public static String fireChars = " ,;+ltgti!lI?/\\|)(1}{][rcvzjftJUOQocxfXhqwWB8&%$#@";

    public static void Main(string[] args) {
        Program f = new Program();
        while (true) {
            Console.Clear();
            f.Resize();
            f.Seed();
            f.Calculate();
            f.Print();
            Thread.Sleep(30);
        }
    }
}