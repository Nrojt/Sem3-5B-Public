using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using webapi.Helpers;
using webapi.Helpers.Constants;
using webapi.Helpers.Enums;
using webapi.Models;

namespace webapi.Hubs;

public class ChatHub : Hub {
  public override async Task OnConnectedAsync() {
    try {
      // Access the claims principal for the current user
      var userClaims = (ClaimsIdentity)Context.User.Identity;

      // Extract user information from claims
      string userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      string userType = userClaims.FindFirst(CustomClaimTypes.UserType)?.Value;

      // Use the extracted information as needed
      Console.WriteLine($"User ID: {userId}, User Type: {userType}");

      // Store user claims in the Context.Items dictionary
      Context.Items["UserClaims"] = userClaims;

      // Call the base OnConnectedAsync method to continue with the default
      // behavior
      await base.OnConnectedAsync();
    } catch (Exception ex) {
      Console.Error.WriteLine($"Error in OnConnectedAsync: {ex.Message}");
    }
  }

  // method to join a chat as a disability expert
  [UserTypeAuthorization("DisabilityExpert", "CompanyApproved")]
  public async Task JoinChatNoResearch(UserConnection userConnection) {
    SetUserConnectionValues(userConnection);

    if (userConnection.CompanyId == null ||
        userConnection.DisabilityExpertId == null ||
        userConnection.GroupId == null) {
      Console.Error.WriteLine(
          "One or more of the user connection values are null");
      Context.ConnectionAborted.Register(
          () => Console.Error.WriteLine("Connection aborted"));
      Context.Abort();
      return;
    }

    try {
      await Groups.AddToGroupAsync(Context.ConnectionId,
                                   userConnection.GroupId);
    } catch (Exception ex) {
      Console.Error.WriteLine($"Error in JoinChat: {ex.Message}");
      Context.ConnectionAborted.Register(
          () => Console.Error.WriteLine("Connection aborted"));
      Context.Abort();
    }
  }

  // method to send a message to a chat
  [UserTypeAuthorization("CompanyApproved", "DisabilityExpert")]
  public async Task SendMessage(string message) {
    // get the group id from the context items dictionary
    string? groupId = (string?)Context.Items["GroupId"];

    // null check
    if (groupId == null) {
      Console.Error.WriteLine("Group id is null");
      Context.ConnectionAborted.Register(
          () => Console.Error.WriteLine("Connection aborted"));
      Context.Abort();
      return;
    }

    try {
      Console.WriteLine($"Sending message: {message} to group: {groupId}");

      // Send the message to all other users in the group
      await Clients.OthersInGroup(groupId).SendAsync("ReceiveMessage", message);
    } catch (Exception ex) {
      Console.Error.WriteLine($"Error in SendMessage: {ex.Message}");
    }
  }

  private void SetUserConnectionValues(UserConnection userConnection) {
    var userClaims = (ClaimsIdentity)Context.Items["UserClaims"];

    // null check
    if (userClaims == null) {
      Console.Error.WriteLine("User claims are null");
      return;
    }

    string? userTypeClaim =
        userClaims.FindFirst(CustomClaimTypes.UserType)?.Value;
    UserTypes userType =
        (UserTypes)Enum.Parse(typeof(UserTypes), userTypeClaim);

    if (userType == UserTypes.CompanyApproved) {
      if (userConnection.DisabilityExpertId == null) {
        Console.Error.WriteLine("Disability expert id is null");
        return;
      }

      userConnection.CompanyId =
          userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    } else if (userType == UserTypes.DisabilityExpertWithoutGuardian ||
               userType == UserTypes.DisabilityExpertWithGuardian) {
      if (userConnection.CompanyId == null) {
        Console.Error.WriteLine("Company id is null");
        return;
      }

      userConnection.DisabilityExpertId =
          userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    } else {
      Console.Error.WriteLine($"Invalid user type: {userType}");
      return;
    }

    userConnection.GroupId = CreateGroupId(userConnection.DisabilityExpertId,
                                           userConnection.CompanyId);

    Console.WriteLine("User connected to group: " + userConnection.GroupId);

    // saving the group id in the context items dictionary
    Context.Items["GroupId"] = userConnection.GroupId;
  }

  // method to create a group id based on the disability expert and company ids
  private string CreateGroupId(string disabilityExpertId, string companyId) {
    return $"{disabilityExpertId}-{companyId}";
  }
}
