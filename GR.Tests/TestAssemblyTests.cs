using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GR.Tests
{
    public class TestAssemblyTests
    {
        private readonly Program _app;

        public TestAssemblyTests()
        {
            _app = new Program
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 1},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item
                    {
                        Name = "Backstage passes to a D498FJ9FJ2N concert",
                        SellIn = 10,
                        Quality = 30
                    },
                    new Item
                    {
                        Name = "Backstage passes to a FH38F39DJ39 concert",
                        SellIn = 5,
                        Quality = 33
                    },
                    new Item
                    {
                        Name = "Backstage passes to a I293JD92J44 concert",
                        SellIn = 6,
                        Quality = 27
                    },
                    new Item
                    {
                        Name = "Backstage passes to a O2848394820 concert",
                        SellIn = 1,
                        Quality = 13
                    },
                    new Item
                    {
                        Name = "Backstage passes to a DEEEADMEEET concert",
                        SellIn = 0,
                        Quality = 25
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };

            _app.UpdateInventory();
        }

        [Fact]
        public void DexterityVest_SellIn_ShouldDecreaseByOne()
        {
            Assert.Equal(9, _app.Items.First(x => x.Name == "+5 Dexterity Vest").SellIn);
        }

        [Fact]
        public void DexterityVest_Quality_ShouldDecreaseByOne()
        {
            Assert.Equal(19, _app.Items.First(x => x.Name == "+5 Dexterity Vest").Quality);
        }

        [Fact]
        public void BackstagePasses_Quality_Validation()
        {
            //Quality should increase by 1 since the sell-in value > 10
            Assert.Equal(21, _app.Items[4].Quality);

            //Quality should increase by 2 since the sell-in value <= 10 and > 5
            Assert.Equal(32, _app.Items[5].Quality);

            //Quality should increase by 3 since the sell-in <= 5
            Assert.Equal(30, _app.Items[7].Quality);

            //Quality should be 0 when the Sell-in value < 0
            Assert.Equal(0, _app.Items[9].Quality);
        }

        [Fact]
        public void AgedBrie_Quality_Validation()
        {
            //Quality value should always increase by 1
            Assert.Equal(2, _app.Items[1].Quality);
        }

        [Fact]
        public void Sulfuras_Validation()
        {
            //Sell-In value should be always 0 for "Sulfuras, Hand of Ragnaros"
            Assert.Equal(0, _app.Items[3].SellIn);

            //Quality value should be always 80 for "Sulfuras, Hand of Ragnaros"
            Assert.Equal(80, _app.Items[3].Quality);
        }

        [Fact]
        public void Conjured_Quality_Validation()
        {
            //Quality value should decrease by 2 since sell-in value >= 0
            Assert.Equal(4, _app.Items[10].Quality);
        }
    }
}