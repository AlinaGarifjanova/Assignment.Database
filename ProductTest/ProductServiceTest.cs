using Assignment_04.Entities;
using Assignment_04.Repositories;
using Assignment_04.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductTest;

public class ProductServiceTest
{
    [Fact]
    public async Task RemoveProductAsync_ProductExists_ReturnsTrue()
    {
        // Arrange
        string productName = "TestProdukt";
        var productRepoMock = new Mock<ProductRepository>();
        productRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ReturnsAsync(true);
        productRepoMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ReturnsAsync(true);

        var productService = new ProductService(productRepoMock.Object, null!, null!);

        // Act
        bool result = await productService.RemoveProductAsync(productName);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task RemoveProductAsync_ProductDoesNotExist_ReturnsFalse()
    {
        // Arrange
        string productName = "IngenExisteradeProd";
        var productRepoMock = new Mock<ProductRepository>();
        productRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ReturnsAsync(false);

        var productService = new ProductService(productRepoMock.Object, null!, null!);

        // Act
        bool result = await productService.RemoveProductAsync(productName);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveProductAsync_ExceptionThrown_ReturnsFalse()
    {
        // Arrange
        string productName = "TestProdukt";
        var productRepoMock = new Mock<ProductRepository>();
        productRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ThrowsAsync(new Exception("Simulerade undantag"));

        var productService = new ProductService(productRepoMock.Object, null!, null!);

        // Act
        bool result = await productService.RemoveProductAsync(productName);

        // Assert
        Assert.False(result);
    }
}


