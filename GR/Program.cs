using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace GR
{
    public class Program
    {
        public IList<Item> Items;

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };

            app.UpdateInventory();

            var filename = $"inventory_{DateTime.Now:yyyyddMM-HHmmss}.txt";
            var inventoryOutput = JsonConvert.SerializeObject(app.Items, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename), inventoryOutput);

            Console.ReadKey();
        }

        /// <summary>
        /// This method updates the inventory of all the items in the Items List by calling the appropiriate methods based on the item names.
        /// </summary>
        public void UpdateInventory()
        {
            Console.WriteLine("Updating inventory");
            foreach (var item in Items)
            {
                Console.WriteLine(" - Item: {0}", item.Name);
                string itemName = ShortName(item.Name);
                switch (itemName)
                {
                    case "Aged Brie":
                        AgedBrie(item);
                        break;
                    case "Backstage passes":
                        BackstagePasses(item);
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        break;
                    case "Conjured Mana Cake":
                        Conjured(item);
                        break;
                    default:
                        AllOtherItems(item);
                        break;
                }
            }
            Console.WriteLine("Inventory update complete");
        }

        /// <summary>
        /// This method returns the short name for the given item name if there is any.
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns>short name if there is any</returns>
        public string ShortName(string itemName)
        {
            if (itemName.Contains("Backstage passes"))
                return "Backstage passes";
            else
                return itemName;
        }

        /// <summary>
        /// This method adjusts(decrease or increase) the quality value based on the input passed
        /// </summary>
        /// <param name="decrease"></param>
        /// <param name="quality"></param>
        /// <param name="count"></param>
        /// <returns>Adjusted quality value</returns>
        public int AdjustQuality(bool decrease, int quality, int count)
        {
            if (decrease)
            {
                if (quality >= count)
                    return (quality - count);
                else
                    return 0;
            }                
            else
            {
                if ((quality + count) <= 50)
                    return (quality + count);
                else
                    return 50;
            }                
        }

        /// <summary>
        /// This methods handles the inventory update for all the common items which dont have any specific logic
        /// </summary>
        /// <param name="item"></param>
        public void AllOtherItems(Item item)
        {
            item.SellIn = item.SellIn - 1;
            if (item.Quality > 0)
            {
                if (item.SellIn >= 0)
                    item.Quality = AdjustQuality(true, item.Quality, 1);
                else
                    item.Quality = AdjustQuality(true, item.Quality, 2); 
            }
        }

        /// <summary>
        /// This methods handles the inventory update for Aged Brie
        /// </summary>
        /// <param name="item"></param>
        public void AgedBrie(Item item)
        {
            item.SellIn = item.SellIn - 1;
            item.Quality = AdjustQuality(false, item.Quality, 1);
        }

        /// <summary>
        /// This methods handles the inventory update for Backstage Passess items
        /// </summary>
        /// <param name="item"></param>
        public void BackstagePasses(Item item)
        {
            item.SellIn = item.SellIn - 1;
            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return;
            }

            if (item.SellIn > 10)
                item.Quality = AdjustQuality(false, item.Quality, 1);
            else if (item.SellIn > 5)
                item.Quality = AdjustQuality(false, item.Quality, 2);
            else
                item.Quality = AdjustQuality(false, item.Quality, 3);
        }

        /// <summary>
        /// This methods handles the inventory update for Conjured items
        /// </summary>
        /// <param name="item"></param>
        public void Conjured(Item item)
        {
            item.SellIn = item.SellIn - 1;

            if (item.SellIn >= 0)
                item.Quality = AdjustQuality(true, item.Quality, 2);
            else
                item.Quality = AdjustQuality(true, item.Quality, 4);
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}