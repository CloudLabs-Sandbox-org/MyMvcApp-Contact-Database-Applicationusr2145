using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Xunit;

public class UserControllerTests
{
    public UserControllerTests()
    {
        // Limpia la lista de usuarios antes de cada prueba
        UserController.userlist.Clear();
    }

    [Fact]
    public void Index_ReturnsViewWithUserList()
    {
        // Arrange
        var controller = new UserController();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });

        // Act
        var result = controller.Index(null) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<User>>(result.Model);
        Assert.Single((List<User>)result.Model);
    }

    [Fact]
    public void Details_ReturnsViewWithUser_WhenUserExists()
    {
        // Arrange
        var controller = new UserController();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });

        // Act
        var result = controller.Details(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<User>(result.Model);
        Assert.Equal("John Doe", ((User)result.Model).Name);
    }

    [Fact]
    public void Details_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var controller = new UserController();

        // Act
        var result = controller.Details(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_AddsUserAndRedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        var controller = new UserController();
        var newUser = new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" };

        // Act
        var result = controller.Create(newUser) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        Assert.Contains(UserController.userlist, u => u.Name == "Jane Doe");
    }

    [Fact]
    public void Edit_UpdatesUserAndRedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        var controller = new UserController();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });
        var updatedUser = new User { Id = 1, Name = "John Smith", Email = "johnsmith@example.com" };

        // Act
        var result = controller.Edit(1, updatedUser) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        Assert.Equal("John Smith", UserController.userlist.First(u => u.Id == 1).Name);
    }

    [Fact]
    public void Delete_RemovesUserAndRedirectsToIndex_WhenUserExists()
    {
        // Arrange
        var controller = new UserController();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });

        var formCollection = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());
        var result = controller.Delete(1, formCollection) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        Assert.DoesNotContain(UserController.userlist, u => u.Id == 1);
    }

    [Fact]
    public void Delete_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var controller = new UserController();

        var formCollection = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());
        var result = controller.Delete(99, formCollection);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}