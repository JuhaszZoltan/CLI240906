namespace CLI240906
{
    internal class LabSim
    {
        private List<string> adatsorok = [];
        private char[,] lab;

        public bool KeresesKesz { get; set; }
        public int KijaratOszlopIndex { get; }
        public int KijaratSorIndex { get; }
        public bool NincsMegoldas { get; set; }
        public int OszlopokSzama { get; }
        public int SorokSzama { get; }

        private void AdatsorokBeolvasasa(string forras) 
        {
            using StreamReader sr = new(@$"..\..\..\src\{forras}");

            while (!sr.EndOfStream) adatsorok.Add(sr.ReadLine());
        }
        private void LabFeltoltese()
        {
            for (int s = 0; s < SorokSzama; s++)
            {
                for (int o = 0; o < OszlopokSzama; o++)
                {
                    lab[s, o] = adatsorok[s][o];
                }
            }
        }
        public void LabKiirasa()
        {
            for (int s = 0; s < SorokSzama; s++)
            {
                for (int o = 0; o < OszlopokSzama; o++)
                {
                    if (lab[s, o] == '-') Console.ForegroundColor = ConsoleColor.Red;
                    if (lab[s, o] == 'O') Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(lab[s, o]);
                    Console.ResetColor();
                }
                Console.Write('\n');
            }
        }
        public void Utkereses()
        {
            KeresesKesz = false;
            NincsMegoldas = false;

            int r = 1;
            int c = 0;

            while(!KeresesKesz && !NincsMegoldas)
            {
                Console.Clear();
                lab[r, c] = 'O';
                if (lab[r, c + 1] == ' ') c++;
                else if (lab[r + 1, c] == ' ') r++;
                else
                {
                    lab[r, c] = '-';
                    if (lab[r, c - 1] == 'O') c--;
                    else r--;
                }

                KeresesKesz = r == KijaratSorIndex
                    && c == KijaratOszlopIndex;
                if (KeresesKesz) lab[r, c] = 'O';
                NincsMegoldas = r == 1 && c == 0;
                LabKiirasa();
                Thread.Sleep(200);
            }
        }

        public LabSim(string forras)
        {
            AdatsorokBeolvasasa(forras);

            SorokSzama = adatsorok.Count;
            OszlopokSzama = adatsorok[0].Length;
            KijaratSorIndex = SorokSzama - 2;
            KijaratOszlopIndex = OszlopokSzama - 1;

            lab = new char[SorokSzama, OszlopokSzama];
            LabFeltoltese();
        }
    }
}
