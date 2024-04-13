using webapi.ApiModels.Register;

namespace webapi.ApiModels.Login;

public class ErrorResponseModel {
  public string? ErrorMessage { get; set; }
  public RegisterModelBase? RegisterModelBase { get; set; }
}
