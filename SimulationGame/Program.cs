//********************************************
// ΠΑΙΧΝΙΔΙ ΠΡΟΣΟΜΟΙΩΣΗΣ ΜΑΧΗΣ ΟΜΑΔΩΝ (C#)
//********************************************

using System;

class Program
{
    static void Main(string[] args)
    {
        //===============================================================
        // ΠΡΟΓΡΑΜΜΑ: Απλή Προσομοίωση Μάχης Ομάδων
        //===============================================================
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("===ΠΡΟΣΟΜΟΙΩΣΗ ΜΑΧΗΣ ΟΜΑΔΩΝ===\n");

        //Δημιουργία ονομάτων ομάδων
        string team1 = "Demon_Slayers".Replace("_", " ");
        string team2 = "Demons".Replace("_", " ");
        if (team1.Length > 15) team1 = team1.Substring(0, 15);
        if (team2.Length > 15) team2 = team2.Substring(0, 15);

        Console.WriteLine("Ομάδα 1: " + team1);
        Console.WriteLine("Ομάδα 2: " + team2 + "\n");

        //=========================================================================
        // ΠΙΝΑΚΕΣ ΜΕ ΟΝΟΜΑΤΑ ΚΑΙ ΖΩΕΣ
        //=========================================================================

        string[] heroes = { "Tanjiro", "Zenitsu", "Inosuke", "Rengoku", "Giyu" };
        string[] monsters = { "Muzan", "Kokushibo", "Akaza", "Gyutaro", "Douma" };
        int[] heroHP = new int[heroes.Length];
        int[] monsterHP = new int[monsters.Length];

        Random rand = new Random();

        //Δίνουμε σε κάθε χαρακτήρα τυχαία ποσότητα ζωής
        for (int i = 0; i < heroHP.Length; i++)
        {
            heroHP[i] = rand.Next(60, 101);  // 60-100HP
            monsterHP[i] = rand.Next(50, 101); // 50-100HP
        }

        // Εμφάνηση αρχικών ζωών
        Console.WriteLine("Αρχικές Ζωές:");
        for (int i = 0;i < heroHP.Length; i++)
        {
            Console.WriteLine($"{heroes[i]}: {heroHP[i]} HP | {monsters[i]}: {monsterHP[i]} HP");
        }
        Console.WriteLine();

        //==========================================
        //ΕΝΑΡΞΗ ΜΑΧΗΣ
        //==========================================
        int round = 1;

        do
        {
            Console.WriteLine($"=== Γύρος {round} ===");

            for (int i=0; i < heroes.Length; i++)
            {
                //Αν και οι δυο εχουν 0 HP, δεν πολεμουν αλλο
                if (heroHP[i] <= 0 && monsterHP[i] <= 0) continue;

                int ev = rand.Next(0, 3); // 0,1 η 2
                

                switch(ev)
                {
                    case 0: // Ηρωας επιτιθεται
                        int dmgHero = rand.Next(5, 26);
                        monsterHP[i] -= dmgHero;
                        Console.WriteLine($"{heroes[i]} επιτίθεται στον {monsters[i]} για {dmgHero} ζημία!");
                        break;

                    case 1: // Τερας επιτιθεται
                        int dmgMonster = rand.Next(3, 29);
                        heroHP[i] -= dmgMonster;
                        Console.WriteLine($"{monsters[i]} επιτίθεται στον {heroes[i]} για {dmgMonster} ζημία!");
                        break;

                    case 2: //Θεραπεια
                        int heal = rand.Next(5, 16);
                        if (rand.NextDouble() < 0.5 && heroHP[i] > 0)
                        {
                            heroHP[i] += heal;
                            Console.WriteLine($"{heroes[i]} αναρρώνει + {heal} HP!");
                        }
                        else if (monsterHP[i] > 0)
                        {
                            monsterHP[i] += heal;
                            Console.WriteLine($"{monsters[i]} αναρρώνει + {heal} HP!");
                        }
                        break;
                }

                //Διορθώνουμε HP αν περάσει το μηδέν
                if (heroHP[i] < 0) heroHP[i] = 0;
                if (monsterHP[i] < 0) monsterHP[i] = 0;

                Console.WriteLine($"{heroes[i]}: {heroHP[i]} HP | {monsters[i]}: {monsterHP[i]} HP");
            }
            // Μήνυμα ολοκλήρωσης γύρου
            string msg = $" Round_{round}_Completed".Replace("_", " ");
            if (msg.Length > 20) msg = msg.Substring(0, 20);
            Console.WriteLine("-- " + msg + " --\n");

            //Έλεγχος αν ζει ακόμα κάποια ομάδα
            bool heroesAlive = false;
            bool monstersAlive = false;

            for (int i = 0; i < heroes.Length; i++)
            {
                if (heroHP[i] > 0) heroesAlive = true;
                if (monsterHP[i] > 0) monstersAlive = true;
            }

            // Αν κάποια ομάδα πέθανε τελείως, σταματάμε
            if (!heroesAlive || !monstersAlive) break;

            round++;

        } while (true);

        //===========================================================
        // ΥΠΟΛΟΗΙΣΜΟΣ ΝΙΚΗΤΗ
        //===========================================================

        int sumHeroes = 0, sumMonsters = 0;
        for (int i = 0; i < heroes.Length; i++)
        {
            sumHeroes += heroHP[i];
            sumMonsters += monsterHP[i];
        }
        Console.WriteLine("=== ΤΕΛΟΣ ΜΑΧΗΣ ===");
        if (sumHeroes > sumMonsters)
        {
            Console.WriteLine($"Οι {team1} αφάνισαν τους Δαίμονες!");
        }
        else if (sumMonsters > sumHeroes)
        {
            Console.WriteLine($"Οι {team2} διέλισαν τους κυνηγούς δαιμόνων!");
        }
        else
        {
            Console.WriteLine("Ισοπαλία! Καμία ομάδα δεν κυριάρχησε!");
        }
        Console.WriteLine("\nΤελικές Ζωές:");
        for (int i = 0; i < heroes.Length; i++)
        {
            Console.WriteLine($" {heroes[i]}: {heroHP[i]} HP | {monsters[i]}: {monsterHP[i]} HP");
        }
        Console.WriteLine("\nΤέλος προσομοίωσης.");
    }
}

