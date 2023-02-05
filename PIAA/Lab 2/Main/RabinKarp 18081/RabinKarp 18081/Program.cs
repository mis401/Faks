

using System.Diagnostics;

public class Program
{

    public static void Main()
    {
        string T, path, pattern;
        path = Console.ReadLine();
        pattern = Console.ReadLine();
        Console.WriteLine();
        using (StreamReader streamIn = new StreamReader($"C:\\Users\\MihajloBencun\\Desktop\\Faks\\PIAA\\Lab 2\\Main\\RabinKarp 18081\\RabinKarp 18081\\{path}.txt"))
        {
            T = streamIn.ReadToEnd();
        }
        List<int> list;
        Stopwatch stopwatch = new Stopwatch();
        
        if (path.Last() == 'h')
        {
            stopwatch.Restart();
            list = RabinKarpHex(T, pattern);
            stopwatch.Stop();
        }
        else if (path.Last() == 'a')
        {
            stopwatch.Restart();
            list = RabinKarpASCII(T, pattern);
            stopwatch.Stop();
        }
            
        else
        {
            Console.WriteLine("Pogresan format fajla");
            return;
        }
        
        Console.WriteLine("Proteklo je: " + stopwatch.Elapsed);

        if (list != null)
        {
            if (list.Count == 0)
                Console.WriteLine("no match");
            else
            {
                using (StreamWriter sw = new StreamWriter($"C:\\Users\\MihajloBencun\\Desktop\\Faks\\PIAA\\Lab 2\\Main\\RabinKarp 18081\\RabinKarp 18081\\hits.txt", false))
                {
                    foreach (int i in list)
                    {
                        sw.WriteLine("Pogodak na pomeraju: "+i);
                    }
                }

            }
        }

    }

    public static List<int> RabinKarpASCII(string T, string pattern, double q = 13)
    {
        List<int> list = new List<int>();
        int d = 256;
        Dictionary<char, int> alphabet = new Dictionary<char, int>();
        for (int i = 0; i < 256; i++)
        {
            alphabet.Add((char)i, i);
        }
        double h = Math.Pow(d, (pattern.Length - 1)) % q;
        double p = 0;
        double t = 0;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (!alphabet.ContainsKey(pattern[i]))
            {
                Console.WriteLine("Pogresan alfabet");
                return null;
            }
            p = ((d * p) + (double)pattern[i]) % q;
            t = ((d * t) + (double)T[i]) % q;
        }
        
        for (int s = 0; s < T.Length - pattern.Length + 1; s++)
        {
            if (p == t)
            {
                string hit = T.Substring(s, pattern.Length);
                if (hit.Equals(pattern))
                    list.Add(s);
            }
            else if (s < T.Length - pattern.Length)
            {
                t =((t - T[s]*h ) * d + T[s + pattern.Length]) % q;
                t = (t + q) % q;
            }
        }
        
        return list;
    }

    public static List<int> RabinKarpHex(string T, string pattern, double q = 13)
    {
        List<int> list = new List<int>();
        int d = 16;

        Dictionary<char, int> alphabet = new Dictionary<char, int>();
        for (short i = 0; i < d; i++)
        {
            alphabet.Add(i.ToString("X").First(), i);
        }

        double h = Math.Pow(d, (pattern.Length - 1)) % q;
        
        double p = 0;
        double t = 0;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (!alphabet.ContainsKey(pattern[i]))
            {
                Console.WriteLine("Pogresan alfabet");
                return null;
            }
            p = ((d * p) + Convert.ToInt32(pattern[i].ToString(), 16)) % q;
            t = ((d * t) + Convert.ToInt32(T[i].ToString(), 16)) % q
                ;
        }

        for (int s = 0; s < T.Length - pattern.Length + 1; s++)
        {
            if (p == t)
            {
                string hit = T.Substring(s, pattern.Length);
                if (hit.Equals(pattern))
                    list.Add(s);
            }
            else if (s < T.Length - pattern.Length)
            {
                t = (((t - Convert.ToInt32(T[s].ToString(), 16) * h) * d + Convert.ToInt32(T[s + pattern.Length].ToString(), 16))) % q;
                t = (t + q) % q;
            }
        }

        return list;
    }


}