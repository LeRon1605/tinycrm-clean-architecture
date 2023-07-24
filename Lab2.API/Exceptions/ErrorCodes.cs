namespace Lab2.API.Exceptions;

public static class ErrorCodes
{
    public const string EntityNotFound = "ENTITY_NOT_FOUND";
    public const string EntityConflict = "ENTITY_CONFLICT";
    public const string AccountPhoneAlreadyExists = "ACCOUNT_PHONE_ALREADY_EXISTS";
    public const string AccountEmailAlreadyExists = "ACCOUNT_EMAIL_ALREADY_EXISTS";
    public const string ProductCodeAlreadyExists = "PRODUCT_CODE_ALREADY_EXISTS";
    public const string LeadInvalidQualifyOrDisqualify = "LEAD_INVALID_QUALIFY_OR_DISQUALIFY";
    public const string LeadInvalidRemove = "LEAD_INVALID_REMOVE";
    public const string DealInvalidRemove = "DEAL_INVALID_REMOVE";
    public const string InternalServerError = "INTERNAL_SERVER_ERROR";
}