using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }

        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }



        // Gráf.SzelességiBejár(kezdopont: egész): 
        // ---> Gráf classban a SzelességiBejár metódus, ami egész értéket vár
        public void SzelessegiBejar(int kezdopont)
        {
            // Kezdetben egy pontot sem jártunk be
            // bejárt = új üres Halmaz()
            HashSet<int> bejart = new HashSet<int>();

            // A következőnek vizsgált elem a kezdőpont
            // következők = új üres Sor()
            Queue<int> kovetkezok = new Queue<int>();

            // következők.hozzáad(kezdőpont)
            // bejárt.hozzáad(kezdőpont)
            kovetkezok.Enqueue(kezdopont);
            bejart.Add(kezdopont);

            // Amíg van következő, addig megyünk
            // Ciklus amíg következők nem üres:
            while (kovetkezok.Count != 0)
            {

                // A sor elejéről vesszük ki
                // k = következők.kivesz()
                int k = kovetkezok.Dequeue();

                // Elvégezzük a bejárási műveletet, pl. a konzolra kiírást:
                // Kiír(this.csúcsok[k])
                Console.WriteLine(this.csucsok[k]);

                // Ciklus él = this.élek elemei:
                foreach (var el in this.elek)
                {
                    // Megkeressük azokat az éleket, amelyek k-ból indulnak
                    // Ha az él másik felét még nem vizsgáltuk, akkor megvizsgáljuk
                    // Ha(él.csúcs1 == k) és(bejárt nem tartalmazza él.csúcs2-t) :
                    if (el.Csucs1==k && !bejart.Contains(el.Csucs2))
                    {
                        // A sor végére és a bejártak közé szúrjuk be
                        // következők.hozzáad(él.csúcs2)
                        // bejárt.hozzáad(él.csúcs2)
                        kovetkezok.Enqueue(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
                // Jöhet a sor szerinti következő elem
            }
        }


        // Gráf.MélységiBejár(kezdopont: egész):
        // ---> egész értéket váró metódus
        public void MelysegiBejar(int kezdopont)
        {
            // Kezdetben egy pontot sem jártunk be
            // bejárt = új üres Halmaz()
            HashSet<int> bejart = new HashSet<int>();

            // A következőnek vizsgált elem a kezdőpont
            // következők = új üres Verem()
            Stack<int> kovetkezok = new Stack<int>();

            // következők.hozzáad(kezdőpont)
            // bejárt.hozzáad(kezdőpont)
            kovetkezok.Push(kezdopont);
            bejart.Add(kezdopont);

            // Amíg van következő, addig megyünk
            // Ciklus amíg következők nem üres:
            while (kovetkezok.Count != 0)
            {
                // A verem tetejéről vesszük le
                // k = következők.kivesz()
                int k = kovetkezok.Pop();

                // Elvégezzük a bejárási műveletet, pl. a konzolra kiírást:
                // Kiír(this.csúcsok[k])
                Console.WriteLine(this.csucsok[k]);

                // Ciklus él = this.élek elemei:
                foreach (var el in this.elek)
                {
                    // Megkeressük azokat az éleket, amelyek k-ból indulnak
                    // Ha az él másik felét még nem vizsgáltuk, akkor megvizsgáljuk
                    // Ha(él.csúcs1 == k) és(bejárt nem tartalmazza él.csúcs2-t) :
                    if (el.Csucs1 == k && !bejart.Contains(el.Csucs2))
                    {
                        // A verem tetejére és a bejártak közé adjuk hozzá
                        // következők.hozzáad(él.csúcs2)
                        // bejárt.hozzáad(él.csúcs2)
                        kovetkezok.Push(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
                // Jöhet a sor szerinti következő elem
            }
        }


        // Gráf.Összefüggő(): logikai
        // ---> bool típusú visszatérési értékű metódus
        public bool Osszefuggo()
        {
            // bejárt = új üres Halmaz()
            HashSet<int> bejart = new HashSet<int>();

            // következők = új üres Sor()
            Queue<int> kovetkezok = new Queue<int>();

            // következők.hozzáad(0) // Tetszőleges, mondjuk 0 kezdőpont
            // bejárt.hozzáad(0)
            kovetkezok.Enqueue(0);
            bejart.Add(0);

            // Ciklus amíg következők nem üres:
            while (kovetkezok.Count != 0)
            {
                // k = következők.kivesz()
                int k = kovetkezok.Dequeue();
                // Bejárás közben nem kell semmit csinálni

                // Ciklus él = this.élek elemei:
                foreach (var el in this.elek)
                {
                    // Ha(él.csúcs1 == k) és(bejárt nem tartalmazza él.csúcs2-t) :
                    if (el.Csucs1 == k && !bejart.Contains(el.Csucs2))
                    {
                        // következők.hozzáad(él.csúcs2)
                        // bejárt.hozzáad(él.csúcs2)
                        kovetkezok.Enqueue(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }

            // A végén megvizsgáljuk, hogy minden pontot bejártunk-e
            // Ha bejárt.elemszám == this.csúcsokSzáma: Igaz, különben hamis
            if (bejart.Count == this.csucsokSzama)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Gráf.Feszitőfa(): Gráf                            
        // ---> Gráf típusú feszítőfa metódus
        public Graf Feszitofa()
        {
            // Új, kezdetben él nélküli gráf
            // fa = új Gráf(this.csúcsokSzáma)
            Graf fa = new Graf(this.csucsokSzama);

            // Bejáráshoz szükséges adatszerkezetek
            // bejárt = új üres Halmaz()
            // következők = új üres Sor()
            HashSet<int> bejart = new HashSet<int>();
            Queue<int> kovetkezok = new Queue<int>();

            // Tetszőleges, mondjuk 0 kezdőpont
            // következők.hozzáad(0)
            // bejárt.hozzáad(0)
            kovetkezok.Enqueue(0);
            bejart.Add(0);

            //// Szélességi bejárás
            //Ciklus amíg következők nem üres:
            while (kovetkezok.Count != 0)
            {
                // k = következők.kivesz()
                int k = kovetkezok.Dequeue();

                // Ciklus él = this.élek elemei:
                foreach (var el in this.elek)
                {
                    // Ha él.csúcs1 == aktuálisCsúcs:
                    if (el.Csucs1 == this.elek[k].Csucs1)
                    {
                        // Ha bejárt nem tartalmazza él.Csúcs2-t:
                        if (!bejart.Contains(el.Csucs2))
                        {
                            // bejárt.hozzáad(él.csúcs2)
                            // következők.hozzáad(él.Csúcs1)
                            bejart.Add(el.Csucs2);
                            kovetkezok.Enqueue(el.Csucs1);

                            // A fába is vegyük bele az élt
                            // fa.hozzáad(él.Csucs1, él.csúcs2)
                            fa.Hozzaad(el.Csucs1,el.Csucs2);
                        }
                    }
                }
            }
            // Az eredményül kapott gráf az eredeti gráf feszítőfája
            //vissza: fa
            return fa;
        }







        /// <summary>
        ///                     HIBÁS
        /// </summary>

        //// Gráf.MohóSzínezés(): Szótár(egész => egész)
        //public void Mohoszinezes(): Dictionary(int => int)
        //{   
        //    // színezés = új üres Szótár()
        //    Dictionary<int, int> szinezes = new Dictionary<int, int>();

        //    // Legrosszabb esetben minden csúcsot különböző színűre kell színezni, ezért ennyi szín elég lesz
        //    // maxSzín = this.csúcsokSzáma
        //    int maxSzin = this.csucsokSzama;

        //    // Ciklus aktuálisCsúcs = 0-tól this.csúcsokSzáma - 1 -ig:
        //    for (int aktualisCsucs = 0; aktualisCsucs < this.csucsokSzama-1; aktualisCsucs++)
        //    {
        //        // Kezdetben bármely színt választhatjuk
        //        // választhatóSzínek = új Halmaz(), amely 0...maxSzín-1 elemekkel van feltöltve
        //        HashSet<int> valaszthatoSzinek = new HashSet<int>();
        //        for (int i = 0; i < maxSzin-1; i++)
        //        {
        //            valaszthatoSzinek.Add(maxSzin);
        //        }
        //        // Vizsgáljuk meg a szomszédos csúcsokat:
        //        // Ciklus él = this.élek elemei:
        //        foreach (var el in this.elek)
        //        {
        //            // Ha él.csúcs1 == aktuálisCsúcs:
        //            // Ha a szomszédos csúcs már be van színezve, azt a színt már nem választhatjuk
        //            if (el.Csucs1 == aktualisCsucs)
        //            {
        //                // Ha színezés.tartalmazKulcsot(él.csúcs2):
        //                if (szinezes.ContainsKey(el.Csucs2))
        //                {
        //                    // szín = színezés[él.csúcs2]
        //                    // választhatóSzínek.kivesz(szín)
        //                    int szin = szinezes[el.Csucs2];
        //                    valaszthatoSzinek.Remove(szin);
        //                }
        //            }

        //        }
        //        // A maradék színekből válasszuk ki a legkisebbet 
        //        // választottSzín = Min(választhatóSzínek)
        //        // színezés.hozzáad(aktuálisCsúcs, választottSzín)
        //        int valasztottSzin = Min(valaszthatoSzinek);
        //        szinezes.Add(aktualisCsucs, valasztottSzin);

        //    }
        //    // vissza: színezés
        //    return szinezes;
        //}



    }
}