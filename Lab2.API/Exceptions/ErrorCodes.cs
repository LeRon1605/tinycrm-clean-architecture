namespace Lab2.API.Exceptions;

public static class ErrorCodes
{
    public static readonly string ENTITY_NOT_FOUND = "ENTITY_NOT_FOUND";
    public static readonly string ENTITY_CONFLICT = "ENTITY_CONFLICT";
    public static readonly string ACCOUNT_PHONE_ALREADY_EXISTS = "ACCOUNT_PHONE_ALREADY_EXISTS";
    public static readonly string ACCOUNT_EMAIL_ALREADY_EXISTS = "ACCOUNT_EMAIL_ALREADY_EXISTS";
    public static readonly string PRODUCT_CODE_ALREADY_EXISTS = "PRODUCT_CODE_ALREADY_EXISTS";
    public static readonly string LEAD_INVALID_QUALIFY_OR_DISQUALIFY = "LEAD_INVALID_QUALIFY_OR_DISQUALIFY";
    public static readonly string LEAD_INVALID_REMOVE = "LEAD_INVALID_REMOVE";
    public static readonly string DEAL_INVALID_REMOVE = "DEAL_INVALID_REMOVE";
    public static readonly string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
}