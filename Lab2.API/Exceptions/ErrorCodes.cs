namespace Lab2.API.Exceptions;

public static class ErrorCodes
{
    public static readonly string ENTITY_NOT_FOUND = "Resource:404000";
    public static readonly string ENTITY_CONFLICT = "Resource:409000";
    public static readonly string ACCOUNT_PHONE_ALREADY_EXISTS = "Account:409001";
    public static readonly string ACCOUNT_EMAIL_ALREADY_EXISTS = "Account:409002";
    public static readonly string PRODUCT_CODE_ALREADY_EXISTS = "Product:409001";
    public static readonly string LEAD_INVALID_QUALIFY_OR_DISQUALIFY = "Lead:400001";
    public static readonly string LEAD_INVALID_REMOVE = "Lead:400002";
    public static readonly string DEAL_INVALID_REMOVE = "Deal:400001";
    public static readonly string INTERNAL_SERVER_ERROR = "Server:500000";
}