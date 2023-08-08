namespace TinyCRM.Domain.Exceptions;

public static class ErrorCodes
{
    // Resource
    public const string ResourceNotFound = "Resource:100";

    public const string ResourceAlreadyExist = "Resource:101";
    public const string ResourceAccessDenied = "Resource:103";
    public const string ResourceInvalidOperation = "Resource:104";

    // Account
    public const string DuplicateAccountPhoneNumber = "Account:200";

    public const string DuplicateAccountEmail = "Account:201";

    // Product
    public const string ProductCodeAlreadyExists = "Product:300";

    // Lead
    public const string ProcessedLead = "Lead:400";

    // Deal
    public const string LineNotBelongToDeal = "Deal:600";

    public const string ProcessedDeal = "Deal:601";

    // User
    public const string ProfileForbidEdit = "Profile:800";

    // Role
    public const string BasicRoleAccessDenied = "Role:900";

    // Permission
    public const string PermissionAlreadyGranted = "Permission:1000";

    public const string PermissionNotGranted = "Permission:1001";

    // System
    public const string SystemError = "500";
}