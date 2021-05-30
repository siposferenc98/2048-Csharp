using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Beadandó
{
    class Program
    {
        public static int[,] matrix = new int[4, 4];
        public static int[,] matrixBack = new int[4, 4];
        public static int pontok = 0;
        public static int cheat = 3;
        public static int defaultszam = 2;
        public static int Esor = 10;
        public static int Eoszlop = 10;


        public static void Feltolt(int[,] matrix)
        {

            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {

                    matrix[i, j] = 0;

                }
            }

        }

        public static void KettesGen(int[,] matrix)
        {
            Random rnd = new Random();
            int a, b;
            bool gen = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(matrix[i,j] == 0)
                    {
                        gen = true;
                        
                    }
                }
            }

            if (gen == true)
            {
                do
                {
                    a = rnd.Next(0, 4);
                    b = rnd.Next(0, 4);

                } while (matrix[a, b] != 0);
                matrix[a, b] = defaultszam;
            }





            Kiir(matrix);
            Nyilak();
        }

        public static void HolRajzol(string a, int x, int y)
        {
            Console.SetCursorPosition(Esor + x, Eoszlop + y);
            Console.Write(a);

        }

        public static void Rajzol()
        {


            for (int i = 1; i < 16; i++)
            {
                HolRajzol("|", 0, i);
            }


           

            for (int i = 1; i < 50; i++)
            {
                HolRajzol("-", i, 16);
            }



            for (int i = 15; i > 0 ; i--)
            {
                HolRajzol("|", 50, i);
            }



            for (int i = 49; i > 0 ; i--)
            {
                HolRajzol("-", i, 0);
            }

            for (int i = 1; i < 16; i++)
            {
                HolRajzol("|", 10, i);
            }

            for (int i = 1; i < 16; i++)
            {
                HolRajzol("|", 25, i);
            }

            for (int i = 1; i < 16; i++)
            {
                HolRajzol("|", 38, i);
            }

            for (int i = 1; i < 50; i++)
            {
                HolRajzol("-", i, 4);
            }

            for (int i = 1; i < 50; i++)
            {
                HolRajzol("-", i, 8);
            }

            for (int i = 1; i < 50; i++)
            {
                HolRajzol("-", i, 12);
            }
        }

        public static void BackupMatrix()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrixBack[i, j] = matrix[i, j];
                }
            }
        }

        public static void VisszaVon()
        {
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        matrix[i, j] = matrixBack[i, j];
                    }
                }
                Kiir(matrix);
                Nyilak();
            }
        }

        public static void Leiras()
        {

            Console.WriteLine("Pontok: " + pontok + "      " + "Cheat lehetőségek: " + cheat);
            Console.WriteLine();
            Console.WriteLine("Mozgatás: Nyilak        Visszavonás: Backspace       Cheat: Home            Kilépés: Escape");
            Console.WriteLine("Táblázat lementése: End      Táblázat betöltése: Delete");

        }
        
        public static void Cheat()
        {
            int ertek = 0;
            if (cheat > 0)
            {
                Console.WriteLine("Írd be a mező koordinátáját vesszővel elválasztva(Első érték:oszlop, Második érték: sor): ");
                string[] kord = Console.ReadLine().Split(',');
                int a = Convert.ToInt32(kord[0]) - 1;
                int b = Convert.ToInt32(kord[1]) - 1;
                if (a<0 || a>4 || b<0 || b>4)
                {
                    Console.WriteLine("Nem jó a koordináta! Írd be újra: ");
                    Cheat();
                }
                else
                {
                    
                    do
                    {
                        Console.WriteLine("Írd be az értéket amire át szeretnéd írni(Csak 2,4,8 lehet): ");
                        ertek = Convert.ToInt32(Console.ReadLine());
                        
                    } while (ertek != 2 && ertek != 4 && ertek != 8);
                }


                cheat--;
                matrix[b, a] = ertek;
                Kiir(matrix);
                Nyilak();

            }
            else
            {
                Console.WriteLine("Nincs több lehetőséged érték változtatásra!");
                Nyilak();
            }
        }

        public static void Nyeres()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[i,j] >= 2048)
                    {
                        Ranglista();
                        
                    }
                }
            }
        }

        public static bool Vege()
        {
            bool vegeE = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[i,j] == 0)
                    {
                        vegeE = false;
                    }

                }

            }
            if (vegeE == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (matrix[i, j] == matrix[i + 1, j] || matrix[i, j] == matrix[i, j + 1])
                        {
                            vegeE = false;
                        }
                    }
                }
                //sarkok
                if (matrix[0, 3] == matrix[1, 3] || matrix[0, 3] == matrix[0, 2])
                {
                    vegeE = false;
                }
                if(matrix[0, 0] == matrix[1, 0] || matrix[0,0] == matrix[0,1])
                {
                    vegeE = false;
                }
                if (matrix[3,0] == matrix[2, 0] || matrix[3, 0] == matrix[3, 1])
                {
                    vegeE = false;
                }
                if (matrix[3, 3] == matrix[3, 2] || matrix[2, 3] == matrix[3, 3])
                {
                    vegeE = false;
                }
            }
            
            return vegeE;
        }

        public static void Ranglista()
        {
            StreamWriter sw = new StreamWriter("highscore.txt",true);
            Console.WriteLine("Ez lementi a highscore.txt fileba az eredményed,a program automatikusan bezárul a név beírása után.");
            Console.WriteLine("Írd be a neved: ");
            string nev = Console.ReadLine();
            DateTime date = DateTime.Now;
            string sor = date +" "+ nev + " " +Convert.ToString(pontok);
            sw.WriteLine(sor);
            sw.Close();
            Environment.Exit(0);


        }

        public static void TablazatBetolt()
        {
            Console.WriteLine("Írd be a file nevét ahonnan be szeretnéd tölteni a táblázatod(.txt végződéssel): ");
            string filenev = Console.ReadLine();
            StreamReader sw = new StreamReader(filenev);
            for (int i = 0; i < 4; i++)
            {
                string sor = sw.ReadLine();
                string[] szamok = sor.Split(' ');
                

                    for (int j = 0; j < 4; j++)
                    {
                    int szam1 = Convert.ToInt32(szamok[j]);
                    matrix[i, j] = szam1;

                    }
            }
            sw.Close();
            Kiir(matrix);
            Nyilak();

        }

        public static void TablazatLement()
        {
            Console.WriteLine("Írd be a file nevét amibe le szeretnéd menteni a táblázatot(.txt végződéssel): ");
            string filenev = Console.ReadLine();
            StreamWriter sw = new StreamWriter(filenev);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string sor = Convert.ToString(matrix[i, j])+" ";
                    sw.Write(sor);
                }
                sw.WriteLine();
            }
            sw.Close();
            Environment.Exit(0);
        }

        public static void Felfele(int[,] matrix)
        {
            BackupMatrix();
            Nyeres();
            Vege();
            if (Vege() == false)
            {
                FelfeleMozg(matrix);
                for (int i = 1; i < 4; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {

                        if (matrix[i - 1, j] == matrix[i, j])
                        {
                            if (matrix[i, j] != 0)
                            {
                                int a = matrix[i - 1, j];
                                int b = matrix[i, j];

                                a = a + b;
                                pontok += a;
                                matrix[i - 1, j] = a;
                                matrix[i, j] = 0;
                            }


                        }


                    }
                }
                KettesGen(matrix);
            }

            else
            {
                Ranglista();
            }
        }

        public static void Lefele(int [,] matrix)
        
        {
            Nyeres();
            BackupMatrix();
            Vege();
            if (Vege() == false)
            {
                LefeleMozg(matrix);
                for (int i = 2; i >= 0; i--)
                {

                    for (int j = 0; j < 4; j++)
                    {

                        if (matrix[i + 1, j] == matrix[i, j])
                        {
                            if (matrix[i, j] != 0)
                            {
                                int a = matrix[i + 1, j];
                                int b = matrix[i, j];

                                a = a + b;
                                pontok += a;
                                matrix[i + 1, j] = a;
                                matrix[i, j] = 0;
                            }


                        }


                    }
                }
                KettesGen(matrix);
            }
            else
            {
                Ranglista();
            }
        }

        public static void Jobbra(int [,] matrix)
        {
            Nyeres();
            BackupMatrix();
            Vege();
            if (Vege() == false)
            {
                JobbraMozg(matrix);
                for (int i = 0; i < 4; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {

                        if (matrix[i, j + 1] == matrix[i, j])
                        {
                            if (matrix[i, j] != 0)
                            {
                                int a = matrix[i, j + 1];
                                int b = matrix[i, j];

                                a = a + b;
                                pontok += a;
                                matrix[i, j + 1] = a;
                                matrix[i, j] = 0;
                            }


                        }


                    }
                }
                KettesGen(matrix);

            }
            else
            {
                Ranglista();
            }
            
        }
    
        public static void Balra(int [,] matrix)
        {
            Nyeres();
            BackupMatrix();
            Vege();
            if (Vege() == false)
            {
                BalraMozg(matrix);
                for (int i = 0; i < 4; i++)
                {

                    for (int j = 1; j < 4; j++)
                    {

                        if (matrix[i, j - 1] == matrix[i, j])
                        {
                            if (matrix[i, j] != 0)
                            {
                                int a = matrix[i, j - 1];
                                int b = matrix[i, j];

                                a = a + b;
                                pontok += a;
                                matrix[i, j - 1] = a;
                                matrix[i, j] = 0;
                            }


                        }


                    }
                }
                KettesGen(matrix);

            }
            else
            {
                Ranglista();
            }
                
            
        }


        public static void FelfeleMozg(int[,] matrix)
        {
            for (int i = 1; i < 4; i++)
            {
                
                for (int j = 0; j < 4; j++)
                {
                    for (int sv = 3; sv > 0; sv--)
                    {
                        if (matrix[sv - 1, j] == 0 && matrix[sv, j] != 0)
                        {
                            matrix[sv - 1, j] = matrix[sv, j];
                            matrix[sv, j] = 0;

                        }
                    }
                }
            }

        }

        public static void LefeleMozg(int [,] matrix)
        {
            
                for (int i = 2; i >= 0; i--)
                {

                    for (int j = 0; j < 4; j++)
                    {
                        for (int sv = 0; sv < 3; sv++)
                        {
                            if (matrix[sv + 1, j] == 0 && matrix[sv, j] != 0)
                            {
                                matrix[sv + 1, j] = matrix[sv, j];
                                matrix[sv, j] = 0;

                            }
                        }
                    }
                }

            
        }

        public static void JobbraMozg(int[,] matrix)
        {
            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    for (int sv = 0; sv < 4; sv++)
                    {
                        if (matrix[sv, j+1] == 0 && matrix[sv, j] != 0)
                        {
                            matrix[sv, j+1] = matrix[sv, j];
                            matrix[sv, j] = 0;

                        }
                    }
                }
            }

        }

        public static void BalraMozg(int[,] matrix)
        {
            for (int i = 0; i < 4; i++)
            {

                for (int j = 1; j < 4; j++)
                {
                    for (int sv = 3; sv >= 0; sv--)
                    {
                        if (matrix[sv, j - 1] == 0 && matrix[sv, j] != 0)
                        {
                            matrix[sv, j - 1] = matrix[sv, j];
                            matrix[sv, j] = 0;

                        }
                    }
                }
            }
        }

        public static void Nyilak ()
            {
                var info = Console.ReadKey();
                if(info.Key == ConsoleKey.UpArrow)
                {
                Felfele(matrix);
                }
                else if (info.Key == ConsoleKey.DownArrow)
                {
                Lefele(matrix);
                }
                else if (info.Key == ConsoleKey.LeftArrow)
                {
                Balra(matrix);
                }
                else if (info.Key == ConsoleKey.RightArrow)
                {
                Jobbra(matrix);
                }
                else if (info.Key == ConsoleKey.Home)
                {
                Cheat();
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                VisszaVon();
                }
                else if (info.Key == ConsoleKey.End)
                {
                TablazatLement();   
                }
                else if (info.Key == ConsoleKey.Delete)
                {
                TablazatBetolt();
                }
            else if (info.Key == ConsoleKey.Escape)
                {
                        Environment.Exit(0);
                }
                else
                {
                    Nyilak();
                }
        
            }

        public static bool PrimE(int szam)
        {
            bool sv = false;
            
            for (int i = 2; i<szam; i++)
            {
                if (szam%i == 0)
                {
                    sv = false;
                    break;
                }
                else
                {
                    sv = true;
                }
            }
            return sv;
        }
        
        public static void Kiir(int [,] matrix)
        {
            Console.Clear();
            Leiras();
            Rajzol();
            Console.WriteLine();

            HolRajzol(Convert.ToString(matrix[0, 0]), 5, 2);
            HolRajzol(Convert.ToString(matrix[0, 1]), 18, 2);
            HolRajzol(Convert.ToString(matrix[0, 2]), 32, 2);
            HolRajzol(Convert.ToString(matrix[0, 3]), 44, 2);
            HolRajzol(Convert.ToString(matrix[1, 0]), 5, 6);
            HolRajzol(Convert.ToString(matrix[1, 1]), 18, 6);
            HolRajzol(Convert.ToString(matrix[1, 2]), 32, 6);
            HolRajzol(Convert.ToString(matrix[1, 3]), 44, 6);
            HolRajzol(Convert.ToString(matrix[2, 0]), 5, 10);
            HolRajzol(Convert.ToString(matrix[2, 1]), 18, 10);
            HolRajzol(Convert.ToString(matrix[2, 2]), 32, 10);
            HolRajzol(Convert.ToString(matrix[2, 3]), 44, 10);
            HolRajzol(Convert.ToString(matrix[3, 0]), 5, 14);
            HolRajzol(Convert.ToString(matrix[3, 1]), 18, 14);
            HolRajzol(Convert.ToString(matrix[3, 2]), 32, 14);
            HolRajzol(Convert.ToString(matrix[3, 3]), 44, 14);


            Console.SetCursorPosition(0,28);
            
            
        }

        public static void KezdoKettes(int [,] matrix)
        {
            Random rnd = new Random();
            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);
            int szam;
            Console.WriteLine("Szeretnéd módosítani a kezdő számot? (Csak prím lehet) [y,n]: ");
            char yn = Convert.ToChar(Console.ReadLine());
            if (yn == 'y')
            {
                do
                {
                    Console.WriteLine("Írd be a prím számot amivel kezdeni szeretnél!(csak prím lehet) :");
                    szam = Convert.ToInt32(Console.ReadLine());
                    PrimE(szam);
                } while (PrimE(szam) != true);

                defaultszam = szam;
                matrix[a, b] = defaultszam;
            }
            else
            {
                matrix[a, b] = defaultszam;
            }

            
        }
        
        public static void Kezdes()
        {
            
            Nyilak();
        }


        static void Main(string[] args)
        {
            Feltolt(matrix);
            KezdoKettes(matrix);
            Kiir(matrix);
            Kezdes();
            Console.ReadKey();
        }

        
    }
}
