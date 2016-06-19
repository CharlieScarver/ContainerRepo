namespace _01_CubicArtillery
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            Queue<Bunker> bunkerQueue = new Queue<Bunker>();
            
            Bunker.MAX_CAPACITY = int.Parse(Console.ReadLine());
            
            string inputLine = Console.ReadLine();
            Bunker currentBunker = null;

            while (inputLine != "Bunker Revision")
            {
                string[] lineData = inputLine.Split(' ');

                for (int i = 0; i < lineData.Length; i++)
                {
                    int weaponCapacity = 0;
                    if (int.TryParse(lineData[i], out weaponCapacity))
                    {
                        // if bunker can store weapon
                        if (currentBunker.Capacity + weaponCapacity <= Bunker.MAX_CAPACITY)
                        {
                            currentBunker.Capacity += weaponCapacity;
                            currentBunker.WeaponsStored.Enqueue(weaponCapacity);
                        }
                        // if bunker can't store weapon/overflows
                        else
                        {   
                            // if there is more than one bunker (check is with 0 because the current element is kept outside the queue)
                            if (bunkerQueue.Count > 0)
                            {
                                // remove/print bunker
                                if (currentBunker.WeaponsStored.Count > 0)
                                {
                                    Console.WriteLine($"{currentBunker.Name} -> {string.Join(", ", currentBunker.WeaponsStored)}");
                                }
                                else
                                {
                                    Console.WriteLine($"{currentBunker.Name} -> Empty");
                                }

                                // remember initial count
                                int bunkerCount = bunkerQueue.Count;

                                // remove bunker
                                //bunkerQueue.Dequeue();

                                // make next bunker current
                                currentBunker = bunkerQueue.Dequeue();
                                //bunkerQueue.Enqueue(currentBunker);
                                
                                // if we can store current weapon in the new current bunker
                                if (currentBunker.Capacity + weaponCapacity <= Bunker.MAX_CAPACITY)
                                {
                                    currentBunker.Capacity += weaponCapacity;
                                    currentBunker.WeaponsStored.Enqueue(weaponCapacity);
                                }
                                // if we can't store it
                                else
                                {
                                    char currBunkerName = currentBunker.Name;

                                    // search for a bunker that can store it
                                    int checkedBunkers = 1;
                                    while (currentBunker.Capacity + weaponCapacity > Bunker.MAX_CAPACITY)
                                    {
                                        /*// making sure we don't over enqueue the current bunker
                                        if (bunkerQueue.Peek().Name == currBunkerName)
                                        {
                                            //currentBunker = bunkerQueue.Peek();
                                            //break;
                                        }*/

                                        // make next bunker current
                                        bunkerQueue.Enqueue(currentBunker);
                                        currentBunker = bunkerQueue.Dequeue();
                                        checkedBunkers++;
                                        if (checkedBunkers == bunkerCount)
                                        {
                                            break;
                                        }
                                    }

                                    // if we can store current weapon in the new current bunker
                                    if (currentBunker.Capacity + weaponCapacity <= Bunker.MAX_CAPACITY)
                                    {
                                        currentBunker.Capacity += weaponCapacity;
                                        currentBunker.WeaponsStored.Enqueue(weaponCapacity);
                                    }
                                    // if we can't we ignore the weapon and continue
                                }
                            }
                            // if there is only one bunker
                            else
                            {
                                // if weapon is storeable
                                if (weaponCapacity <= Bunker.MAX_CAPACITY)
                                {
                                    // remember initial count of stored weapons
                                    int weaponsStored = currentBunker.WeaponsStored.Count;
                                    // start removing weapons to free capacity
                                    for (int j = 0; j < weaponsStored; j++)
                                    {
                                        currentBunker.Capacity -= currentBunker.WeaponsStored.Dequeue();
                                        // if we freed enough capacity store the weapon
                                        if (currentBunker.Capacity + weaponCapacity <= Bunker.MAX_CAPACITY)
                                        {
                                            currentBunker.Capacity += weaponCapacity;
                                            currentBunker.WeaponsStored.Enqueue(weaponCapacity);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        char bunkerName = char.Parse(lineData[i]);
                        Bunker bunker = new Bunker(bunkerName, Bunker.MAX_CAPACITY);
                        
                        if (currentBunker != null)
                        {
                            bunkerQueue.Enqueue(bunker);
                        }
                        else
                        {
                            currentBunker = bunker;
                        }
                    }
                }

                inputLine = Console.ReadLine();
            }
        
        }

        public class Bunker
        {
            public static int MAX_CAPACITY;

            public Bunker(char name, int capacity)
            {
                this.Name = name;
                this.Capacity = 0;
                this.WeaponsStored = new Queue<int>();
            }

            public char Name { get; set; }
            public int Capacity { get; set; }
            public Queue<int> WeaponsStored { get; set; } 
        }
    }
}
