namespace Lab2.API.Exceptions;

public static class ErrorCodes
{
    // Entity
    public const string EntityNotFound = "ENTITY_NOT_FOUND";
    public const string EntityConflict = "ENTITY_CONFLICT";

    // Account
    public const string AccountPhoneAlreadyExists = "ACCOUNT_PHONE_ALREADY_EXISTS";
    public const string AccountEmailAlreadyExists = "ACCOUNT_EMAIL_ALREADY_EXISTS";

    // Product
    public const string ProductCodeAlreadyExists = "PRODUCT_CODE_ALREADY_EXISTS";

    // Lead
    public const string LeadInvalidQualifyOrDisqualify = "LEAD_INVALID_QUALIFY_OR_DISQUALIFY";
    public const string LeadInvalidRemove = "LEAD_INVALID_REMOVE";

    // Deal
    public const string DealInvalidRemove = "DEAL_INVALID_REMOVE";

    // Auth
    public const string AccountLockedOut = "AUTH_LOCKED_OUT";
    public const string InvalidCredential = "AUTH_INCORRECT_ACCOUNT_INFO";
    public const string NotImplementTwoFactor = "AUTH_NOT_IMPLEMENT_2F";

    // User
    public const string ProfileForbidEdit = "PROFILE_FORBID_EDIT";

    // Server
    public const string InternalServerError = "INTERNAL_SERVER_ERROR";
}