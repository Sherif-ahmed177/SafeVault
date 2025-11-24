using Xunit;
using System.ComponentModel.DataAnnotations;
using SafeVault.Models;
using System.Collections.Generic;
using System.Linq;

public class SecurityTests
{
    [Fact]
    public void ModelValidation_InvalidEmail_Fails()
    {
        var dto = new UserInputDto { Username = "bob", Email = "notanemail", Amount = 10, FreeText = "ok" };
        var ctx = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, ctx, results, true);
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(dto.Email)));
    }

    [Fact]
    public void ModelValidation_ValidData_Passes()
    {
        var dto = new UserInputDto { Username = "bob", Email = "bob@example.com", Amount = 10, FreeText = "hello" };
        var ctx = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, ctx, results, true);
        Assert.True(isValid);
    }
}
