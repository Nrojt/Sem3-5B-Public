using System.ComponentModel.DataAnnotations;
using webapi.Models;
using webapi.Models.Accounts;
using webapi.Models.Dto;

namespace webapi_unit_tests;

public class ResearchModelTest {
  [Fact]
  public void Research_GeldigeGegevens_ZouValidatieDoorstaan() {
    // Arrange
    var research =
        new Research { ResearchId = 1,
                       Title = "Test",
                       Disabilities = new List<DisabilityDto>(),
                       DisabilityExperts = new List<DisabilityExpertDto>(),
                       Company = new Company(),
                       AgeRange = "18-30",
                       Description = "Test",
                       Date = DateTime.Now,
                       Location = "Den Haag",
                       Reward = "Een sticker",
                       ResearchType = "Interview" };

    // Act and Assert
    Assert.True(ValidateModel(research));
  }

  [Fact]
  public void Research_Zonder_Titel_ZouValidatieMoetenFalen() {
    // Arrange
    var research =
        new Research { ResearchId = 1,
                       Title = null,
                       Disabilities = new List<DisabilityDto>(),
                       DisabilityExperts = new List<DisabilityExpertDto>(),
                       Company = new Company(),
                       AgeRange = "18-30",
                       Description = "Test",
                       Date = DateTime.Now,
                       Location = "Den Haag",
                       Reward = "Een sticker",
                       ResearchType = "Interview" };

    // Act and Assert
    Assert.False(ValidateModel(research));
  }

  private bool ValidateModel(Research research) {
    // Valideert het model
    var validationContext =
        new ValidationContext(research, serviceProvider: null, items: null);
    var validationResults = new List<ValidationResult>();
    return Validator.TryValidateObject(research, validationContext,
                                       validationResults, true);
  }

  [Fact]
  public void Research_GeldigeDisabilities_ZouMoetenSlagen() {
    // Arrange
    var research = new Research {
      ResearchId = 1,
      Title = "Sample Research",
      Disabilities =
          new List<DisabilityDto> { new DisabilityDto { DisabilityId = 1 },
                                    new DisabilityDto { DisabilityId = 2 } },
      DisabilityExperts = new List<DisabilityExpertDto>(),
      Company = new Company(),
      AgeRange = "18-30",
      Description = "Sample description",
      Date = DateTime.Now,
      Location = "Sample location",
      Reward = "Sample reward",
      ResearchType = "Sample type"
    };

    // Act and Assert
    Assert.True(ValidateModel(research));
  }
}
