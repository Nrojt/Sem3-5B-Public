using System.ComponentModel.DataAnnotations;

namespace webapi.Helpers.Validators;

public class EnumValidationAttribute<T> : ValidationAttribute
    where T : Enum {
  protected override
      ValidationResult? IsValid(object? value,
                                ValidationContext validationContext) {
    if (value is T) {
      // If the value is already of type T, no need for additional validation
      return ValidationResult.Success;
    }

    if (value is string enumString) {
      // If the value is a string, we can try to parse it to T, for example, if
      // the value is "DisabilityExpertWithGuardian" and T is UserTypes, it will
      // be parsed to UserTypes.DisabilityExpertWithGuardian
      if (Enum.TryParse(typeof(T), enumString, out _)) {
        return ValidationResult.Success;
      }
      return new ValidationResult(
          $"The {validationContext.DisplayName} field is not a valid {typeof(T).Name}.");
    }

    if (value is int enumInt) {
      // If the value is an int, we can try to parse it to T, for example, if
      // the value is 0 and T is UserTypes, it will be parsed to
      // UserTypes.DisabilityExpertWithGuardian
      if (Enum.IsDefined(typeof(T), enumInt)) {
        return ValidationResult.Success;
      }

      return new ValidationResult(
          $"The {validationContext.DisplayName} field is not a valid {typeof(T).Name}.");
    }

    return new ValidationResult(
        $"Invalid type for {validationContext.DisplayName}.");
  }
}
