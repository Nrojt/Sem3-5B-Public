using Microsoft.Extensions.DependencyInjection;

namespace webapi_unit_tests.Services.database;

public class ServiceProviderWrapper {
  // Serviceprovider wrapper for the GetRequiredService method since the actual
  // method is an extension method and cannot be mocked
  public virtual T GetRequiredService<T>(IServiceProvider serviceProvider) {
    return serviceProvider.GetRequiredService<T>();
  }
}
