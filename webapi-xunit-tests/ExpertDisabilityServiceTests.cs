using webapi.Models.Accounts;
using webapi.Models.Disabilities;
using webapi.Services;

public class ExpertDisabilityServiceTests {

  [Fact]
  public void
  CheckDisabilityExpertAndDisability_ShouldReturnSuccess_WhenExpertHasDisability() {
    // Arrange
    var service = new ExpertDisabilityService();
    var disabilityExpert = new DisabilityExpert();
    var disability = new Disability { DisabilityId = 1 };
    disabilityExpert.ExpertDisabilities.Add(
        new ExpertDisability { DisabilityId = 1 });

    // Act
    var result = service.CheckDisabilityExpertAndDisability(
        disabilityExpert, disability, shouldHaveDisability: true);

    // Assert
    Assert.True(result.succes);
  }

  [Fact]
  public void
  CheckDisabilityExpertAndDisability_ShouldReturnSuccess_WhenExpertDoesNotHaveDisability() {
    // Arrange
    var service = new ExpertDisabilityService();
    var disabilityExpert = new DisabilityExpert();
    var disability = new Disability { DisabilityId = 1 };

    // Act
    var result = service.CheckDisabilityExpertAndDisability(
        disabilityExpert, disability, shouldHaveDisability: false);

    // Assert
    Assert.True(result.succes);
  }

  [Fact]
  public void
  CheckDisabilityExpertAndDisability_ShouldReturnFailure_WhenDisabilityIsNull() {
    // Arrange
    var service = new ExpertDisabilityService();
    var disabilityExpert = new DisabilityExpert();

    // Act
    var result = service.CheckDisabilityExpertAndDisability(
        disabilityExpert, null, shouldHaveDisability: true);

    // Assert
    Assert.False(result.succes);
  }

  [Fact]
  public void
  CheckDisabilityExpertAndDisability_ShouldReturnFailure_WhenExpertIsNull() {
    // Arrange
    var service = new ExpertDisabilityService();
    var disability = new Disability { DisabilityId = 1 };

    // Act
    var result = service.CheckDisabilityExpertAndDisability(
        null, disability, shouldHaveDisability: true);

    // Assert
    Assert.False(result.succes);
  }
}
