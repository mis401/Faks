




//na izlaz mozemo da posaljemo samo ono sto postoji u nasem recniku
//sve dok nam se sekvenca charova poklapa sa onim sto imamo vec u recniku budzimo je
//kad prestane preklapanje ono sto imamo prosledjujemo na izlaz i smestamo nov kod u recnik
//ustedeli smo na celom patternu ponovljenom do tada
//trim the fat, intovi ce biti nepotrebno veliki, short int za kodiranje? to je 2 bajta, 2^16 - 1 kombinacija za ushort posto svakako kodiram stvari
//bit za znak je nepotreban
//da li bi ovaj memorijski overhead mogao da se nadoknadi vektorskim operacijama po cenu performansa? da li bih mogao u c# da tretiram short kao array bitova i da koristim bitwise operacije? 
//nijedan broj se ne uklapa fino u 2^ jpk mora ushort
//c# string je immutable ne moze shift da bi se izbacilo slovo, mora da se pamti prethodni pattern


//BIT ARRAY IDE OD LSB DO MSB I SKRACENJE LENGHTA OSTAVLJA LSB
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Unesite izvorni fajl koji zelite da kompresujete:");

        string source = Console.ReadLine();
        Console.WriteLine("Unesite putanju gde zelite da bude kompresovani fajl:");
        string compressed = Console.ReadLine();
        Console.WriteLine("Unesite gde zelite da se fajl dekompresuje: ");
        string decompressed = Console.ReadLine();
        Stopwatch sw = Stopwatch.StartNew();
        CompressVar(source, compressed);
        sw.Stop();
        Console.WriteLine("Kompresija je trajala: " + sw.Elapsed);
        sw.Restart();
        DecompressVar(compressed, decompressed);
        sw.Stop();
        Console.WriteLine("Dekompresija je trajala: " + sw.Elapsed);

    }


    //funkcija koja ce da vrsi kompresiju
    public static void CompressShort(string path)//izmedju 60k i 70k loremipsum reci
    {
        Dictionary<string, ushort> map = new Dictionary<string, ushort>(); //nov recnik
        List<ushort> ret = new List<ushort>();
        using (StreamReader sr = new StreamReader(path))
        {
            string str = sr.ReadToEnd();
            //populacija recnika ASCII tabelom
            for (int i = 0; i < 256; i++)
            {
                map.Add(((char)i).ToString(), (ushort)i);
            }
            //incijalizujemo potragu za patternima
            string pattern = string.Empty;
            foreach (char c in str)
            {
                string newPattern = pattern + c;
                if (map.ContainsKey(newPattern))//da li imamo taj pattern vec?
                    pattern = newPattern;
                else
                {
                    map.Add(newPattern, (ushort)map.Count);//dodajemo pattern, samim tim ga kodiramo
                    ret.Add(map[pattern]);//na izlaz saljemo postojeci kod
                    pattern = c.ToString();//posto je pao pattern, moramo ispocetka
                }
                if (map.Count == ushort.MaxValue)
                {
                    throw new Exception();
                }
            }
            ret.Add(map[pattern]); //poslednji sto ostane nakon foreach
        }
        using (StreamWriter sw = new StreamWriter("compressed.txt"))
        {
            foreach (ushort code in ret)
            {
                sw.Write(code);
            }
        }
    }


    public static List<uint> CompressInt(string str)
    {
        Dictionary<string, uint> map = new Dictionary<string, uint>(); //nov recnik
        List<uint> ret = new List<uint>();
        //populacija recnika ASCII tabelom
        for (int i = 0; i < 256; i++)
        {
            map.Add(((char)i).ToString(), (uint)i);
        }
        //incijalizujemo potragu za patternima
        string pattern = string.Empty;
        foreach (char c in str)
        {
            string newPattern = pattern + c;
            if (map.ContainsKey(newPattern))//da li imamo taj pattern vec?
                pattern = newPattern;
            else
            {
                map.Add(newPattern, (uint)map.Count);//dodajemo pattern, samim tim ga kodiramo
                ret.Add(map[pattern]);//na izlaz saljemo postojeci kod
                pattern = c.ToString();//posto je pao pattern, moramo ispocetka
            }
            if (map.Count == int.MaxValue)
            {
                throw new Exception();
            }
        }
        ret.Add(map[pattern]); //poslednji sto ostane nakon foreach
        return ret;
    }
    
    public static bool CompressVar(string pathSrc, string pathDest)//PROMENLJIVA DUZINA KODA
    {

        int codeLength = 9;

        FileStream sourceFile = new FileStream(pathSrc, FileMode.Open, FileAccess.Read);
        using (StreamReader sr = new StreamReader(sourceFile))
        {
            FileStream destFile = new FileStream(pathDest, FileMode.OpenOrCreate, FileAccess.Write);
            using (BinaryWriter bw = new BinaryWriter(destFile))
            {


                //ako su sve 1 racunacemo kao kraj duzine i prelazimo na vecu duzinu
                string pattern = String.Empty;//string jeste immutable ali prezalicemo perfomance hit
                string newPattern;
                ushort buffer = 0;
                ushort queue = 0;
                Dictionary<string, ushort> map = new Dictionary<string, ushort>(); //nov recnik
                                                                                   //populacija recnika ASCII tabelom
                for (int i = 0; i < 256; i++)
                {
                    map.Add(((char)i).ToString(), (ushort)i);
                }
                int bufferSpace = 16; //glavni bafer za upis
                while (!sr.EndOfStream)
                {

                    if (map.Count == Math.Pow(2, 13) - 1) //da bismo ogranicili rast tablice do 12 bitova
                    {
                        codeLength = 9;
                        map.Clear();
                        for (int i = 0; i < 256; i++)
                        {
                            map.Add(((char)i).ToString(), (ushort)i);
                        }
                        pattern = "";
                        newPattern = "";
                    }

                    char readChar = (char)sr.Read();
                    newPattern = pattern + readChar;//proveravamo novi 
                    if (map.ContainsKey(newPattern.ToString()))
                    {
                        pattern = newPattern;
                    }
                    else
                    {

                        map.Add(newPattern.ToString(), (ushort)map.Count); //kodiramo novi pattern
                        ushort code = map[pattern.ToString()];

/*                        ushort log = (ushort)(Math.Log2(code) + 1);
                        codeLength = log < 9 ? 9 : log;*/

                        //int pending = 16 - (ushort)(codeLength); //koliko je dugacak trenutni kod sa kojim radimo? mozda i moze codeLenght
                        if (bufferSpace >= codeLength) //da li imamo prostora u baferu da smestimo taj kod
                        {
                            code = (ushort)(code << 16 - bufferSpace);
                            buffer = (ushort)(buffer | code); //smesti ga u donje bitove
                            bufferSpace -= codeLength; //smanji prostor dostupan u baferu
                            if (bufferSpace == 0) //msm da nece nikad da se desi ali neka ga
                            {
                                bw.Write(buffer);
                                //Console.Write(" "+buffer);
                                buffer = 0;
                                bufferSpace = 16;
                            }
                        }
                        else if (bufferSpace < codeLength && bufferSpace > 0) //moze da se smesti kod ali samo delimicno
                        {
                            int fragmentSpace = bufferSpace; //odredjujemo koliki deo koda moze
                            int queueSpace = codeLength - fragmentSpace; //koliki deo ne moze
                            ushort mask = (ushort)(Math.Pow(2, fragmentSpace) - 1);//pravimo masku za izvlacenje ova dva dela
                            ushort fragment = 0;
                            fragment = (ushort)(mask & code);//izvlacimo deo koji moze, nizi
                            mask = (ushort)(~mask);//sredjujemo masku za deo koji ne moze, visi
                            queue = (ushort)(mask & code);//izvlacimo deo koji ne 
                            fragment = (ushort)(fragment << 16 - bufferSpace);//pomeramo ga na vise bitove jer su oni dostupni u baferu
                            buffer = (ushort)(buffer | fragment);//dodajemo ih u bafer
                            bw.Write(buffer);//praznimo bafer u strim
                            //Console.Write(" "+buffer);
                            buffer = 0;
                            bufferSpace = 16;
                            queue = (ushort)(queue >> fragmentSpace);
                            buffer = (ushort)(buffer | queue);//dodajemo deo koji nije mogao da se smesti
                            bufferSpace -= queueSpace;//smanjujemo prostor u baferu
                        }
                        else//ne bi trebalo da se desi nikad
                        {
                            bw.Write(buffer);
                            //Console.Write(buffer);
                            buffer = 0;
                            bufferSpace = 16;
                        }

                        pattern = readChar.ToString(); //resetujemo pattern
                        if (map.Count == Math.Pow(2, codeLength) - 1)
                            codeLength++;//povecamo kod
                    }

                }
                bw.Write(buffer);//poslednji flush ako je nesto ostalo
            }
            destFile.Close();
        }
        sourceFile.Close();
        return true;
    }

    public static bool DecompressVar(string srcFile, string destFile)//PROMENLJIVA DUZINA KODA
    {
        //populisemo recnik asciiem
        Dictionary<ushort, string> map = new Dictionary<ushort, string>();

        for (int i = 0; i < 256; i++)
        {
            map.Add((ushort)i, ((char)i).ToString());
        }

        string str = string.Empty;
        string pattern = string.Empty;
        int codeLength = 9;
        ushort inputBuffer = 0;
        ushort mask=0;
        int bufferSpace = 16;
        char[] inBytes = new char[2];
        ushort tempBuffer = 0;
        ushort code=0;
       // ushort testChar2;

        using (FileStream src = new FileStream(srcFile, FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader br = new BinaryReader(src))
            {
                using (FileStream dest = new FileStream(destFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(dest))
                    {
                        int count = 0;
                        while (br.BaseStream.Position != br.BaseStream.Length)
                        {

                            ushort outputMask = (ushort)(Math.Pow(2, codeLength)-1);
                            if (bufferSpace <= 16 - codeLength)
                            {
                                code = (ushort)(inputBuffer & outputMask);//cita se kod
                                inputBuffer = (ushort)(inputBuffer >> codeLength);//izbacuje se kod iz bafera
                                bufferSpace += codeLength;
                            }
                            else
                            {
                                tempBuffer = br.ReadUInt16();
                                int set = 16 - bufferSpace;
                                int missing = codeLength - set;
                                ushort missingMask = (ushort)(tempBuffer & (ushort)(Math.Pow(2, missing) - 1));//bitovi koji nedostaju
                                missingMask = (ushort)(missingMask << set);//pomeraju se na mesto
                                inputBuffer = (ushort)(inputBuffer | missingMask);//ubacuju u bafer
                                tempBuffer = (ushort)(tempBuffer >> missing);//temp se pomera za toliko

                                bufferSpace -= missing;//smanjuje se prostor u baferu



                                code = (ushort)(inputBuffer & outputMask);//cita se kod
                                inputBuffer = (ushort)(inputBuffer >> codeLength);//izbacuje se kod iz bafera
                                bufferSpace += codeLength;

                                inputBuffer = (ushort)(inputBuffer | tempBuffer);
                                bufferSpace = 16 - (16 - missing);
                            }

                            if (!map.ContainsKey(code))
                            {
                                map.Add(code, pattern + pattern[0]);
                            }

                            if (map.ContainsKey(code))
                            {
                                sw.Write(map[code]);
                                //Console.Write(map[code]);
                                pattern += map[code].FirstOrDefault();
                            }
                            if (!map.ContainsValue(pattern))
                            {
                                map.Add((ushort)map.Count, pattern);
                                pattern = map[code];
                                if (map.Count == Math.Pow(2, codeLength)-2)
                                    codeLength++;
                                if (map.Count == Math.Pow(2, 13) - 1)
                                {
                                    codeLength = 9;
                                    map = new Dictionary<ushort, string>();
                                    for (int i = 0; i < 256; i++)
                                    {
                                        map.Add((ushort)i, ((char)i).ToString());
                                    }
                                }

                            }
                            count++;
                        }
                    }
                }
            }
        }
     return true;
    }


    public static void DecompressShort(List<ushort> compressed, string path)
    {
        //cita sve dok nije veci od 255 (ascii), i gradi nove kodove
        Dictionary<ushort, string> map = new Dictionary<ushort, string>();

        for (int i = 0; i < 256; i++)
        {
            map.Add((ushort)i, ((char)i).ToString());
        }

        string str = string.Empty;
        string pattern = string.Empty;
        foreach (ushort c in compressed)
        {

            //string newPattern = c.ToString();
            if(!map.ContainsKey(c))
            {
                map.Add(c, pattern + pattern[0]);
            }
            if (map.ContainsKey(c))
            {
                str += map[c];
                pattern += map[c].FirstOrDefault();
            }
            if (!map.ContainsValue(pattern))
            {
                map.Add((ushort)map.Count, pattern);
                pattern = map[c];
            }
        }
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(str);
        }
    }
    public static string DecompressInt(List<uint> compressed)
    {
        //cita sve dok nije veci od 255 (ascii), i gradi nove kodove
        Dictionary<uint, string> map = new Dictionary<uint, string>();

        for (int i = 0; i < 256; i++)
        {
            map.Add((uint)i, ((char)i).ToString());
        }

        string str = string.Empty;
        string pattern = string.Empty;
        foreach (uint c in compressed)
        {

            if (map.ContainsKey(c))
            {
                str += map[c];
                pattern += map[c].FirstOrDefault();
            }
            if (!map.ContainsValue(pattern))
            {
                map.Add((ushort)map.Count, pattern);
                pattern = map[c];
            }
        }
        return str;
    }

    public static string Read(string name)
    {
        string ret;
        using (StreamReader sr = new StreamReader(name))
        {
            ret = sr.ReadToEnd();
        }
        return ret;
    }
}