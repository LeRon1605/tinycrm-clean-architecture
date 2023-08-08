using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Common.Constants;

public static class Permissions
{
    public const string Default = "TinyCrm.Permissions";

    public static List<PermissionContent> Provider => new()
    {
        new(Products.Create, "Create product."),
        new(Products.View, "View products."),
        new(Products.Edit, "Edit products."),
        new(Products.Delete, "Delete products."),

        new(Accounts.Create, "Create account."),
        new(Accounts.View, "View accounts."),
        new(Accounts.Edit, "Edit accounts."),
        new(Accounts.Delete, "Delete accounts."),

        new(Contacts.Create, "Create contact."),
        new(Contacts.View, "View contacts."),
        new(Contacts.Edit, "Edit contacts."),
        new(Contacts.Delete, "Delete contacts."),

        new(Deals.Create, "Create deal."),
        new(Deals.View, "View deals."),
        new(Deals.Edit, "Edit deals."),
        new(Deals.Delete, "Delete deals."),
        new(Deals.ViewProduct, "View products in deal."),
        new(Deals.CreateProduct, "Create product in deal."),
        new(Deals.EditProduct, "Edit product in deal."),
        new(Deals.DeleteProduct, "Delete product in deal."),
        new(Deals.Statistic, "View deal's statistic result."),

        new(Leads.Create, "Create lead."),
        new(Leads.View, "View leads."),
        new(Leads.Edit, "Edit leads."),
        new(Leads.Delete, "Delete leads."),
        new(Leads.Qualify, "Qualify leads."),
        new(Leads.Disqualify, "Disqualify leads."),
        new(Leads.Statistic, "View lead's statistic result."),

        new(Users.Create, "Create user."),
        new(Users.View, "View users."),
        new(Users.Edit, "Edit users."),
        new(Users.Delete, "Delete users."),

        new(Roles.Create, "Create role."),
        new(Roles.View, "View roles."),
        new(Roles.Edit, "Edit roles."),
        new(Roles.Delete, "Delete roles."),

        new(PermissionManagement.View, "View permissions."),
        new(PermissionManagement.AddToRole, "Add permission to role."),
        new(PermissionManagement.AddToUser, "Add permission to user."),
        new(PermissionManagement.RemoveFromRole, "Remove permission from role."),
        new(PermissionManagement.RemoveFromUser, "Remove permission from user."),
    };

    public static class Products
    {
        public const string Group = $"{Default}.Products";

        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.Edit";
        public const string Create = $"{Group}.Create";
        public const string Delete = $"{Group}.Delete";
    }

    public static class Accounts
    {
        public const string Group = $"{Default}.Accounts";

        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.Edit";
        public const string Create = $"{Group}.Create";
        public const string Delete = $"{Group}.Delete";
    }

    public static class Contacts
    {
        public const string Group = $"{Default}.Contacts";

        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.Edit";
        public const string Create = $"{Group}.Create";
        public const string Delete = $"{Group}.Delete";
    }

    public static class Deals
    {
        public const string Group = $"{Default}.Deals";

        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.Edit";
        public const string Create = $"{Group}.Create";
        public const string Delete = $"{Group}.Delete";
        public const string Statistic = $"{Group}.Statistic";

        public const string ViewProduct = $"{Group}.ViewProduct";
        public const string EditProduct = $"{Group}.EditProduct";
        public const string CreateProduct = $"{Group}.CreateProduct";
        public const string DeleteProduct = $"{Group}.DeleteProduct";
    }

    public static class Leads
    {
        public const string Group = $"{Default}.Leads";

        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.Edit";
        public const string Create = $"{Group}.Create";
        public const string Delete = $"{Group}.Delete";

        public const string Qualify = $"{Group}.Qualify";
        public const string Disqualify = $"{Group}.Disqualify";

        public const string Statistic = $"{Group}.Statistic";
    }

    public static class Users
    {
        public const string Group = $"{Default}.Users";

        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.Edit";
        public const string Create = $"{Group}.Create";
        public const string Delete = $"{Group}.Delete";
    }

    public static class Roles
    {
        public const string Group = $"{Default}.Roles";

        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.Edit";
        public const string Create = $"{Group}.Create";
        public const string Delete = $"{Group}.Delete";
    }

    public static class PermissionManagement
    {
        public const string Group = $"{Default}.PermissionManagement";

        public const string View = $"{Group}.View";
        public const string AddToRole = $"{Group}.AddToRole";
        public const string AddToUser = $"{Group}.AddToUser";
        public const string RemoveFromRole = $"{Group}.RemoveFromRole";
        public const string RemoveFromUser = $"{Group}.RemoveFromUser";
    }

    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"{Default}.{module}.Create",
            $"{Default}.{module}.View",
            $"{Default}.{module}.Edit",
            $"{Default}.{module}.Delete",
        };
    }
}