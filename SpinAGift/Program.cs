using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayGame
{
    internal class Program
    {
        static int secondsLeft = 30;
        static bool refreshShop = false;
        static int coins = 100;
        static Random random = new Random();
        static int[] stock = new int[9];

        static string[] gifts =
            {
                "Common Gift",
                "Rare Gift",
                "Epic Gift",
                "Legendary Gift",
                "Exotic Gift",
                "Mythical Gift",
                "Secret Gift",
                "Grinch's Special Gift",
                "Ultimate Gift",
            };

        static int[] giftPrices =
        {
            10,
            25,
            60,
            150,
            375,
            950,
            2400,
            6000,
            15000
        };

        static string[] Common_Epic =
        {
                "Cookie",
                "Milk",
                "Car toy",
                "Chocolate",
                "Stuffed Toy",
        };

        static string[] Legendary_Mythical =
        {
                "Christmas Tree",
                "Super Soldier Serum",
                "Car",
                "Chocolate Factory",
                "Canada",
        };

        static string[] Secret_ultimate =
        {
                "Anti-matter star tree",
                "Godzilla",
                "Super Particle Accelerator",
                "The Christmas Spirit",
                "Santa Clause's Essence",
        };

        static Dictionary<string, int> inventory =
            new Dictionary<string, int>();

        static Dictionary<string, int> sellPrices =
            new Dictionary<string, int>()
            {
                { "Cookie", 100},
                { "Milk", 100 },
                { "Car toy", 150 },
                { "Chocolate", 200 },
                { "Stuffed Toy", 250 },

                { "Christmas Tree", 1000 },
                { "Super Soldier Serum", 2500 },
                { "Car", 3000 },
                { "Chocolate Factory", 3000 },
                { "Canada", 3500 },

                { "Anti-matter star tree", 5000 },
                { "Godzilla", 5500 },
                { "Super Particle Accelerator", 6500 },
                { "The Christmas Spirit", 7000 },
                { "Santa Clause's Essence", 10000 }
            };

        static void Main(string[] args)
        {
            // Initial shop stock
            RefreshShop(stock, random);

            // Start timer thread
            Thread timerThread = new Thread(Timer);
            timerThread.Start();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("            MARCO'S SECRET GIFT SHOP");
                Console.WriteLine("========================================");
                Console.WriteLine();

                Console.WriteLine($"Coins: {coins}");

                Console.WriteLine();

                Console.WriteLine(
                    $"Shop refreshes in: {secondsLeft} seconds"
                );

                Console.WriteLine();

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("                SHOP");
                Console.WriteLine("----------------------------------------");

                for (int i = 0; i < gifts.Length; i++)
                {
                    string rarity;

                    if (i < 3)
                    {
                        rarity = "COMMON";
                    }
                    else if (i < 6)
                    {
                        rarity = "RARE";
                    }
                    else if (i < 8)
                    {
                        rarity = "LEGENDARY";
                    }
                    else
                    {
                        rarity = "ULTIMATE";
                    }

                    if (stock[i] <= 0)
                    {
                        Console.WriteLine(
                            $"{i + 1}. {gifts[i],-25} " +
                            $"[{rarity,-9}] UNAVAILABLE"
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            $"{i + 1}. {gifts[i],-25} " +
                            $"[{rarity,-9}] Stock: {stock[i],2}x " +
                            $"Price: {giftPrices[i]} coins"
                        );
                    }
                }

                Console.WriteLine("----------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Enter the number of a gift to buy.");
                Console.WriteLine("Press S to sell items.");
                Console.WriteLine("Type Q to quit.");
                Console.WriteLine();

                Console.Write("Choice: ");

                string input = "";

                while (true)
                {
                    if (refreshShop)
                    {
                        RefreshShop(stock, random);
                        refreshShop = false;

                        Console.Clear();

                        Console.WriteLine("========================================");
                        Console.WriteLine("            MARCO'S SECRET GIFT SHOP");
                        Console.WriteLine("========================================");
                        Console.WriteLine();

                        Console.WriteLine($"Coins: {coins}");

                        Console.WriteLine();

                        Console.WriteLine(
                            $"Shop refreshes in: {secondsLeft} seconds"
                        );

                        Console.WriteLine();

                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("                SHOP");
                        Console.WriteLine("----------------------------------------");

                        for (int i = 0; i < gifts.Length; i++)
                        {
                            string rarity;

                            if (i < 3)
                            {
                                rarity = "COMMON";
                            }
                            else if (i < 6)
                            {
                                rarity = "RARE";
                            }
                            else if (i < 8)
                            {
                                rarity = "LEGENDARY";
                            }
                            else
                            {
                                rarity = "ULTIMATE";
                            }

                            if (stock[i] <= 0)
                            {
                                Console.WriteLine(
                                    $"{i + 1}. {gifts[i],-25} " +
                                    $"[{rarity,-9}] UNAVAILABLE"
                                );
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"{i + 1}. {gifts[i],-25} " +
                                    $"[{rarity,-9}] Stock: {stock[i],2}x " +
                                    $"Price: {giftPrices[i]} coins"
                                );
                            }
                        }

                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine("Enter the number of a gift to buy.");
                        Console.WriteLine("Press S to sell items.");
                        Console.WriteLine("Type Q to quit.");
                        Console.WriteLine();

                        Console.Write("Choice: ");
                    }

                    if (Console.KeyAvailable)
                    {
                        input = Console.ReadLine();
                        break;
                    }

                    int cursorLeft = Console.CursorLeft;
                    int cursorTop = Console.CursorTop;

                    Console.SetCursorPosition(0, 6);

                    Console.Write(
                        $"Shop refreshes in: {secondsLeft} seconds   "
                    );

                    Console.SetCursorPosition(
                        cursorLeft,
                        cursorTop
                    );

                    Thread.Sleep(100);
                }

                if (input?.ToUpper() == "Q")
                {
                    break;
                }

                if (input?.ToUpper() == "S")
                {
                    SellItems();
                    continue;
                }

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a number from 1-9.");
                    Thread.Sleep(1500);
                    continue;
                }

                if (choice < 1 || choice > 9)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid gift number.");
                    Thread.Sleep(1500);
                    continue;
                }

                int index = choice - 1;

                // Check if gift is unavailable
                if (stock[index] <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("This gift is currently unavailable!");
                    Console.WriteLine("Wait for the shop to refresh.");
                    Thread.Sleep(1500);
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine($"Price: {giftPrices[index]} coins");
                Console.WriteLine($"Available stock: {stock[index]}x");
                Console.WriteLine($"Your coins: {coins}");

                Console.Write("How many would you like to buy? ");

                string quantityInput = Console.ReadLine();

                if (!int.TryParse(quantityInput, out int quantity))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid quantity.");
                    Thread.Sleep(1500);
                    continue;
                }

                if (quantity <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("You must buy at least 1.");
                    Thread.Sleep(1500);
                    continue;
                }

                if (quantity > stock[index])
                {
                    Console.WriteLine();
                    Console.WriteLine("You cannot buy more than the available stock!");
                    Thread.Sleep(1500);
                    continue;
                }

                int totalPrice = giftPrices[index] * quantity;

                if (totalPrice > coins)
                {
                    Console.WriteLine();
                    Console.WriteLine("You don't have enough coins!");
                    Console.WriteLine($"You need {totalPrice - coins} more coins.");
                    Thread.Sleep(1500);
                    continue;
                }

                coins -= totalPrice;

                // Remove the selected quantity from stock
                stock[index] -= quantity;

                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("            MARCO'S SECRET GIFT SHOP");
                Console.WriteLine("========================================");
                Console.WriteLine();

                Console.WriteLine($"Buying {quantity}x {gifts[index]}");
                Console.WriteLine($"Cost: {totalPrice} coins");
                Console.WriteLine($"Coins left: {coins}");

                Console.Write("Opening");

                for (int i = 0; i < 10; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(150);
                }

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Opening the gifts...");
                Console.WriteLine();

                for (int i = 0; i < quantity; i++)
                {
                    string reward = OpenGift(index, random);

                    if (inventory.ContainsKey(reward))
                    {
                        inventory[reward]++;
                    }
                    else
                    {
                        inventory.Add(reward, 1);
                    }

                    Console.WriteLine($"Gift #{i + 1}: {reward}");

                    Thread.Sleep(500);
                }

                Console.WriteLine();
                Console.WriteLine("NICE ONE!");
                Console.WriteLine($"You opened {quantity}x {gifts[index]}!");
                Console.WriteLine($"Remaining stock: {stock[index]}x");

                Console.WriteLine();
                Console.WriteLine("Press ENTER to return to the shop.");
                Console.ReadLine();
            }
        }

        static void Timer()
        {
            while (true)
            {
                Thread.Sleep(1000);

                secondsLeft--;

                if (secondsLeft <= 0)
                {
                    refreshShop = true;
                    secondsLeft = 30;
                }
            }
        }

        static void RefreshShop(int[] stock, Random random)
        {
            for (int i = 0; i < stock.Length; i++)
            {
                // Reset stock first
                stock[i] = 0;

                int availabilityChance = random.Next(1, 101);

                if (i >= 0 && i <= 2)
                {
                    if (availabilityChance <= 50)
                    {
                        stock[i] = random.Next(10, 21);
                    }
                }

                else if (i >= 3 && i <= 5)
                {
                    if (availabilityChance <= 10)
                    {
                        stock[i] = random.Next(5, 10);
                    }
                }

                else
                {
                    if (availabilityChance <= 1)
                    {
                        stock[i] = random.Next(1, 4);
                    }
                }
            }
        }

        static string OpenGift(int giftIndex, Random random)
        {
            string[] rewards;

            // Common, Rare, Epic
            if (giftIndex >= 0 && giftIndex <= 2)
            {
                rewards = Common_Epic;
            }

            // Legendary, Exotic, Mythical
            else if (giftIndex >= 3 && giftIndex <= 5)
            {
                rewards = Legendary_Mythical;
            }

            // Secret, Grinch's Special, Ultimate
            else
            {
                rewards = Secret_ultimate;
            }

            int rewardIndex = random.Next(rewards.Length);

            return rewards[rewardIndex];
        }

        static void SellItems()
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine("              SELL ITEMS");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.WriteLine($"Coins: {coins}");
            Console.WriteLine();

            if (inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty!");
                Console.WriteLine();
                Console.WriteLine("Press ENTER to return.");
                Console.ReadLine();
                return;
            }

            List<string> items = inventory.Keys.ToList();

            for (int i = 0; i < items.Count; i++)
            {
                string item = items[i];

                Console.WriteLine(
                    $"{i + 1}. {item,-30} " +
                    $"Stock: {inventory[item],2}x " +
                    $"Sell: {sellPrices[item]} coins"
                );
            }

            Console.WriteLine();
            Console.WriteLine("Enter the number of an item to sell.");
            Console.WriteLine("Type A to sell everything.");
            Console.WriteLine("Type Q to return.");
            Console.WriteLine();

            Console.Write("Choice: ");

            string input = Console.ReadLine();

            if (input?.ToUpper() == "Q")
            {
                return;
            }

            if (input?.ToUpper() == "A")
            {
                int totalMoney = 0;

                foreach (string item in items)
                {
                    totalMoney +=
                        sellPrices[item] * inventory[item];
                }

                Console.WriteLine();
                Console.WriteLine(
                    $"You will receive {totalMoney} coins."
                );

                Console.Write(
                    "Are you sure? (Y/N): "
                );

                string confirm = Console.ReadLine();

                if (confirm?.ToUpper() == "Y")
                {
                    coins += totalMoney;

                    inventory.Clear();

                    Console.WriteLine();
                    Console.WriteLine(
                        $"Sold everything for {totalMoney} coins!"
                    );

                    Console.WriteLine(
                        $"You now have {coins} coins."
                    );

                    Thread.Sleep(2000);
                }

                return;
            }

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid choice.");
                Thread.Sleep(1500);
                return;
            }

            if (choice < 1 || choice > items.Count)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid item number.");
                Thread.Sleep(1500);
                return;
            }

            string selectedItem = items[choice - 1];

            Console.WriteLine();
            Console.WriteLine(
                $"You have {inventory[selectedItem]}x {selectedItem}"
            );

            Console.WriteLine(
                $"Sell price: {sellPrices[selectedItem]} coins each"
            );

            Console.Write("How many would you like to sell? ");

            string quantityInput = Console.ReadLine();

            if (!int.TryParse(quantityInput, out int quantity))
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid quantity.");
                Thread.Sleep(1500);
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("You must sell at least 1.");
                Thread.Sleep(1500);
                return;
            }

            if (quantity > inventory[selectedItem])
            {
                Console.WriteLine();
                Console.WriteLine("You don't have that many!");
                Thread.Sleep(1500);
                return;
            }

            int moneyEarned =
                sellPrices[selectedItem] * quantity;

            coins += moneyEarned;

            inventory[selectedItem] -= quantity;

            if (inventory[selectedItem] <= 0)
            {
                inventory.Remove(selectedItem);
            }

            Console.WriteLine();
            Console.WriteLine(
                $"Sold {quantity}x {selectedItem}!"
            );

            Console.WriteLine(
                $"You earned {moneyEarned} coins."
            );

            Console.WriteLine(
                $"You now have {coins} coins."
            );

            Thread.Sleep(2000);
        }
    }
}