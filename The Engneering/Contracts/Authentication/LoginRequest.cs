namespace The_Engneering.Contracts.Authentication;

public record LoginRequest
(
    string Email,
    string Password
);
