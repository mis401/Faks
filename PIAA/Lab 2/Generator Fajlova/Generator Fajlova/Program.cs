
using System.Reflection.Metadata.Ecma335;

public class Program
{
    public static void Main()
    {
        int x;
        x = Convert.ToInt32(Console.ReadLine(), 10);
        char mux = (char)Console.Read();
        if (mux == 'a')
            generisiASCII(x);
        else if(mux == 'h')
            generisiHex(x);
    }

    public static void generisiHex(int x)
    {
        Random r = new Random();
        using (StreamWriter sw = new StreamWriter($"C:\\Users\\MihajloBencun\\Desktop\\Faks\\PIAA\\Lab 2\\Main\\RabinKarp 18081\\RabinKarp 18081\\{x}h.txt"))
        {
            char a;
            for (int i = 0; i < x; i++)
            {
                a = r.Next(16).ToString("X").First();
                sw.Write(a);
            }
        }
    }

    public static void generisiASCII(int x)
    {
        Random rword = new Random();
        Random randlenght = new Random();
        using (StreamWriter sw = new StreamWriter($"C:\\Users\\MihajloBencun\\Desktop\\Faks\\PIAA\\Lab 2\\Main\\RabinKarp 18081\\RabinKarp 18081\\{x}a.txt"))
        {
            for (int j = 0; j < x; j++)
            {
                char a;
                int lenght = randlenght.Next(1, 30);
                for (int i = 0; i < lenght; i++)
                {
                    a = (char)rword.Next(0, 255);
                    sw.Write(a);
                }
                sw.Write(' ');
            }
        }
    }
}