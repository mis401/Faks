using System.Diagnostics;

public class LevensteinDistance
{
    public static void Main()
    {
        string text, path;
        Console.WriteLine("Unesite putanju do fajla");
        path = Console.ReadLine();
        using (StreamReader sr = new StreamReader(path))
        {
            text = sr.ReadToEnd();
        }
        string word;
        Console.WriteLine("Unesite rec za koju zelite da pokrenete algoritam");
        word = Console.ReadLine();
        Console.WriteLine("Za koju Levenstajn distancu zelite rezultate (ukljucjuci i nju)?");
        int d = int.Parse(Console.ReadLine());
        Console.WriteLine("Da li zelite da radi rekurzivno? y/n");
        char type = (Console.ReadLine())[0];
        bool recursive = (type == 'y' ? true : false);
        char[] tokens = { ' ', '\n', '\r', '.', ',' };

        string[] words = text.Split(tokens);
        List<string> hits = new List<string>();

        Stopwatch sw = Stopwatch.StartNew();
        foreach(string token in words)
        {
            if (token == "")
                continue;

            if (Levenstein(token, word, recursive) <= d)
            {
                hits.Add(token);
            }

        }
        sw.Stop();

        if (hits.Count == 0)
            Console.WriteLine("No hits");
        else
        {
            using (StreamWriter writer = new StreamWriter("hits.txt", false))
            {
                foreach (string s in hits)
                {
                    writer.WriteLine(s);
                }
            }
        }
        Console.WriteLine(sw.Elapsed);
    }

    public static int Levenstein(string w1, string w2, bool type)
    {
        int result = 0;
        if (w1.Length == 0)
            return w2.Length;
        else if (w2.Length == 0)
            return w1.Length;

        int[,] matrix = new int[w2.Length +1, w1.Length+1];
        
        for (int i = 0; i < w2.Length; i++) //ciscenje matrice
        {
            for (int j = 0; j < w1.Length; j++)
            {
                matrix[i, j] = 0;
            }
        }

        for (int i = 0; i <= w1.Length; i++) //inicijalizacija prve vrste i kolone 
        {
            matrix[0, i] = i;
        }
        for (int i = 0; i <= w2.Length; i++)
        {
            matrix[i, 0] = i;
        }


        if (type) 
        return result = Matrix( matrix, w2.Length, w1.Length, w1,w2);
        else
        {
            for (int i = 1; i <= w2.Length; i++)
            {
                for (int j = 1; j <= w1.Length; j++)
                {
                    matrix[i, j] = Minimum3(matrix[i, j - 1] + 1,                                           //levo
                                            matrix[i - 1, j] + 1,                                           //gore 
                                            matrix[i - 1, j - 1] + 1 * ((w2[i-1] == w1[j-1] ? 0 : 1)));     //dijagonalno
                }
            }
            return matrix[w2.Length, w1.Length];
        }
        
    }

    public static int Matrix( int[,] matrix, int i, int j, string w1, string w2)
    {
        if (i == 1 && j == 1)
        {
            return Minimum3(matrix[1, 0] + 1,                                       //levo
                            matrix[0, 1] + 1,                                       //gore
                            matrix[0, 0] + 1 * (w1[0] == w2[0] ? 0 : 1));           //dijagonalno
        }
        else if (i == 1)
        {
            return Minimum3(Matrix( matrix, i, j - 1, w1, w2) + 1, 
                                    matrix[0, j] + 1, 
                                    matrix[0, j - 1] + 1 * (w1[j - 1] == w2[i - 1] ? 0 : 1));
        }
        else if (j == 1)
        {
            return Minimum3(matrix[i, 0] + 1, 
                            Matrix( matrix, i - 1, j, w1, w2) + 1,
                            matrix[i - 1, 0] + 1 * (w1[j - 1] == w2[i - 1] ? 0 : 1));
        }
        else
        {
            return Minimum3(Matrix( matrix, i, j - 1, w1, w2) +1,
                            Matrix( matrix, i - 1, j, w1, w2)+1, 
                            Matrix( matrix, i - 1, j - 1, w1, w2) + 1 * (w1[j - 1] == w2[i - 1] ? 0 : 1));
        }
    }
    public static int Minimum3(int a, int b, int c)
    {
        return (a<b?a:b) < c ? (a<b?a:b) : c;
    }
}