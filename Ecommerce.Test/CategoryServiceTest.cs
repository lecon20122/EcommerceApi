using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Test
{
    public class CategoryServiceTest
    {
        [Fact]
        public async void AddCategory()
        {
            // Arrange
            var category = new Category
            {
                Name = "CategoryOne",
            };

            var dbContextOption = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddCategory")
                .Options;

            var dbContext = new ApplicationDbContext(dbContextOption);

            var categoryService = new CategoryService(dbContext);

            // Act

            var result = await categoryService.AddCategory(category);

            // Assert

            Assert.Equal(category.Name, result.Name);
        }

        [Fact]
        public async void AddCategory_CannotAddSameCategoryNameTwice()
        {
            var category = new Category
            {
                Name = "CategoryOne",
            };

            var dbContextOption = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddCategory")
                .Options;

            var dbContext = new ApplicationDbContext(dbContextOption);

            var categoryService = new CategoryService(dbContext);

            var firstTime = await categoryService.AddCategory(category);

            var result = await Assert.ThrowsAsync<Exception>(() => categoryService.AddCategory(category));

            Assert.Equal($"This Name {category.Name} already exits", result.Message);
        }
    }
}