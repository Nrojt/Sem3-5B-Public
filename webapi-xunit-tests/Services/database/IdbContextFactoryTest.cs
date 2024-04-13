using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using webapi.Data.Bases;
using webapi.Data.Databases;
using webapi.Helpers.Enums;
using webapi.Services.database;

namespace webapi_unit_tests.Services.database;

[TestSubject(typeof(IdbContextFactory))]
public class IdbContextFactoryTest {

  [Theory]
  [InlineData(UserTypes.CompanyApproved, typeof(CompanyResearchContext))]
  [InlineData(UserTypes.CompanyUnApproved, typeof(CompanyResearchContext))]
  [InlineData(UserTypes.DisabilityExpertWithGuardian,
              typeof(DisabilityExpertGuardiansContext))]
  [InlineData(UserTypes.DisabilityExpertWithoutGuardian,
              typeof(DisabilityExpertGuardiansContext))]
  [InlineData(UserTypes.Guardian, typeof(DisabilityExpertGuardiansContext))]
  [InlineData(UserTypes.Employee, typeof(EmployeeContext))]
  [InlineData(null, null)]
  public void
  GetContext_ShouldReturnCorrectContextBasedOnUserType(UserTypes? userType,
                                                       Type? expectedType) {
    // Arrange
    var serviceProviderMock = new Mock<IServiceProvider>();
    var serviceProviderWrapperMock = new Mock<ServiceProviderWrapper>();

    // Empty db context options
    var companyResearchContextOptions =
        new DbContextOptions<CompanyResearchContext>();
    var disabilityExpertGuardiansContextOptions =
        new DbContextOptions<DisabilityExpertGuardiansContext>();
    var employeeContextOptions = new DbContextOptions<EmployeeContext>();

    // setting up the service provider mocks
    SetupServiceProviderMock(
        serviceProviderMock, serviceProviderWrapperMock,
        new CompanyResearchContext(companyResearchContextOptions));
    SetupServiceProviderMock(serviceProviderMock, serviceProviderWrapperMock,
                             new DisabilityExpertGuardiansContext(
                                 disabilityExpertGuardiansContextOptions));
    SetupServiceProviderMock(serviceProviderMock, serviceProviderWrapperMock,
                             new EmployeeContext(employeeContextOptions));

    var factory = new IdbContextFactory(serviceProviderMock.Object);

    // Act
    var actual = factory.GetContext(userType);

    // Assert
    Assert.Equal(expectedType, actual?.GetType());
  }

  private void SetupServiceProviderMock<T>(
      Mock<IServiceProvider> serviceProviderMock,
      Mock<ServiceProviderWrapper> serviceProviderWrapper, T context)
      where T : IdentityBaseContext {
    // Mocking the GetRequiredService method of the service provider wrapper
    serviceProviderWrapper
        .Setup(x => x.GetRequiredService<T>(serviceProviderMock.Object))
        .Returns(context);

    // Mocking the GetService method of the service provider
    // This is done to ensure that the GetRequiredService method of the service
    // provider wrapper is called
    serviceProviderMock.Setup(x => x.GetService(typeof(T)))
        .Returns(serviceProviderWrapper.Object.GetRequiredService<T>(
            serviceProviderMock.Object));
  }
}
