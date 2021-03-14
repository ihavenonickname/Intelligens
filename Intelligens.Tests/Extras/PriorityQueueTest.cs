using Intelligens.Extras;
using Xunit;

namespace Intelligens.Tests.Extras
{
    public class PriorityQueueTest
    {
        [Fact]
        public void Add_ShouldAddItem_WhenQueueIsEmpty()
        {
            // Arrange
            var q = new PriorityQueue<string, int>(3);
            var expected = new [] {"item"};

            // Act
            q.Add("item", 0);
            var actual = q.Items;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ShouldNotAdd_WhenQueueIsFull()
        {
            // Arrange
            var q = new PriorityQueue<string, int>(3);
            var expected = new [] {"item1", "item2", "item3"};

            // Act
            q.Add("item1", 4);
            q.Add("item2", 3);
            q.Add("item3", 2);
            q.Add("item4", 1);
            var actual = q.Items;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ShouldPreservePriorityOrder_RegardlessOfAddOrder()
        {
            // Arrange
            var q = new PriorityQueue<string, int>(3);
            var expected = new [] {"item5", "item4", "item3"};

            // Act
            q.Add("item2", 2);
            q.Add("item1", 1);
            q.Add("item5", 5);
            q.Add("item4", 4);
            q.Add("item3", 3);
            var actual = q.Items;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
