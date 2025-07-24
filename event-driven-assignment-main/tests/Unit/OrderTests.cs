using Xunit;
using Moq;
using System;

public class OrderTests
{
    private readonly Mock<IClock> _mockClock = new();

    [Fact]
    public void Cancel_ShouldChangeStatusToCanceled_IfWithin15Minutes()
    {
        // Arrange
        var createdAt = DateTime.UtcNow.AddMinutes(-10);
        _mockClock.Setup(c => c.UtcNow).Returns(DateTime.UtcNow);
        var order = new Order(1, createdAt);
        // Act
        order.Cancel(_mockClock.Object.UtcNow);
        // Assert
        Assert.Equal(OrderStatus.Canceled, order.Status);
    }

    [Fact]
    public void Cancel_ShouldThrowException_IfBeyond15Minutes()
    {
        var createdAt = DateTime.UtcNow.AddMinutes(-16);
        _mockClock.Setup(c => c.UtcNow).Returns(DateTime.UtcNow);
        var order = new Order(1, createdAt);

        var ex = Assert.Throws<InvalidOperationException>(() => order.Cancel(_mockClock.Object.UtcNow));
        Assert.Equal("Cancel window has expired.", ex.Message);
    }

    [Fact]
    public void Cancel_ShouldThrowException_IfOrderIsNotCreated()
    {
        var createdAt = DateTime.UtcNow;
        var order = new Order(1, createdAt);
        var now = createdAt.AddMinutes(5);

        // Simulate order progressing to Processin
        typeof(Order).GetProperty("Status")!.SetValue(order, OrderStatus.Processing);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => order.Cancel(now));
        Assert.Equal("Only newly created orders can be canceled.", ex.Message);
    }
}
