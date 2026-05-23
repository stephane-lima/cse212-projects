using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities and remove one item
    // Expected Result: The item with the highest priority is removed and returned
    // Defect(s) Found: This found that the queue was not always checking the item with the highest priority in the Dequeue method
                    //  The error was in the for loop condition.
                    //  To fix this, the loop needed to check all items in the queue, including the last item.
    public void TestPriorityQueue_HighestPriorityIsDequeued()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Add two items with the same priority and remove one
    // Expected Result: The first item added with that priority is removed first (FIFO rule)
    // Defect(s) Found: This found that the queue was removing the last matching priority instead of the first one added in the Dequeue method.
                    //  The error was in the priority comparison statement.
                    //  To fix this, the comparison needed to use ">" instead of ">=" so the first matching item stays selected
    public void TestPriorityQueue_TieBreakerFIFO()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 10);
        priorityQueue.Enqueue("Second", 10);
        //priorityQueue.Enqueue("Third", 10);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("First", result);
    }

    [TestMethod]
    // Scenario: Add multiple items and remove two items in a row
    // Expected Result: Items are removed from the queue after being dequeued, and next correct item is returned
    // Defect(s) Found: This found that items were not being removed from the queue after dequeueing in the Dequeue method.
                    //  The error was after retrieving the value from the queue.
                    //  To fix this, RemoveAt(highPriorityIndex) needed to be called before returning the value.
    public void TestPriorityQueue_ItemsAreRemovedAfterDequeue()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 10);
        priorityQueue.Enqueue("C", 5);

        var first = priorityQueue.Dequeue();
        var second = priorityQueue.Dequeue();

        Assert.AreEqual("B", first);
        Assert.AreEqual("C", second);
    }

    [TestMethod]
    // Scenario: Attempt to remove an item from an empty queue
    // Expected Result: The program throws an InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyQueueThrowsException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Add items where the highest priority item is the last item added.
    // Expected Result: The last item should still be checked and returned if it has the highest priority
    // Defect(s) Found: This found that the last item in the queue was not being checked in the Dequeue method.
                    //  The error was in the for loop condition using "index < _queue.Count - 1".
                    //  To fix this, the condition needed to use "index < _queue.Count".
    public void TestPriorityQueue_HighPriorityAtEnd()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 10);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 20); // Highest Priority is last

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("C", result);
    }

    [TestMethod]
    // Scenario: Add multiple items and check queue order without removing anything
    // Expected Result: Items appear in the queue in the same order they were added
    // Defect(s) Found: None
    public void TestPriorityQueue_EnqueueOrderVerificationByDequeue()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 1);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }
}