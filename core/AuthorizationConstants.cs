namespace core.Constants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

    public static class Permissions
    {
        public const string ClaimType = "Permission";
        public const string TasksDelete = "Tasks.Delete";
    }

    public static class Policies
    {
        public const string AdminOnly = "AdminOnly";
        public const string UserOrAdmin = "UserOrAdmin";
        public const string CanDeleteTasks = "CanDeleteTasks";
    }
}