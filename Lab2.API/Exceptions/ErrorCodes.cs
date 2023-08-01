namespace Lab2.API.Exceptions;

public static class ErrorCodes
{
    // Entity
    public const string EntityNotFound = "100";
    public const string EntityConflict = "101";

    // Account
    public const string AccountPhoneAlreadyExists = "200";
    public const string AccountEmailAlreadyExists = "201";

    // Product
    public const string ProductCodeAlreadyExists = "300";

    // Lead
    public const string LeadInvalidQualifyOrDisqualify = "400";
    public const string LeadInvalidRemove = "401";

    // Deal
    public const string DealInvalidRemove = "500";

    // Auth
    public const string AccountLockedOut = "600";
    public const string InvalidCredential = "601";
    public const string NotImplementTwoFactor = "602";

    // User
    public const string ProfileForbidEdit = "700";

    // Server
    public const string InternalServerError = "999";
}